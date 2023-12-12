using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record FunctionType(
    string Urn,
    string ItemName,
    string ItemCategory,
    TemplateMetadata.TransformOutputs TransformItemToOutputs,
    ImmutableArray<(string field, string? section)> Fields
) : IPulumiItemType
{
    public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item)
    {
        return TransformItemToOutputs(this, item, null);
    }
}
