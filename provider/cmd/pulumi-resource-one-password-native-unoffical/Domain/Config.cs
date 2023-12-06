using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using static pulumi_resource_one_password_native_unoffical.TemplateMetadata;
using static pulumi_resource_one_password_native_unoffical.Domain.CommonExtensions;

namespace pulumi_resource_one_password_native_unoffical.Domain;

public record Config
{
    public string? Vault { get; init; }
    public string? ServiceAccountToken { get; init; }
    public string? ConnectHost { get; init; }
    public string? ConnectToken { get; init; }
}

public static class ConfigExtensions
{
    public static Config ConvertToConfig(ImmutableDictionary<string, PropertyValue> inputs)
    {
        var result = new Config();
        if (inputs.TryGetValue("vault", out var vault))
        {
            result = result with { Vault = ConvertToString(vault), };
        }
        if (inputs.TryGetValue("serviceAccountToken", out var serviceAccountToken))
        {
            result = result with { Vault = ConvertToString(serviceAccountToken), };
        }
        if (inputs.TryGetValue("connectHost", out var connectHost))
        {
            result = result with { Vault = ConvertToString(connectHost), };
        }
        if (inputs.TryGetValue("connectToken", out var connectToken))
        {
            result = result with { Vault = ConvertToString(connectToken), };
        }

        return result;
    }

}
