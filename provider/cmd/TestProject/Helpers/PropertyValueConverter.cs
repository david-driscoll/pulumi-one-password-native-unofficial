using Pulumi.Experimental.Provider;

namespace TestProject.Helpers;

class DictionaryPropertyValueConverter : WriteOnlyJsonConverter<IDictionary<string, PropertyValue>>
{
    private readonly PropertyValueConverter _converter = new();
    private static readonly HashSet<string> ServerGeneratedFields = new(StringComparer.OrdinalIgnoreCase) { "id", "uuid", "reference", "title" };

    public override void Write(VerifyJsonWriter writer, IDictionary<string, PropertyValue> value)
    {
        writer.WriteStartObject();
        foreach (var item in value.OrderBy(z => z.Key))
        {
            writer.WritePropertyName(item.Key);
            if (ServerGeneratedFields.Contains(item.Key))
            {
                writer.WriteValue(item.Key.Equals("title", StringComparison.OrdinalIgnoreCase) && item.Value.ToString().Length > 8 ? item.Value.ToString()[..^8] + "abcd1234" : "[redacted]");
            }
            else
            {
                _converter.Write(writer, item.Value);
            }
        }

        writer.WriteEndObject();
    }
}

class PropertyValueConverter : WriteOnlyJsonConverter<PropertyValue>
{
    private readonly PropertyValueSerializer _serializer;
    private static readonly HashSet<string> ServerGeneratedFields = new(StringComparer.OrdinalIgnoreCase) { "id", "uuid", "reference", "title" };

    public PropertyValueConverter()
    {
        _serializer = new PropertyValueSerializer();
    }

    public override void Write(VerifyJsonWriter writer, PropertyValue value)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        if (value.TryGetObject(out var @object))
        {
            writer.WriteStartObject();
            foreach (var item in @object.OrderBy(z => z.Key))
            {
                writer.WritePropertyName(item.Key);
                if (ServerGeneratedFields.Contains(item.Key))
                {
                    writer.WriteValue(item.Key.Equals("title", StringComparison.OrdinalIgnoreCase) ? item.Key[..^8] : "[redacted]");
                }
                else
                {
                    Write(writer, item.Value);
                }
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
        else if (value.TryGetString(out var @string))
        {
            writer.WriteValue(@string);
        }
        else if (value.TryGetNumber(out var number))
        {
            writer.WriteValue(number);
        }
        else if (value.TryGetBool(out var boolean))
        {
            writer.WriteValue(boolean);
        }
        else if (value.TryGetOutput(out var @output))
        {
            Write(writer, @output.Value);
        }
        else if (value.TryGetArchive(out var archive))
        {
            writer.WriteValue(AssetOrArchiveExtensions.HashAssetOrArchive(archive));
        }
        else if (value.TryGetAsset(out var asset))
        {
            writer.WriteValue(AssetOrArchiveExtensions.HashAssetOrArchive(asset));
        }
        else if (value.TryGetSecret(out var secret))
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
        else
        {
            throw new NotSupportedException($"Unsupported property value type {value.Type}");
        }
    }
}
