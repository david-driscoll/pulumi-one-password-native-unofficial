using System.Diagnostics;
using System.Net.Http.Headers;
using GeneratedCode;
using Humanizer;
using Json.Patch;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Refit;

namespace pulumi_resource_one_password_native_unofficial;

public static class DebugHelper
{
    public static void WaitForDebugger()
    {
        // if (!Environment.GetCommandLineArgs().Any(z => z is "--debugger" or "--debug")) return;
        // var count = 0;
        // while (!Debugger.IsAttached)
        // {
        //     Thread.Sleep(1000);
        //     if (count++ > 30)
        //     {
        //         break;
        //     }
        // }
    }
}

public static class Helpers
{
    public static string ToPropertyPath(this PatchOperation operation)
    {
        return string.Join(".", operation.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries).Select(z => z.Camelize()));
    }
    public static I1PasswordConnect CreateConnectClient(string url, string token)
    {
        return RestService.For<I1PasswordConnect>(url, new RefitSettings()
        {
            HttpMessageHandlerFactory = () => new AuthHeaderHandler(token) { InnerHandler = new HttpClientHandler() }
        });
    }

    class AuthHeaderHandler : DelegatingHandler
        {
            private readonly string _token;

    public AuthHeaderHandler(string token)
    {
        _token = token;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
    
    public static string GetNameFromUrn(string urn)
    {
        return urn.Split(':').Last();
    }

    public static string NewUniqueName(string prefix, int? randomSeed = null, int randlen = 8, int? maxlen = null, string? charset = null)
    {
        if (randlen <= 0)
        {
            randlen = 8;
        }

        if (maxlen.HasValue && maxlen > 0 && prefix.Length + randlen > maxlen)
        {
            throw new Exception($"name '{prefix}' plus {randlen} random chars is longer than maximum length {maxlen}");
        }

        if (charset is null)
        {
            charset = "0123456789abcdefghijklmnopqrstuvwxyz";
        }

        if (!randomSeed.HasValue)
        {
            randomSeed = new Random().Next();
        }

        var r = new Random(randomSeed.Value);
        var randomSuffix = "";
        for (var i = 0; i < randlen; i++)
        {
            randomSuffix += charset[r.Next(0, charset.Length - 1)];
        }

        return prefix + randomSuffix;
    }
    
    public static T? GetValue<T>(this IDictionary<string, object>? objects, string key)
    {
        if (objects is null)
        {
            return default;
        }
        if (!objects.TryGetValue(key, out var value))
        {
            throw new Exception($"Missing required property '{key}'");
        }

        if (value is not T)
        {
            throw new Exception($"Property '{key}' is not of type '{typeof(T).Name}'");
        }

        return (T) value;
    }
}
