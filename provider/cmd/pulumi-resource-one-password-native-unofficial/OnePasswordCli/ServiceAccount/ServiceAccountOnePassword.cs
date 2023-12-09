using System.Text.Json;
using System.Text.Json.Serialization;
using CliWrap;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePassword : ServiceAccountOnePasswordBase, IOnePassword
{
    private readonly Lazy<IOnePasswordItems> _items;
    private readonly Lazy<IOnePasswordVaults> _vaults;

    public ServiceAccountOnePassword(OnePasswordOptions options, ILogger logger) : base(Cli.Wrap("op"), new(), options, logger,
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        })
    {
        Options = options;
        _items = new Lazy<IOnePasswordItems>(() => new ServiceAccountOnePasswordItems(Command, ArgsBuilder, options, Logger, SerializerOptions));
        _vaults = new Lazy<IOnePasswordVaults>(() => new ServiceAccountOnePasswordVaults(Command, ArgsBuilder, options, Logger, SerializerOptions));
    }

    public OnePasswordOptions Options { get; }
    public IOnePasswordItems Items => _items.Value;
    public IOnePasswordVaults Vaults => _vaults.Value;

    public async Task<WhoAmIResponse> WhoAmI(CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("whoami").Build()),
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);

        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<WhoAmIResponse>(result.StandardOutput)!;
    }

    public async Task<string> Read(string reference, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("read").Add(reference).Build()),
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);
        return result.StandardOutput.TrimEnd();
    }

    public async Task<string> Inject(string template, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("inject").Build()),
            template,
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);
        return result.StandardOutput.TrimEnd();
    }
}
