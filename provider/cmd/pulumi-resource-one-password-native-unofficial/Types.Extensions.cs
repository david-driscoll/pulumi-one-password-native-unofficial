using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Serialization;
using GeneratedCode;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;
using Pulumi;
using pulumi_resource_one_password_native_unofficial.Domain;
using File = System.IO.File;
using Item = pulumi_resource_one_password_native_unofficial.OnePasswordCli.Item;

namespace pulumi_resource_one_password_native_unofficial;

public static partial class TemplateMetadata
{
    private static ImmutableDictionary<string, ResourceType> ResourceTypesDictionary { get; } = ResourceTypes.ToImmutableDictionary(z => z.Urn);
    private static ImmutableDictionary<string, FunctionType> FunctionTypesDictionary { get; } = FunctionTypes.ToImmutableDictionary(z => z.Urn);

    public static ResourceType? GetResourceTypeFromUrn(string urn)
    {
        return CollectionExtensions.GetValueOrDefault(ResourceTypesDictionary, Type(urn));
    }

    public static FunctionType? GetFunctionType(string urn)
    {
        return CollectionExtensions.GetValueOrDefault(FunctionTypesDictionary, urn);
    }
    
    private static string Type(string urn)
    {
        var parts = urn.Split("::");
        var typeParts = parts[2].Split("$");
        return typeParts[^1];
    }

    public interface IPulumiItemType
    {
        string Urn { get; }
        string ItemName { get; }
        string ItemCategory { get; }
        ImmutableArray<(string field, string? section)> Fields { get; }
    }

    public record ResourceType(
        string Urn,
        string ItemName,
        string ItemCategory,
        TransformInputs TransformInputsToTemplate,
        TransformOutputs TransformItemToOutputs,
        ImmutableArray<(string field, string? section)> Fields
    ) : IPulumiItemType
    {
        public Inputs TransformInputs(ImmutableDictionary<string, PropertyValue> properties)
        {
            return TransformInputsToTemplate(this, properties);
        }

        public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item, ImmutableDictionary<string, PropertyValue>? inputs)
        {
            return TransformItemToOutputs(this, item, inputs);
        }
    }

    public record FunctionType(
        string Urn,
        string ItemName,
        string ItemCategory,
        TransformOutputs TransformItemToOutputs,
        ImmutableArray<(string field, string? section)> Fields
    ) : IPulumiItemType
    {
        public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item)
        {
            return TransformItemToOutputs(this, item, null);
        }
    }

    public delegate Inputs TransformInputs(ResourceType resourceType, ImmutableDictionary<string, PropertyValue> properties);

    public delegate ImmutableDictionary<string, PropertyValue> TransformOutputs(IPulumiItemType resourceType, Item.Response template,
        ImmutableDictionary<string, PropertyValue>? inputs);

    public static string? GetStringValue(ImmutableDictionary<string, PropertyValue> values, string fieldName)
    {
        return values.TryGetValue(fieldName, out var f) && f.TryUnwrap(out f) && f.TryGetString(out var field) ? field : null;
    }

    public static string? GetVaultName(ImmutableDictionary<string, PropertyValue> values)
    {
        if (GetBoolValue(values, "defaultVault")) return null;
        if (values.TryGetValue("vault", out var v))
        {
            if (v.Type == PropertyValueType.String) return v.TryGetString(out var va) ? va : null;
            if (v.Type == PropertyValueType.Object) return v.TryGetObject(out var vault) ? GetStringValue(vault, "name") : null;
        }

        return null;
    }

    public static bool GetBoolValue(ImmutableDictionary<string, PropertyValue> values, string fieldName)
    {
        return values.TryGetValue(fieldName, out var f) && f.TryUnwrap(out f) && f.TryGetBool(out var field) && field;
    }

    public static ImmutableArray<string> GetArrayValues(ImmutableDictionary<string, PropertyValue> values, string fieldName)
    {
        return values.TryGetValue(fieldName, out var f) && f.TryUnwrap(out f) && f.TryGetArray(out var array)
            ? array.Select(z => z.TryGetString(out var s) ? s! : "").Where(z => !string.IsNullOrWhiteSpace(z)).ToImmutableArray()
            : ImmutableArray<string>.Empty;
    }

    public static ImmutableArray<Item.Url> GetUrlValues(ImmutableDictionary<string, PropertyValue> values, string fieldName)
    {
        var result = new List<Item.Url>();
        if (values.TryGetValue(fieldName, out var f) && f.TryUnwrap(out f) && f.TryGetArray(out var array))
        {
            foreach (var item in array)
            {
                if (item.TryGetObject(out var obj))
                {
                    result.Add(new Item.Url()
                    {
                        Href = GetStringValue(obj, "href") ?? "",
                        Label = GetStringValue(obj, "label"),
                        Primary = GetBoolValue(obj, "primary")
                    });
                }
                else if (item.TryGetString(out var s))
                {
                    result.Add(new Item.Url() { Href = s! });
                }
            }
        }

        return result.ToImmutableArray();
    }

    public static Inputs AssignOtherInputs(ImmutableDictionary<string, PropertyValue> root, IEnumerable<TemplateField> fields, Inputs inputs)
    {
        inputs = inputs with
        {
            Fields = fields.ToImmutableDictionary(z => z.Id ?? z.Label!, z => z)
        };
        if (root.TryGetValue("generatePassword", out var gp))
        {
            if (!inputs.Fields.TryGetValue("password", out var passwordField))
            {
                passwordField = new TemplateField()
                {
                    Value = null!,
                    Label = "password",
                    Type = "CONCEALED",
                    Id = "password",
                    Purpose = "PASSWORD",
                };
                inputs = inputs with { Fields = inputs.Fields.Add("password", passwordField) };
            }

            if (gp.TryGetBool(out var b) && b)
            {
                inputs = inputs with { GeneratePassword = new PasswordGeneratorRecipe() };
            }
            else if (gp.TryGetObject(out var obj))
            {
                inputs = inputs with
                {
                    GeneratePassword = new PasswordGeneratorRecipe()
                    {
                        Length = GetObjectNumberValue(obj, "length"),
                    }
                };
                if (GetObjectBoolValue(obj, "digits") ?? false)
                {
                    inputs = inputs with
                    {
                        GeneratePassword = inputs.GeneratePassword with
                        {
                            CharacterSets = (inputs.GeneratePassword.CharacterSets ?? ImmutableArray<CharacterSets>.Empty).Add(CharacterSets.DIGITS)
                        }
                    };
                }

                if (GetObjectBoolValue(obj, "letters") ?? false)
                {
                    inputs = inputs with
                    {
                        GeneratePassword = inputs.GeneratePassword with
                        {
                            CharacterSets = (inputs.GeneratePassword.CharacterSets ?? ImmutableArray<CharacterSets>.Empty).Add(CharacterSets.LETTERS)
                        }
                    };
                }

                if (GetObjectBoolValue(obj, "symbols") ?? false)
                {
                    inputs = inputs with
                    {
                        GeneratePassword = inputs.GeneratePassword with
                        {
                            CharacterSets = (inputs.GeneratePassword.CharacterSets ?? ImmutableArray<CharacterSets>.Empty).Add(CharacterSets.SYMBOLS)
                        }
                    };
                }
            }
        }

        return inputs;
    }


    public static IEnumerable<TemplateField> AssignGenericElements(ImmutableDictionary<string, PropertyValue> root, IEnumerable<TemplateField> fields)
    {
        // DebugHelper.WaitForDebugger();
        var wellKnownFields = fields.ToImmutableArray();

        return AssignFields(root, wellKnownFields)
            .Concat(AssignAttachments(root, wellKnownFields))
            .Concat(AssignReferences(root))
            .Concat(AssignSections(root, wellKnownFields))
            .ToArray();
    }

    public static void AssignCommonOutputs(
        ImmutableDictionary<string, PropertyValue>.Builder outputs,
        IPulumiItemType resourceType,
        Item.Response item,
        ImmutableDictionary<string, PropertyValue>? inputs
    )
    {
        DebugHelper.WaitForDebugger();
        // item = item with
        // {
        //     Fields = item.Fields
        //         .GroupBy(z => z.Id)
        //         .Select(z =>
        //         {
        //             var field = z.First();
        //             return z.Aggregate(field, (field, next) => field with { Value = field.Value ?? next.Value });
        //         })
        //         .ToImmutableArray()
        // };

        outputs.Add("id", new PropertyValue(item.Id));
        outputs.Add("category", new PropertyValue(resourceType.ItemName));
        outputs.Add("title", new PropertyValue(item.Title));
        outputs.Add("notes", new PropertyValue(GetField(item, "notesPlain")?.Value ?? ""));
        outputs.Add("defaultVault", new PropertyValue(inputs?.ContainsKey("vault") != true));
        outputs.Add("vault", new PropertyValue(
                ImmutableDictionary.Create<string, PropertyValue>()
                    .Add("id", new(item.Vault.Id))
                    .Add("name", new(item.Vault.Name))
            )
        );

        outputs.Add("tags", new PropertyValue(item.Tags.Select(z => new PropertyValue(z)).ToImmutableArray()));
        outputs.Add("urls", new PropertyValue(item.Urls
            .Select(z => new PropertyValue(ImmutableDictionary.Create<string, PropertyValue>()
                .Add("label", new(z.Label!))
                .Add("href", new(z.Href))
                .Add("primary", new(z.Primary)))
            ).ToImmutableArray())
        );
        var fields = item.Fields
            .Where(z => !z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
            .Where(z => z.Section is null)
            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id, new(CreateField(inputs, item, field))))
            .ToArray();

        var attachments = item.Files
            .Where(z => z.Section is null)
            .Select(file => new KeyValuePair<string, PropertyValue>(file.Name, new(CreateAttachment(inputs, item, file))))
            .ToArray();

        var references = item.Fields
            .Where(z => z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
            .Where(z => z.Section is null)
            .Select(field => new PropertyValue(CreateField(inputs, item, field)));

        var sections = item.Fields.Where(z => z.Section is not null).Select(z => z.Section)
            .Concat(item.Files.Where(z => z.Section is not null).Select(z => z.Section))
            .GroupBy(z => z.Id)
            .Select(z => z.First())
            .ToImmutableArray();

        outputs.Add("fields", new(ImmutableDictionary.Create<string, PropertyValue>().AddRange(fields)));
        outputs.Add("attachments", new(ImmutableDictionary.Create<string, PropertyValue>().AddRange(attachments)));
        outputs.Add("references", new(ImmutableArray.Create<PropertyValue>().AddRange(references)));

        outputs.Add("sections", new(
            ImmutableDictionary.Create<string, PropertyValue>()
                .AddRange(
                    sections
                        // ReSharper disable once NullableWarningSuppressionIsUsed
                        .Select(section => new KeyValuePair<string, PropertyValue>((section!.Label ?? section.Id), new(
                            ImmutableDictionary.Create<string, PropertyValue>()
                                .Add("id", new(section.Id))
                                .Add("label", new(section.Label ?? section.Id))
                                .Add("fields", new(ImmutableDictionary.Create<string, PropertyValue>()
                                    .AddRange(
                                        item.Fields
                                            .Where(z => !z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
                                            .Where(z => z.Section is not null)
                                            .Where(z => z.Section?.Id == section.Id)
                                            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id!, new(CreateField(inputs, item, field))))
                                    )))
                                .Add("references", new (ImmutableArray.Create<PropertyValue>()
                                    .AddRange(
                                        item.Fields
                                            .Where(z => z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
                                            .Where(z => z.Section is not null)
                                            .Where(z => z.Section?.Id == section.Id)
                                            .Select(field => new PropertyValue(CreateField(inputs, item, field))))
                                    ))
                                .Add("attachments", new(ImmutableDictionary.Create<string, PropertyValue>()
                                    .AddRange(
                                        item.Files
                                            .Where(z => z.Section is not null)
                                            .Where(z => z.Section?.Id == section.Id)
                                            .Select(file => new KeyValuePair<string, PropertyValue>(file.Name, new(CreateAttachment(inputs, item, file))))
                                    )))
                        ))))
        ));

        static ImmutableDictionary<string, PropertyValue> CreateField(ImmutableDictionary<string, PropertyValue>? inputs, Item.Response item, Item.Field field)
        {
            var purpose = field.Purpose;
            var type = field.Type;
            {
                if (field is { Section.Id: not null }
                    && inputs is not null
                    && GetSection(inputs, field.Section.Id) is { Count: > 0 } section
                    && field.Id is { Length: > 0 }
                    && GetField(section, field.Id) is { Count: > 0 } fieldInput
                    && GetObjectStringValue(fieldInput, "purpose") is { Length: > 0 } inputPurpose)
                {
                    purpose = inputPurpose;
                }
                if (field.Id is { Length: > 0 }
                    && inputs is not null
                    && GetField(inputs, field.Id) is { Count: > 0 } sfieldInput
                    && GetObjectStringValue(sfieldInput, "purpose") is { Length: > 0 } sinputPurpose)
                {
                    purpose = sinputPurpose;
                }
            }

            {
                if (field is { Section.Id: not null }
                    && inputs is not null
                    && GetSection(inputs, field.Section.Id) is { Count: > 0 } section
                    && field.Id is { Length: > 0 }
                    && GetField(section, field.Id) is { Count: > 0 } sfieldInput
                    && GetObjectStringValue(sfieldInput, "type") is { Length: > 0 } sinputType)
                {
                    type = sinputType;
                }
                if (field.Id is { Length: > 0 }
                    && inputs is not null
                    && GetField(inputs, field.Id) is { Count: > 0 } fieldInput
                    && GetObjectStringValue(fieldInput, "type") is { Length: > 0 } inputType)
                {
                    type = inputType;
                }
            }

            return ImmutableDictionary.Create<string, PropertyValue>()
                .Add("id", field.Id is null ? PropertyValue.Null : new(field.Id))
                .Add("value", field.Value is null ? PropertyValue.Null : new(field.Value))
                .Add("purpose", purpose is null ? PropertyValue.Null : new(purpose))
                .Add("type", new(type))
                .Add("label", field.Label is null ? PropertyValue.Null : new(field.Label))
                .Add("reference", new(MakeReference(item, field)));
        }

        static ImmutableDictionary<string, PropertyValue> CreateAttachment(ImmutableDictionary<string, PropertyValue>? inputs, Item.Response item,
            Item.File file)
        {
            // DebugHelper.WaitForDebugger();
            string? hash = null;
            PropertyValue? asset = null;
            if (file is { Section.Id: { } } && inputs is not null)
            {
                var section = GetSection(inputs, file.Section.Id);
                asset = GetAttachment(section!, file.Name);
                hash = AssetOrArchiveExtensions.HashAssetOrArchive(asset);
            }
            else if (inputs is not null)
            {
                asset = GetAttachment(inputs, file.Name);
                hash = AssetOrArchiveExtensions.HashAssetOrArchive(asset);
            }

            return ImmutableDictionary.Create<string, PropertyValue>()
                .Add("id", new(file.Id))
                .Add("name", new(file.Name))
                .Add("size", new(file.Size))
                // have to get from the input fields.
                .Add("hash", new(hash))
                .Add("asset", new PropertyValue(asset))
                .Add("reference", new(MakeReference(item, file)));
        }
    }

    private static string MakeReference(Item.Response item, Item.Field field)
    {
        return field is { Section: not null }
            ? $"op://{item.Vault.Id}/{item.Id}/{field.Section.Id}/{field.Id}"
            : $"op://{item.Vault.Id}/{item.Id}/{field.Id}";
    }

    private static string MakeReference(Item.Response item, Item.File field)
    {
        return field is { Section: not null }
            ? $"op://{item.Vault.Id}/{item.Id}/{field.Section.Id}/{field.Id}"
            : $"op://{item.Vault.Id}/{item.Id}/{field.Id}";
    }

    public static IEnumerable<TemplateField> AssignFields(ImmutableDictionary<string, PropertyValue> root, IReadOnlyList<TemplateField> values,
        TemplateSection? section = null)
    {
        if (!root.TryGetValue("fields", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetObject(out var fields)) yield break;

        var fieldsAlreadyAdded = values.Select(z => z.Id).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var field in fields)
        {
            if (fieldsAlreadyAdded.Contains(field.Key)) continue;
            if (!field.Value.TryGetObject(out var data)) continue;

            yield return CreateTemplateField(data, field.Key, section);
        }
    }

    public static IEnumerable<TemplateField> AssignReferences(ImmutableDictionary<string, PropertyValue> root, TemplateSection? section = null)
    {
        if (!root.TryGetValue("references", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetArray(out var fields)) yield break;

        foreach (var field in fields)
        {
            if (!field.TryGetObject(out var data)) continue;
            yield return CreateReferenceTemplateField(data, null, section);
        }
    }

    public static TemplateField CreateReferenceTemplateField(ImmutableDictionary<string, PropertyValue> data, string? id, TemplateSection? section = null)
    {
        var itemId = GetObjectStringValue(data, "itemId");
        var label = GetObjectStringValue(data, "label");
        return new TemplateField()
        {
            Id = id ?? itemId,
            Value = itemId!,
            Label = label ?? itemId,
            Type = "REFERENCE",
            Section = section,
        };
    }

    public static TemplateField CreateTemplateField(ImmutableDictionary<string, PropertyValue> data, string? id, TemplateSection? section = null)
    {
        string? value = GetObjectStringValue(data, "value");
        string? type = GetObjectStringValue(data, "type");
        string? label = GetObjectStringValue(data, "label") ?? id;
        string? purpose = GetObjectStringValue(data, "purpose");
        // if (id is not null && section is not null)
        // {
        //     id = $"{section.Id}.{id}";
        // }

        id ??= GetObjectStringValue(data, "id");
        if (section is null && data.TryGetValue("section", out var sectionValue) && sectionValue.TryGetObject(out var sectionData))
        {
            section = new TemplateSection()
            {
                Id = GetObjectStringValue(sectionData, "id")!,
                Label = GetObjectStringValue(sectionData, "label") ?? GetObjectStringValue(sectionData, "id")!,
            };
        }
        // string? purpose = GetObjectStringValue(data, "purpose");

        return new TemplateField()
        {
            Id = id,
            Value = value ?? "",
            Label = label,
            Type = type,
            Purpose = purpose,
            Section = section
        };
    }

    public static IEnumerable<TemplateField> AssignAttachments(ImmutableDictionary<string, PropertyValue> root, IReadOnlyList<TemplateField> values,
        TemplateSection? section = null)
    {
        // DebugHelper.WaitForDebugger();
        if (!root.TryGetValue("attachments", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetObject(out var attachments)) yield break;
        var filesAlreadyAdded = values.Where(z => z.Type?.Equals("file", StringComparison.OrdinalIgnoreCase) == true)
            .Select(z => z.Id)
            .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        // might need to be done through assignments?

        foreach (var attachment in attachments)
        {
            if (filesAlreadyAdded.Contains(attachment.Key)) continue;
            if (AssetOrArchiveExtensions.GetAssetOrArchive(attachment.Value) is { } assetOrArchive)
            {
                yield return new TemplateAttachment()
                {
                    Id = attachment.Key,
                    Value = AssetOrArchiveExtensions.HashAssetOrArchive(assetOrArchive),
                    Label = attachment.Key,
                    Type = "FILE",
                    Section = section,
                    Asset = assetOrArchive,
                };
            }
            else if (attachment.Value.TryGetObject(out var data))
            {
                var id = GetObjectStringValue(data, "id")!;
                string name = GetObjectStringValue(data, "name")!;
                string? hash = GetObjectStringValue(data, "hash") ?? "";

                yield return new TemplateField()
                {
                    Id = id,
                    Value = hash,
                    Label = name,
                    Type = "FILE",
                    Section = section,
                };
            }
        }
    }

    public static IEnumerable<TemplateField> AssignSections(ImmutableDictionary<string, PropertyValue> root, IReadOnlyList<TemplateField> values)
    {
        if (!root.TryGetValue("sections", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetObject(out var sections)) yield break;

        foreach (var section in sections)
        {
            if (!section.Value.TryGetObject(out var data)) continue;
            var fieldsAlreadyAdded = values
                .Where(z => z.Section is { Id.Length: > 0 })
                .Where(z => z.Section.Id == section.Key)
                .Select(z => z.Id)
                .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);

            string? label = GetObjectStringValue(data, "label");

            var templateSection = new TemplateSection()
            {
                Id = section.Key,
                Label = label ?? section.Key,
            };

            foreach (var field in AssignFields(data, values.Where(z => z.Section?.Id == section.Key).ToImmutableArray(), templateSection))
            {
                if (fieldsAlreadyAdded.Contains(field.Id)) continue;
                yield return field;
            }

            foreach (var attachment in AssignAttachments(data, values.Where(z => z.Section?.Id == section.Key).ToImmutableArray(), templateSection))
            {
                if (fieldsAlreadyAdded.Contains(attachment.Id)) continue;
                yield return attachment;
            }
            
            foreach (var field in AssignReferences(data, templateSection))
            {
                if (fieldsAlreadyAdded.Contains(field.Id)) continue;
                yield return field;
            }
        }
    }

    public static PropertyValue GetAttachment(ImmutableDictionary<string, PropertyValue> root, string name)
    {
        if (!root.TryGetValue("attachments", out var f)) return PropertyValue.Null;
        if (!f.TryUnwrap(out f)) return PropertyValue.Null;
        if (!f.TryGetObject(out var fields)) return PropertyValue.Null;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        if (!fields!.TryGetValue(name, out var v)) return PropertyValue.Null;
        return v;
    }

    public static ImmutableDictionary<string, PropertyValue>? GetField(ImmutableDictionary<string, PropertyValue> root, string name)
    {
        if (!root.TryGetValue("fields", out var f)) return null;
        if (!f.TryUnwrap(out f)) return null;
        if (!f.TryGetObject(out var fields)) return null;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        if (!fields!.TryGetValue(name, out var v)) return null;
        return !v.TryGetObject(out var field) ? null : field;
    }

    public static string? GetObjectStringValue(IReadOnlyDictionary<string, PropertyValue> root, string name)
    {
        return !(GetObjectValue(root, name) ?? PropertyValue.Null).TryGetString(out var s) ? null : s;
    }

    public static int? GetObjectNumberValue(IReadOnlyDictionary<string, PropertyValue> root, string name)
    {
        return !(GetObjectValue(root, name) ?? PropertyValue.Null).TryGetNumber(out var s) ? null : Convert.ToInt32(s);
    }

    public static bool? GetObjectBoolValue(IReadOnlyDictionary<string, PropertyValue> root, string name)
    {
        return !(GetObjectValue(root, name) ?? PropertyValue.Null).TryGetBool(out var s) ? null : s;
    }

    public static string? GetObjectStringValue(PropertyValue root, string name)
    {
        return !root.TryGetObject(out var v) ? null : GetObjectStringValue(v, name);
    }

    public static PropertyValue? GetObjectValue(IReadOnlyDictionary<string, PropertyValue> root, string name)
    {
        if (!root.TryGetValue(name, out var value)) return null;
        if (!value.TryUnwrap(out var unwrap)) return null;
        return unwrap;
    }

    public static Item.Field? GetField(Item.Response item, string name, string? sectionName = null)
    {
        return item.Fields
            .Where(z => sectionName == null || z.Section?.Id == sectionName)
            .FirstOrDefault(z => z.Id == name);
    }

    public static void AddFieldToSection(ImmutableDictionary<string, PropertyValue>.Builder outputs, string sectionName, string fieldName,
        PropertyValue fieldValue)
    {
        if (outputs.TryGetValue(sectionName, out var s) && s.TryGetObject(out var section))
        {
            outputs[sectionName] = new PropertyValue(section!.Add(fieldName, fieldValue));
        }
        else
        {
            outputs[sectionName] = new PropertyValue(ImmutableDictionary.Create<string, PropertyValue>().Add(fieldName, fieldValue));
        }
    }

    public static Item.File? GetFile(Item.Response item, string name, string? sectionName = null)
    {
        return item.Files
            .Where(z => sectionName == null || z.Section?.Id == sectionName)
            .FirstOrDefault(z => z.Id == name);
    }

    public static ImmutableDictionary<string, PropertyValue>? GetSection(ImmutableDictionary<string, PropertyValue> root, string name)
    {
        if (!root.TryGetValue("sections", out var f)) return null;
        if (!f.TryUnwrap(out f)) return null;
        if (!f.TryGetObject(out var sections)) return null;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        if (!sections!.TryGetValue(name, out var v)) return null;
        return !v.TryGetObject(out var field) ? null : field;
    }

    public record Template
    {
        public required ImmutableArray<TemplateField> Fields { get; init; } = ImmutableArray<TemplateField>.Empty;
        public required ImmutableArray<TemplateUrl> Urls { get; init; } = ImmutableArray<TemplateUrl>.Empty;
        // public required ImmutableArray<TemplateFile> Files { get; init; } = ImmutableArray<TemplateField>.Empty;

        public ImmutableArray<TemplateSection> Sections =>
            Fields.Where(z => z.Section is not null).GroupBy(z => z.Section?.Id).Select(z => z.First().Section).ToImmutableArray();

        public (ImmutableArray<TemplateField> fields, ImmutableArray<TemplateAttachment> attachments, ImmutableArray<TemplateSection> sections)
            GetFieldsAndAttachments()
        {
            var fields = Fields.Where(x => x is not TemplateAttachment).ToImmutableArray();
            var attachments = Fields.OfType<TemplateAttachment>().ToImmutableArray();
            return (fields, attachments, Sections);
        }
    }


    public static async Task<string> ResolveAssetPath(this AssetOrArchive asset, string tempDirectory, string fileName, CancellationToken cancellationToken)
    {
        if (asset is FileAsset fileAsset)
        {
            return Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileAsset));
        }

        if (asset is FileArchive fileArchive)
        {
            return Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileArchive));
        }

        if (asset is StringAsset stringAsset)
        {
            var tempFilePath = Path.Combine(tempDirectory, fileName);
            await File.WriteAllTextAsync(tempFilePath, AssetOrArchiveExtensions.GetAssetValue(stringAsset), cancellationToken);
            return tempFilePath;
        }

        throw new($"Asset {asset.GetType().FullName} not supported!");
    }

    public static async Task<byte[]> ResolveAsset(this AssetOrArchive asset, CancellationToken cancellationToken)
    {
        if (asset is FileAsset fileAsset)
        {
            var path = Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileAsset));
            return await File.ReadAllBytesAsync(path, cancellationToken);
        }

        // if (asset is FileArchive fileArchive)
        // {
        //     return Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileArchive));
        // }

        if (asset is StringAsset stringAsset)
        {
            return Encoding.UTF8.GetBytes(AssetOrArchiveExtensions.GetAssetValue(stringAsset));
        }

        throw new($"Asset {asset.GetType().FullName} not supported!");
    }

    public record Inputs
    {
        public string? Title { get; init; }
        public required string Category { get; init; }
        public ImmutableDictionary<string, TemplateField> Fields { get; init; } = ImmutableDictionary<string, TemplateField>.Empty;
        public ImmutableArray<Item.Url> Urls { get; init; } = ImmutableArray<Item.Url>.Empty;
        public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
        public string? Vault { get; init; }
        public PasswordGeneratorRecipe? GeneratePassword { get; init; }

        public static implicit operator Template(Inputs inputs)
        {
            return new Template()
            {
                Fields = inputs.Fields.Values.ToImmutableArray(),
                Urls = inputs.Urls.Select(z => new TemplateUrl() { Href = z.Href, Label = z.Label, Primary = z.Primary }).ToImmutableArray(),
            };
        }
    }

    public class TemplateSection
    {
        public string? Id { get; init; }
        public required string Label { get; init; }
    }

    public record TemplateField
    {
        public string? Id { get; init; }
        public string? Label { get; init; }
        public string? Type { get; init; }
        public string? Purpose { get; init; }
        public TemplateSection? Section { get; init; }
        public required string Value { get; set; }
    }

    public record TemplateUrl
    {
        public string? Label { get; init; }
        public bool Primary { get; init; }
        public string Href { get; init; } = "";
    }

    public record TemplateAttachment : TemplateField
    {
        [JsonIgnore] public required AssetOrArchive Asset { get; init; }
    }
}

internal static class Constants
{
    /// <summary>
    /// Unknown values are encoded as a distinguished string value.
    /// </summary>
    public const string UnknownValue = "04da6b54-80e4-46f7-96ec-b56ff0331ba9";

    /// <summary>
    /// SpecialSigKey is sometimes used to encode type identity inside of a map. See sdk/go/common/resource/properties.go.
    /// </summary>
    public const string SpecialSigKey = "4dabf18193072939515e22adb298388d";

    /// <summary>
    /// SpecialAssetSig is a randomly assigned hash used to identify assets in maps. See sdk/go/common/resource/asset.go.
    /// </summary>
    public const string SpecialAssetSig = "c44067f5952c0a294b673a41bacd8c17";

    /// <summary>
    /// SpecialArchiveSig is a randomly assigned hash used to identify archives in maps. See sdk/go/common/resource/asset.go.
    /// </summary>
    public const string SpecialArchiveSig = "0def7320c3a5731c473e5ecbe6d01bc7";

    /// <summary>
    /// SpecialSecretSig is a randomly assigned hash used to identify secrets in maps. See sdk/go/common/resource/properties.go.
    /// </summary>
    public const string SpecialSecretSig = "1b47061264138c4ac30d75fd1eb44270";

    /// <summary>
    /// SpecialResourceSig is a randomly assigned hash used to identify resources in maps. See sdk/go/common/resource/properties.go.
    /// </summary>
    public const string SpecialResourceSig = "5cf8f73096256a8f31e491e813e4eb8e";

    /// <summary>
    /// SpecialOutputValueSig is a randomly assigned hash used to identify outputs in maps. See sdk/go/common/resource/properties.go.
    /// </summary>
    public const string SpecialOutputValueSig = "d0e6a833031e9bbcd3f4e8bde6ca49a4";

    public const string SecretName = "secret";
    public const string ValueName = "value";
    public const string DependenciesName = "dependencies";

    public const string AssetTextName = "text";
    public const string ArchiveAssetsName = "assets";

    public const string AssetOrArchivePathName = "path";
    public const string AssetOrArchiveUriName = "uri";

    public const string ResourceUrnName = "urn";
    public const string ResourceIdName = "id";
    public const string ResourceVersionName = "packageVersion";

    public const string IdPropertyName = "id";
    public const string UrnPropertyName = "urn";
}
