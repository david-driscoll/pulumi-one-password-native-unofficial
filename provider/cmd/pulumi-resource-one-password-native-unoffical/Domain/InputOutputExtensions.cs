// using System.Collections.Immutable;
// using Pulumi.Experimental.Provider;
// using static pulumi_resource_one_password_native_unoffical.TemplateMetadata;
// using static pulumi_resource_one_password_native_unoffical.Domain.CommonExtensions;
//
// namespace pulumi_resource_one_password_native_unoffical.Domain;
//
// public static class InputOutputExtensions
// {
//     public static Inputs ConvertToInputs(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> inputs)
//     {
//         var result = new Inputs(
//             ImmutableDictionary<string, InputField>.Empty,
//             ImmutableDictionary<string, InputAttachment>.Empty,
//             ImmutableDictionary<string, InputSection>.Empty,
//             ImmutableArray<InputReference>.Empty,
//             ImmutableArray<InputUrl>.Empty,
//             ImmutableArray<string>.Empty,
//             "",
//             "",
//             "",
//             ""
//         );
//         if (inputs.TryGetValue("fields", out var fields))
//         {
//             result = result with { Fields = ConvertToDictionary(fields, ConvertToInputField), };
//         }
//         if (inputs.TryGetValue("attachments", out var attachments))
//         {
//             result = result with { Attachments = ConvertToDictionary(attachments, ConvertToInputAttachment), };
//         }
//         if (inputs.TryGetValue("sections", out var sections))
//         {
//             result = result with { Sections = ConvertToDictionary(sections, ConvertToInputSection), };
//         }
//         if (inputs.TryGetValue("urls", out var urls))
//         {
//             result = result with { Urls = ConvertToArray(urls, ConvertToInputUrl), };
//         }
//         if (inputs.TryGetValue("tags", out var tags))
//         {
//             result = result with { Tags = ConvertToArray(tags, x => x.TryGetString(out var t) ? t! : ""), };
//         }
//         if (inputs.TryGetValue("references", out var references))
//         {
//             result = result with { References = ConvertToArray(references, ConvertToInputReference), };
//         }
//         if (inputs.TryGetValue("title", out var title))
//         {
//             result = result with { Title = ConvertToString(title) };
//         }
//         if (inputs.TryGetValue("category", out var category))
//         {
//             result = result with { Category = ConvertToString(category) };
//         }
//         if (inputs.TryGetValue("vault", out var vault))
//         {
//             result = result with { Vault = ConvertToString(vault) };
//         }
//         if (inputs.TryGetValue("notes", out var notes))
//         {
//             result = result with { Notes = ConvertToString(notes) };
//         }
//         foreach (var prop in resourceType.Fields)
//         {
//             if (prop.section is { } && inputs.TryGetValue(prop.section, out var wksection) && wksection.TryGetObject(out var sectionDictionary) && sectionDictionary?.GetString(prop.field) is { } value)
//             {
//                 if (!result.Sections.TryGetValue(prop.section, out var section))
//                 {
//                     section = new InputSection(ImmutableDictionary<string, InputField>.Empty, ImmutableDictionary<string, InputAttachment>.Empty);
//                 }
//                 result = result with { Sections = result.Sections.SetItem(prop.section, section with { Fields = section.Fields.SetItem(prop.field, new InputField(value, null, null)) }) };
//             }
//             else
//             {
//                 if (inputs.GetString(prop.field) is { } v)
//                 {
//                     result = result with { Fields = result.Fields.SetItem(prop.field, new InputField(v, null, null)) };
//                 }
//             }
//         }
//         // result = result with { WellKnownFields = inputs };
//         return result;
//     }
//
//     private static InputAttachment ConvertToInputAttachment(PropertyValue propertyValue)
//     {
//         if (propertyValue.TryGetArchive(out var archive) && archive is not null)
//         {
//             var hash = AssetOrArchiveExtensions.HashAssetOrArchive(archive);
//             return new InputAttachment(hash);
//         }
//
//         if (propertyValue.TryGetAsset(out var asset) && asset is not null)
//         {
//             var hash = AssetOrArchiveExtensions.HashAssetOrArchive(asset);
//             return new InputAttachment(hash);
//         }
//
//         throw new Exception("Unknown asset or archive type");
//     }
//
//     private static InputUrl ConvertToInputUrl(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var url) || url is null) throw new Exception("Expected object");
//
//         return new InputUrl(
//             url.TryGetValue("label", out var label) ? ConvertToString(label) : null,
//             url.GetBool("primary"),
//             url.TryGetValue("href", out var href) ? ConvertToString(href) : ""
//         );
//     }
//
//     private static InputReference ConvertToInputReference(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");
//
//         return new InputReference(field.GetString("itemId"));
//     }
//
//     private static InputSection ConvertToInputSection(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var section) || section is null) throw new Exception("Expected object");
//
//         return new InputSection(
//             section.TryGetValue("fields", out var fields) ? ConvertToDictionary(fields, ConvertToInputField) : ImmutableDictionary<string, InputField>.Empty,
//             section.TryGetValue("attachments", out var attachments) ? ConvertToDictionary(attachments, ConvertToInputAttachment) : ImmutableDictionary<string, InputAttachment>.Empty
//         );
//     }
//
//     public static Outputs ConvertToOutputs(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> outputs)
//     {
//         var result = new Outputs(
//             ImmutableDictionary<string, OutputAttachment>.Empty,
//             ImmutableDictionary<string, OutputField>.Empty,
//             ImmutableDictionary<string, OutputSection>.Empty,
//             ImmutableArray<OutputReference>.Empty,
//             ImmutableArray<OutputUrl>.Empty,
//             "",
//             "",
//             "",
//             "",
//             new OutputVault("", ""),
//             ImmutableArray<string>.Empty
//         );
//         if (outputs.TryGetValue("attachments", out var attachments))
//         {
//             result = result with { Attachments = ConvertToDictionary(attachments, ConvertToOutputAttachment), };
//         }
//         if (outputs.TryGetValue("fields", out var fields))
//         {
//             result = result with { Fields = ConvertToDictionary(fields, ConvertToOutputField), };
//         }
//         if (outputs.TryGetValue("sections", out var sections))
//         {
//             result = result with { Sections = ConvertToDictionary(sections, ConvertToOutputSection), };
//         }
//         if (outputs.TryGetValue("references", out var references))
//         {
//             result = result with { References = ConvertToArray(references, ConvertToOutputReference), };
//         }
//         if (outputs.TryGetValue("urls", out var urls))
//         {
//             result = result with { Urls = ConvertToArray(urls, ConvertToOutputUrl), };
//         }
//         if (outputs.TryGetValue("category", out var category))
//         {
//             result = result with { Category = ConvertToString(category) };
//         }
//         if (outputs.TryGetValue("uuid", out var uuid))
//         {
//             result = result with { Uuid = ConvertToString(uuid) };
//         }
//         if (outputs.TryGetValue("title", out var title))
//         {
//             result = result with { Title = ConvertToString(title) };
//         }
//         if (outputs.TryGetValue("notes", out var notes))
//         {
//             result = result with { Notes = ConvertToString(notes) };
//         }
//         if (outputs.TryGetValue("vault", out var vault))
//         {
//             result = result with { Vault = ConvertToOutputVault(vault) };
//         }
//         if (outputs.TryGetValue("tags", out var tags))
//         {
//             result = result with { Tags = ConvertToArray(tags, ConvertToString), };
//         }
//
//         foreach (var prop in resourceType.Fields)
//         {
//             if (prop.section is { } && outputs.TryGetValue(prop.section, out var wksection) && wksection.TryGetObject(out var sectionDictionary) && sectionDictionary?.GetString(prop.field) is { } value)
//             {
//                 if (!result.Sections.TryGetValue(prop.section, out var section))
//                 {
//                     section = new OutputSection(ImmutableDictionary<string, OutputField>.Empty, ImmutableDictionary<string, OutputAttachment>.Empty, "", "");
//                 }
//                 result = result with { Sections = result.Sections.SetItem(prop.section, section with { Fields = section.Fields.SetItem(prop.field, new OutputField(value, null, null, "", prop.field, "")) }) };
//             }
//             else
//             {
//                 if (outputs.GetString(prop.field) is { } v)
//                 {
//                     result = result with { Fields = result.Fields.SetItem(prop.field, new OutputField(v, null, null, "", prop.field, "")) };
//                 }
//             }
//         }
//
//         return result;
//     }
//
//     private static OutputVault ConvertToOutputVault(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var vault) || vault is null) throw new Exception("Expected object");
//
//         return new OutputVault(
//             vault.GetString("uuid"),
//             vault.GetString("name")
//         );
//     }
//
//     private static OutputUrl ConvertToOutputUrl(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var url) || url is null) throw new Exception("Expected object");
//
//         return new OutputUrl(
//             url.GetString("label"),
//             url.GetBool("primary"),
//             url.GetString("href")
//         );
//     }
//
//     private static OutputReference ConvertToOutputReference(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");
//
//         return new OutputReference(
//             field.GetString("itemId"),
//             field.GetString("uuid"),
//             field.GetString("label"),
//             field.GetString("reference")
//         );
//     }
//
//     private static OutputSection ConvertToOutputSection(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var section) || section is null) throw new Exception("Expected object");
//
//         return new OutputSection(
//             section.GetDictionary("fields", ConvertToOutputField),
//             section.GetDictionary("attachments", ConvertToOutputAttachment),
//             section.GetString("uuid"),
//             section.GetString("label")
//         );
//     }
//
//     private static OutputField ConvertToOutputField(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");
//
//         return new OutputField(
//             field.TryGetValue("value", out var value) ? ConvertToString(value) : null,
//             field.TryGetValue("purpose", out var purpose) && purpose.TryGetString(out var p) ? Enum.Parse<OnePassword.Items.FieldPurpose>(p!, true) : null,
//             field.TryGetValue("type", out var type) && type.TryGetString(out var t) ? Enum.Parse<OnePassword.Items.FieldType>(t!, true) : null,
//             field.GetString("uuid"),
//             field.GetString("label"),
//             field.GetString("reference")
//         );
//     }
//
//     private static OutputAttachment ConvertToOutputAttachment(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetObject(out var attachment) || attachment is null) throw new Exception("Expected object");
//
//         return new OutputAttachment(
//             attachment.GetString("uuid"),
//             attachment.GetString("name"),
//             attachment.TryGetValue("size", out var size) && size.TryGetNumber(out var s) ? (int)s : 0,
//             attachment.GetString("reference"),
//             attachment.GetString("hash")
//         );
//     }
//
//
//
//     internal static string ConvertToString(PropertyValue propertyValue)
//     {
//         if (!propertyValue.TryGetString(out var s) || s is null) return "";
//         return s;
//     }
//
//
//     public static Inputs ConvertToInputs(this Outputs outputs)
//     {
//         return new Inputs(
//             outputs.Fields.ToImmutableDictionary(x => x.Key, x => new InputField(x.Value.Value, x.Value.Purpose, x.Value.Type)),
//             outputs.Attachments.ToImmutableDictionary(x => x.Key, x => new InputAttachment(x.Value.Hash)),
//             outputs.Sections.ToImmutableDictionary(x => x.Key, x =>
//             new InputSection(x.Value.Fields.ToImmutableDictionary(x => x.Key,
//              x => new InputField(x.Value.Value, x.Value.Purpose, x.Value.Type)), x.Value.Attachments.ToImmutableDictionary(x => x.Key, x => new InputAttachment(x.Value.Hash)))),
//             outputs.References.ToImmutableArray().Select(x => new InputReference(x.ItemId)).ToImmutableArray(),
//             outputs.Urls.ToImmutableArray().Select(x => new InputUrl(x.Label, x.Primary, x.Href)).ToImmutableArray(),
//             outputs.Tags.ToImmutableArray(),
//             outputs.Title,
//             outputs.Category,
//             outputs.Vault.Name,
//             outputs.Notes
//         );
//     }
//
// }
