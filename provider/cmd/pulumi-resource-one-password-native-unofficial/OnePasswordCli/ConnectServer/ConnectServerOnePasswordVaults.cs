using Serilog;
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordVaults(OnePasswordOptions options, ILogger logger)
    : ConnectServerOnePasswordBase(options, logger), IOnePasswordVaults
{
    public async Task<VaultResponse> Get(string? vault = null, CancellationToken cancellationToken = default)
    {
        var vaultId = await GetVaultUuid(vault ?? options.Vault);
        var result = await Connect.GetVaultById(vaultId);
        return new VaultResponse(result.Id, result.AdditionalProperties.GetValue<string>("name") ?? result.Id);
    }
}
