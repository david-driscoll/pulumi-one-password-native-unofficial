using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public interface IPulumiItemType
{
    string Urn { get; }
    string ItemName { get; }
    string ItemCategory { get; }
    ImmutableArray<(string field, string? section)> Fields { get; }
}