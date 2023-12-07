using System.Collections.Immutable;
using Pulumi.Experimental.Provider;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public static class CommonExtensions
{
    internal static string ConvertToString(PropertyValue propertyValue)
    {
        if (!propertyValue.TryGetString(out var s) || s is null) return "";
        return s;
    }
    internal static ImmutableDictionary<string, T> ConvertToDictionary<T>(PropertyValue propertyValue, Func<PropertyValue, T> convert)
    {
        if (!propertyValue.TryGetObject(out var @object) || @object is null) return ImmutableDictionary<string, T>.Empty;
        return @object.ToImmutableDictionary(x => x.Key, x => convert(x.Value));
    }

    internal static ImmutableArray<T> ConvertToArray<T>(PropertyValue propertyValue, Func<PropertyValue, T> convert)
    {
        if (!propertyValue.TryGetArray(out var array)) return ImmutableArray<T>.Empty;
        return array.Select(z => convert(z)).ToImmutableArray();
    }
    //
    // internal static InputField ConvertToInputField(PropertyValue propertyValue)
    // {
    //     if (!propertyValue.TryGetObject(out var field) || field is null) throw new Exception("Expected object");
    //
    //     return new InputField(
    //         field.TryGetValue("value", out var value) ? value.TryUnwrap(out var v) ? ConvertToString(v) : null : null,
    //         field.TryGetValue("purpose", out var purpose) && purpose.TryGetString(out var p) ? Enum.Parse<OnePassword.Items.FieldPurpose>(p!, true) : null,
    //         field.TryGetValue("type", out var type) && type.TryGetString(out var t) ? Enum.Parse<OnePassword.Items.FieldType>(t!, true) : null
    //     );
    // }
    internal static string GetString(this ImmutableDictionary<string, PropertyValue> values, string key) => values.TryGetValue(key, out var value) ? ConvertToString(value) : "";
    internal static bool GetBool(this ImmutableDictionary<string, PropertyValue> values, string key) => values.TryGetValue(key, out var value) && value.TryGetBool(out var b) ? b : false;
    internal static ImmutableDictionary<string, T> GetDictionary<T>(this ImmutableDictionary<string, PropertyValue> values, string key, Func<PropertyValue, T> convert) => values.TryGetValue(key, out var value) ? ConvertToDictionary(value, convert) : ImmutableDictionary<string, T>.Empty;
    internal static ImmutableArray<T> GetArray<T>(this ImmutableDictionary<string, PropertyValue> values, string key, Func<PropertyValue, T> convert) => values.TryGetValue(key, out var value) ? ConvertToArray(value, convert) : ImmutableArray<T>.Empty;

}
