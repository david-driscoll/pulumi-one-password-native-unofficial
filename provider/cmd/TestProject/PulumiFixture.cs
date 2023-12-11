using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using Pulumi;
using Pulumi.Experimental.Provider;
using TestProject.Helpers;
// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject;

public class PulumiFixture : IAsyncLifetime
{
    public string BackendUrl { get; private set; } = "";
    public string TemporaryDirectory { get; private set; } = "";
    public PropertyValueSerializer Serializer { get; } = new();
    public ImmutableDictionary<string, string?> EnvironmentVariables { get; private set; } = ImmutableDictionary<string, string?>.Empty;

    public Task InitializeAsync()
    {
        TemporaryDirectory = Path.Combine(Path.GetTempPath(), "pulumi", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(TemporaryDirectory);
        Directory.CreateDirectory(Path.Combine(TemporaryDirectory, "backend-dir"));

        BackendUrl = new UriBuilder() { Host = "", Path = Path.Combine(TemporaryDirectory, "backend-dir"), Scheme = "file" }.Uri.ToString();
        if (OperatingSystem.IsWindows())
        {
            BackendUrl = BackendUrl.Replace("C:", "", StringComparison.OrdinalIgnoreCase);
        }

        EnvironmentVariables = EnvironmentVariables
            .Add("PULUMI_BACKEND_URL", BackendUrl)
            .Add("PULUMI_CONFIG_PASSPHRASE", "backup_password");
        return Task.CompletedTask;
    }

    public void Connect(ConnectServerFixture connectServerFixture)
    {
        EnvironmentVariables = EnvironmentVariables
            .Add("OP_CONNECT_HOST", connectServerFixture.ConnectHost.ToString())
            .Add("OP_CONNECT_TOKEN", Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_TOKEN"));
    }

    public Task DisposeAsync()
    {
        Directory.Delete(TemporaryDirectory, true);
        return Task.CompletedTask;
    }

    public string ResourcePath(string path, [CallerFilePath] string? pathBase = null)
    {
        var dir = Path.GetDirectoryName(pathBase) ?? ".";
        return Path.Combine(dir, path);
    }

    public async Task<(ImmutableDictionary<string, PropertyValue> Request, string Urn)> CreateRequestObject<TOutputs, TInputs>(string name, TInputs inputs)
        where TOutputs : CustomResource
        where TInputs : ResourceArgs
    {
        var request = await Serializer.Serialize(inputs);
        if (!request.TryGetObject(out var requestObject)) throw new Exception("failed to serialize request");
        var urn = await Urn.Create(name, typeof(TOutputs).GetCustomAttribute<ResourceTypeAttribute>()?.Type ??  throw new Exception("Could not Item Type"), project: "project", stack: "testing");
        return (Request: requestObject!, Urn: urn);
    }

    public void ServiceAccount(ServiceAccountFixture serviceAccountFixture)
    {
        EnvironmentVariables = EnvironmentVariables
            .Add("OP_SERVICE_ACCOUNT_TOKEN", serviceAccountFixture.Token);
    }
}
