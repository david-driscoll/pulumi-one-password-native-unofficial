using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using static pulumi_resource_one_password_native_unofficial.Domain.CommonExtensions;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public static class ConfigExtensions
{
    public static OnePasswordOptions ConvertToConfig(ImmutableDictionary<string, PropertyValue> inputs)
    {
        var result = new OnePasswordOptions()
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
