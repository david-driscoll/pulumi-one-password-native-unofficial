using System.Diagnostics;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
using Serilog;

// See https://aka.ms/new-console-template for more information

// while (!Debugger.IsAttached)
// {
//     await Task.Delay(1000);
// }

await Provider.Serve(args, null, host =>
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Sink(new HostSink(host))
        //.WriteTo.Console()
        .Enrich.FromLogContext()
        .CreateLogger();
    return new OnePasswordProvider(Log.Logger);
}, CancellationToken.None);
