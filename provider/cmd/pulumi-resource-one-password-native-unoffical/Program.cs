using System.Reflection;
using pulumi_resource_one_password_native_unoffical;
using Pulumi.Experimental.Provider;
using Serilog;
using System.Text.Json;
using System.Diagnostics;
// See https://aka.ms/new-console-template for more information

// while (!Debugger.IsAttached)
// {
//     await Task.Delay(1000);
// }

await Provider.Serve(args, null, host =>
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Sink(new HostSink(host))
        .CreateLogger();
    return new OnePasswordProvider(Log.Logger);
}, CancellationToken.None);
