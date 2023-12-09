using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public record ItemRequestBase
{
    public string? Title { get; init; }
    public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
    public string? Vault { get; init; }
}