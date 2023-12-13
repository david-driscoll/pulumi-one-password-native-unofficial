using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public interface IPulumiItemType
{
    string Urn { get; }
    string InputCategory { get; }
    string OutputCategory { get; }
}
