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
        return array.Select(convert).ToImmutableArray();
    }

    internal static PropertyValue GetProperty(this PropertyValue? value, string propertyPath)
    {
        if (value is null) return PropertyValue.Null;
        if (!value.TryUnwrap(out var v)) return PropertyValue.Null;
        return v.TryGetObject(out var obj) ? GetProperty(obj!, propertyPath) :
            v.TryGetArray(out var array) ? GetProperty(array, propertyPath) : value;
    }

    internal static PropertyValue GetProperty(this ImmutableDictionary<string, PropertyValue>? values, string propertyPath)
    {
        if (values is null) return PropertyValue.Null;
        var paths = propertyPath.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length == 0) return PropertyValue.Null;
        return values.TryGetValue(paths[0], out var value) ? GetProperty(value, string.Join(".", paths.Skip(1))) : PropertyValue.Null;
    }

    internal static PropertyValue GetProperty(this ImmutableArray<PropertyValue>? values, string propertyPath)
    {
        if (!values.HasValue) return PropertyValue.Null;
        var paths = propertyPath.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length == 0) return PropertyValue.Null;
        return int.TryParse(paths[0], out var index) ? GetProperty(values.Value.ElementAt(index), string.Join(".", paths.Skip(1))) : PropertyValue.Null;
    }

    internal static string? GetStringProperty(this PropertyValue? value, string propertyPath) =>
        GetProperty(value, propertyPath)?.TryGetString(out var s) == true ? s : null;

    internal static string? GetStringProperty(this ImmutableDictionary<string, PropertyValue>? values, string propertyPath) =>
        GetProperty(values, propertyPath)?.TryGetString(out var s) == true ? s : null;

    internal static string? GetStringProperty(this ImmutableArray<PropertyValue>? values, string propertyPath) =>
        GetProperty(values, propertyPath)?.TryGetString(out var s) == true ? s : null;

    internal static ImmutableDictionary<string, T> GetDictionary<T>(this ImmutableDictionary<string, PropertyValue> values, string key,
        Func<PropertyValue, T> convert) => values.TryGetValue(key, out var value) ? ConvertToDictionary(value, convert) : ImmutableDictionary<string, T>.Empty;

    internal static ImmutableArray<T> GetArray<T>(this ImmutableDictionary<string, PropertyValue> values, string key, Func<PropertyValue, T> convert) =>
        values.TryGetValue(key, out var value) ? ConvertToArray(value, convert) : ImmutableArray<T>.Empty;
}
