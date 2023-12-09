using Serilog;

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
