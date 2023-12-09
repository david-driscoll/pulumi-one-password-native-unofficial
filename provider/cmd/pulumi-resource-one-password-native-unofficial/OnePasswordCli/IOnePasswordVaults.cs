namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public interface IOnePasswordVaults
{
    Task<VaultResponse> Get(string? vault = null, CancellationToken cancellationToken = default);
}