using System.Collections.Frozen;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;
using Serilog;
using File = GeneratedCode.File;

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
            var connected = await Connect.GetHeartbeat();
            return new WhoAmIResponse(options.ConnectHost, "CONNECT", "", "");
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error getting heartbeat");
            throw;
        }
    }


    public Task<string> Read(string reference, CancellationToken cancellationToken = default)
    {
        return _cli.Value.Read(reference, cancellationToken);
    }

    public Task<string> Inject(string template, CancellationToken cancellationToken = default)
    {
        return _cli.Value.Inject(template, cancellationToken);
    }
}
