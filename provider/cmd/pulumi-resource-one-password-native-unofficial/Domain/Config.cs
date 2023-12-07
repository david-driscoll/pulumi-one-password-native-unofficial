using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using static pulumi_resource_one_password_native_unofficial.TemplateMetadata;
using static pulumi_resource_one_password_native_unofficial.Domain.CommonExtensions;

namespace pulumi_resource_one_password_native_unofficial.Domain;

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
        var result = new Config()
        {
            ConnectHost = GetConfigOrEnvironmentVariable(inputs, "connectHost", "OP_CONNECT_HOST"),
            ConnectToken = GetConfigOrEnvironmentVariable(inputs, "connectToken", "OP_CONNECT_TOKEN"),
            ServiceAccountToken = GetConfigOrEnvironmentVariable(inputs, "serviceAccountToken", "OP_SERVICE_ACCOUNT_TOKEN"),
        };
        
        if (inputs.TryGetValue("vault", out var vault))
        {
            result = result with { Vault = ConvertToString(vault), };
        }

        return result;
    }
    

    private static string? GetConfigOrEnvironmentVariable(ImmutableDictionary<string, PropertyValue> args, string variable, string environmentKey)
    {
        return args.TryGetValue(variable, out var v) && v.TryUnwrap(out v) && v.TryGetString(out var r) ? r : Environment.GetEnvironmentVariable(environmentKey);
    }
}
