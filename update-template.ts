import { Field, GenericField, ItemTemplate, NotesField, OtpField, PasswordField, Section, UsernameField, item } from "@1password/op-js"
import { readFileSync, writeFileSync } from 'fs';
import { camelCase } from 'lodash'

const templates = item.template.list();
const schema = JSON.parse(readFileSync('./schema.json').toString('ascii'));

for (const template of templates) {
    console.log(template.name)

    const templateSchema = item.template.get(template.name as any) as any as ItemTemplate
    const resourceName = `onepassword:index:${template.name.replace(/ /g, '')}`

    const currentResource = schema.resources[resourceName] ??= {
        "isComponent": false,
    };
    currentResource.isComponent = false;
    currentResource.inputProperties ??= {};
    currentResource.requiredInputs ??= [];
    currentResource.properties ??= {};
    currentResource.required ??= [];
    currentResource.inputProperties['tags'] = {
        type: 'array',
        items: {
            type: 'string'
        }
    }
    currentResource.properties['tags'] = {
        type: 'array',
        items: {
            type: 'string'
        }
    }

    const sections = templateSchema.fields
        .filter(z => !!z.section)
        .reduce((o, v) => {
            o[getSectionKey(template.name, v.section!)] = { "type": "object", "properties": {} };
            currentResource.inputProperties[camelCase(v.section!.label ?? v.section!.id)] = { '$ref': `#/types/${getSectionKey(template.name, v.section!)}` };
            return o;
        }, {} as Record<string, any>);

    for (const field of templateSchema.fields) {

        const fieldInfo = getFieldType(field);
        if (field.section) {
            const sectionKey = getSectionKey(template.name, field.section!);
            const section = sections[sectionKey];

            const sectionProperties = section.properties;
            sectionProperties[camelCase(field.label ?? field.id)] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            if (fieldInfo.default) {
                sectionProperties[camelCase(field.label ?? field.id)].default = fieldInfo.default
            }

        } else {
            currentResource.inputProperties[camelCase(field.label ?? field.id)] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
            }
            if (fieldInfo.default) {
                currentResource.inputProperties[camelCase(field.label ?? field.id)].default = fieldInfo.default
            }
        }
    }

    // console.log(JSON.stringify(templateSchema, null, 4))
}


console.log()
writeFileSync('./schema.json', JSON.stringify(schema, null, 4))

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
    return `${camelCase(template)}:section:${camelCase(section!.label || section!.id)}`;
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
        field.label = 'notes';
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