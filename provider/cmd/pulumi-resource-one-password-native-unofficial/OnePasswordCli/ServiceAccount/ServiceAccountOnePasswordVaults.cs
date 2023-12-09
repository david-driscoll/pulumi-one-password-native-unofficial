using System.Text.Json;
using CliWrap;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePasswordVaults(Command command, ArgsBuilder argsBuilder, OnePasswordOptions options, ILogger logger, JsonSerializerOptions serializerOptions)
    : ServiceAccountOnePasswordBase(command, argsBuilder.Add("vault"), options, logger, serializerOptions), IOnePasswordVaults
{
    public async Task<VaultResponse> Get(string? vault = null, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("get").Add(vault ?? options.Vault).Build()),
            cancellationToken
        );
        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<VaultResponse>(result.StandardOutput, SerializerOptions)!;
    }
}
