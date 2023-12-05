using System.Collections.Immutable;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pulumi;
using Pulumi.Experimental.Provider;
using Standart.Hash.xxHash;

namespace pulumi_resource_one_password_native_unoffical.Domain;


public record InputField(string? Value, OnePassword.Items.FieldPurpose? Purpose, OnePassword.Items.FieldType? Type);

public record InputSection(ImmutableDictionary<string, InputField> Fields, ImmutableDictionary<string, InputAttachment> Attachments);

public record InputReference(string ItemId);
public record InputUrl(string? Label, bool Primary, string Href);

public record Inputs(
    ImmutableDictionary<string, InputField> Fields,
    ImmutableDictionary<string, InputAttachment> Attachments,
    ImmutableDictionary<string, InputSection> Sections,
    ImmutableArray<InputReference> References,
    ImmutableArray<InputUrl> Urls,
    ImmutableArray<string> Tags,
    string Title,
    string Category,
    string Vault,
    string Notes,
    ImmutableDictionary<string, PropertyValue> WellKnownFields
);

public record InputAttachment(string Hash);
public record OutputAttachment(string Uuid, string Name, int Size, string Reference, string Hash);

public record OutputReference(string ItemId, string Uuid, string Label, string Reference);


public record OutputField
(
    OnePassword.Items.FieldType Type,
    OnePassword.Items.FieldPurpose Purpose,
    string? Value,
    string Uuid,
    string Label,
    string Reference,
    ImmutableDictionary<string, PropertyValue> Data
);

public record OutputSection(ImmutableDictionary<string, OutputField> Fields, ImmutableDictionary<string, OutputAttachment> Attachments, string Uuid, string Label);

public record OutputUrl(string Label, bool Primary, string Href);

public record Outputs
(
    ImmutableDictionary<string, OutputAttachment> Attachments,
    ImmutableDictionary<string, OutputField> Fields,
    ImmutableDictionary<string, OutputSection> Sections,
    ImmutableArray<OutputReference> References,
    ImmutableArray<OutputUrl> Urls,
    string Category,
    string Uuid,
    string Title,
    string Notes,
    OutputVault Vault,
    ImmutableArray<string> Tags,
    ImmutableDictionary<string, PropertyValue> WellKnownFields
);

public record OutputVault(string Uuid, string Name);

public static class InputOutputExtensions
{
    public static Inputs ConvertToInputs(ImmutableDictionary<string, PropertyValue> inputs)
    {
        var result = new Inputs(
            ImmutableDictionary<string, InputField>.Empty,
            ImmutableDictionary<string, InputAttachment>.Empty,
            ImmutableDictionary<string, InputSection>.Empty,
            ImmutableArray<InputReference>.Empty,
            ImmutableArray<InputUrl>.Empty,
            ImmutableArray<string>.Empty,
            "",
            "",
            "",
            "",
            ImmutableDictionary<string, PropertyValue>.Empty
        );
        if (inputs.TryGetValue("fields", out var fields))
        {
            result = result with { Fields = ConvertToDictionary(fields, ConvertToInputField), };
            inputs = inputs.Remove("fields");
        }
        if (inputs.TryGetValue("attachments", out var attachments))
        {
            result = result with { Attachments = ConvertToDictionary(attachments, ConvertToInputAttachment), };
            inputs = inputs.Remove("attachments");
        }
        if (inputs.TryGetValue("sections", out var sections))
        {
            result = result with { Sections = ConvertToDictionary(sections, ConvertToInputSection), };
            inputs = inputs.Remove("sections");
        }
        if (inputs.TryGetValue("urls", out var urls))
        {
            result = result with { Urls = ConvertToArray(urls, ConvertToInputUrl), };
            inputs = inputs.Remove("urls");
        }
        if (inputs.TryGetValue("tags", out var tags))
        {
            result = result with { Tags = ConvertToArray(tags, x => x.TryGetString(out var t) ? t! : ""), };
            inputs = inputs.Remove("tags");
        }
        if (inputs.TryGetValue("references", out var references))
        {
            result = result with { References = ConvertToArray(references, ConvertToInputReference), };
            inputs = inputs.Remove("references");
        }
        if (inputs.TryGetValue("title", out var title))
        {
            result = result with { Title = ConvertToString(title) };
            inputs = inputs.Remove("title");
        }
        if (inputs.TryGetValue("category", out var category))
        {
            result = result with { Category = ConvertToString(category) };
            inputs = inputs.Remove("category");
        }
        if (inputs.TryGetValue("vault", out var vault))
        {
            result = result with { Vault = ConvertToString(vault) };
            inputs = inputs.Remove("vault");
        }
        if (inputs.TryGetValue("notes", out var notes))
        {
            result = result with { Notes = ConvertToString(notes) };
            inputs = inputs.Remove("notes");
        }
        foreach (var input in inputs)
        {
            if (input.Value.TryGetObject(out var wkfields) && wkfields is not null)
            {
                if (!result.Sections.TryGetValue(input.Key, out var section))
                {
                    section = new InputSection(ImmutableDictionary<string, InputField>.Empty, ImmutableDictionary<string, InputAttachment>.Empty);
                }
                foreach (var field in wkfields)
                {
                    result = result with { Sections = result.Sections.SetItem(input.Key, section with { Fields = section.Fields.SetItem(field.Key, ConvertToInputField(field.Value)) }) };
                }
            }
            else
            {
                result = result with { Fields = result.Fields.SetItem(input.Key, ConvertToInputField(input.Value)) };
            }
        }
        result = result with { WellKnownFields = inputs };
        return result;
    }

    private static ImmutableDictionary<string, T> ConvertToDictionary<T>(PropertyValue propertyValue, Func<PropertyValue, T> convert)
    {
        if (!propertyValue.TryGetObject(out var @object) || @object is null) return ImmutableDictionary<string, T>.Empty;
        return @object.ToImmutableDictionary(x => x.Key, x => convert(x.Value));
    }

    private static ImmutableArray<T> ConvertToArray<T>(PropertyValue propertyValue, Func<PropertyValue, T> convert)
    {
        if (!propertyValue.TryGetArray(out var array)) return ImmutableArray<T>.Empty;
        return array.Select(z => convert(z)).ToImmutableArray();
    }

    private static InputField ConvertToInputField(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");

        return new InputField(
            field.TryGetValue("value", out var value) ? value.TryUnwrap(out var v) ? ConvertToString(v) : null : null,
            field.TryGetValue("purpose", out var purpose) && purpose.TryGetString(out var p) ? Enum.Parse<OnePassword.Items.FieldPurpose>(p!, true) : null,
            field.TryGetValue("type", out var type) && type.TryGetString(out var t) ? Enum.Parse<OnePassword.Items.FieldType>(t!, true) : null
        );
    }

    private static InputAttachment ConvertToInputAttachment(PropertyValue propertyValue)
    {
        if (propertyValue.TryGetArchive(out var archive) && archive is not null)
        {
            var hash = AssetOrArchiveExtensions.HashAssetOrArchive(archive);
            return new InputAttachment(hash);
        }

        if (propertyValue.TryGetAsset(out var asset) && asset is not null)
        {
            var hash = AssetOrArchiveExtensions.HashAssetOrArchive(asset);
            return new InputAttachment(hash);
        }

        throw new Exception("Unknown asset or archive type");
    }

    private static InputUrl ConvertToInputUrl(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var url) || url is null) throw new Exception("Expected object");

        return new InputUrl(
            url.TryGetValue("label", out var label) ? ConvertToString(label) : null,
            url.GetBool("primary"),
            url.TryGetValue("href", out var href) ? ConvertToString(href) : ""
        );
    }

    private static string ConvertToString(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetString(out var s) || s is null) return "";
        return s;
    }

    private static InputReference ConvertToInputReference(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");

        return new InputReference(field.GetString("itemId"));
    }

    private static InputSection ConvertToInputSection(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var section) || section is null) throw new Exception("Expected object");

        return new InputSection(
            section.TryGetValue("fields", out var fields) ? ConvertToDictionary(fields, ConvertToInputField) : ImmutableDictionary<string, InputField>.Empty,
            section.TryGetValue("attachments", out var attachments) ? ConvertToDictionary(attachments, ConvertToInputAttachment) : ImmutableDictionary<string, InputAttachment>.Empty
        );
    }

    public static Outputs ConvertToOutputs(ImmutableDictionary<string, PropertyValue> outputs)
    {
        var result = new Outputs(
            ImmutableDictionary<string, OutputAttachment>.Empty,
            ImmutableDictionary<string, OutputField>.Empty,
            ImmutableDictionary<string, OutputSection>.Empty,
            ImmutableArray<OutputReference>.Empty,
            ImmutableArray<OutputUrl>.Empty,
            "",
            "",
            "",
            "",
            new OutputVault("", ""),
            ImmutableArray<string>.Empty,
            ImmutableDictionary<string, PropertyValue>.Empty
        );
        if (outputs.TryGetValue("attachments", out var attachments))
        {
            result = result with { Attachments = ConvertToDictionary(attachments, ConvertToOutputAttachment), };
            outputs = outputs.Remove("attachments");
        }
        if (outputs.TryGetValue("fields", out var fields))
        {
            result = result with { Fields = ConvertToDictionary(fields, ConvertToOutputField), };
            outputs = outputs.Remove("fields");
        }
        if (outputs.TryGetValue("sections", out var sections))
        {
            result = result with { Sections = ConvertToDictionary(sections, ConvertToOutputSection), };
            outputs = outputs.Remove("sections");
        }
        if (outputs.TryGetValue("references", out var references))
        {
            result = result with { References = ConvertToArray(references, ConvertToOutputReference), };
            outputs = outputs.Remove("references");
        }
        if (outputs.TryGetValue("urls", out var urls))
        {
            result = result with { Urls = ConvertToArray(urls, ConvertToOutputUrl), };
            outputs = outputs.Remove("urls");
        }
        if (outputs.TryGetValue("category", out var category))
        {
            result = result with { Category = ConvertToString(category) };
            outputs = outputs.Remove("category");
        }
        if (outputs.TryGetValue("uuid", out var uuid))
        {
            result = result with { Uuid = ConvertToString(uuid) };
            outputs = outputs.Remove("uuid");
        }
        if (outputs.TryGetValue("title", out var title))
        {
            result = result with { Title = ConvertToString(title) };
            outputs = outputs.Remove("title");
        }
        if (outputs.TryGetValue("notes", out var notes))
        {
            result = result with { Notes = ConvertToString(notes) };
            outputs = outputs.Remove("notes");
        }
        if (outputs.TryGetValue("vault", out var vault))
        {
            result = result with { Vault = ConvertToOutputVault(vault) };
            outputs = outputs.Remove("vault");
        }
        if (outputs.TryGetValue("tags", out var tags))
        {
            result = result with { Tags = ConvertToArray(tags, ConvertToString), };
            outputs = outputs.Remove("tags");
        }

        foreach (var output in outputs)
        {
            if (output.Value.TryGetObject(out var wkfields) && wkfields is not null)
            {
                if (!result.Sections.TryGetValue(output.Key, out var section))
                {
                    section = new OutputSection(ImmutableDictionary<string, OutputField>.Empty, ImmutableDictionary<string, OutputAttachment>.Empty, "", "");
                }
                foreach (var field in wkfields)
                {
                    result = result with { Sections = result.Sections.SetItem(output.Key, section with { Fields = section.Fields.SetItem(field.Key, ConvertToOutputField(field.Value)) }) };
                }
            }
            else
            {
                result = result with { Fields = result.Fields.SetItem(output.Key, ConvertToOutputField(output.Value)) };
            }
        }

        result = result with { WellKnownFields = outputs };

        return result;
    }

    private static OutputVault ConvertToOutputVault(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var vault) || vault is null) throw new Exception("Expected object");

        return new OutputVault(
            vault.GetString("uuid"),
            vault.GetString("name")
        );
    }

    private static OutputUrl ConvertToOutputUrl(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var url) || url is null) throw new Exception("Expected object");

        return new OutputUrl(
            url.GetString("label"),
            url.GetBool("primary"),
            url.GetString("href")
        );
    }

    private static OutputReference ConvertToOutputReference(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");

        return new OutputReference(
            field.GetString("itemId"),
            field.GetString("uuid"),
            field.GetString("label"),
            field.GetString("reference")
        );
    }

    private static OutputSection ConvertToOutputSection(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var section) || section is null) throw new Exception("Expected object");

        return new OutputSection(
            section.GetDictionary("fields", ConvertToOutputField),
            section.GetDictionary("attachments", ConvertToOutputAttachment),
            section.GetString("uuid"),
            section.GetString("label")
        );
    }

    private static OutputField ConvertToOutputField(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");

        return new OutputField(
            field.TryGetValue("type", out var type) && type.TryGetString(out var t) ? Enum.Parse<OnePassword.Items.FieldType>(t!, true) : OnePassword.Items.FieldType.String,
            field.TryGetValue("purpose", out var purpose) && purpose.TryGetString(out var p) ? Enum.Parse<OnePassword.Items.FieldPurpose>(p!, true) : OnePassword.Items.FieldPurpose.Unknown,
            field.TryGetValue("value", out var value) ? ConvertToString(value) : null,
            field.GetString("uuid"),
            field.GetString("label"),
            field.GetString("reference"),
            field.GetDictionary("data", x => x)
        );
    }

    private static OutputAttachment ConvertToOutputAttachment(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetObject(out var attachment) || attachment is null) throw new Exception("Expected object");

        return new OutputAttachment(
            attachment.GetString("uuid"),
            attachment.GetString("name"),
            attachment.TryGetValue("size", out var size) && size.TryGetNumber(out var s) ? (int)s : 0,
            attachment.GetString("reference"),
            attachment.GetString("hash")
        );
    }





    public static string GetString(this ImmutableDictionary<string, PropertyValue> values, string key) => values.TryGetValue(key, out var value) ? ConvertToString(value) : "";
    public static bool GetBool(this ImmutableDictionary<string, PropertyValue> values, string key) => values.TryGetValue(key, out var value) && value.TryGetBool(out var b) ? b : false;
    public static ImmutableDictionary<string, T> GetDictionary<T>(this ImmutableDictionary<string, PropertyValue> values, string key, Func<PropertyValue, T> convert) => values.TryGetValue(key, out var value) ? ConvertToDictionary(value, convert) : ImmutableDictionary<string, T>.Empty;
    public static ImmutableArray<T> GetArray<T>(this ImmutableDictionary<string, PropertyValue> values, string key, Func<PropertyValue, T> convert) => values.TryGetValue(key, out var value) ? ConvertToArray(value, convert) : ImmutableArray<T>.Empty;



    public static Inputs ConvertToInputs(this Outputs outputs)
    {
        return new Inputs(
            outputs.Fields.ToImmutableDictionary(x => x.Key, x => new InputField(x.Value.Value, x.Value.Purpose, x.Value.Type)),
            outputs.Attachments.ToImmutableDictionary(x => x.Key, x => new InputAttachment(x.Value.Hash)),
            outputs.Sections.ToImmutableDictionary(x => x.Key, x =>
            new InputSection(x.Value.Fields.ToImmutableDictionary(x => x.Key,
             x => new InputField(x.Value.Value, x.Value.Purpose, x.Value.Type)), x.Value.Attachments.ToImmutableDictionary(x => x.Key, x => new InputAttachment(x.Value.Hash)))),
            outputs.References.ToImmutableArray().Select(x => new InputReference(x.ItemId)).ToImmutableArray(),
            outputs.Urls.ToImmutableArray().Select(x => new InputUrl(x.Label, x.Primary, x.Href)).ToImmutableArray(),
            outputs.Tags.ToImmutableArray(),
            outputs.Title,
            outputs.Category,
            outputs.Vault.Name,
            outputs.Notes,
            outputs.WellKnownFields
        );
    }

}

public static class AssetOrArchiveExtensions
{
    private static PropertyInfo GetValueMethod = typeof(AssetOrArchive).GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance)!;
    public static string HashAssetOrArchive(AssetOrArchive assetOrArchive)
    {
        if (assetOrArchive is FileAsset or RemoteAsset or StringAsset or FileArchive or RemoteArchive)
        {
            var value = (string)GetValueMethod.GetValue(assetOrArchive)!;
            return xxHash128.ComputeHash(value).ToGuid().ToString("N");
        }
        if (assetOrArchive is AssetArchive assetArchive)
        {
            var value = (ImmutableDictionary<string, AssetOrArchive>)GetValueMethod.GetValue(assetOrArchive)!;
            var hashes = value.OrderBy(z => z.Key).Select(z => HashAssetOrArchive(z.Value)).ToImmutableArray();
            return xxHash128.ComputeHash(string.Join(":", hashes)).ToGuid().ToString("N");
        }
        throw new Exception("Unknown asset or archive type");
    }
}