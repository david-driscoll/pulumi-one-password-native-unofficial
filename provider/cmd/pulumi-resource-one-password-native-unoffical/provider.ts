import * as pulumi from "@pulumi/pulumi";
import * as provider from "@pulumi/pulumi/provider";
import { ItemType, ResourceTypes, ItemTypeNames, FunctionTypes, PropertyPaths } from './types'
import { FieldAssignmentType, FieldPurpose, item, vault, read, ResponseFieldType, FieldAssignment } from "@1password/op-js"
import { createHash, randomBytes } from "crypto";
import { RNGFactory, Random } from "random/dist/cjs/random";
import { camelCase, cloneDeep, get, last, set, uniq, valuesIn } from "lodash";
import { Operation, diff } from "just-diff";
import * as fs from "fs";
import { basename, resolve } from "path";

const propertiesToIgnore = ['category', 'fields', 'sections', 'tags', 'title', 'vault', 'uuid', 'generatePassword', 'attachments', 'references'];


// import { StaticPage, StaticPageArgs } from "./staticPage";

export class Provider implements provider.Provider {
    resolvedSchema: typeof import('./schema.json');
    constructor(readonly version: string, readonly schema: string) {
        this.resolvedSchema = JSON.parse(schema);
    }

    /**
     * Construct creates a new component pulumi.
     *
     * @param name The name of the resource to create.
     * @param type The type of the resource to create.
     * @param inputs The inputs to the pulumi.
     * @param options the options for the pulumi.
     */
    async construct(name: string, type: string, inputs: pulumi.Inputs,
        options: pulumi.ComponentResourceOptions): Promise<provider.ConstructResult> {

        // TODO: Add support for additional component resources here.
        switch (type) {
            default:
                throw new Error(`unknown resource type ${type} name ${name}`);
        }
    }

    /**
     * Create allocates a new instance of the provided resource and returns its unique ID afterwards.
     * If this call fails, the resource must not have been created (i.e., it is "transactional").
     *
     * @param inputs The properties to set during creation.
     */
    async create(urn: pulumi.URN, inputs: CommonProperties): Promise<provider.CreateResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);
        const resourceScheme = this.resolvedSchema.resources[resourceType];

        const disposables: AsyncDisposable[] = [];
        try {
            const assignments: FieldAssignment[] = [];

            const fields: Record<string, Field> = {}
            const sections: Record<string, Section> = {}
            const attachments: Record<string, string> = {}
            Object.assign(fields, inputs.fields ?? {});
            Object.assign(sections, inputs.sections ?? {});
            Object.assign(attachments, inputs.attachments ?? {});


            for (const [name, propScheme] of Object.entries<any>(resourceScheme.inputProperties).filter(z => !propertiesToIgnore.includes(z[0]))) {
                if (inputs[name] != null) {
                    if (propScheme.refName) {
                        const sectionScheme = (this.resolvedSchema.types as any)[propScheme.refName];
                        // Can we have deeply nested sections? I dunno.
                        sections[name] = { fields: {} };
                        for (const [fieldName, sPropSchema] of Object.entries((sectionScheme?.properties ?? {}) as Record<string, any>)) {
                            if (inputs[name]?.[fieldName]) {
                                sections[name].fields[fieldName] = {
                                    value: inputs?.[name]?.[fieldName],
                                    type: sPropSchema.kind ?? null,
                                    purpose: sPropSchema.purpose ?? null
                                };
                            }
                        }
                    } else {
                        fields[name] = {
                            value: inputs[name],
                            type: propScheme.kind ?? null,
                            purpose: propScheme.purpose ?? null
                        };
                    }
                }
            }

            assignments.push(...assignFields(fields));
            assignments.push(...assignSections(sections));
            assignments.push(...await assignAttachments(attachments, disposables));
            // console.log(assignments);
            const name = inputs.title ?? newUniqueName(getNameFromUrn(urn));

            const result = item.create(
                assignments as any,
                {
                    category: resourceType === ItemType.Item ? 'Secure Note' : ItemTypeNames[resourceType] as any,
                    vault: inputs.vault,
                    tags: inputs.tags ?? [],
                    title: name,
                    generatePassword: createPasswordRecipe(inputs.generatePassword)
                }
            )

            const output = convertResultToOutputs(resourceType, result);
            // console.log(output);

            return {
                id: result.id,
                outs: output
            }
        }
        finally {
            for (const disposable of disposables) {
                await disposable[Symbol.asyncDispose]()
            }
        }

    }

    /**
     * Reads the current live state associated with a pulumi.  Enough state must be included in the inputs to uniquely
     * identify the resource; this is typically just the resource ID, but it may also include some properties.
     */
    async read(id: pulumi.ID, urn: pulumi.URN, props?: CommonProperties): Promise<provider.ReadResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);

        const result = item.get(id, { vault: props?.vault })

        const outputs = convertResultToOutputs(resourceType, result);
        return {
            id: result.id,
            props: outputs
        }
    }

    /**
     * Update updates an existing resource with new values.
     *
     * @param id The ID of the resource to update.
     * @param olds The old values of properties to update.
     * @param news The new values of properties to update.
     */
    async update(id: pulumi.ID, urn: pulumi.URN, olds: CommonProperties, news: CommonProperties): Promise<provider.UpdateResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);
        const resourceScheme = this.resolvedSchema.resources[resourceType];

        const delta = this.diffValues(resourceType, olds, news)
            ;
        const assignments: FieldAssignment[] = [];
        // console.log({ olds, news });
        // console.log(delta);



        const getFieldPath = (path: (string | number)[]) => {
            return this.getFieldPathParts(path).join('.')
        }

        const disposables: AsyncDisposable[] = [];
        try {
            const replacedFields: Record<string, Field> = {}
            const deletes: string[] = []
            for (const item of delta) {
                if (item.path[0] === 'sections' || item.path[0] === 'fields') {
                    if (item.path[2] === 'value' || item.path[4] === 'value' || item.path[2] === 'type' || item.path[4] === 'type' || item.path[2] === 'purpose' || item.path[4] === 'purpose') {
                        if (item.op === "remove") {

                            const path = item.path.concat();
                            path.pop();
                            deletes.push(getFieldPath(item.path));
                        }
                        if ((item.op === "add" || item.op === "replace") && item.path.length > 1) {
                            const path = item.path.concat();
                            path.pop();
                            replacedFields[getFieldPath(path)] = get(news, path.join('.'))
                        }
                    } else if ((item.path[0] === 'fields' && item.path.length === 2) || (item.path[0] === 'sections' && item.path.length === 4)) {
                        if (item.op === "remove") {
                            deletes.push(getFieldPath(item.path));
                        }
                        if ((item.op === "add" || item.op === "replace") && item.path.length > 1) {
                            replacedFields[getFieldPath(item.path)] = get(news, item.path.join('.'))
                        }
                    } else if (item.path[0] === 'sections' && item.path.length === 2) {
                        if (item.op === "remove") {
                            deletes.push(...Object.keys(olds.sections?.[item.path[1]]?.fields ?? {})
                                .map(z => getFieldPath(item.path.concat(z))));
                        }
                        if ((item.op === "add" || item.op === "replace") && item.path.length > 1) {
                            Object.entries(olds.sections?.[item.path[1]]?.fields ?? {})
                                .forEach(z => {
                                    replacedFields[getFieldPath(item.path.concat(z[0]))] = z[1];
                                });

                        }
                    }
                    continue;
                }
                if (item.path[0] === 'attachments' && (item.op === "add" || item.op === "replace")) {
                    if (item.path.length === 0) {
                        assignments.push(...await assignAttachments(item.value, disposables))
                    } else if (item.path.length === 2) {
                        // console.log(item.value);
                        assignments.push(...await assignAttachments({ [item.path[1].toString()]: item.value }, disposables))
                    }
                    continue;
                }

                // console.log(item);

                if (item.op === "add" || item.op === "replace") {
                    const propScheme = this.getPropertySchema(resourceType, item);
                    assignments.push(createAssignment(item.path.join('.'), { value: propScheme.secret ? item.value.value : item.value, purpose: propScheme.purpose, type: propScheme.purpose === 'PASSWORD' ? 'concealed' : propScheme.kind }))
                }
                else if (item.op === "remove") {
                    deletes.push(item.path.join('.'))
                }

            }

            assignments.push(...assignFields(replacedFields));


            for (const item of uniq(deletes)) {
                if (assignments.some(z => z[0] === item)) {
                    continue;
                }
                // these can't be removed
                if (item === "notesPlain") continue;
                assignments.push([item, 'delete', ''])
            }
            // console.log(assignments);

            const name = news.title ?? newUniqueName(getNameFromUrn(urn));
            const result = item.edit(id, assignments as any, {
                vault: news.vault,
                tags: news.tags ?? [],
                title: name,
                generatePassword: createPasswordRecipe(news.generatePassword)
            });

            const output = convertResultToOutputs(resourceType, result);
            // console.log(output);

            return {
                outs: output
            }
        }
        finally {
            for (const disposable of disposables) {
                await disposable[Symbol.asyncDispose]()
            }
        }
    }

    /**
     * Delete tears down an existing resource with the given ID.  If it fails, the resource is assumed to still exist.
     *
     * @param id The ID of the resource to delete.
     * @param props The current properties on the pulumi.
     */
    async delete(id: pulumi.ID, urn: pulumi.URN, props: CommonProperties): Promise<void> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);
        item.delete(id);
    }

    /**
     * Check validates that the given property bag is valid for a resource of the given type.
     *
     * @param olds The old input properties to use for validation.
     * @param news The new input properties to use for validation.
     */
    async check(urn: pulumi.URN, olds: CommonProperties, news: CommonProperties): Promise<provider.CheckResult> {
        const failures: provider.CheckFailure[] = [];
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) return {};

        const typeName = ItemTypeNames[resourceType]
        if (resourceType !== ItemType.Item && news.category !== typeName) {
            failures.push({
                property: "category",
                reason: `Category must be ${typeName}`
            })
        }
        if (news.fields) {
            Object.entries(news.fields)
                .filter(z => z[0]?.includes('.') || z[0]?.includes('\\') || z[0]?.includes('='))
                .forEach(field => failures.push({
                    property: `fields.${field[0]}`,
                    reason: 'Field labels cannot contain a period, equals sign or backslash'
                }));
        }
        if (news.sections) {
            for (const section of Object.entries(news.sections)) {
                if (section[0]?.includes('.') || section[0]?.includes('\\') || section[0]?.includes('=')) {
                    failures.push({
                        property: `sections.${section[0]}`,
                        reason: 'Section labels cannot contain a period, equals sign or backslash'
                    })
                }
                for (const field of Object.entries(section[1].fields).filter(z => z[0]?.includes('.') || z[0]?.includes('\\') || z[0]?.includes('='))) {
                    failures.push({
                        property: `sections.${section[0]}.fields.${field[0]}`,
                        reason: 'Field labels cannot contain a period, equals sign or backslash'
                    });
                }
            }
        }
        return failures.length ? { inputs: news, failures } : {};
    }

    /**
     * Diff checks what impacts a hypothetical update will have on the resource's properties.
     *
     * @param id The ID of the resource to diff.
     * @param olds The old values of properties to diff.
     * @param news The new values of properties to diff.
     */
    async diff(id: pulumi.ID, urn: pulumi.URN, olds: CommonProperties, news: CommonProperties): Promise<provider.DiffResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);

        const delta = this.diffValues(resourceType, olds, news);
        console.log('delta', delta)

        return {
            changes: delta.length > 0,
            //deleteBeforeReplace ??
            // replaces ??
        }

    }

    private diffValues(resourceType: pulumi.URN, olds: CommonProperties, news: CommonProperties) {
        // console.log(JSON.stringify({ olds: prepareForDiff(olds), news: prepareForDiff(news) }, null, 2))
        return diff(prepareForDiff(olds), prepareForDiff(news))
            .filter(item => !(item.path.length === 1 && propertiesToIgnore.includes(item.path[0].toString())))
            .filter(item => !['tags', 'reference', 'label', 'hash', pulumi.runtime.specialSigKey].includes(item.path[item.path.length - 1].toString()))
            .filter(item => item.path[0] === 'attachments' && !['name', 'uuid', 'size'].includes(item.path[item.path.length - 1].toString()))
            .filter(item => !['references'].includes(item.path[0].toString()))
            .filter(item => {
                if (item.path?.[0] === "notesPlain") return false;
                const propScheme = this.getPropertySchema(resourceType, item);
                if (!propScheme) return true;

                if (propScheme.onePasswordId === "password" || propScheme.onePasswordId === "username" || propScheme.onePasswordId === "notesPlain") return false;
                return true;
            })
    }


    private getFieldPathParts(path: (string | number)[]) {
        return path.filter(z => z !== 'sections' && z !== 'fields')
    }

    private getPropertySchema(resourceType: string, item: { path: Array<string | number>; }) {
        const path = this.getFieldPathParts(item.path);
        const propScheme = ((this.resolvedSchema.resources as any)[resourceType].properties as any)[path[0]]!;
        if (propScheme?.refName) {
            const sectionScheme = (this.resolvedSchema.types as any)[propScheme.refName];
            return sectionScheme.properties[path[1]];
        } else {
            return propScheme;
        }
    }

    /**
     * Invoke calls the indicated function.
     *
     * @param token The token of the function to call.
     * @param inputs The inputs to the function.
     */
    async invoke(token: string, inputs: any): Promise<provider.InvokeResult> {
        const functionType = getFunctionType(token);
        if (!functionType) throw new Error(`unknown function type ${token}`);
        switch (functionType) {
            case 'one-password-native-unoffical:index:GetVault': return this.getVault(inputs);
            case 'one-password-native-unoffical:index:GetSecretReference': return this.getSecretReference(inputs);
            case 'one-password-native-unoffical:index:GetAttachment': return this.getAttachment(inputs);
            default: return this.getItem(functionType, inputs)
        }
    }
    /**
     * Call calls the indicated method.
     *
     * @param token The token of the method to call.
     * @param inputs The inputs to the method.
     */
    async call(token: string, inputs: any): Promise<provider.InvokeResult> {
        const functionType = getMethodType(token);
        if (!functionType) throw new Error(`unknown function type ${token}`);
        if (functionType.name === "attachment") {
            const hasSection = findSection(inputs?.name);
            const reference = hasSection ? makeReference(inputs?.__self__?.vault, inputs?.__self__?.uuid, hasSection[0], hasSection[1]) : makeReference(inputs?.__self__?.vault, inputs?.__self__?.uuid, inputs?.name)
            return this.getAttachment({ reference });
        }

        throw new Error(`unknown method type ${token}`);

        function findSection(name: string) {
            let escaped = false
            for (let i = 0; i < name.length; i++) {
                if (name[i] === '\\') {
                    escaped = true;
                    continue
                }
                if (!escaped && name[i] === '.') {
                    return [name.substring(0, i), name.substring(i + 1)] as const;
                }
                escaped = false
            }
            return undefined;
        }
    }

    private getVault(inputs: { vault: string }): provider.InvokeResult {
        const failures: provider.CheckFailure[] = [];
        if (!inputs.vault) failures.push({ property: 'vault', reason: `Must give a vault in order to get a Vault` });
        if (failures.length > 0) {
            return { failures };
        }

        const result = vault.get(inputs.vault);
        return {
            outputs: {
                name: result.name,
                uuid: result.id
            }
        }
    }

    private getSecretReference(inputs: { reference: string }): provider.InvokeResult {
        const failures: provider.CheckFailure[] = [];
        if (!inputs.reference) failures.push({ property: 'reference', reason: `Must give a reference in order to get an field by reference uri` });
        if (failures.length > 0) {
            return { failures };
        }

        const result = read.parse(inputs.reference);
        return {
            outputs: {
                value: result
            }
        }
    }

    private getAttachment(inputs: { reference?: string; }): provider.InvokeResult {
        const failures: provider.CheckFailure[] = [];
        if (!inputs.reference) failures.push(
            { property: 'reference', reason: `Must give the reference path for in order to get an Attachment` }
        );
        if (failures.length > 0) {
            return { failures };
        }

        const result = read.parse(inputs.reference!);
        return {
            outputs: {
                value: result
            }
        }
    }

    private getItem(token: ReturnType<NonNullable<typeof getFunctionType>>, inputs: { title?: string; uuid?: string; vault: string; }): provider.InvokeResult {
        const name = inputs.title ?? inputs.uuid;
        const failures: provider.CheckFailure[] = [];
        if (!name) failures.push({ property: 'title', reason: `Must give the title or uuid for in order to get a ${(ItemTypeNames as any)[token!]}` }, { property: 'uuid', reason: `Must give the title or uuid for in order to get a ${(ItemTypeNames as any)[token!]}` });
        if (!inputs.vault) failures.push({ property: 'vault', reason: `Must give a vault in order to get a ${(ItemTypeNames as any)[token!]}` });
        if (failures.length > 0) {
            return { failures };
        }

        const result = item.get(name!, { vault: inputs.vault })

        const outputs = convertResultToOutputs(token!, result);
        return { outputs };
    }
}

function getResourceTypeFromUrn(urn: pulumi.URN) {
    return ResourceTypes.find(z => urn?.includes(`:${z}:`));
}
function getFunctionType(urn: string) {
    return FunctionTypes.find(z => z === urn);
}
function getMethodType(urn: string) {
    const method = FunctionTypes.filter(z => z.indexOf('/') > -1).find(z => z === urn);
    if (!method) return undefined;
    return { method, name: last(method.split('/')) } as const;
}
function getNameFromUrn(urn: pulumi.URN) {
    const parts = urn.split(':');
    return parts[parts.length - 1]
}

function newUniqueName(prefix: string, randomSeed?: Buffer, randlen = 8, maxlen?: number, charset?: string): string {
    if (randlen <= 0) {
        randlen = 8
    }
    if (maxlen && maxlen > 0 && prefix.length + randlen > maxlen) {
        throw new Error(`name '${prefix}' plus ${randlen} random chars is longer than maximum length ${maxlen}`)
    }

    if (!charset) {
        charset = "0123456789abcdef";
    }

    let r: import("random").Random;
    if (!randomSeed || randomSeed.length === 0) {
        r = new Random(RNGFactory(randomBytes(randlen).toString('hex')))
    } else {
        const hash = createHash('sha256');
        hash.write(randomSeed);
        const seed = hash.digest('hex')
        r = new Random(RNGFactory(seed));
    }

    let randomSuffix = '';
    for (let i = 0; i < randlen; i++) {
        randomSuffix += charset[r.int(0, charset.length - 1)]
    }

    return prefix + randomSuffix;
}

interface CommonProperties { category: string; fields?: Record<string, Field>; sections?: Record<string, Section>;[key: string]: any };
interface Field {
    purpose?: FieldPurpose;
    type?: FieldAssignmentType;
    value: string;
}
interface Section {
    fields: Record<string, Field>;
}
interface OutAttachment {
    uuid: string;
    name: string;
    size: number;
    reference?: string;
}
interface OutField {
    type?: ResponseFieldType;
    value?: string;
    uuid: string;
    label: string;
    reference?: string;
}
interface OutSection {
    fields: Record<string, OutField>;
    uuid: string;
    label: string;
}

function createAssignment(path: string, field: Field): FieldAssignment {
    // not currently supported vial the cli
    // if (field.type === 'reference') {
    //     return ['linked items.' + last(path.split('.')), field.type, field.value];
    // }
    const isPassword = field.purpose === 'PASSWORD' || field.type === 'concealed';
    return [path, field.purpose === 'PASSWORD' ? 'concealed' : field.type ?? 'text', isPassword ? ((field.value as any)?.value ?? field.value) : field.value, field.purpose];
}

function assignFields(fields: Record<string, Field>, prefix?: string) {
    const assignments: FieldAssignment[] = [];
    for (const field of Object.entries(fields)) {
        const path = prefix ? `${prefix}.${field[0]}` : field[0];
        assignments.push(createAssignment(path, field[1]))
    }
    return assignments;
}

async function assignAttachments(attachments: Record<string, string>, disposables: AsyncDisposable[]) {
    const assignments: FieldAssignment[] = [];
    const tempDirectory = await fs.promises.mkdtemp(newUniqueName('attachments'))
    for (const [name, asset] of Object.entries(attachments)) {

        // console.log((asset as any)?.constructor, asset);
        const resolvedAsset = await resolveAsset(name, asset, tempDirectory);
        disposables.push(resolvedAsset);
        if (basename(resolvedAsset.value) === name) {
            assignments.push(['', 'file' as any, resolvedAsset.value])
        } else {
            assignments.push([name, 'file' as any, resolvedAsset.value])
        }
    }
    disposables.push({ [Symbol.asyncDispose]: () => fs.promises.rmdir(tempDirectory) });
    return assignments;
}


function isAsset(item: any): item is pulumi.asset.Asset {
    return item[pulumi.runtime.specialSigKey] === pulumi.runtime.specialAssetSig;
}
function isFileAsset(item: any): item is pulumi.asset.FileAsset {
    return item['path'] !== undefined && isAsset(item)
}

function isStringAsset(item: any): item is pulumi.asset.StringAsset {
    return item['text'] !== undefined && isAsset(item)
}

function isRemoteAsset(item: any): item is pulumi.asset.RemoteAsset {
    return item['uri'] !== undefined && isAsset(item)
}
function isArchive(item: any): item is pulumi.asset.Archive {
    return item[pulumi.runtime.specialSigKey] === pulumi.runtime.specialArchiveSig;
}
function isFileArchive(item: any): item is pulumi.asset.FileArchive {
    return item['path'] !== undefined && isArchive(item)
}

function isAssetArchive(item: any): item is pulumi.asset.AssetArchive {
    return item['assets'] !== undefined && isArchive(item)
}

function isRemoteArchive(item: any): item is pulumi.asset.RemoteArchive {
    return item['uri'] !== undefined && isArchive(item)
}

async function resolveAsset(name: string, item: pulumi.asset.Asset | pulumi.asset.Archive, tempDirectory: string) {
    if (isFileAsset(item) || isFileArchive(item)) {
        return Promise.resolve(item.path)
            .then(z => resolve(z))
            .then(emptyAsset)
    }
    if (isStringAsset(item)) {
        return Promise.resolve(item.text)
            .then(content => {
                const tempFilePath = resolve(tempDirectory, name);
                return fs.promises.writeFile(tempFilePath, content).then(() => tempFilePath);
            })
            .then(returnAsset);
    }
    if (isRemoteAsset(item) || isRemoteArchive(item)) {
        throw new Error("Remote assets are not yet supported")
        // return Promise.resolve((await fetch(await item.uri)).text())
    }
    if (isAssetArchive(item)) {
        // TODO:
        throw new Error("ArchiveAsset is not yet supported")
    }
    throw new Error("Unknown asset type")


    function returnAsset(file: string) {
        return {
            [Symbol.asyncDispose]: () => fs.promises.unlink(file),
            value: file
        }
    }

    function emptyAsset(file: string) {
        return {
            [Symbol.asyncDispose]: () => Promise.resolve(void 0),
            value: file
        }
    }
}


function assignSections(sections: Record<string, Section>) {

    const assignments: FieldAssignment[] = [];
    for (const section of Object.entries(sections)) {
        assignments.push(...assignFields(section[1].fields, section[0]))
    }
    return assignments;

}

function makeReference(vaultOrUuid: string, itemOrUuid: string, field: string): string;
function makeReference(vaultOrUuid: string, itemOrUuid: string, section: string, field: string): string;
function makeReference(vaultOrUuid: string, itemOrUuid: string, fieldOrSection: string, field?: string) {
    if (!field) {
        return `op://${vaultOrUuid}/${itemOrUuid}/${fieldOrSection}`
    }
    return `op://${vaultOrUuid}/${itemOrUuid}/${fieldOrSection}/${field}`
}

function prepareForDiff(object: any) {
    const v = cloneDeep(object);
    for (const [key, value] of Object.entries<any>(object)) {
        if (value?.[pulumi.runtime.specialSigKey] === pulumi.runtime.specialSecretSig) {
            v[key] = value.value;
        }
        if (typeof v[key] === 'object') {
            v[key] = prepareForDiff(v[key])
        }
    }
    return v;
}

function createPasswordRecipe(value: boolean | { "letters": "boolean", "digits": "boolean", "symbols": "boolean", "length": "number" } | undefined) {
    if (!value) return false;
    if (value === true) return true;
    return Object.entries(value).map((value) => ((value as any)[1] === true) ? value[0] : value[1]).join(',')
}

function convertResultToOutputs(kind: string, opResult: import('@1password/op-js').Item) {
    opResult.fields ??= [];
    opResult.files ??= [];
    const output = {
        attachments: opResult.files!.reduce((result, value) => {
            result[value.name] = {
                name: value.name,
                size: value.size,
                uuid: value.id,
                reference: makeReference(opResult.vault.id, opResult.id, value.id)!
            }

            return result;
        }, {} as Record<string, OutAttachment>),
        references: opResult.fields
            .filter(z => z.type === "REFERENCE")
            .reduce((result, value) => {
                result[camelCase(value.label ?? value.id)] = {
                    label: value.label ?? null,
                    uuid: value.id ?? null,
                    type: value.type as any ?? null,
                    reference: value.reference ?? makeReference(opResult.vault.id, opResult.id, value.id)!
                };
                if (value.value) {
                    let v: any = value.value;
                    result[camelCase(value.label ?? value.id)].value = v;
                }
                return result;
            }, {} as Record<string, OutField>),
        fields: opResult.fields
            .reduce((result, value) => {
                result[camelCase(value.label ?? value.id)] = {
                    label: value.label ?? null,
                    uuid: value.id ?? null,
                    type: value.type as any ?? null,
                    reference: value.reference ?? makeReference(opResult.vault.id, opResult.id, value.id)!
                };
                if (value.value) {
                    let v: any = value.value;
                    result[camelCase(value.label ?? value.id)].value = v;
                }
                return result;
            }, {} as Record<string, OutField>),
        sections: opResult.fields
            .filter(z => z.type !== "REFERENCE")
            .filter(z => !!z.section)
            .reduce((result, value) => {
                const sectionId = value.section!.id!;
                const field = camelCase(value.section!.label ?? value.section!.id);
                result[field] ??= {
                    label: value.section!.label!,
                    uuid: value.section!.id,
                    fields: {}
                };
                result[field].fields[camelCase(value.label ?? value.id)] = {
                    label: value.label ?? null,
                    uuid: value.id ?? null,
                    type: value.type as any ?? null,
                    reference: value.reference ?? makeReference(opResult.vault.id, opResult.id, sectionId, value.id)!
                }
                if (value.value) {
                    let v: any = value.value;
                    result[camelCase(value.section?.label ?? value.section!.id)].fields[camelCase(value.label ?? value.id)].value = v;
                }
                return result;
            }, {} as Record<string, OutSection>)
    } as Record<string, any>

    for (const item of PropertyPaths[kind] ?? []) {
        const outField: OutField = (item[1] && output.sections?.[item[1]!]?.fields?.[item[0]]) ?? output.fields?.[item[0]];
        if (!outField || outField.type === "MONTH_YEAR" && outField.value === "0") {
            continue;
        }
        if (outField.value) {
            set(output, item.reverse().join('.'), outField.value);
        }
    }

    return {
        ...output,
        uuid: opResult.id
    };
}