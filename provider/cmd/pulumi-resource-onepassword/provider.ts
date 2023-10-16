import * as pulumi from "@pulumi/pulumi";
import * as provider from "@pulumi/pulumi/provider";
import { ItemType, ResourceTypes, ItemTypeNames, FunctionTypes, PropertyPaths } from './types'
import { FieldAssignment, FieldAssignmentType, FieldPurpose, item, vault, read, ResponseFieldType } from "@1password/op-js"
import { createHash, randomBytes } from "crypto";
import { RNGFactory, Random } from "random/dist/cjs/random";
import { camelCase, get, set, uniq } from "lodash";
import { diff } from "just-diff";

const propertiesToIgnore = ['category', 'fields', 'sections', 'tags', 'title', 'vault', 'uuid'];


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

        const assignments: FieldAssignment[] = [];
        // TODO: well known fields, sections

        const fields: Record<string, Field> = {}
        const sections: Record<string, Section> = {}
        Object.assign(fields, inputs.fields ?? {});
        Object.assign(sections, inputs.sections ?? {});
        console.log(JSON.stringify(inputs, null, 4))


        for (const [name, propScheme] of Object.entries<any>(resourceScheme.inputProperties).filter(z => !propertiesToIgnore.includes(z[0]))) {
            if (inputs[name] != null) {
                if (propScheme.refName) {
                    const sectionScheme = (this.resolvedSchema.types as any)[propScheme.refName];
                    // Can we have deeply nested sections? I dunno.
                    sections[name] = { fields: {} };
                    for (const [sName, sPropSchema] of Object.entries((sectionScheme?.properties ?? {}) as Record<string, any>)) {
                        if (inputs?.[name]?.[sName]) {
                            sections[name].fields[sName] = {
                                value: inputs?.[name]?.[sName],
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
        const name = inputs.title ?? newUniqueName(getNameFromUrn(urn));

        console.log(JSON.stringify(assignments, null, 4))

        const result = item.create(
            assignments,
            {
                category: resourceType === ItemType.Item ? 'Secure Note' : ItemTypeNames[resourceType] as any,
                vault: inputs.vault,
                tags: inputs.tags ?? [],
                title: name,
            }
        )

        const output = convertResultToOutputs(resourceType, result);

        return {
            id: result.id,
            outs: output
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
     * Invoke calls the indicated function.
     *
     * @param token The token of the function to call.
     * @param inputs The inputs to the function.
     */
    async invoke(token: string, inputs: any): Promise<provider.InvokeResult> {
        const functionType = getFunctionType(token);
        if (!functionType) throw new Error(`unknown function type ${token}`);
        switch (functionType) {
            case 'onepassword:index:GetVault': return this.getVault(inputs)
            case 'onepassword:index:GetSecretReference': return this.getSecretReference(inputs)
            default: return this.getItem(functionType, inputs)
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

        const delta = diff(olds, news).filter(item => {
            return !(item.path.length === 1 && propertiesToIgnore.includes(item.path[0].toString()))
        });
        const assignments: FieldAssignment[] = [];

        const replaces: Record<string, Field> = {}
        const deletes: string[] = []
        for (const item of delta) {
            if (item.path.length > 1 && (item.path[0] === 'sections' || item.path[0] === 'fields')) {
                // do the other thing
                if (item.op === "add" || item.op === "replace") {
                    replaces[getFieldPath(item.path)] = get(news, item.path.slice(1).join('.'))
                }
                else if (item.op === "remove") {
                    const field: OutField = get(news, item.path.slice(1).join('.'));
                    // month tear seems to default to "0" as the default value, in this case do not try to delete it
                    if (field.value === "0" && field.type === "MONTH_YEAR") {
                        continue;
                    }
                    deletes.push(getFieldPath(item.path));
                }
                continue;
            }

            if (item.op === "add" || item.op === "replace") {
                const propScheme = (resourceScheme.inputProperties as any)[item.path[0]]!;
                if (propScheme.refName) {
                    const sectionScheme = (this.resolvedSchema.types as any)[propScheme.refName];
                    const sPropSchema = sectionScheme.properties[item.path[1]];
                    assignments.push(createAssignment(item.path.join('.'), { value: item.value, purpose: sPropSchema.purpose, type: sPropSchema.purpose === 'PASSWORD' ? 'concealed' : 'text' }))
                } else {
                    assignments.push(createAssignment(item.path.join('.'), { value: item.value, purpose: propScheme.purpose, type: propScheme.purpose === 'PASSWORD' ? 'concealed' : 'text' }))
                }
            }
            else if (item.op === "remove") {
                deletes.push(item.path.join('.'))
            }

        }

        assignments.push(...assignFields(replaces));
        for (const item of uniq(deletes)) {
            assignments.push([item, 'delete', ''])
        }

        const name = news.title ?? newUniqueName(getNameFromUrn(urn));
        const result = item.edit(id, assignments, {
            vault: news.vault,
            tags: news.tags ?? [],
            title: name,
        });

        const output = convertResultToOutputs(resourceType, result);

        return {
            outs: output
        }

        function getFieldPath(path: (string | number)[]) {
            return path.filter(z => z !== 'sections' && z !== 'fields').join('.')
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
                .filter(z => z[0].includes('.') || z[0].includes('\\') || z[0].includes('='))
                .forEach(field => failures.push({
                    property: `fields.${field[0]}`,
                    reason: 'Field labels cannot contain a period, equals sign or backslash'
                }));
        }
        if (news.sections) {
            for (const section of Object.entries(news.sections)) {
                if (section[0].includes('.') || section[0].includes('\\') || section[0].includes('=')) {
                    failures.push({
                        property: `sections.${section[0]}`,
                        reason: 'Section labels cannot contain a period, equals sign or backslash'
                    })
                }
                for (const field of Object.entries(section[1].fields).filter(z => z[0].includes('.') || z[0].includes('\\') || z[0].includes('='))) {
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

        const delta = diff(olds, news).filter(item => {
            return !(item.path.length === 1 && propertiesToIgnore.includes(item.path[0].toString()))
        });
        console.log(JSON.stringify({ olds, news, delta }, null, 2))



        return {
            changes: delta.length > 0,
            //deleteBeforeReplace ??
            // replaces ??
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

    private getItem(token: ReturnType<NonNullable<typeof getFunctionType>>, inputs: { title?: string; uuid?: string; vault: string; }): provider.InvokeResult {
        const name = inputs.title ?? inputs.uuid;
        const failures: provider.CheckFailure[] = [];
        if (!name) failures.push({ property: 'title', reason: `Must give the title or uuid for in order to get a ${ItemTypeNames[token!]}` }, { property: 'uuid', reason: `Must give the title or uuid for in order to get a ${ItemTypeNames[token!]}` });
        if (!inputs.vault) failures.push({ property: 'vault', reason: `Must give a vault in order to get a ${ItemTypeNames[token!]}` });
        if (failures.length > 0) {
            return { failures };
        }

        const result = item.get(name!, { vault: inputs.vault })

        const outputs = convertResultToOutputs(token!, result);
        return { outputs };
    }
    // /**
    //  * Call calls the indicated method.
    //  *
    //  * @param token The token of the method to call.
    //  * @param inputs The inputs to the method.
    //  */
    // async call(token: string, inputs: pulumi.Inputs): Promise<provider.InvokeResult> { }
}

function getResourceTypeFromUrn(urn: pulumi.URN) {
    return ResourceTypes.find(z => urn.includes(`:${z}:`));
}
function getFunctionType(urn: string) {
    return FunctionTypes.find(z => z === urn);
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
interface Field {
    purpose?: FieldPurpose;
    type?: FieldAssignmentType;
    value: string;
}
interface Section {
    fields: Record<string, Field>;
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
    const isPassword = field.purpose === 'PASSWORD' || field.type === 'concealed';
    return [path, field.purpose === 'PASSWORD' ? 'concealed' : 'text', isPassword ? (field.value as any)?.value : field.value, field.purpose];
}

function assignFields(fields: Record<string, Field>, prefix?: string) {
    const assignments: FieldAssignment[] = [];
    for (const field of Object.entries(fields)) {
        const path = prefix ? `${prefix}.${field[0]}` : field[0];
        assignments.push(createAssignment(path, field[1]))
    }
    return assignments;
}
function assignSections(sections: Record<string, Section>) {

    const assignments: FieldAssignment[] = [];
    for (const section of Object.entries(sections)) {
        assignments.push(...assignFields(section[1].fields, section[0]))
    }
    return assignments;

}

function convertResultToOutputs(kind: string, result: import('@1password/op-js').Item) {
    const output = {
        fields: result.fields?.reduce((result, value) => {
            result[camelCase(value.label ?? value.id)] = {
                label: value.label ?? null,
                uuid: value.id ?? null,
                type: value.type as any ?? null,
                reference: value.reference ?? null!
            };
            if (value.value) {
                let v: any = value.value;
                result[camelCase(value.label ?? value.id)].value = v;
            }
            return result;
        }, {} as Record<string, OutField>),
        sections: result.fields?.filter(z => !!z.section).reduce((result, value) => {
            result[camelCase(value.section?.label ?? value.section!.id)] ??= {
                label: value.section!.label!,
                uuid: value.section!.id,
                fields: {}
            };
            result[camelCase(value.section?.label ?? value.section!.id)].fields[camelCase(value.label ?? value.id)] = {
                label: value.label ?? null,
                uuid: value.id ?? null,
                type: value.type as any ?? null,
                reference: value.reference ?? null!
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
        uuid: result.id
    };
}