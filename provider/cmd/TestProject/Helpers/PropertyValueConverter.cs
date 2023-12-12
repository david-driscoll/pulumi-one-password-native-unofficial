using pulumi_resource_one_password_native_unofficial.Domain;
using Pulumi.Experimental.Provider;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

class DictionaryPropertyValueConverter : WriteOnlyJsonConverter<IDictionary<string, PropertyValue>>
{
    private readonly PropertyValueConverter _converter = new();

    public override void Write(VerifyJsonWriter writer, IDictionary<string, PropertyValue> value)
    {
        writer.WriteStartObject();
        foreach (var item in value.OrderBy(z => z.Key))
        {
            writer.WritePropertyName(item.Key);
            _converter.Write(writer, item.Value, item.Key);
        }

        writer.WriteEndObject();
    }
}

class PropertyValueConverter : WriteOnlyJsonConverter<PropertyValue?>
{
    private static readonly HashSet<string> ServerGeneratedFields = new(StringComparer.OrdinalIgnoreCase) { "id", "uuid", "reference", "title" };

    public override void Write(VerifyJsonWriter writer, PropertyValue? value)
    {
        Write(writer, value, null);
    }

    internal void Write(VerifyJsonWriter writer, PropertyValue? value, string? parentProperty)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        if (value.TryGetObject(out var @object))
        {
            writer.WriteStartObject();
            foreach (var item in @object!.OrderBy(z => z.Key))
            {
                writer.WritePropertyName(item.Key);
                Write(writer, item.Value, item.Key);
            }

            writer.WriteEndObject();
        }
        else if (value.TryGetArray(out var array))
        {
            writer.WriteStartArray();
            foreach (var item in array)
            {
                Write(writer, item);
            }

            writer.WriteEndArray();
        }
        else if (value.TryGetSecret(out _))
        {
            writer.WriteValue("[secret]");
        }
        else if (value.IsNull)
        {
            writer.WriteNull();
        }
        else if (value.IsComputed)
        {
            writer.WriteValue("[computed]");
        }
        else if (value.TryGetOutput(out var @output))
        {
            Write(writer, @output.Value!);
        }
        else if (value.TryGetArchive(out var archive))
        {
            writer.WriteValue(AssetOrArchiveExtensions.HashAssetOrArchive(archive));
        }
        else if (value.TryGetAsset(out var asset))
        {
            writer.WriteValue(AssetOrArchiveExtensions.HashAssetOrArchive(asset));
        }
        else if (value.TryGetString(out var @string))
        {
            if (@parentProperty is not null && ServerGeneratedFields.Contains(@parentProperty))
            {
                writer.WriteValue(@parentProperty.Equals("title", StringComparison.OrdinalIgnoreCase) && @string is { Length: > 8 }
                    ? @string[..^8] + "abcd1234"
                    : "[redacted]");
            }
            else
            {
                writer.WriteValue(@string);
            }
        }
        else if (value.TryGetNumber(out var number))
        {
            writer.WriteValue(number);
        }
        else if (value.TryGetBool(out var boolean))
        {
            writer.WriteValue(boolean);
        }
        else
        {
            throw new NotSupportedException($"Unsupported property value type {value.Type}");
        }
    }
}
