import { Field, FieldAssignmentType, FieldPurpose, GenericField, ItemTemplate, NotesField, OtpField, PasswordField, Section, UsernameField, item } from "@1password/op-js"
import { readFileSync, writeFileSync } from 'fs';
import { camelCase, uniq, orderBy, cloneDeep, last } from 'lodash'

const templates = item.template.list().concat({
    name: 'Item',
    uuid: "-1"
});
const schema = JSON.parse(readFileSync('./schema.json').toString('ascii'));

const allTemplates = orderBy(templates.map(z => camelCase(z.name)[0].toUpperCase() + camelCase(z.name).substring(1)));
const resourcePropPaths: Record<string, [field: string, section?: string][]> = {};
const functionPropPaths: Record<string, [field: string, section?: string][]> = {};

// TODOS: Documents, password recipes, date fields, url fields, etc.

schema.functions = {
    "onepassword:index:GetItem": {
        "description": "Use this data source to get details of an item by its vault uuid and either the title or the uuid of the item.",
        type: "object",
        inputs: {
            properties: {
                "title": {
                    "type": "string",
                    "description": "The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.\n"
                },
                "uuid": {
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
    "onepassword:index:GetVault": {
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
                "uuid": {
                    "type": "string",
                    "description": "The UUID of the vault to retrieve. This field will be populated with the UUID of the vault if the vault it looked up by its name.\n"
                }
            }
        }
    },
    "onepassword:index:GetSecretReference": {
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
    }
}



schema.types = {
    "onepassword:index:Category": {
        "type": "string",
        "description": `The category of the item. One of [${allTemplates.join(', ')}]\n`,
        enum: orderBy(templates, z => z.name).map(t => ({
            "name": camelCase(t.name)[0].toUpperCase() + camelCase(t.name).substring(1),
            "value": t.name
        }))
    },
    "onepassword:index:OutSection": {
        "properties": {
            "fields": {
                "type": "object",
                "additionalProperties": {
                    "$ref": "#/types/onepassword:index:OutField"
                }
            },
            "uuid": {
                "type": "string"
            },
            "label": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "fields",
            "uuid",
            "label"
        ]
    },
    "onepassword:index:Section": {
        "properties": {
            "fields": {
                "type": "object",
                "additionalProperties": {
                    "$ref": "#/types/onepassword:index:Field"
                }
            }
        },
        "type": "object",
        "required": [
            "fields"
        ]
    },
    "onepassword:index:OutAttachment": {
        "properties": {
            "uuid": { "type": "string" },
            "name": { "type": "string" },
            "reference": { "type": "string" },
            "size": { "type": "integer" },
        },
        "type": "object",
        "required": [
            "uuid", "name", "size", "reference"
        ]
    },
    "onepassword:index:OutField": {
        "properties": {
            "uuid": {
                "type": "string"
            },
            "label": {
                "type": "string"
            },
            "type": {
                "$ref": "#/types/onepassword:index:ResponseFieldType"
            },
            "value": {
                "type": "string",
                "secret": true
            },
            "reference": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "uuid",
            "label",
            "type",
            "value",
            "reference"
        ]
    },
    "onepassword:index:Field": {
        "properties": {
            "type": {
                "$ref": "#/types/onepassword:index:FieldAssignmentType",
                "default": "text"
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
    "onepassword:index:FieldPurpose": {
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
    "onepassword:index:FieldAssignmentType": {
        "type": "string",
        "enum": [
            {
                "name": "Concealed",
                "value": "concealed"
            },
            {
                "name": "Text",
                "value": "text"
            },
            {
                "name": "Email",
                "value": "email"
            },
            {
                "name": "Url",
                "value": "url"
            },
            {
                "name": "Date",
                "value": "date"
            },
            {
                "name": "MonthYear",
                "value": "monthYear"
            },
            {
                "name": "Phone",
                "value": "phone"
            },
            // not currently supported for input
            // {
            //     "name": "Reference",
            //     "value": "reference"
            // },
            // {
            //     "name": "Otp",
            //     "value": "otp"
            // },
            // {
            //     "name": "SshKey",
            //     "value": "sshkey"
            // }
        ]
    },
    "onepassword:index:ResponseFieldType": {
        "type": "string",
        "enum": [
            {
                "name": "Unknown",
                "value": "UNKNOWN"
            },
            {
                "name": "Address",
                "value": "ADDRESS"
            },
            {
                "name": "Concealed",
                "value": "CONCEALED"
            },
            {
                "name": "CreditCardNumber",
                "value": "CREDIT_CARD_NUMBER"
            },
            {
                "name": "CreditCardType",
                "value": "CREDIT_CARD_TYPE"
            },
            {
                "name": "Date",
                "value": "Date"
            },
            {
                "name": "Email",
                "value": "EMAIL"
            },
            {
                "name": "Gender",
                "value": "GENDER"
            },
            {
                "name": "Menu",
                "value": "MENU"
            },
            {
                "name": "MonthYear",
                "value": "MONTH_YEAR"
            },
            {
                "name": "Otp",
                "value": "OTP"
            },
            {
                "name": "Phone",
                "value": "PHONE"
            },
            {
                "name": "Reference",
                "value": "REFERENCE"
            },
            {
                "name": "String",
                "value": "STRING"
            },
            {
                "name": "Url",
                "value": "URL"
            },
            {
                "name": "File",
                "value": "FILE"
            },
            {
                "name": "SshKey",
                "value": "SSHKEY"
            }
        ]
    },
    "onepassword:index:PasswordRecipe": {
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



    const templateSchema = template.name === 'Item' ? { fields: [] } : item.template.get(template.name as any) as any as ItemTemplate
    const resourceName = template.name === 'Item' ? `onepassword:index:Item` : `onepassword:index:${template.name.replace(/ /g, '')}Item`;
    (template as any).resourceName = resourceName

    const currentResource = schema.resources[resourceName] ??= {
        "isComponent": false,
    };

    currentResource.isComponent = false;
    currentResource.inputProperties = {};
    currentResource.requiredInputs = [];
    currentResource.requiredInputs = ['vault'];
    currentResource.properties = {};
    currentResource.required = [];
    currentResource.required = uniq(currentResource.required.concat('tags', 'uuid', 'title', 'vault', 'category'));

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
    currentResource.inputProperties['sections'] = {
        "type": "object",
        "additionalProperties": { "$ref": "#/types/onepassword:index:Section" }
    };
    currentResource.inputProperties['fields'] = {
        "type": "object",
        "additionalProperties": { "$ref": "#/types/onepassword:index:Field" }
    };
    currentResource.inputProperties['attachments'] = {
        "type": "object",
        "additionalProperties": { "$ref": "pulumi.json#/Asset" }
    };
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

    const functionName = `onepassword:index:Get${template.name.replace(/ /g, '')}`;
    (template as any).functionName = functionName
    const currentFunction = schema.functions[functionName] = {} as any;
    currentFunction.inputs = {
        "properties": {
            "title": {
                "type": "string",
                "description": "The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.\n"
            },
            "uuid": {
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

    schema.functions[resourceName + '/attachment'] = {
        inputs: {
            properties: {
                "__self__": {
                    "$ref": `#/resources/${resourceName}`
                },
                "name": {
                    "type": "string",
                    "description": "The name or uuid of the attachment to get"
                }
            },
            required: ['__self__', 'name']
        },
        outputs: {
            "properties": {
                "value": {
                    description: 'the value of the attachment',
                    "type": "string",
                    "secret": true
                }
            },
            required: ['value'],
            "description": "The resolved reference value"
        }
    }
    currentResource.methods = { attachment: resourceName + '/attachment' }

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

    for (const field of templateSchema.fields) {

        resourcePropPaths[resourceName] ??= [];
        functionPropPaths[functionName] ??= [];
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
                    oneOf: [{ type: "boolean" }, { '$ref': '#/types/onepassword:index:PasswordRecipe' }]
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

schema.resources[`onepassword:index:Item`].inputProperties['category'] = {
    "oneOf": [
        {
            "$ref": "#/types/onepassword:index:Category"
        },
        {
            "type": "string"
        }
    ],
    "willReplaceOnChanges": true,
    default: 'Item'
};


writeFileSync('./schema.json', JSON.stringify(schema, null, 4))

writeFileSync('./provider/cmd/pulumi-resource-onepassword/types.ts', `
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
            `"onepassword:index:GetVault": "Vault"`,
            `"onepassword:index:GetSecretReference": "Secret Reference"`
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
    return `onepassword:${camelCase(template)}:${camelCase(section!.label || section!.id)}Section`;
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
        kind: field.type?.toLowerCase() as FieldAssignmentType,
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
    switch (field.type) {
        case "DATE":
            fieldData.kind = "date"
            break;
        case "EMAIL":
            fieldData.kind = "email"
            break;
        case "MONTH_YEAR":
            fieldData.kind = "monthYear"
            break;
        case "PHONE":
            fieldData.kind = "phone"
            break;
        case "URL":
            fieldData.kind = "url"
            break;
        case "ADDRESS":
        case "REFERENCE":
        case "STRING":
            fieldData.kind = "text"
            break;
    }

    fieldData.secret = fieldData.secret || fieldData.kind === 'concealed'

    return fieldData;
}

function applyDefaultOutputProperties(item: any) {
    item.required.push('tags', 'uuid', 'title', 'vault', 'category', 'fields', 'sections', 'attachments', 'references');
    Object.assign(item.properties, {
        ['uuid']: {
            "type": "string",
            "description": "The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.\n"
        },
        ['title']: {
            "type": "string",
            "description": "The title of the item.\n"
        },
        ['vault']: {
            "type": "string",
            "description": "The UUID of the vault the item is in.\n"
        },
        ['sections']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/onepassword:index:OutSection" },
            secret: true
        },
        ['fields']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/onepassword:index:OutField" },
            secret: true
        },
        ['attachments']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/onepassword:index:OutField" },
            secret: true
        },
        ['references']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/onepassword:index:OutField" },
            secret: true
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
                    "$ref": "#/types/onepassword:index:Category"
                },
                {
                    "type": "string"
                }
            ]
        }

    });
    return item
}