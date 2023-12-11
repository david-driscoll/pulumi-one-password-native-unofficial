using System.Text;
using System.Text.Json;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.Exceptions;
using Polly;
using Polly.Fallback;
using Polly.Retry;
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

    private protected ResiliencePipeline<BufferedCommandResult> Policy { get; } =
        new ResiliencePipelineBuilder<BufferedCommandResult>()
            .AddFallback(new FallbackStrategyOptions<BufferedCommandResult>()
            {
                ShouldHandle = arguments =>
                    ValueTask.FromResult(arguments.Outcome.Exception is CommandExecutionException exception && exception.Message.Contains("(429)")),
                FallbackAction = arguments => arguments.Outcome.Exception is CommandExecutionException exception
                    ? ValueTask.FromResult(Outcome.FromException<BufferedCommandResult>(new TimeoutException(exception.Message)))
                    : ValueTask.FromResult(Outcome.FromException<BufferedCommandResult>(new TimeoutException("Unknown error")))
            })
            .AddRetry(new RetryStrategyOptions<BufferedCommandResult>()
            {
                Delay = TimeSpan.FromSeconds(1),
                MaxRetryAttempts = 5,
                BackoffType = DelayBackoffType.Exponential,
                ShouldHandle = arguments =>
                    ValueTask.FromResult(arguments.Outcome.Exception is CommandExecutionException e && e.Message.Contains("(409)")),
                UseJitter = true
            }).Build();

    protected ValueTask<BufferedCommandResult> ExecuteCommand(Command command, CancellationToken cancellationToken)
    {
        // Logger.Information("Executing command: {Command} {Input}", command.Arguments);
        return Policy.ExecuteAsync(
            static async (command, ct) => await command.ExecuteBufferedAsync(ct),
            command.WithEnvironmentVariables(options.Apply),
            cancellationToken
        );
    }

    protected ValueTask<BufferedCommandResult> ExecuteCommand(Command command, string standardInput, CancellationToken cancellationToken)
    {
        // Logger.Information("Executing command: {Command} - {Input}", command.Arguments, standardInput);
        return Policy.ExecuteAsync(
            static async (command, ct) => await command.ExecuteBufferedAsync(ct),
            command.WithEnvironmentVariables(options.Apply).WithStandardInputPipe(PipeSource.FromString(standardInput)),
            cancellationToken
        );
    }

    protected ValueTask<BufferedCommandResult> ExecuteCommand(Command command, object standardInput, CancellationToken cancellationToken)
    {
        return ExecuteCommand(command, JsonSerializer.Serialize(standardInput, SerializerOptions), cancellationToken);
    }
}
