using System.Collections.Immutable;
using System.Runtime;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;
using Humanizer;

namespace pulumi_resource_one_password_native_unofficial;

public static partial class TemplateMetadata
{
    private static ImmutableDictionary<string, ResourceType> ResourceTypesDictionary { get; } = ResourceTypes.ToImmutableDictionary(z => z.Urn);
    private static ImmutableDictionary<string, FunctionType> FunctionTypesDictionary { get; } = FunctionTypes.ToImmutableDictionary(z => z.Urn);

    public static ResourceType? GetResourceTypeFromUrn(string urn)
    {
        return ResourceTypes.FirstOrDefault(z => urn.Contains($":{z.Urn}:"));
    }

    public static FunctionType? GetFunctionType(string urn)
    {
        return FunctionTypesDictionary.TryGetValue(urn, out var value) ? value : null;
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

        public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item, Inputs? inputs)
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

    public delegate ImmutableDictionary<string, PropertyValue> TransformOutputs(IPulumiItemType resourceType, Item.Response template, Inputs? inputs);

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

    public static ImmutableArray<string> GetUrlValues(ImmutableDictionary<string, PropertyValue> values, string fieldName)
    {
        var result = new List<string>();
        if (values.TryGetValue(fieldName, out var f) && f.TryUnwrap(out f) && f.TryGetArray(out var array))
        {
            foreach (var item in array)
            {
                if (item.TryGetObject(out var obj) && obj.TryGetValue("href", out var href) && href.TryGetString(out var hrefString))
                {
                    result.Add(hrefString!);
                }
                else if (item.TryGetString(out var s))
                {
                    result.Add(s!);
                }
            }
        }
        return result.ToImmutableArray();
    }


    public static IEnumerable<TemplateField> AssignGenericElements(ImmutableDictionary<string, PropertyValue> root, IEnumerable<TemplateField> fields)
    {
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
        Inputs inputs
    )
    {
        outputs.Add("id", new PropertyValue(item.Id));
        outputs.Add("category", new PropertyValue(resourceType.ItemName));
        outputs.Add("title", new PropertyValue(item.Title));
        outputs.Add("notes", new PropertyValue(GetField(item, "notesPlain")?.Value ?? ""));
        outputs.Add("defaultVault", new PropertyValue(inputs.Vault is null));
        outputs.Add("vault", new PropertyValue(
                ImmutableDictionary.Create<string, PropertyValue>()
                    .Add("id", new(item.Vault.Id))
                    .Add("name", new(item.Vault.Name))
            )
        );

        outputs.Add("tags", new PropertyValue(item.Tags.Select(z => new PropertyValue(z)).ToImmutableArray()));
        outputs.Add("urls", new PropertyValue(item.Urls
            .Select(z => new PropertyValue(ImmutableDictionary.Create<string, PropertyValue>()
                .Add("label", new(z.Label))
                .Add("href", new(z.Href))
                .Add("primary", new(z.Primary)))
            ).ToImmutableArray())
        );
        outputs.Add("fields", new(
                ImmutableDictionary.Create<string, PropertyValue>()
                    .AddRange(
                        item.Fields
                            .Where(z => !z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
                            .Where(z => z.Section is null)
                            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id, new(CreateField(item, field))))
                    )
            )
        );
        outputs.Add("attachments", new(
                ImmutableDictionary.Create<string, PropertyValue>()
                    .AddRange(
                        item.Files
                            .Where(z => z.Section is null)
                            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id, new(CreateAttachment(inputs, item, field))))
                    )
            )
        );

        outputs.Add("references", new(ImmutableArray.Create<PropertyValue>()
            .AddRange(
                item.Fields
                    .Where(z => z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
                    .Select(field => new PropertyValue(CreateField(item, field)))
            ))
        );

        var sections = item.Fields.Where(z => z.Section is not null).Select(z => z.Section)
            .Concat(item.Files.Where(z => z.Section is not null).Select(z => z.Section))
            .GroupBy(z => z.Id)
            .Select(z => z.First())
            .ToImmutableArray();

        outputs.Add("sections", new(
            ImmutableDictionary.Create<string, PropertyValue>()
                .AddRange(
                    sections
                        // ReSharper disable once NullableWarningSuppressionIsUsed
                        .Select(section => new KeyValuePair<string, PropertyValue>((section!.Label ?? section.Id).Camelize(), new(
                            ImmutableDictionary.Create<string, PropertyValue>()
                                .Add("id", new(section.Id))
                                .Add("label", new(section.Label ?? section.Id))
                                .Add("fields", new(ImmutableDictionary.Create<string, PropertyValue>()
                                    .AddRange(
                                        item.Fields
                                            .Where(z => !z.Type.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase))
                                            .Where(z => z.Section?.Id == section.Id)
                                            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id, new(CreateField(item, field))))
                                    )))
                                .Add("attachments", new(ImmutableDictionary.Create<string, PropertyValue>()
                                    .AddRange(
                                        item.Files
                                            .Where(z => z.Section?.Id == section.Id)
                                            .Select(field => new KeyValuePair<string, PropertyValue>(field.Id, new(CreateAttachment(inputs, item, field))))
                                    )))
                        ))))
        ));

        static ImmutableDictionary<string, PropertyValue> CreateField(Item.Response item, Item.Field field)
        {
            return ImmutableDictionary.Create<string, PropertyValue>()
                .Add("id", field.Id is null ? PropertyValue.Null : new(field.Id))
                .Add("value", field.Value is null ? PropertyValue.Null : new(field.Value))
                .Add("purpose", field.Purpose is null ? PropertyValue.Null : new(field.Purpose))
                .Add("type", field.Purpose is null ? PropertyValue.Null : new(field.Type))
                .Add("label", field.Label is null ? PropertyValue.Null : new(field.Label))
                .Add("reference", new(MakeReference(item, field)));
        }

        static ImmutableDictionary<string, PropertyValue> CreateAttachment(Inputs inputs, Item.Response item, Item.File field)
        {
            /*
             * name: value.name,
            size: value.size,
            uuid: value.id,
            reference: makeReference(opResult.vault.id, opResult.id, value.id)!,
            hash: asset.hash
             */
            return ImmutableDictionary.Create<string, PropertyValue>()
                .Add("id", new(field.Id))
                .Add("name", new(field.Name))
                .Add("size", new(field.Size))
                // have to get from the input fields.
                .Add("hash", new(inputs.Fields.Single(z => z.Section?.Id == field.Section?.Id && z.Id == field.Name).Value))
                .Add("reference", new(MakeReference(item, field)));
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

        var fieldsAlreadyAdded = values.Select(z => z.Label).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
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
            yield return CreateTemplateField(data, null, section);
        }
    }

    public static TemplateField CreateTemplateField(ImmutableDictionary<string, PropertyValue> data, string? id, TemplateSection? section = null)
    {
        id ??= GetObjectStringValue(data, "id");
        string? value = GetObjectStringValue(data, "value");
        string? type = GetObjectStringValue(data, "type");
        string? label = GetObjectStringValue(data, "label");
        if (section is null && data.TryGetValue("section", out var sectionValue) && sectionValue.TryGetObject(out var sectionData))
        {
            section = new TemplateSection()
            {
                Id = GetObjectStringValue(sectionData, "id"),
                Label = GetObjectStringValue(sectionData, "label"),
            };
        }
        // string? purpose = GetObjectStringValue(data, "purpose");

        return new TemplateField()
        {
            Id = id,
            Value = value ?? "",
            Label = label ?? id,
            Type = type,
            Section = section
        };
    }

    public static IEnumerable<TemplateField> AssignAttachments(ImmutableDictionary<string, PropertyValue> root, IReadOnlyList<TemplateField> values,
        TemplateSection? section = null)
    {
        if (!root.TryGetValue("attachments", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetObject(out var attachments)) yield break;
        var filesAlreadyAdded = values.Where(z => z.Type?.Equals("file", StringComparison.OrdinalIgnoreCase) == true).Select(z => z.Id)
            .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        // might need to be done through assignments?

        var fieldsAlreadyAdded = values.Select(z => z.Id).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var attachment in attachments)
        {
            if (fieldsAlreadyAdded.Contains(attachment.Key)) continue;
            if (!attachment.Value.TryGetObject(out var data)) continue;

            string? hash = GetObjectStringValue(data, "hash");

            // TODO: handle asset/archive here
            // yield return new TemplateField()
            // {
            //     Id = attachment.Key,
            //     Value = hash ?? "",
            //     Label = label ?? attachment.Key,
            //     Type = "FILE",
            //     Section = section
            // };
        }
    }

    public static IEnumerable<TemplateField> AssignSections(ImmutableDictionary<string, PropertyValue> root, IReadOnlyList<TemplateField> values)
    {
        if (!root.TryGetValue("sections", out var f)) yield break;
        if (!f.TryUnwrap(out f)) yield break;
        if (!f.TryGetObject(out var sections)) yield break;

        var fieldsAlreadyAdded = values.Where(z => z.Section is not null).Select(z => $"{z.Id}:{z.Section?.Id}")
            .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var section in sections)
        {
            if (!section.Value.TryGetObject(out var data)) continue;

            string? id = GetObjectStringValue(data, "id");
            string? label = GetObjectStringValue(data, "label");

            var templateSection = new TemplateSection()
            {
                Id = id,
                Label = label,
            };

            foreach (var field in AssignFields(data, values.Where(z => z.Section?.Id == section.Key).ToImmutableArray(), templateSection))
            {
                if (fieldsAlreadyAdded.Contains($"{field.Id}:{field.Section?.Id}")) continue;
                yield return field;
            }

            foreach (var attachment in AssignAttachments(data, values.Where(z => z.Section?.Id == section.Key).ToImmutableArray(), templateSection))
            {
                if (fieldsAlreadyAdded.Contains($"{attachment.Id}:{attachment.Section?.Id}")) continue;
                yield return attachment;
            }
        }
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

    public static string? GetObjectStringValue(ImmutableDictionary<string, PropertyValue> root, string name)
    {
        if (!root.TryGetValue(name, out var value)) return null;
        if (!value.TryUnwrap(out var unwrap)) return null;
        return !unwrap.TryGetString(out var s) ? null : s;
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
    }

    public record Inputs
    {
        public required string Title { get; init; }
        public required string Category { get; init; }
        public required ImmutableArray<TemplateField> Fields { get; init; } = ImmutableArray<TemplateField>.Empty;
        public required ImmutableArray<string> Urls { get; init; } = ImmutableArray<string>.Empty;
        public required ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
        public string? Vault { get; init; }

        public static implicit operator Template(Inputs inputs)
        {
            return new Template()
            {
                Fields = inputs.Fields,
            };
        }
    }

    public class TemplateSection
    {
        public string? Id { get; set; }
        public string? Label { get; set; }
    }

    public record TemplateField
    {
        public string? Id { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; }
        public string? Purpose { get; set; }
        public TemplateSection? Section { get; set; }
        public required string Value { get; set; }
    }
}
