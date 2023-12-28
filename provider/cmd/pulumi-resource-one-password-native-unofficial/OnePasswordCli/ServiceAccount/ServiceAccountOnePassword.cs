using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CliWrap;
using Serilog;
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

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

    public async Task<byte[]> Read(string reference, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        var errorResult = new StringBuilder();
        var result = await ExecuteUnbufferedCommand(
            Command
                .WithArguments(ArgsBuilder.Add("read").Add($"\"{reference.Trim('\"')}\"").Build())
                .WithStandardOutputPipe(PipeTarget.ToStream(memoryStream))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorResult)),
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(errorResult.ToString());
        return memoryStream.ToArray();
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
