using System.Text.Json;
using CliWrap;
using Serilog;
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePasswordVaults(Command command, ArgsBuilder argsBuilder, OnePasswordOptions options, ILogger logger, JsonSerializerOptions serializerOptions)
    : ServiceAccountOnePasswordBase(command, argsBuilder.Add("vault"), options, logger, serializerOptions), IOnePasswordVaults
{
    public async Task<VaultResponse> Get(string? vault = null, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            // ReSharper disable once NullableWarningSuppressionIsUsed
            Command.WithArguments(ArgsBuilder.Add("get").Add(vault ?? options.Vault!).Build()),
            cancellationToken
        );
        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<VaultResponse>(result.StandardOutput, SerializerOptions)!;
    }
}
