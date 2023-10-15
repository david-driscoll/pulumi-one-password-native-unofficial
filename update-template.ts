import { Field, GenericField, ItemTemplate, NotesField, OtpField, PasswordField, Section, UsernameField, item } from "@1password/op-js"
import { readFileSync, writeFileSync } from 'fs';
import { camelCase, uniq, orderBy, cloneDeep, last } from 'lodash'

const templates = item.template.list().concat({
    name: 'Item',
    uuid: "-1"
});
const schema = JSON.parse(readFileSync('./schema.json').toString('ascii'));

const allTemplates = orderBy(templates.map(z => camelCase(z.name)[0].toUpperCase() + camelCase(z.name).substring(1)));

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
            properties: {}
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
                "description": {
                    "type": "string",
                    "description": "The description of the vault.\n"
                },
                "id": {
                    "type": "string"
                },
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
    "onepassword:index:GetSection": {
        "properties": {
            "fields": {
                "type": "object",
                "additionalProperties": {
                    "$ref": "#/types/onepassword:index:GetField"
                }
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
    "onepassword:index:GetField": {
        "properties": {
            "id": {
                "type": "string"
            },
            "label": {
                "type": "string"
            },
            "purpose": {
                "$ref": "#/types/onepassword:index:FieldPurpose"
            },
            "value": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "id",
            "label",
            "purpose",
            "value"
        ]
    },
    "onepassword:index:Field": {
        "properties": {
            "purpose": {
                "$ref": "#/types/onepassword:index:FieldPurpose",
                "default": "NOTE",
                "willReplaceOnChanges": true
            },
            "value": {
                "type": "string"
            }
        },
        "type": "object",
        "required": [
            "purpose",
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
    }
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
    currentResource.inputProperties ??= {};
    currentResource.requiredInputs ??= [];
    currentResource.requiredInputs = ['vault'];
    currentResource.properties ??= {};
    currentResource.required ??= [];
    currentResource.required = uniq(currentResource.required.concat('tags', 'id', 'uuid', 'title', 'vault', 'category'));

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
    currentResource.inputProperties['vault'] = {
        "type": "string",
        "description": "The UUID of the vault the item is in.\n",
        "willReplaceOnChanges": true
    };

    applyDefaultOutputProperties(currentResource);

    const functionName = `onepassword:index:Get${template.name.replace(/ /g, '')}`
    const currentFunction = schema.functions[functionName] ??= {};
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
    currentFunction.outputs = applyDefaultOutputProperties({ properties: {} });

    const sections = templateSchema.fields
        .filter(z => !!z.section)
        .reduce((o, v) => {
            schema.types[getSectionKey(template.name, v.section!)] = o[getSectionKey(template.name, v.section!)] = { "type": "object", "properties": {} };
            currentResource.inputProperties[camelCase(v.section!.label ?? v.section!.id)] = { '$ref': `#/types/${getSectionKey(template.name, v.section!)}` };
            currentResource.properties[camelCase(v.section!.label ?? v.section!.id)] = { '$ref': `#/types/${getSectionKey(template.name, v.section!)}` };
            currentFunction.outputs.properties[camelCase(v.section!.label ?? v.section!.id)] = { '$ref': `#/types/${getSectionKey(template.name, v.section!)}` };
            return o;
        }, {} as Record<string, any>);

    for (const field of templateSchema.fields) {

        const fieldInfo = getFieldType(field);
        if (field.section) {
            const sectionKey = getSectionKey(template.name, field.section!);
            const section = sections[sectionKey];

            const sectionProperties = section.properties;
            sectionProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            if (fieldInfo.default) {
                sectionProperties[fieldInfo.name].default = fieldInfo.default
            }

        } else {
            currentResource.inputProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            currentResource.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            currentFunction.outputs.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            if (fieldInfo.default) {
                currentResource.inputProperties[fieldInfo.name].default = fieldInfo.default
                currentResource.properties[fieldInfo.name].default = fieldInfo.default
                currentFunction.outputs.properties[fieldInfo.name].default = fieldInfo.default
            }
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


console.log()
writeFileSync('./schema.json', JSON.stringify(schema, null, 4))

writeFileSync('./provider/cmd/pulumi-resource-onepassword/types.ts', `
export const ItemType = {
${Object.keys(schema.resources).map(z => `"${last(z.split(':'))}": "${z}"`).join(',\n')}
} as const
export const ItemTypeNames = {
${templates.map(z => `"${(z as any).resourceName}": "${z.name}"`).join(',\n')}
} as const
export const ResourceTypes = [${Object.keys(schema.resources).map(z => `"${z}"`).join(', ')}] as const
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
        secret: false,
        default: field.value,
        type: 'string',
    };

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
        case "ADDRESS":
            break;
        case "DATE":
            break;
        case "EMAIL":
            break;
        case "MONTH_YEAR":
            break;
        case "PHONE":
            break;
        case "REFERENCE":
            break;
        case "STRING":
            break;
        case "URL":
            break;
    }

    return fieldData;
}

function applyDefaultOutputProperties(item: any) {
    Object.assign(item.properties, {
        ['id']: {
            "type": "string"
        },
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
            "additionalProperties": { "$ref": "#/types/onepassword:index:GetSection" }
        },
        ['fields']: {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/onepassword:index:GetField" }
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