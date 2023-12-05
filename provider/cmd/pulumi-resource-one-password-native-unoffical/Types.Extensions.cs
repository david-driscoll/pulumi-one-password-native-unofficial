
using System.Collections.Immutable;
using System.Runtime;
using Pulumi.Experimental.Provider;

namespace pulumi_resource_one_password_native_unoffical;
public static partial class TemplateMetadata
{

    private static ImmutableDictionary<string, _ResourceType> ResourceTypesDictionary { get; } = ResourceTypes.ToImmutableDictionary(z => z.Urn);
    private static ImmutableDictionary<string, _FunctionType> FunctionTypesDictionary { get; } = FunctionTypes.ToImmutableDictionary(z => z.Urn);

    public static _ResourceType? GetResourceTypeFromUrn(string urn)
    {
        return ResourceTypes.FirstOrDefault(z => urn.Contains($":{z.Urn}:"));
    }

    public static _FunctionType? GetFunctionType(string urn)
    {
        return FunctionTypesDictionary.TryGetValue(urn, out var value) ? value : null;
    }

    public interface IPulumiItemType
    {
        string Urn { get; }
        string ItemName { get; }
    }
    public record _ResourceType(string Urn, string ItemName, ImmutableArray<(string field, string? section)> Fields) : IPulumiItemType
    {
    }
    public record _FunctionType(string Urn, string ItemName, ImmutableArray<(string field, string? section)> Fields) : IPulumiItemType
    {
    }
}