using System.Text;
using System.Text.Json;
using CliWrap;
using CliWrap.Buffered;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public abstract class ServiceAccountOnePasswordBase(
    Command command,
    ArgsBuilder argsBuilder,
    OnePasswordOptions options,
    ILogger logger,
    JsonSerializerOptions serializerOptions)
{
    private protected JsonSerializerOptions SerializerOptions => serializerOptions;
    private protected Command Command => command;
    private protected ArgsBuilder ArgsBuilder => argsBuilder;
    private protected ILogger Logger => logger;

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, CancellationToken cancellationToken)
    {
        // Logger.Information("Executing command: {Command} {Input}", command.Arguments);
        return command
            .WithEnvironmentVariables(options.Apply)
            .ExecuteBufferedAsync(cancellationToken);
    }

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, string standardInput, CancellationToken cancellationToken)
    {
        // Logger.Information("Executing command: {Command} - {Input}", command.Arguments, standardInput);
        return command
            .WithEnvironmentVariables(options.Apply)
            .WithStandardInputPipe(PipeSource.FromString(standardInput))
            .ExecuteBufferedAsync(cancellationToken);
    }

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, object standardInput, CancellationToken cancellationToken)
    {
        return ExecuteCommand(command, JsonSerializer.Serialize(standardInput, SerializerOptions), cancellationToken);
    }
}
