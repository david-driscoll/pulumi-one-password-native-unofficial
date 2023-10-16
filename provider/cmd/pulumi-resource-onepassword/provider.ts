import * as pulumi from "@pulumi/pulumi";
import * as provider from "@pulumi/pulumi/provider";
import { ItemType, ResourceTypes, ItemTypeNames, FunctionTypes, PropertyPaths } from './types'
import { FieldAssignment, FieldAssignmentType, FieldPurpose, item, vault, read } from "@1password/op-js"
import { createHash, randomBytes } from "crypto";
import { RNGFactory, Random } from "random/dist/cjs/random";
import { camelCase } from "lodash";

const propertiesToIgnore = ['category', 'fields', 'sections', 'tags', 'title', 'vault'];


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
        const fieldPropPaths: [field: string, section?: string][] = [];
        Object.assign(fields, inputs.fields ?? {});
        Object.assign(sections, inputs.sections ?? {});

        for (const [name, propScheme] of Object.entries(resourceScheme.inputProperties).filter(z => !propertiesToIgnore.includes(z[0]))) {
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
                            fieldPropPaths.push([sName, name]);
                        }
                    }
                } else {
                    fields[name] = {
                        value: inputs[name],
                        type: propScheme.kind ?? null,
                        purpose: propScheme.purpose ?? null
                    };
                    fieldPropPaths.push([name]);
                }
            }
        }
        assignments.push(...assignFields(fields));
        assignments.push(...assignSections(sections));

        const name = inputs.title ?? newUniqueName(getNameFromUrn(urn));

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
            outs: {
                ...output,
                uuid: result.id
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
     * Update updates an existing resource with new values.
     *
     * @param id The ID of the resource to update.
     * @param olds The old values of properties to update.
     * @param news The new values of properties to update.
     */
    // async update(id: pulumi.ID, urn: pulumi.URN, olds: any, news: any): Promise<provider.UpdateResult> {

    // }

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

    private getVault(inputs: { vault: string }): provider.InvokeResult {
        const failures: provider.CheckFailure[] = [];
        if (!inputs.vault) failures.push({ property: 'vault', reason: `Must give a vault in order to get a Vault` });
        if (failures.length > 0) {
            return { failures };
        }

        const result = vault.get(inputs.vault);
        return {
            outputs: {
                description: '',
                name: result.name,
                uuid: result.id,
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
        return { outputs: { value: result } }
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
    //  * Diff checks what impacts a hypothetical update will have on the resource's properties.
    //  *
    //  * @param id The ID of the resource to diff.
    //  * @param olds The old values of properties to diff.
    //  * @param news The new values of properties to diff.
    //  */
    // async diff(id: pulumi.ID, urn: pulumi.URN, olds: any, news: any): Promise<provider.DiffResult> { }
    // /**
    //  * Reads the current live state associated with a pulumi.  Enough state must be included in the inputs to uniquely
    //  * identify the resource; this is typically just the resource ID, but it may also include some properties.
    //  */
    // async read(id: pulumi.ID, urn: pulumi.URN, props?: any): Promise<provider.ReadResult> { }
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
    type?: FieldAssignmentType;
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

function assignFields(fields: Record<string, Field>, prefix?: string) {
    const assignments: FieldAssignment[] = [];
    for (const field of Object.entries(fields)) {
        const path = prefix ? `${prefix}.${field[0]}` : field[0];
        assignments.push([path, field[1].purpose === 'PASSWORD' ? 'concealed' : 'text', field[1].value, field[1].purpose])
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
                result[camelCase(value.label ?? value.id)].value = value.value;
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
                result[camelCase(value.section?.label ?? value.section!.id)].fields[camelCase(value.label ?? value.id)].value = value.value;
            }
            return result;
        }, {} as Record<string, OutSection>)
    } as Record<string, any>

    for (const item of PropertyPaths[kind] ?? []) {
        if (item[1] && output.sections?.[item[1]!]?.fields?.[item[0]]?.value) {
            output[item[1]] ??= {};
            Object.defineProperty(output[item[1]], item[0], {
                enumerable: true,
                get() {
                    return output.sections?.[item[1]!]?.fields?.[item[0]]?.value ?? null
                }
            })
        } else if (!item[1] && output.fields?.[item[0]]?.value) {
            Object.defineProperty(output, item[0], {
                enumerable: true,
                get() {
                    return output.fields?.[item[0]]?.value ?? null
                }
            })
        }
    }

    return {
        ...output,
        uuid: result.id
    };
}