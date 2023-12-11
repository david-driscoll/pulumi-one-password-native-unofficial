"use strict";
var _a, _b, _c, _d;
var _e;
Object.defineProperty(exports, "__esModule", { value: true });
var op_js_1 = require("@1password/op-js");
var fs_1 = require("fs");
var lodash_1 = require("lodash");
var templates = (0, lodash_1.orderBy)(op_js_1.item.template.list().concat({
    name: 'Item',
    uuid: "-1"
}), function (z) { return z.name; });
var schema = JSON.parse((0, fs_1.readFileSync)('./schema.json').toString('ascii'));
var allTemplates = templates.map(function (z) { return (0, lodash_1.camelCase)(z.name)[0].toUpperCase() + (0, lodash_1.camelCase)(z.name).substring(1); });
var resourcePropPaths = {};
var functionPropPaths = {};
// TODOS: Documents, password recipes, date fields, url fields, etc.
schema.resources = {};
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
                "uuid": {
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
    "one-password-native-unofficial:index:GetAttachment": {
        inputs: {
            properties: {
                "reference": {
                    "type": "string",
                    "description": "The 1Password secret reference path to the item.  eg: op://vault/item/[section]/file \n"
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
};
schema.types = {
    "one-password-native-unofficial:index:Category": {
        "type": "string",
        "description": "The category of the item. One of [".concat(allTemplates.join(', '), "]\n"),
        enum: (0, lodash_1.orderBy)(templates, function (z) { return z.name; }).map(function (t) { return ({
            "name": (0, lodash_1.camelCase)(t.name)[0].toUpperCase() + (0, lodash_1.camelCase)(t.name).substring(1),
            "value": t.name
        }); })
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
            // "label": {
            //     "type": "string"
            // }
        },
        "type": "object",
        "required": [
            "fields"
        ]
    },
    "one-password-native-unofficial:index:OutputAttachment": {
        "properties": {
            "uuid": { "type": "string" },
            "name": { "type": "string" },
            "reference": { "type": "string" },
            "size": { "type": "integer" },
        },
        "type": "object",
        "required": ["uuid", "name", "size", "reference"]
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
            "uuid": {
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
            "uuid",
            "label",
            "itemId",
            "reference"
        ]
    },
    "one-password-native-unofficial:index:OutputField": {
        "properties": {
            "uuid": {
                "type": "string"
            },
            "label": {
                "type": "string"
            },
            "type": {
                "$ref": "#/types/one-password-native-unofficial:index:ResponseFieldType"
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
            "uuid",
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
                "$ref": "#/types/one-password-native-unofficial:index:FieldAssignmentType",
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
    "one-password-native-unofficial:index:FieldAssignmentType": {
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
    "one-password-native-unofficial:index:ResponseFieldType": {
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
            // {
            //     "name": "Menu",
            //     "value": "MENU"
            // },
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
};
var _loop_1 = function (template) {
    console.log(template.name);
    var templateSchema = template.name === 'Item' ? op_js_1.item.template.get("Secure Note") : op_js_1.item.template.get(template.name);
    var resourceName = template.name === 'Item' ? "one-password-native-unofficial:index:Item" : "one-password-native-unofficial:index:".concat(template.name.replace(/ /g, ''), "Item");
    template.resourceName = resourceName;
    template.templateSchema = templateSchema;
    var currentResource = (_a = (_e = schema.resources)[resourceName]) !== null && _a !== void 0 ? _a : (_e[resourceName] = {
        "isComponent": false,
    });
    currentResource.isComponent = false;
    currentResource.inputProperties = {};
    currentResource.requiredInputs = [];
    currentResource.requiredInputs = ['vault'];
    currentResource.properties = {};
    currentResource.required = [];
    currentResource.required = (0, lodash_1.uniq)(currentResource.required.concat('tags', 'uuid', 'title', 'vault', 'category'));
    currentResource.inputProperties['tags'] = {
        type: 'array',
        items: {
            type: 'string'
        },
        "description": "An array of strings of the tags assigned to the item.\n"
    };
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
        items: { "$ref": "#/types/one-password-native-unofficial:index:Url" },
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
    };
    currentResource.inputProperties['category'] = {
        "type": "string",
        "description": "The category of the vault the item is in.\n",
        "willReplaceOnChanges": true,
        "const": template.name,
    };
    applyDefaultOutputProperties(currentResource);
    var functionName = "one-password-native-unofficial:index:Get".concat(template.name.replace(/ /g, ''));
    template.functionName = functionName;
    var currentFunction = schema.functions[functionName] = {};
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
    var sections = templateSchema.fields
        .filter(function (z) { return !!z.section; })
        .reduce(function (o, v) {
        var _a, _b, _c, _d;
        var _e, _f, _g;
        var fieldInfo = getFieldType(v);
        var sectionKey = getSectionKey(template.name, v.section);
        var objectKey = (0, lodash_1.camelCase)((_a = v.section.label) !== null && _a !== void 0 ? _a : v.section.id);
        schema.types[sectionKey] = o[sectionKey] = { "type": "object", "properties": {} };
        (_b = (_e = currentResource.inputProperties)[objectKey]) !== null && _b !== void 0 ? _b : (_e[objectKey] = { '$ref': "#/types/".concat(sectionKey), refName: sectionKey });
        (_c = (_f = currentResource.properties)[objectKey]) !== null && _c !== void 0 ? _c : (_f[objectKey] = { '$ref': "#/types/".concat(sectionKey), refName: sectionKey });
        currentResource.properties[objectKey].secret = currentResource.properties[objectKey].secret || fieldInfo.secret;
        (_d = (_g = currentFunction.outputs.properties)[objectKey]) !== null && _d !== void 0 ? _d : (_g[objectKey] = { '$ref': "#/types/".concat(sectionKey), refName: sectionKey });
        currentFunction.outputs.properties[objectKey].secret = currentFunction.outputs.properties[objectKey].secret || fieldInfo.secret;
        return o;
    }, {});
    (_b = resourcePropPaths[resourceName]) !== null && _b !== void 0 ? _b : (resourcePropPaths[resourceName] = []);
    (_c = functionPropPaths[functionName]) !== null && _c !== void 0 ? _c : (functionPropPaths[functionName] = []);
    for (var _f = 0, _g = templateSchema.fields; _f < _g.length; _f++) {
        var field = _g[_f];
        var fieldInfo = getFieldType(field);
        if (field.section) {
            var sectionKey = getSectionKey(template.name, field.section);
            var section = sections[sectionKey];
            var sectionProperties = section.properties;
            sectionProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            };
            if (fieldInfo.default) {
                sectionProperties[fieldInfo.name].default = fieldInfo.default;
            }
            var objectKey = (0, lodash_1.camelCase)((_d = field.section.label) !== null && _d !== void 0 ? _d : field.section.id);
            resourcePropPaths[resourceName].push([fieldInfo.name, objectKey]);
            functionPropPaths[functionName].push([fieldInfo.name, objectKey]);
        }
        else {
            currentResource.inputProperties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            };
            if (fieldInfo.purpose === 'PASSWORD' && fieldInfo.name === 'password') {
                currentResource.inputProperties['generatePassword'] = {
                    description: '',
                    oneOf: [{ type: "boolean" }, { '$ref': '#/types/one-password-native-unofficial:index:PasswordRecipe' }]
                };
            }
            currentResource.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            };
            currentFunction.outputs.properties[fieldInfo.name] = {
                type: fieldInfo.type,
                secret: fieldInfo.secret,
                purpose: fieldInfo.purpose,
                kind: fieldInfo.kind,
                onePasswordId: fieldInfo.id,
            };
            if (fieldInfo.default) {
                currentResource.inputProperties[fieldInfo.name].default = fieldInfo.default;
                currentResource.properties[fieldInfo.name].default = fieldInfo.default;
                currentFunction.outputs.properties[fieldInfo.name].default = fieldInfo.default;
            }
            resourcePropPaths[resourceName].push([fieldInfo.name]);
            functionPropPaths[functionName].push([fieldInfo.name]);
        }
    }
};
for (var _i = 0, templates_1 = templates; _i < templates_1.length; _i++) {
    var template = templates_1[_i];
    _loop_1(template);
}
schema.resources["one-password-native-unofficial:index:Item"].inputProperties['category'] = {
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
(0, fs_1.writeFileSync)('./schema.json', JSON.stringify(schema, null, 4));
(0, fs_1.writeFileSync)('./provider/cmd/pulumi-resource-one-password-native-unofficial/types.ts', "\nexport const ItemType = {\n".concat(Object.keys(schema.resources)
    .concat(Object.keys(schema.functions))
    .map(function (z) { return "\"".concat((0, lodash_1.last)(z.split(':')), "\": \"").concat(z, "\""); }).join(',\n'), "\n} as const;\nexport const ItemTypeNames = {\n").concat(templates.map(function (z) { return "\"".concat(z.resourceName, "\": \"").concat(z.name, "\""); })
    .concat(templates.map(function (z) { return "\"".concat(z.functionName, "\": \"").concat(z.name, "\""); }))
    .concat([
    "\"one-password-native-unofficial:index:GetVault\": \"Vault\"",
    "\"one-password-native-unofficial:index:GetSecretReference\": \"Secret Reference\"",
    "\"one-password-native-unofficial:index:GetAttachment\": \"Attachment\""
])
    .join(',\n'), "\n} as const;\nexport const ResourceTypes = [").concat(Object.keys(schema.resources).map(function (z) { return "\"".concat(z, "\""); }).join(', '), "] as const;\nexport const FunctionTypes = [").concat(Object.keys(schema.functions).map(function (z) { return "\"".concat(z, "\""); }).join(', '), "] as const;\nexport const PropertyPaths: Record<string, [field: string, section?: string][]> = {\n    ").concat(Object.entries(resourcePropPaths)
    .concat(Object.entries(functionPropPaths))
    .map(function (v) { return "\"".concat(v[0], "\": ").concat(JSON.stringify(v[1])); }).join(',\n'), "\n}\n"));
(0, fs_1.writeFileSync)('./provider/cmd/pulumi-resource-one-password-native-unofficial/Types.cs', "\nusing System.Collections.Immutable;\n\nnamespace pulumi_resource_one_password_native_unofficial;\npublic static class ItemType\n{\n    ".concat(Object.keys(schema.resources)
    .concat(Object.keys(schema.functions))
    .map(function (z) { return "public static string ".concat((0, lodash_1.last)(z.split(':')), " { get; } = \"").concat(z, "\";"); }).join('\n'), "\n}\npublic static partial class TemplateMetadata\n{\n    private static ImmutableArray<ResourceType> ResourceTypes = [\n        ").concat(templates
    .filter(function (z) { return !!z.resourceName; })
    .map(function (template) {
    var properties = resourcePropPaths[template.resourceName];
    var fields = properties.map(function (z) { return "(\"".concat(z[0], "\", ").concat(z[1] ? ('"' + z[1] + '"') : "null", ")"); }).join(', ');
    return "new(\"".concat(template.resourceName, "\", \"").concat(template.name, "\", \"").concat(template.templateSchema.category, "\", [").concat(fields, "], Transform").concat(template.name.replace(/ /g, ''), ")");
}).join(',\n'), "];\n    private static ImmutableArray<FunctionType> FunctionTypes = [\n        ").concat(templates
    .filter(function (z) { return !!z.functionName; })
    .map(function (template) {
    var properties = functionPropPaths[template.functionName];
    var fields = properties.map(function (z) { return "(\"".concat(z[0], "\", ").concat(z[1] ? ('"' + z[1] + '"') : "null", ")"); }).join(', ');
    return "new(\"".concat(template.functionName, "\", \"").concat(template.name, "\", [").concat(fields, "]})");
}).join(',\n'), "];\n}\n"));
(0, fs_1.writeFileSync)('./provider/cmd/pulumi-resource-one-password-native-unofficial/Transforms.cs', "\n\npublic static partial class TemplateMetadata\n{\n    public static TemplateTransform Transform = (resourceType, values) => {\n        \n    };\n}\n");
templates.map(function (template) {
    var schema = template.templateSchema;
    var methods = [];
    for (var _i = 0, _a = schema.fields; _i < _a.length; _i++) {
        var field = _a[_i];
        if (isNotesField(field)) {
            methods.push(templateGetTemplateField(field, 'notes'));
            methods.push(templateGetWellknownField(field, 'notes'));
        }
        else {
            methods.push(templateGetTemplateField(field));
            methods.push(templateGetWellknownField(field));
        }
        // UsernameField | PasswordField | OtpField | NotesField | GenericField
    }
    return "\n    public static TemplateTransform Transform".concat(template.name.replace(/ /g, ''), " = (resourceType, inputs) => {    \n        string? title = null;\n        if (inputs.TryGetValue(\"title\", out var t) && t.TryGetString(out title)) { }\n        var fields = new List<TemplateField>();\n        ").concat(methods.join('\n'), "\n        fields.AddRange(AssignGenericElements(inputs, fields));\n        return new Template()\n        {\n            Title = title ?? \"\",\n            Category = \"").concat(template.templateSchema.category, "\",\n            \n        };\n    };\n    ");
});
// GetSection(values, "info") is {} section && GetField(section, "notes") is {} field
function templateGetTemplateField(field, fieldName) {
    "\n    {\n        if (values.TryGetValue(\"".concat(fieldName, "\", out var v) && v.TryGetString(out var value) && !string.IsNullOrWhiteSpace(value))\n        {\n            fields.Add(new TemplateField()\n            {\n                Value = value ?? \"\",\n                ").concat(templateAssignFieldMetadata(field), "\n            });\n        }\n    }\n    ");
}
function templateAssignFieldMetadata(field) {
    return "\n                Label = \"".concat(field.label, "\",\n                Type = \"").concat(field.type, "\",\n                Id = \"").concat(field.id, "\",\n                Purpose = ").concat((0, lodash_1.has)(field, 'purpose') ? '"' + (0, lodash_1.get)(field, 'purpose') + '"' : '', ",\n                ").concat(field.section ? "Section = new () { Id = \"".concat(field.section.id, "\", Label = ").concat(field.section.label ? "\"".concat(field.section.label, "\"") : null, " },") : '', "\n    ");
}
function templateGetWellknownField(field, fieldName) {
    "\n    {\n        if (".concat(field.section ? "GetSection(values, \"".concat((0, lodash_1.camelCase)(field.section.label || field.section.id), "\") is {} section && GetField(section, \"").concat(fieldName !== null && fieldName !== void 0 ? fieldName : field.id, "\") is {} field") : "GetField(values, \"".concat(fieldName !== null && fieldName !== void 0 ? fieldName : field.id, "\")"), " is {} field  && field.TryGetValue(\"value\", out var value) && nn.TryGetString(out var stringValue))\n        {\n            fields.Add(new TemplateField()\n            {\n                Value = stringValue ?? \"\",\n                ").concat(templateAssignFieldMetadata(field), "\n            });\n        }\n    }\n    ");
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
function getSectionKey(template, section) {
    return "one-password-native-unofficial:".concat((0, lodash_1.camelCase)(template), ":").concat((0, lodash_1.camelCase)(section.label || section.id), "Section");
}
function isUserNameField(field) {
    return field.type === "STRING" && field.purpose === "USERNAME";
}
function isPasswordField(field) {
    return field.type === "CONCEALED" && field.purpose === "PASSWORD";
}
function isNotesField(field) {
    return field.type === "STRING" && field.purpose === "NOTES";
}
function isOtpField(field) {
    return field.type === "OTP";
}
function isGenericField(field) {
    return !isOtpField(field) && !isNotesField(field) && !isPasswordField(field) && !isUserNameField(field);
}
function getFieldType(field) {
    var _a;
    var fieldData = {
        name: (0, lodash_1.camelCase)(field.label),
        original: field.label,
        id: field.id,
        secret: false,
        default: field.value,
        type: 'string',
        kind: (_a = field.type) === null || _a === void 0 ? void 0 : _a.toLowerCase(),
        purpose: field.purpose
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
            fieldData.kind = "date";
            break;
        case "EMAIL":
            fieldData.kind = "email";
            break;
        case "MONTH_YEAR":
            fieldData.kind = "monthYear";
            break;
        case "PHONE":
            fieldData.kind = "phone";
            break;
        case "URL":
            fieldData.kind = "url";
            break;
        case "ADDRESS":
        case "REFERENCE":
        case "STRING":
            fieldData.kind = "text";
            break;
    }
    fieldData.kind = fieldData.kind === 'menu' ? 'text' : fieldData.kind;
    fieldData.secret = fieldData.secret || fieldData.kind === 'concealed';
    return fieldData;
}
function applyDefaultOutputProperties(item) {
    var _a, _b;
    item.required.push('tags', 'uuid', 'title', 'vault', 'category', 'fields', 'sections', 'attachments', 'references');
    Object.assign(item.properties, (_a = {},
        _a['uuid'] = {
            "type": "string",
            "description": "The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.\n"
        },
        _a['title'] = {
            "type": "string",
            "description": "The title of the item.\n"
        },
        _a['notes'] = {
            "type": "string",
            "description": "The notes of the item.\n"
        },
        _a['vault'] = {
            "type": "object",
            required: ['name', 'uuid'],
            properties: (_b = {
                    name: {
                        "type": "string",
                        "description": "The name of the vault item is in.\n"
                    }
                },
                _b['uuid'] = {
                    "type": "string",
                    "description": "The UUID of the vault the item is in.\n"
                },
                _b)
        },
        _a['sections'] = {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputSection" },
            secret: true
        },
        _a['fields'] = {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputField" },
            secret: true
        },
        _a['attachments'] = {
            "type": "object",
            "additionalProperties": { "$ref": "#/types/one-password-native-unofficial:index:OutputAttachment" },
            secret: true
        },
        _a['references'] = {
            "type": "array",
            "items": { "$ref": "#/types/one-password-native-unofficial:index:OutputReference" },
        },
        _a['urls'] = {
            "type": "array",
            items: { "$ref": "#/types/one-password-native-unofficial:index:OutputUrl" }
        },
        _a['tags'] = {
            type: 'array',
            items: {
                type: 'string'
            },
            "description": "An array of strings of the tags assigned to the item.\n"
        },
        _a['category'] = {
            "oneOf": [
                {
                    "$ref": "#/types/one-password-native-unofficial:index:Category"
                },
                {
                    "type": "string"
                }
            ]
        },
        _a));
    return item;
}
