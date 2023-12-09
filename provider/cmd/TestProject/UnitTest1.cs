using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Rocket.Surgery.OnePasswordNativeUnofficial;

namespace TestProject;

using Pulumi.Automation;

[Collection("Connect collection")]
public class UnitTest1 : IClassFixture<PulumiFixture>
{
    private readonly PulumiFixture _fixture;

    public UnitTest1(PulumiFixture fixture, ConnectServerFixture connectServerFixture)
    {
        _fixture = fixture;
        fixture.Connect(connectServerFixture);
    }

    [Fact]
    public async Task Should_Create_Login_Item()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new LoginItem("login", new()
            {
                Title = "Test Login",
                Username = "myusername",
                Password = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "pulumi-testing",
                Urls = "http://notlocalhost.com",
                Notes = "this is a note"
            });
        });

        var stack = await LocalWorkspace.CreateStackAsync(new InlineProgramArgs("onepassword", "testing", program)
        {
            EnvironmentVariables = _fixture.EnvironmentVariables,
        });
        var up = await stack.UpAsync();
        await stack.DestroyAsync();
    }
}

[CollectionDefinition("Connect collection")]
public class DatabaseCollection : ICollectionFixture<ConnectServerFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class ConnectServerFixture : IAsyncLifetime
{
    private IContainer _connectApi;

    public async Task InitializeAsync()
    {
        if (Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_JSON") is not { Length: > 0 })
        {
            throw new Exception("PULUMI_ONEPASSWORD_CONNECT_JSON is not set");
        }

        if (Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_TOKEN") is not { Length: > 0 })
        {
            throw new Exception("PULUMI_ONEPASSWORD_CONNECT_TOKEN is not set");
        }

        _connectApi = new ContainerBuilder()
            .WithImage("1password/connect-api:latest")
            .WithResourceMapping(Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_JSON"), "/home/opuser/.op/1password-credentials.json")
            .WithPortBinding(8080, true)
            .Build();

        await _connectApi.StartAsync();

        ConnectHost = new UriBuilder()
        {
            Host = _connectApi.Hostname,
            Port = _connectApi.GetMappedPublicPort(8080)
        }.Uri;
    }

    public Uri ConnectHost { get; private set; }

    public async Task DisposeAsync()
    {
        await _connectApi.DisposeAsync();
    }
}

public class PulumiFixture : IAsyncLifetime
{
    public string TemporaryDirectory { get; private set; } = "";
    public ImmutableDictionary<string, string?> EnvironmentVariables { get; private set; } = ImmutableDictionary<string, string?>.Empty;

    public LocalWorkspace Workspace { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        TemporaryDirectory = Path.Combine(Path.GetTempPath(), "pulumi", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(TemporaryDirectory);
        Directory.CreateDirectory(Path.Combine(TemporaryDirectory, "backend-dir"));

        EnvironmentVariables = EnvironmentVariables
            .Add("PULUMI_BACKEND_URL", $"file:///{Path.Combine(TemporaryDirectory, "backend-dir").Replace("\\", "/")}")
            .Add("PULUMI_CONFIG_PASSPHRASE", "backup_password");
    }

    public void Connect(ConnectServerFixture connectServerFixture)
    {
        EnvironmentVariables = EnvironmentVariables.Add("OP_CONNECT_HOST", connectServerFixture.ConnectHost.ToString())
            .Add("OP_CONNECT_TOKEN", Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_TOKEN"));
    }

    public Task DisposeAsync()
    {
        Directory.Delete(TemporaryDirectory, true);
        return Task.CompletedTask;
    }

    public string ResourcePath(string path, [CallerFilePath] string pathBase = null!)
    {
        var dir = Path.GetDirectoryName(pathBase) ?? ".";
        return Path.Combine(dir, path);
    }
}
