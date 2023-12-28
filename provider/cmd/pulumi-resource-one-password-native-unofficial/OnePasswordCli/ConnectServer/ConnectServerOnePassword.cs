using pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;
using Serilog;
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePassword(
    OnePasswordOptions options,
    ILogger logger) : ConnectServerOnePasswordBase(options, logger), IOnePassword
{
    private readonly Lazy<IOnePasswordItems> _items = new(() => new ConnectServerOnePasswordItems(options, logger));
    private readonly Lazy<IOnePasswordVaults> _vaults = new(() => new ConnectServerOnePasswordVaults(options, logger));
    private readonly Lazy<IOnePassword> _cli = new(() => new ServiceAccountOnePassword(options, logger));
    public IOnePasswordItems Items => _items.Value;
    public IOnePasswordVaults Vaults => _vaults.Value;
    public OnePasswordOptions Options => options;

    public async Task<WhoAmIResponse> WhoAmI(CancellationToken cancellationToken = default)
    {
        try
        {
            await Connect.GetHeartbeat();
            // ReSharper disable once NullableWarningSuppressionIsUsed
            return new WhoAmIResponse(options.ConnectHost!, "CONNECT", "", "");
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error getting heartbeat");
            throw;
        }
    }


    public Task<byte[]> Read(string reference, CancellationToken cancellationToken = default)
    {
        return _cli.Value.Read(reference, cancellationToken);
    }

    public Task<string> Inject(string template, CancellationToken cancellationToken = default)
    {
        return _cli.Value.Inject(template, cancellationToken);
    }
}
