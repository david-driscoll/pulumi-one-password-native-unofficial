using Pulumi.Experimental.Provider;
using Serilog.Core;
using Serilog.Events;

namespace pulumi_resource_one_password_native_unoffical;

class HostSink : ILogEventSink
{
    private readonly IHost _host;
    private readonly IFormatProvider? _formatProvider;

    public HostSink(IHost host, IFormatProvider? formatProvider = null)
    {
        this._host = host;
        _formatProvider = formatProvider;
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        var urnString = "one-password-native-unoffical";
        if (logEvent.Properties.TryGetValue("Urn", out var urn) || logEvent.Properties.TryGetValue("urn", out urn))
        {
            urnString = urn.ToString(null, _formatProvider);
        }
        _host.LogAsync(new(GetLogSeverity(logEvent.Level), message, urnString)).Wait();
    }

    private static LogSeverity GetLogSeverity(LogEventLevel level) => level switch
    {
        LogEventLevel.Verbose => LogSeverity.Debug,
        LogEventLevel.Debug => LogSeverity.Debug,
        LogEventLevel.Information => LogSeverity.Info,
        LogEventLevel.Warning => LogSeverity.Warning,
        LogEventLevel.Error => LogSeverity.Error,
        LogEventLevel.Fatal => LogSeverity.Error,
        _ => LogSeverity.Debug
    };
}
