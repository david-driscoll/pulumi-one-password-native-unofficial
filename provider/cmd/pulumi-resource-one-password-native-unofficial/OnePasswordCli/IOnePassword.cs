namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public interface IOnePassword
{
    OnePasswordOptions Options { get; }
    IOnePasswordItems Items { get; }
    IOnePasswordVaults Vaults { get; }
    Task<WhoAmIResponse> WhoAmI(CancellationToken cancellationToken = default);
    Task<string> Read(string reference, CancellationToken cancellationToken = default);
    Task<string> Inject(string template, CancellationToken cancellationToken = default);
}
