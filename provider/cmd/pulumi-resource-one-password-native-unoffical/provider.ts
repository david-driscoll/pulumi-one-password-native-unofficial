import * as pulumi from "@pulumi/pulumi";
import * as provider from "@pulumi/pulumi/provider";
import { ItemType, ResourceTypes, ItemTypeNames, FunctionTypes, PropertyPaths } from './types'
import { FieldAssignmentType, FieldPurpose, item, vault, read, ResponseFieldType, FieldAssignment, setConnect } from "@1password/op-js"
import { createHash, randomBytes } from "crypto";
import { RNGFactory, Random } from "random/dist/cjs/random";
import { camelCase, clone, cloneDeep, get, isEmpty, last, orderBy, set, sortBy, uniq, unset, valuesIn } from "lodash";
import { Operation, diff } from "just-diff";
import * as fs from "fs";
import { basename, resolve } from "path";

const propertiesThatCannotBeRemoved = ['category', 'fields', 'sections', 'tags', 'title', 'vault', 'uuid', 'generatePassword', 'notes', 'attachments', 'references', 'createdAt', 'updatedAt', 'password'];

interface InputField {
    purpose?: FieldPurpose;
    type?: FieldAssignmentType;
    value: string | PulumiSecretSpecial | PulumiArchiveSpecial | PulumiArchiveSpecial | PulumiResourcesSpecial | PulumiOutputValueSpecial;
}
interface InputSection {
    // label: string;
    fields: Record<string, InputField>;
    attachments: Record<string, pulumi.asset.Asset>;
}
interface InputReference {
    itemId: string;
}
interface InputUrl {
    label?: string;
    primary: boolean;
    href: string;
}

type InputFields = Record<string, InputField>;
type InputAttachments = Record<string, pulumi.asset.Asset>;
interface Inputs {
    fields?: InputFields;
    attachments?: InputAttachments;
    sections?: Record<string, InputSection>;
    references?: InputReference[];
    urls?: InputUrl[];
    tags?: string[];
    title?: string;
    category?: string;
    vault?: string;
    [field: string]: string | any;
    notes?: string;
    // deal with category, createdAt, updatedAt, lastEditedBy, references
}

interface OutputAttachment {
    uuid: string;
    name: string;
    size: number;
    reference: string;
    hash?: string;
}
interface OutputReference {
    itemId: string;
    uuid: string;
    label: string;
    reference: string;
}
interface OutputField {
    type?: ResponseFieldType;
    purpose?: string;
    value: string;
    uuid: string;
    label: string;
    reference: string;
    data: Record<string, any>;
}
interface OutputSection {
    fields: Record<string, OutputField>;
    attachments: Record<string, OutputAttachment>;
    uuid: string;
    label: string;
}
interface OutputUrl {
    label?: string;
    primary: boolean;
    href: string;
}

interface Outputs {
    attachments: Record<string, OutputAttachment>;
    fields: Record<string, OutputField>;
    sections: Record<string, OutputSection>;
    references: OutputReference[];
    urls: OutputUrl[];
    category: string;
    uuid: string;
    title: string;
    notes: string;
    vault: {
        name: string;
        uuid: string;
    };
    tags: string[];
    createdAt: string;
    updatedAt: string;
    [field: string]: string | any;
}



// import { StaticPage, StaticPageArgs } from "./staticPage";

export class Provider implements provider.Provider {
    resolvedSchema: typeof import('./schema.json');
    config: pulumi.Config;
    constructor(readonly version: string, readonly schema: string) {
        this.resolvedSchema = JSON.parse(schema);
        this.config = new pulumi.Config("one-password-native-unoffical");
    }

    /**
     * Create allocates a new instance of the provided resource and returns its unique ID afterwards.
     * If this call fails, the resource must not have been created (i.e., it is "transactional").
     *
     * @param inputs The properties to set during creation.
     */
    async create(urn: pulumi.URN, inputs: Inputs): Promise<provider.CreateResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);
        const resourceScheme = this.resolvedSchema.resources[resourceType];

        const disposables: AsyncDisposable[] = [];
        try {
            doLog(`==== Create: ${urn} ====`);
            doLog(Object.keys(this));
            // doLog('inputs', inputs)
            // NOTE: we need to filter the template fields from the fields object
            // then we can simply just let the field override the template field, if the user sets it so.
            const assignments: FieldAssignment[] = [];

            const fields: Record<string, InputField> = {}
            const sections: Record<string, InputSection> = {}
            const attachments: Record<string, InputAttachments> = {}
            const references: InputReference[] = inputs.references ?? []
            Object.assign(fields, inputs.fields ?? {});
            Object.assign(sections, inputs.sections ?? {});
            Object.assign(attachments, inputs.attachments ?? {});
            inputs.category = (resourceType === ItemType.Item ? inputs.category ?? 'Secure Note' : (ItemTypeNames as any)[resourceType as any] as any);
            inputs.title ??= newUniqueName(getNameFromUrn(urn));

            for (const path of PropertyPaths[resourceType] ?? []) {
                const [name, section] = path;
                const value = get(inputs, path.reverse().join('.'))
                if (value == null) continue;


                const propScheme = (resourceScheme.inputProperties as any)[path[0]];
                if (propScheme.refName) {
                    const sectionScheme = (this.resolvedSchema.types as any)[propScheme.refName];
                    // Can we have deeply nested sections? I dunno.
                    sections[name] ??= { fields: {}, attachments: {} }
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

            assignments.push(...assignFields(fields));
            // not currently supported by the cli
            // assignments.push(...assignReferences(references));
            assignments.push(...await assignSections(sections, disposables));
            assignments.push(...await assignAttachments(attachments, disposables));
            for (let index = 0; index < assignments.length; index++) {
                const element = assignments[index];
                assignments[index] = element.map(z => z === null || z === undefined ? '' : z) as any;
            }
            doLog('assignments', assignments);

            ensure1PasswordEnvironmentVariables(this.config);
            const result = item.create(
                assignments as any,
                {
                    category: inputs.category! as any,
                    vault: inputs.vault,
                    tags: inputs.tags ?? [],
                    title: inputs.title!,
                    generatePassword: createPasswordRecipe(inputs.generatePassword)
                }
            )
            // doLog('result', result);

            const output = convertResultToOutputs(resourceType, inputs, result);
            doLog(`==== DONE: ${urn} ====`);

            return {
                id: result.id,
                outs: output
            }
        }
        catch (e) {
            console.error(e);
            throw e;
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
    async read(id: pulumi.ID, urn: pulumi.URN, props?: { vault?: string; }): Promise<provider.ReadResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);

        ensure1PasswordEnvironmentVariables(this.config);
        const result = item.get(id, { vault: props?.vault })

        const outputs = convertResultToOutputs(resourceType, {}, result);
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
    async update(id: pulumi.ID, urn: pulumi.URN, _olds: Outputs, news: Inputs): Promise<provider.UpdateResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);
        const resourceScheme = this.resolvedSchema.resources[resourceType];

        doLog(`==== Update: ${urn} ====`);
        // const merged =
        const olds = convertOutputsToInputs(resourceType, _olds);

        news.category = (resourceType === ItemType.Item ? news.category ?? 'Secure Note' : (ItemTypeNames as any)[resourceType as any] as any);
        news.title ??= olds.title
        const delta = this.diffValues(resourceType, olds, news)
            // doLog('delta', delta);
            ;
        const assignments: FieldAssignment[] = [];



        const getFieldPath = (path: (string | number)[]) => {
            return this.getFieldPathParts(path).join('.')
        }

        const disposables: AsyncDisposable[] = [];
        const replacedFields: Record<string, InputField> = {}
        const deletes: string[] = []

        const handleField = (item: ReturnType<typeof diff>[number] & { path: (string | number)[] }) => {

            if (item.op === "remove") {
                deletes.push(getFieldPath(item.path));
            }
            if ((item.op === "add" || item.op === "replace")) {
                replacedFields[getFieldPath(item.path)] = get(news, item.path.join('.'))
            }
        };

        const handleFields = (item: ReturnType<typeof diff>[number] & { path: (string | number)[] }) => {
            if (item.op === "remove") {
                deletes.push(...
                    Object
                        .keys(get(olds, item.path.join('.')) ?? {})
                        .map(z => getFieldPath(item.path.concat(z)))
                );
            }
            if (item.op === "add" || item.op === "replace") {
                Object
                    .entries(get(news, item.path.join('.')) ?? {})
                    .forEach(z => {
                        replacedFields[getFieldPath(item.path.concat(z[0]))] = z[1] as any;
                    });
            }
        };
        try {
            for (const item of delta) {
                if (item.path[item.path.length - 1] === 'fields') {
                    handleFields(item)
                    continue;
                }
                if (item.path[item.path.length - 2] === 'fields') {
                    handleField(item)
                    continue;
                }
                if (item.path[item.path.length - 1] === 'attachments' && (item.op === "add" || item.op === "replace")) {
                    const section = item.path.length > 2 ? item.path[item.path.length - 2].toString() : undefined;
                    const values = get(news, item.path.join('.'));
                    assignments.push(...await assignAttachments(values, disposables, section))
                    continue;
                }
                if (item.path[item.path.length - 2] === 'attachments' && (item.op === "add" || item.op === "replace")) {
                    const name = item.path[item.path.length - 1].toString();
                    const section = item.path.length > 3 ? item.path[item.path.length - 3].toString() : undefined;
                    const value = get(news, item.path.join('.'));
                    assignments.push(...await assignAttachments({ [name]: value }, disposables, section))
                    continue;
                }

                // not currently supported by the cli
                // if (item.path[0] === 'references' && (item.op === "add" || item.op === "replace")) {
                //     if (item.path.length === 0) {
                //         assignments.push(...await assignReferences(item.value))
                //     } else if (item.path.length === 2) {
                //         assignments.push(assignReference(item.value));
                //     }
                //     continue;
                // }

                if (item.op === "add" || item.op === "replace") {
                    const propScheme = this.getPropertySchema(resourceType, item);
                    assignments.push(createAssignment(item.path.join('.'), { value: propScheme.secret ? item.value.value : item.value, purpose: propScheme.purpose, type: propScheme.purpose === 'PASSWORD' ? 'concealed' : propScheme.kind }))
                }
                else if (item.op === "remove") {
                    deletes.push(getFieldPath(item.path));
                }

            }

            assignments.push(...assignFields(replacedFields));


            for (const item of uniq(deletes)) {
                if (assignments.some(z => z[0] === item)) {
                    continue;
                }
                assignments.push([item, 'delete', ''])
            }
            doLog('assignments', assignments);

            ensure1PasswordEnvironmentVariables(this.config);
            const result = item.edit(id, assignments as any, {
                vault: news.vault,
                tags: news.tags ?? [],
                title: news.title,
                generatePassword: createPasswordRecipe(news.generatePassword)
            });
            // doLog('result', result);

            const output = convertResultToOutputs(resourceType, news, result);
            doLog(`==== DONE: ${urn} ====`);
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
    async delete(id: pulumi.ID, urn: pulumi.URN): Promise<void> {
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
    async check(urn: pulumi.URN, olds: Outputs, news: Inputs): Promise<provider.CheckResult> {
        const failures: provider.CheckFailure[] = [];
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);

        doLog(Object.keys(this))

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
    async diff(id: pulumi.ID, urn: pulumi.URN, olds: Outputs, news: Inputs): Promise<provider.DiffResult> {
        const resourceType = getResourceTypeFromUrn(urn);
        if (!resourceType) throw new Error(`unknown resource type ${urn}`);

        news.category = (resourceType === ItemType.Item ? news.category ?? 'Secure Note' : (ItemTypeNames as any)[resourceType as any] as any);
        news.title ??= olds.title
        const delta = this.diffValues(resourceType, convertOutputsToInputs(resourceType, olds), news);

        return {
            changes: delta.length > 0,
            deleteBeforeReplace: true,
            replaces: delta.some(z => z.path.length === 1 && z.op === 'replace' && z.path.includes('category')) ? ['category'] : undefined,
            stables: ['uuid'],
            //deleteBeforeReplace ??
            // replaces ??
        }
    }

    private diffValues(resourceType: ReturnType<typeof getResourceTypeFromUrn>, olds: Inputs, news: Inputs) {
        for (const [field, section] of PropertyPaths[resourceType as any] ?? []) {
            if (section && news?.sections?.[section]?.fields?.[field] != null) {
                news[section] ??= {};
                news[section][field] = news.sections?.[section]?.fields?.[field].value;
                delete news?.sections?.[section]?.fields?.[field];
            } else if (news?.fields?.[field] != null) {
                news[field] = news?.fields?.[field].value;
                delete news?.fields?.[field];
            }
        }
        olds = prepareForDiff(olds)
        news = prepareForDiff(news)
        if (olds.tags)
            olds.tags = orderBy(olds.tags, z => z);

        if (news.tags)
            news.tags = orderBy(news.tags, z => z);

        doLog('diffValues', JSON.stringify({ olds, news }, null, 2))
        const result = diff(olds, news)
            .filter(handleReservedProperties)
            .map(z => {
                doLog(z);
                return z;
            })
            .filter(handleImmutableAttachmentProperties)
            .filter(handleCannotDeleteReservedProperties)
            ;
        doLog('diffValues result', result)

        return result;

        function handleCannotDeleteReservedProperties(item: ReturnType<typeof diff>[number] & { path: (string | number)[] }) {
            return item.op === 'remove' && item.path.length === 1 ? !propertiesThatCannotBeRemoved.includes(item.path[0].toString()) : true;
        }
        function handleReservedProperties(item: ReturnType<typeof diff>[number] & { path: (string | number)[] }) {
            if (item.path.length !== 1) return true;
            if (resourceType == 'one-password-native-unoffical:index:Item' && item.path[0].toString() === 'category') return true;
            return !['category', 'uuid', 'sections', 'attachments', 'fields', 'references'].includes(item.path[0].toString());
        }
        function handleImmutableAttachmentProperties(item: ReturnType<typeof diff>[number] & { path: (string | number)[] }) {
            return (item.path[0] === 'attachments' || item.path[2] === 'attachments') ? !['name', 'uuid', 'size'].includes(item.path[item.path.length - 1].toString()) : true;
        }

    }


    private getFieldPathParts(path: (string | number)[]) {
        return path.filter(z => z !== 'sections' && z !== 'fields' && z !== 'attachments')
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

        ensure1PasswordEnvironmentVariables(this.config);
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

        ensure1PasswordEnvironmentVariables(this.config);
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

        ensure1PasswordEnvironmentVariables(this.config);
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

        ensure1PasswordEnvironmentVariables(this.config);
        const result = item.get(name!, { vault: inputs.vault })

        const outputs = convertResultToOutputs(token as any, inputs, result);
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

function createAssignment(path: string, field: InputField): FieldAssignment {
    if (typeof field.value === 'string') {
        return [path === 'notes' ? 'notesPlain' : path, field.purpose === 'PASSWORD' ? 'concealed' : field.type ?? 'text', field.value, field.purpose];
    }
    return [path, field.purpose === 'PASSWORD' ? 'concealed' : field.type ?? 'text', unwrapSpecialValue(field.value) as any, field.purpose];
}

function assignFields(fields: Record<string, InputField> | undefined, prefix?: string) {
    const assignments: FieldAssignment[] = [];
    for (const field of Object.entries(fields ?? {})) {
        const path = prefix ? `${prefix}.${field[0]}` : field[0];
        assignments.push(createAssignment(path, field[1]))
    }
    return assignments;
}

function assignReferences(references: InputReference[] | undefined) {
    return references?.map(assignReference) ?? [];
}

function assignReference(reference: InputReference): FieldAssignment {
    return ['linked items.' + last(reference.itemId), 'reference' as any, reference.itemId];
}

async function assignAttachment(name: string, asset: pulumi.asset.Asset, options: { disposables: AsyncDisposable[]; tempDirectory?: string; }, prefix?: string): Promise<FieldAssignment> {
    const tempDirectory = options.tempDirectory ?? await fs.promises.mkdtemp(newUniqueName('attachments'))
    const resolvedAsset = await resolveAsset(name, asset, tempDirectory);
    options.disposables.push(resolvedAsset);
    if (!options.tempDirectory) {
        options.disposables.push({ [Symbol.asyncDispose]: () => fs.promises.rmdir(tempDirectory) });
    }
    const path = prefix ? `${prefix}.${name}` : name;
    return [path, 'file' as any, resolvedAsset.value];
}

async function assignAttachments(attachments: Record<string, pulumi.asset.Asset> | undefined, disposables: AsyncDisposable[], prefix?: string) {
    const assignments: FieldAssignment[] = [];
    const tempDirectory = await fs.promises.mkdtemp(newUniqueName('attachments'))
    for (const [name, asset] of Object.entries(attachments ?? {})) {
        assignments.push(await assignAttachment(name, asset, { disposables, tempDirectory }, prefix))
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


async function assignSections(sections: Record<string, InputSection> | undefined, disposables: AsyncDisposable[]) {
    const assignments: FieldAssignment[] = [];
    for (const section of Object.entries(sections ?? {})) {
        // if (section[1].label) {
        //     assignments.push(createAssignment(`${section[0]}.label`, { value: section[1].label }))
        // }
        assignments.push(...assignFields(section[1].fields, section[0]))
        assignments.push(...await assignAttachments(section[1].attachments, disposables, section[0]))
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
        v[key] = unwrapSpecialValue(value)
        if (typeof v[key] === 'object') {
            v[key] = prepareForDiff(v[key])
        }
    }
    for (const [key, value] of Object.entries(v)) {
        if (isEmpty(value)) {
            delete v[key]
        }
    }
    return v;
}

interface PulumiSecretSpecial {
    [pulumi.runtime.specialSigKey]?: typeof pulumi.runtime.specialSecretSig;
    value: string;
}
interface PulumiResourcesSpecial {
    [pulumi.runtime.specialSigKey]?: typeof pulumi.runtime.specialResourceSig;
}
interface PulumiArchiveSpecial {
    [pulumi.runtime.specialSigKey]?: typeof pulumi.runtime.specialArchiveSig;
    hash: string;
}
interface PulumiAssetSpecial {
    [pulumi.runtime.specialSigKey]?: typeof pulumi.runtime.specialAssetSig;
    hash: string;
}
interface PulumiOutputValueSpecial {
    [pulumi.runtime.specialSigKey]?: typeof pulumi.runtime.specialOutputValueSig;
}

function isSpecialSecret(val: any): val is PulumiSecretSpecial {
    return val?.[pulumi.runtime.specialSigKey] === pulumi.runtime.specialSecretSig;
}

function unwrapSpecialValue(val: PulumiSecretSpecial | PulumiResourcesSpecial | PulumiArchiveSpecial | PulumiAssetSpecial | PulumiOutputValueSpecial): any {
    if (typeof val !== 'object') return val;
    if (isSpecialSecret(val)) {
        return val.value;
    }
    if (val?.[pulumi.runtime.specialSigKey] === pulumi.runtime.specialAssetSig || val?.[pulumi.runtime.specialSigKey] === pulumi.runtime.specialArchiveSig) {
        return { hash: val.hash }
    }
    if (val?.[pulumi.runtime.specialSigKey]) throw new Error("unknown signature key " + JSON.stringify({ val }))
    return val;
}

function createPasswordRecipe(value: boolean | { "letters": "boolean", "digits": "boolean", "symbols": "boolean", "length": "number" } | undefined) {
    if (!value) return false;
    if (value === true) return true;
    return Object.entries(value).map((value) => ((value as any)[1] === true) ? value[0] : value[1]).join(',')
}

//new method to handle converting a "cached" object back to it's consituent pieces
// diff updated to handle duplicate fields data (or part of the new method maybe?)
function convertOutputsToInputs(kind: string, olds: Outputs): Inputs {
    const result: Inputs = {
        attachments: {},
        fields: {},
        sections: {},
        references: [],
        urls: [],
        category: olds.category,
        vault: olds.vault.name,
        title: olds.title,
        tags: olds.tags,
        notes: olds.notes ?? '',
    };
    olds = prepareForDiff(olds);
    olds.attachments ??= {};
    olds.fields ??= {};
    olds.sections ??= {};
    olds.references ??= [];
    olds.urls ??= [];

    setAttachmentData(olds.attachments, result.attachments!);
    setFieldData(olds.fields, result.fields!)
    for (const reference of orderBy(olds.references, z => z.uuid)) {
        result.references?.push({
            itemId: reference.itemId
        });
    }
    result.urls = olds.urls.concat()
    for (const [name, section] of Object.entries(olds.sections)) {
        const sectionData = result.sections![name] = { attachments: {}, fields: {} }
        setAttachmentData(section.attachments ?? {}, sectionData.attachments);
        setFieldData(section.fields ?? {}, sectionData.fields)
    }

    for (const [field, section] of PropertyPaths[kind] ?? []) {
        const outField = section ? olds.sections?.[section]?.fields?.[field] : olds.fields?.[field];
        if (!outField || (outField.type === "MONTH_YEAR" || outField.type === "DATE") && outField.value === "0") {
            unset(result, section ? `sections.${section}.fields.${field}` : `fields.${field}`)
            continue;
        }

        set(result, [section, field].filter(z => !!z).join('.'), outField.value ?? '');
        unset(result, section ? `sections.${section}.fields.${field}` : `fields.${field}`)
    }

    return result;
}

function setAttachmentData(inputFields: Record<string, OutputAttachment>, outputFields: InputAttachments) {
    for (const [name, value] of Object.entries(inputFields)) {
        outputFields[name] = { hash: value.hash }
    }
}

function setFieldData(inputFields: Record<string, OutputField>, outputFields: Record<string, InputField>) {
    for (const [name, value] of Object.entries(inputFields)) {
        outputFields[name] = { value: value.value }
        if (value.type) {
            outputFields[name].type = convertResponseFieldTypeToFieldAssignmentType(value.type)
        }
        if (value.purpose) {
            outputFields[name].purpose = value.purpose.toUpperCase() as any;
        }
    }
}

function convertResponseFieldTypeToFieldAssignmentType(value: ResponseFieldType) {
    switch (value) {
        // "concealed" | "text" | "email" | "url" | "date" | "monthYear" | "phone" | "delete"
        case 'DATE': return 'date';
        case 'EMAIL': return 'email';
        case 'URL': return 'url';
        case 'PHONE': return 'phone';
        case 'CONCEALED': return 'concealed';
        case 'MONTH_YEAR': return 'monthYear';
        default:
            return 'text';
        // "REFERENCE"
        // "FILE"
        // "SSHKEY"
        // "STRING"
        // "GENDER"
        // "OTP"
        // "MENU"
    }
}

function convertResultToOutputs(kind: string, inputs: Inputs, opResult: import('@1password/op-js').Item): Outputs {

    // console.log('convertResultToOutputs')
    // console.log(kind, opResult)
    opResult.fields ??= [];
    opResult.files ??= [];
    opResult.urls ??= [];

    const sections: Record<string, OutputSection> = {};
    opResult.fields
        .filter(z => z.type !== "REFERENCE")
        .filter(hasSection)
        .reduce((result, value) => setOutputSectionField(value as any, result), sections);
    opResult.files
        .filter(hasSection)
        .reduce((result, value) => setOutputSectionAttachment(value as any, result, inputs.sections?.[value.section!.label!]!?.attachments?.[value.name] as any), sections);

    const result: Outputs = {
        attachments: opResult.files
            .filter(hasNoSection)
            .reduce((result, value) => setOutputAttachment(value, result, inputs.attachments![value.name] as any), {} as Record<string, OutputAttachment>),
        references: opResult.fields
            .filter(z => z.type === "REFERENCE")
            .map((value) => <OutputReference>{
                label: value.label,
                reference: value.reference,
                uuid: value.id,
                itemId: value.value
            }),
        fields: opResult.fields
            .filter(hasNoSection)
            .reduce((result, value) => setOutputField(value, result), {} as Record<string, OutputField>),
        urls: opResult.urls
            .map((value) => value as OutputUrl),
        sections,
        uuid: opResult.id,
        title: opResult.title ?? inputs.title,
        vault: {
            name: opResult.vault.name,
            uuid: opResult.vault.id
        },
        category: inputs.category!,
        tags: opResult.tags ?? [],
        createdAt: opResult.created_at,
        updatedAt: opResult.updated_at,
        notes: opResult.fields.find(z => z.id === 'notesPlain' || z.id === 'notes')?.value ?? inputs.notes ?? '',
    };

    for (const [field, section] of PropertyPaths[kind] ?? []) {
        const outField = section ? result.sections?.[section]?.fields?.[field] : result.fields?.[field];
        if (!outField || (outField.type === "MONTH_YEAR" || outField.type === "DATE") && outField.value === "0") {
            continue;
        }
        set(result, [section, field].filter(z => !!z).join('.'), outField.value ?? '');
    }


    // todo: in the future
    // if (opResult.last_edited_by)
    //     result.lastEditedBy = opResult.last_edited_by;
    // if (opResult.additional_information)
    //     result.additionalInformation = opResult.additional_information;
    return result;

    function setOutputField(value: import('@1password/op-js').Field, result: Record<string, OutputField>, section?: string) {
        const key = getOutputKey(value);
        const out = result[key] = {
            label: value.label!,
            uuid: value.id!,
            type: value.type?.toUpperCase(),
            reference: value.reference ?? (section ? makeReference(opResult.vault.id, opResult.id, section, value.id)! : makeReference(opResult.vault.id, opResult.id, value.id)!),
            data: {}
        } as OutputField;
        if (isOtpField(value)) {
            out.data['totp'] = value.totp;
        } else if (isNotesField(value)) {
            out.data['purpose'] = value.purpose;
        } else if (isPasswordField(value)) {
            out.data['purpose'] = value.purpose;
            if (value.password_details?.entropy ?? value.entropy) {
                out.data['entropy'] = value.password_details?.entropy ?? value.entropy;
            }
            out.data['generated'] = value.password_details?.generated ?? false;
            out.data['strength'] = value.password_details?.strength;
        } else if (isUserNameField(value)) {
            out.data['purpose'] = value.purpose;
        }
        if (value.value != null) {
            let v: any = value.value;
            out.value = v;
        }
        return result;
    }
    function setOutputAttachment(value: import('@1password/op-js').File, result: Record<string, OutputAttachment>, asset: { hash?: string }) {
        result[value.name] = {
            name: value.name,
            size: value.size,
            uuid: value.id,
            reference: makeReference(opResult.vault.id, opResult.id, value.id)!,
            hash: asset.hash
        }
        return result;
    }
    function setOutputSectionField(value: import('@1password/op-js').Field & { section: import('@1password/op-js').Section }, result: Record<string, OutputSection>) {
        setOutputField(value, setSection(value.section, result).fields, getOutputKey(value.section));
        return result;
    }
    function setOutputSectionAttachment(value: import('@1password/op-js').File & { section: import('@1password/op-js').Section }, result: Record<string, OutputSection>, asset: { hash?: string }) {
        setOutputAttachment(value, setSection(value.section, result).attachments, asset);
        return result;
    }
}

function getOutputKey(value: { label?: string | undefined; id: string }) { return value.id === 'notesPlain' ? 'notes' : camelCase(value.label ?? value.id); }
function setSection(section: import('@1password/op-js').Section, result: Record<string, OutputSection>) {
    const sectionKey = getOutputKey(section);
    result[sectionKey] ??= {
        label: section!.label!,
        uuid: section!.id,
        fields: {},
        attachments: {},
    };
    return result[sectionKey];
}


function isUserNameField(field: import('@1password/op-js').Field): field is import('@1password/op-js').UsernameField {
    return field.type === "STRING" && (field as any).purpose === "USERNAME";
}

function isPasswordField(field: import('@1password/op-js').Field): field is import('@1password/op-js').PasswordField {
    return field.type === "CONCEALED" && (field as any).purpose === "PASSWORD";
}

function isNotesField(field: import('@1password/op-js').Field): field is import('@1password/op-js').NotesField {
    return field.type === "STRING" && (field as any).purpose === "NOTES";
}

function isOtpField(field: import('@1password/op-js').Field): field is import('@1password/op-js').OtpField {
    return field.type === "OTP";
}

function isGenericField(field: import('@1password/op-js').Field): field is (import('@1password/op-js').GenericField) {
    return !isOtpField(field) && !isNotesField(field) && !isPasswordField(field) && !isUserNameField(field);
}

function doLog(message?: any, ...optionalParams: any[]) {
    console.log(message, ...optionalParams);
}
function doLog2(message?: any, ...optionalParams: any[]) {
    console.log(message, ...optionalParams);
}
function hasSection(z: { section?: import('@1password/op-js').Section }) {
    return !!z.section?.label;
}
function hasNoSection(z: { section?: import('@1password/op-js').Section }) {
    return !hasSection(z);
}
function ensure1PasswordEnvironmentVariables(config: pulumi.Config) {
    if (config.get("serviceAccountToken")) {
        process.env['OP_SERVICE_ACCOUNT_TOKEN'] = config.get("serviceAccountToken")!;
    }
    if (config.get("connectHost") && config.get("connectToken")) {
        setConnect(config.get("connectHost")!, config.get("connectToken")!)
    }
}