using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public record ItemResponseBase(string Title, string Vault, ImmutableArray<string> Tags);