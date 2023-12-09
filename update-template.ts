import {
    Field,
    FieldAssignmentType,
    FieldPurpose,
    GenericField,
    ItemTemplate,
    ListItemTemplate,
    NotesField,
    OtpField,
    PasswordField,
    Section,
    UsernameField,
    ValueField,
    item
} from "@1password/op-js"
import { readFileSync, writeFileSync } from 'fs';
import { camelCase, uniq, orderBy, cloneDeep, last, has, get } from 'lodash'

const templates = orderBy(item.template.list().concat({
    name: 'Item',
    uuid: "-1"
}), z => z.name) as (ListItemTemplate & { resourceName: string; functionName: string; templateSchema: ItemTemplate })[];
const schema = JSON.parse(readFileSync('./schema.json').toString('ascii'));

const allTemplates = templates.map(z => camelCase(z.name)[0].toUpperCase() + camelCase(z.name).substring(1));
const resourcePropPaths: Record<string, [field: string, section?: string][]> = {};
const functionPropPaths: Record<string, [field: string, section?: string][]> = {};

// TODOS: Documents, password recipes, date fields, url fields, etc.
schema.resources = {}
schema.functions = {
    "one-password-native-unofficial:index:GetItem": {
        "description": "Use this data source to get details of an item by its vault uuid and either the title or the uuid of the item.",
        type: "object",
        inputs: {
            properties: {
                "title": {
                    "type": "string",
                    "description": "The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.\n"
                },
                "id": {
                    "type": "string",
                    "description": "The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.\n"
                },
                "vault": {
                    "type": "string",
                    "description": "The UUID of the vault the item is in.\n"
                }
            },
            required: ['vault']
        },
        outputs: applyDefaultOutputProperties({
            properties: {},
            required: []
        })
    },
    "one-password-native-unofficial:index:GetVault": {
        "description": "Use this data source to get details of a vault by either its name or uuid.\n",
        inputs: {
            properties: {
                "vault": {
                    "type": "string",
                    "description": "The vault to get information of.  Can be either the name or the UUID.\n"
                }
            },
            required: ['vault']
        },
        outputs: {
            properties: {
                "name": {
                    "type": "string",
                    "description": "The name of the vault to retrieve. This field will be populated with the name of the vault if the vault it looked up by its UUID.\n"
                },
                "id": {
                    "type": "string",
                    "description": "The UUID of the vault to retrieve. This field will be populated with the UUID of the vault if the vault it looked up by its name.\n"
                }
            }
        }
    },
    "one-password-native-unofficial:index:GetSecretReference": {
        inputs: {
            properties: {
                "reference": {
                    "type": "string",
                    "description": "The 1Password secret reference path to the item.  eg: op://vault/item/[section]/field \n"
                },
            },
            required: ['reference']
        },
        outputs: {
            "properties": {
                "value": {
                    "type": "string",
                    "secret": true
                }
            },
            "description": "The resolved reference value"
        }
    },
    "one-password-native-unofficial:index:Read": {
        inputs: {
            properties: {
                "reference": {
                    "type": "string",
                    "description": "The 1Password secret reference path to the item.  eg: op://vault/item/[section]/field \n"
                },
            },
            required: ['reference']
        },
        outputs: {
            "properties": {
                "value": {
                    "type": "string",
                    "secret": true
                }
            },
            "description": "The resolved reference value"
        }
    },
    "one-password-native-unofficial:index:Inject": {
        inputs: {
            properties: {
                "template": {
                    "type": "string",
                    "description": "The template that you want transformed with secrets"
                },
            },
            required: ['template']
        },
        outputs: {
            "properties": {
                "result": {
                    "type": "string",
                    "secret": true
                }
            },
            "description": "The result template with secrets replaced"
        }
    },
    "one-password-native-unofficial:index:GetAttachment": {
        inputs: {
            properties: {
                "reference": {
                    "type": "string",
                    "description": "The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file \n"
                },
            },
            required: ['reference']
        },
        outputs: {
            "properties": {
                "value": {
                    "type": "string",
                    "secret": true
                }
            },
            "description": "The attachment"
        }
    }
}


schema.types = {
    "one-password-native-unofficial:index:Category": {
        "type": "string",
        "description": `The category of the item. One of [${allTemplates.join(', ')}]\n`,
        enum: orderBy(templates, z => z.name).map(t => ({
            "name": camelCase(t.name)[0].toUpperCase() + camelCase(t.name).substring(1),
            "value": t.name
        }))
    },
    "one-password-native-unofficial:index:OutputSection": {
        "properties": {
            "fields": {
                "type": "object",
                "additionalProperties": {
                    "$ref": "#/types/one-password-native-unofficial:index:OutputField"
                }
            },
            'attachments': {
                "type": "object",
                "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputAttachment" },
            },
            "id": {
                "type": "string"
            },
            "label": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "fields",
            "id",
            "label"
        ]
    },
    "one-password-native-unofficial:index:Section": {
        "properties": {
            "fields": {
                "type": "object",
                "additionalProperties": {
                    "$ref": "#/types/one-password-native-unofficial:index:Field"
                }
            },
            'attachments': {
                "type": "object",
                "additionalProperties": { "$ref": "pulumi.json#/Asset" }
            },
            "label": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "fields"
        ]
    },
    "one-password-native-unofficial:index:OutputAttachment": {
        "properties": {
            "id": { "type": "string" },
            "name": { "type": "string" },
            "reference": { "type": "string" },
            "size": { "type": "integer" },
        },
        "type": "object",
        "required": ["id", "name", "size", "reference"]
    },
    "one-password-native-unofficial:index:Url": {
        "properties": {
            "label": { "type": "string" },
            "primary": { "type": "boolean" },
            "href": { "type": "string" },
        },
        "type": "object",
        "required": ["primary", "href"]
    },
    "one-password-native-unofficial:index:OutputUrl": {
        "properties": {
            "label": { "type": "string" },
            "primary": { "type": "boolean" },
            "href": { "type": "string" },
        },
        "type": "object",
        "required": ["primary", "href"]
    },
    "one-password-native-unofficial:index:Reference": {
        "properties": {
            "itemId": {
                "type": "string"
            },
        },
        "type": "object",
        "required": [
            "itemId"
        ]
    },
    "one-password-native-unofficial:index:OutputReference": {
        "properties": {
            "id": {
                "type": "string"
            },
            "label": {
                "type": "string"
            },
            "itemId": {
                "type": "string"
            },
            "reference": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "id",
            "label",
            "itemId",
            "reference"
        ]
    },
    "one-password-native-unofficial:index:OutputField": {
        "properties": {
            "id": {
                "type": "string"
            },
            "label": {
                "type": "string"
            },
            "type": {
                "$ref": "#/types/one-password-native-unofficial:index:FieldType"
            },
            "value": {
                "type": "string",
                "secret": true
            },
            "reference": {
                "type": "string"
            },
            "data": {
                "type": "object",
                "additionalProperties": { "$ref": "pulumi.json#/Any" }
            }
        },
        "type": "object",
        "required": [
            "id",
            "label",
            "type",
            "value",
            "reference",
            "data"
        ]
    },
    "one-password-native-unofficial:index:Field": {
        "properties": {
            "type": {
                "$ref": "#/types/one-password-native-unofficial:index:FieldType",
                "default": "STRING"
            },
            "label": {
                "type": "string",
            },
            "value": {
                "type": "string",
                "secret": true
            }
        },
        "type": "object",
        "required": [
            "value"
        ]
    },
    "one-password-native-unofficial:index:FieldPurpose": {
        "type": "string",
        "enum": [
            {
                "name": "Username",
                "value": "USERNAME"
            },
            {
                "name": "Password",
                "value": "PASSWORD"
            },
            {
                "name": "Note",
                "value": "NOTE"
            }
        ]
    },
    "one-password-native-unofficial:index:FieldType": {
        "type": "string",
        "enum": [
            { "name": "Unknown", "value": "UNKNOWN" },
            { "name": "Address", "value": "ADDRESS" },
            { "name": "Concealed", "value": "CONCEALED" },
            { "name": "CreditCardNumber", "value": "CREDIT_CARD_NUMBER" },
            { "name": "CreditCardType", "value": "CREDIT_CARD_TYPE" },
            { "name": "Date", "value": "DATE" },
            { "name": "Email", "value": "EMAIL" },
            { "name": "Gender", "value": "GENDER" },
            { "name": "Menu", "value": "MENU" },
            { "name": "MonthYear", "value": "MONTH_YEAR" },
            { "name": "Otp", "value": "OTP" },
            { "name": "Phone", "value": "PHONE" },
            { "name": "Reference", "value": "REFERENCE" },
            { "name": "String", "value": "STRING" },
            { "name": "Url", "value": "URL" },
            { "name": "File", "value": "FILE" },
            { "name": "SshKey", "value": "SSHKEY" }
        ]
    },
    "one-password-native-unofficial:index:PasswordRecipe": {
        "properties": {
            "letters": {
                type: "boolean"
            },
            "digits": {
                type: "boolean"
            },
            "symbols": {
                type: "boolean"
            },
            "length": {
                type: "integer"
            },
        },
        "type": "object",
        "required": [
            "length"
        ]
    },
}

for (const template of templates) {
    console.log(template.name)


    const templateSchema = template.name === 'Item' ? item.template.get("Secure Note") as any as ItemTemplate : item.template.get(template.name as any) as any as ItemTemplate
    const resourceName = template.name === 'Item' ? `one-password-native-unofficial:index:Item` : `one-password-native-unofficial:index:${template.name.replace(/ /g, '')}Item`;
    template.resourceName = resourceName
    template.templateSchema = templateSchema as any;

    const currentResource = schema.resources[resourceName] ??= {
        "isComponent": false,
    };

    currentResource.isComponent = false;
    currentResource.inputProperties = {};
    currentResource.requiredInputs = [];
    currentResource.requiredInputs = ['vault'];
    currentResource.properties = {};
    currentResource.required = [];
    currentResource.required = uniq(currentResource.required.concat('tags', 'id', 'title', 'vault', 'category'));

    currentResource.inputProperties['tags'] = {
        type: 'array',
        items: {
            type: 'string'
        },
        "description": "An array of strings of the tags assigned to the item.\n"
    }
    currentResource.inputProperties['title'] = {
        "type": "string",
        "description": "The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.\n"
    };
    currentResource.inputProperties['notes'] = {
        "type": "string",
        "description": "The notes of the item.\n"
    };
    currentResource.inputProperties['sections'] = {
        "type": "object",
        "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:Section" }
    };
    currentResource.inputProperties['fields'] = {
        "type": "object",
        "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:Field" }
    };
    currentResource.inputProperties['attachments'] = {
        "type": "object",
        "additionalProperties": { "$ref": "pulumi.json#/Asset" }
    };
    // disabled until cli can actually input them.
    // currentResource.inputProperties['references'] = {
    //     "type": "array",
    //     "items": { "$ref": "#/types/one-password-native-unofficial:index:Reference" },
    // };
    currentResource.inputProperties['urls'] = {
        "type": "array",
        items: { "$ref": "#/types/one-password-native-unofficial:index:Url" }
    },
    currentResource.inputProperties['vault'] = {
        "type": "string",
        "description": "The UUID of the vault the item is in.\n",
        "willReplaceOnChanges": true
    };
    currentResource.stateInputs = {
        properties: {
            vault: Object.assign({}, currentResource.inputProperties['vault'])
        },
        required: ['vault']
    }
    currentResource.inputProperties['category'] = {
        "type": "string",
        "description": "The category of the vault the item is in.\n",
        "willReplaceOnChanges": true,
        "const": template.name,
    };

    applyDefaultOutputProperties(currentResource);

    const functionName = `one-password-native-unofficial:index:Get${template.name.replace(/ /g, '')}`;
    template.functionName = functionName
    const currentFunction = schema.functions[functionName] = {} as any;
    currentFunction.inputs = {
        "properties": {
            "title": {
                "type": "string",
                "description": "The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.\n"
            },
            "id": {
                "type": "string",
                "description": "The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.\n"
            },
            "vault": {
                "type": "string",
                "description": "The UUID of the vault the item is in.\n"
            }
        },
        "required": [
            "vault"
        ]
    };
    currentFunction.outputs = applyDefaultOutputProperties({ properties: {}, required: [] });

    // schema.functions[resourceName + '/attachment'] = {
    //     inputs: {
    //         properties: {
    //             "__self__": {
    //                 "$ref": `#/resources/${resourceName}`
    //             },
    //             "name": {
    //                 "type": "string",
    //                 "description": "The name or uuid of the attachment to get"
    //             }
    //         },
    //         required: ['__self__', 'name']
    //     },
    //     outputs: {
    //         "properties": {
    //             "value": {
    //                 description: 'the value of the attachment',
    //                 "type": "string",
    //                 "secret": true
    //             }
    //         },
    //         required: ['value'],
    //         "description": "The resolved reference value"
    //     }
    // }
    // Currently bugged.
    // currentResource.methods = { getAttachment: resourceName + '/attachment' }

    const sections = templateSchema.fields
        .filter(z => !!z.section)
        .reduce((o, v) => {
            const fieldInfo = getFieldType(v);
            const sectionKey = getSectionKey(template.name, v.section!);
            const objectKey = camelCase(v.section!.label ?? v.section!.id);
            schema.types[sectionKey] = o[sectionKey] = { "type": "object", "properties": {} };
            currentResource.inputProperties[objectKey] ??= { '$ref': `#/types/${sectionKey}`, refName: sectionKey };
            currentResource.properties[objectKey] ??= { '$ref': `#/types/${sectionKey}`, refName: sectionKey };
            currentResource.properties[objectKey].secret = currentResource.properties[objectKey].secret || fieldInfo.secret;
            currentFunction.outputs.properties[objectKey] ??= { '$ref': `#/types/${sectionKey}`, refName: sectionKey };
            currentFunction.outputs.properties[objectKey].secret = currentFunction.outputs.properties[objectKey].secret || fieldInfo.secret;
            return o;
        }, {} as Record<string, any>);

    resourcePropPaths[resourceName] ??= [];
    functionPropPaths[functionName] ??= [];
    for (const field of templateSchema.fields) {

        const fieldInfo = getFieldType(field);
        if (field.section) {
            const sectionKey = getSectionKey(template.name, field.section!);
            const section = sections[sectionKey];

            const sectionProperties = section.properties;
            sectionProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            }
            if (fieldInfo.default) {
                sectionProperties[fieldInfo.name].default = fieldInfo.default
            }
            const objectKey = camelCase(field.section!.label ?? field.section!.id);
            resourcePropPaths[resourceName].push([fieldInfo.name, objectKey])
            functionPropPaths[functionName].push([fieldInfo.name, objectKey])

        } else {
            currentResource.inputProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            }
            if (fieldInfo.purpose === 'PASSWORD' && fieldInfo.name === 'password') {
                currentResource.inputProperties['generatePassword'] = {
                    description: '',
                    oneOf: [{ type: "boolean" }, { '$ref': '#/types/one-password-native-unofficial:index:PasswordRecipe' }]
                }
            }
            currentResource.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            }
            currentFunction.outputs.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            }
            if (fieldInfo.default) {
                currentResource.inputProperties[fieldInfo.name].default = fieldInfo.default
                currentResource.properties[fieldInfo.name].default = fieldInfo.default
                currentFunction.outputs.properties[fieldInfo.name].default = fieldInfo.default
            }
            resourcePropPaths[resourceName].push([fieldInfo.name])
            functionPropPaths[functionName].push([fieldInfo.name])
        }
    }

}

schema.resources[`one-password-native-unofficial:index:Item`].inputProperties['category'] = {
    "oneOf": [
        {
            "$ref": "#/types/one-password-native-unofficial:index:Category"
        },
        {
            "type": "string"
        }
    ],
    "willReplaceOnChanges": true,
    default: 'Item'
};


writeFileSync('./schema.json', JSON.stringify(schema, null, 4))

writeFileSync('./provider/cmd/pulumi-resource-one-password-native-unofficial/types.ts', `
export const ItemType = {
${Object.keys(schema.resources)
        .concat(Object.keys(schema.functions))
        .map(z => `"${last(z.split(':'))}": "${z}"`).join(',\n')}
} as const;
export const ItemTypeNames = {
${templates.map(z => `"${(z as any).resourceName}": "${z.name}"`)
        .concat(
            templates.map(z => `"${(z as any).functionName}": "${z.name}"`)
        )
        .concat([
            `"one-password-native-unofficial:index:GetVault": "Vault"`,
            `"one-password-native-unofficial:index:GetSecretReference": "Secret Reference"`,
            `"one-password-native-unofficial:index:GetAttachment": "Attachment"`,
            `"one-password-native-unofficial:index:Read": "Read"`,
            `"one-password-native-unofficial:index:Inject": "Inject"`,
        ])
        .join(',\n')}
} as const;
export const ResourceTypes = [${Object.keys(schema.resources).map(z => `"${z}"`).join(', ')}] as const;
export const FunctionTypes = [${Object.keys(schema.functions).map(z => `"${z}"`).join(', ')}] as const;
export const PropertyPaths: Record<string, [field: string, section?: string][]> = {
    ${Object.entries(resourcePropPaths)
        .concat(Object.entries(functionPropPaths))
        .map((v) => `"${v[0]}": ${JSON.stringify(v[1])}`).join(',\n')}
}
`)

writeFileSync('./provider/cmd/pulumi-resource-one-password-native-unofficial/Types.cs', `
using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial;
public static class ItemType
{
    ${Object.keys(schema.resources)
        .concat(Object.keys(schema.functions))
        .map(z => `public static string ${last(z.split(':'))} { get; } = "${z}";`).join('\n')}
}
public static partial class TemplateMetadata
{
    private static ImmutableArray<ResourceType> ResourceTypes = [
        ${templates
        .filter(z => !!z.resourceName)
        .map(template => {
            var properties = resourcePropPaths[template.resourceName!];
            const fields = properties.map(z => `("${z[0]}", ${z[1] ? ('"' + z[1] + '"') : "null"})`).join(', ');
            return `new("${template.resourceName}", "${template.name}", "${template.templateSchema.category}", TransformInputsTo${template.name.replace(/ /g, '')}, TransformOutputsTo${template.name.replace(/ /g, '')}, [${fields}])`;
        }).join(',\n')}];
    private static ImmutableArray<FunctionType> FunctionTypes = [
        ${templates
        .filter(z => !!z.functionName)
        .map(template => {
            var properties = functionPropPaths[template.functionName!];
            const fields = properties.map(z => `("${z[0]}", ${z[1] ? ('"' + z[1] + '"') : "null"})`).join(', ');
            return `new("${template.functionName}", "${template.name}", "${template.templateSchema.category}", TransformOutputsTo${template.name.replace(/ /g, '')}, [${fields}])`;
        }).join(',\n')}];
}
`)

writeFileSync('./provider/cmd/pulumi-resource-one-password-native-unofficial/Types.Transforms.cs', `using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;

namespace pulumi_resource_one_password_native_unofficial;

public static partial class TemplateMetadata
{
${templates.map(template => {
    const schema = template.templateSchema;
    const inputMethods = [];
    const outputMethods = [];
    for (const field of schema.fields) {
        if (isNotesField(field)) {
            inputMethods.push(templateGetTemplateField(field, 'notes'));
            outputMethods.push(outputsGetOutputField(field, 'notes'));
        } else {
            inputMethods.push(templateGetTemplateField(field));
            outputMethods.push(outputsGetOutputField(field));
        }

        // UsernameField | PasswordField | OtpField | NotesField | GenericField
    }
    /*


        {
            if (GetField(item, "myfield") is { } field)
            {
                // TODO date properties, etc.
                outputs.Add(field.Id, new PropertyValue(field.Value));
            }
        }*/

    /*
    Urls = urls.Order().ToImmutableArray(),
    Tags = tags.Order().ToImmutableArray(),
    Vault = vault,
    Fields = fields.OrderBy(z => z.Id).ThenBy(z => z.Section?.Id).ToImmutableArray()
    */
    return `
    public static Inputs TransformInputsTo${template.name.replace(/ /g, '')}(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
        ${inputMethods.join('\n')}
        fields.AddRange(AssignGenericElements(values, fields));
        return new Inputs()
        {
            Title = title ?? "",
            Category = ${template.name === 'Item' ? 'category' : 'resourceType.ItemName'},
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableArray()
        };
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsTo${template.name.replace(/ /g, '')}(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
        ${outputMethods.join('\n')}
        return outputs.ToImmutableDictionary();
    }
    `;

}).join('\n')}
}
`);


// GetSection(values, "info") is {} section && GetField(section, "notes") is {} field
function templateGetTemplateField(field: Field, fieldName?: string) {
    return `    {
        if (values.TryGetValue("${fieldName ?? field.id}", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                ${templateAssignFieldMetadata(field)}
            });
        }
        else if (${field.section ? `GetSection(values, "${camelCase(field.section.label ?? field.section.id)}") is {} section && GetField(section, "${fieldName ?? field.id}")` : `GetField(values, "${fieldName ?? field.id}")`} is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                ${templateAssignFieldMetadata(field)}
            });
        }
    }`
}

function outputsGetOutputField(field: Field, fieldName?: string) {
    return `    {
        if (GetField(template, "${fieldName ?? field.id}"${field.section?.id ? `, "${field.section?.id}"` : ''}) is { } field)
        {
            // TODO date properties, etc.
            ${field.section?.id ? `AddFieldToSection(outputs, "${field.section?.id}", "${fieldName ?? field.id}", new PropertyValue(field.Value));` : `outputs.Add("${fieldName ?? field.id}", new PropertyValue(field.Value))`};
        }
    }`
}

function templateAssignFieldMetadata(field: Field) {
    return `    Label = "${field.label}",
                Type = "${field.type}",
                Id = "${field.id}",
                Purpose = ${has(field, 'purpose') ? '"' + get(field, 'purpose') + '"' : 'null'},
                ${field.section ? `Section = new () { Id = "${field.section.id}", Label = ${field.section.label ? `"${field.section.label}"` : null} },` : ''}`;
}


/*
{
            "isComponent": true,
            "inputProperties": {
                "indexContent": {
                    "type": "string",
                    "description": "The HTML content for index.html."
                }
            },
            "requiredInputs": [
                "indexContent"
            ],
            "properties": {
                "bucket": {
                    "$ref": "/aws/v4.0.0/schema.json#/resources/aws:s3%2Fbucket:Bucket",
                    "description": "The bucket resource."
                },
                "websiteUrl": {
                    "type": "string",
                    "description": "The website URL."
                }
            },
            "required": [
                "bucket",
                "websiteUrl"
            ]
        }
 */

function getSectionKey(template: string, section: Section) {
    return `one-password-native-unofficial:${camelCase(template)}:${camelCase(section!.label || section!.id)}Section`;
}

function isUserNameField(field: Field): field is UsernameField {
    return field.type === "STRING" && (field as any).purpose === "USERNAME";
}

function isPasswordField(field: Field): field is PasswordField {
    return field.type === "CONCEALED" && (field as any).purpose === "PASSWORD";
}

function isNotesField(field: Field): field is NotesField {
    return field.type === "STRING" && (field as any).purpose === "NOTES";
}

function isOtpField(field: Field): field is OtpField {
    return field.type === "OTP";
}

function isGenericField(field: Field): field is GenericField {
    return !isOtpField(field) && !isNotesField(field) && !isPasswordField(field) && !isUserNameField(field);
}

function getFieldType(field: Field) {
    const fieldData = {
        name: camelCase(field.label),
        original: field.label,
        id: field.id,
        secret: false,
        default: field.value,
        type: 'string',
        kind: field.type,
        purpose: (field as any).purpose as FieldPurpose
    };

    // "concealed" | "text" | "email" | "url" | "date" | "monthYear" | "phone"

    if (isUserNameField(field)) {
    }
    if (isOtpField(field)) {
        fieldData.secret = true;
    }
    if (isNotesField(field)) {
        fieldData.name = 'notes';
    }
    if (isPasswordField(field)) {
        fieldData.secret = true;
    }
    /*
                        "boolean",
                        "integer",
                        "number",
                        "string"
                        "object"
                         */

    fieldData.secret = fieldData.secret || fieldData.kind === 'CONCEALED'

    return fieldData;
}

function applyDefaultOutputProperties(item: any) {
    item.required.push('tags', 'id', 'title', 'vault', 'category', 'fields', 'sections', 'attachments', 'references');
    Object.assign(item.properties, {
        ['id']: {
            "type": "string",
            "description": "The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.\n"
        },
        ['title']: {
            "type": "string",
            "description": "The title of the item.\n"
        },
        ['notes']: {
            "type": "string",
            "description": "The notes of the item.\n"
        },
        ['vault']: {
            "type": "object",
            required: ['name', 'id'],
            properties: {
                name: {

                    "type": "string",
                    "description": "The name of the vault item is in.\n"
                },
                ['id']: {
                    "type": "string",
                    "description": "The UUID of the vault the item is in.\n"
                },
            }
        },
        ['sections']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputSection" },
            secret: true
        },
        ['fields']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputField" },
            secret: true
        },
        ['attachments']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputAttachment" },
            secret: true
        },
        ['references']: {
            "type": "array",
            "items": { "$ref": "#/types/one-password-native-unofficial:index:OutputReference" },
        },
        ['urls']: {
            "type": "array",
            items: { "$ref": "#/types/one-password-native-unofficial:index:OutputUrl" }
        },
        ['tags']: {
            type: 'array',
            items: {
                type: 'string'
            },
            "description": "An array of strings of the tags assigned to the item.\n"
        },
        ['category']: {
            "oneOf": [
                {
                    "$ref": "#/types/one-password-native-unofficial:index:Category"
                },
                {
                    "type": "string"
                }
            ]
        },

        /*
        lastEditedBy: opResult.last_edited_by,
        createdAt: opResult.created_at,
        updatedAt: opResult.updated_at,
        additionalInformation: opResult.additional_information,
         */
        // ['lastEditedBy']: {
        //     "type": "string"
        // },
        // ['createdAt']: {
        //     "type": "string"
        // },
        // ['updatedAt']: {
        //     "type": "string"
        // },
        // ['additionalInformation']: {
        //     "type": "string"
        // },

    });
    return item
}
