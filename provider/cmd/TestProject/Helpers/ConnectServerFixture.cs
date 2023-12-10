using System.Collections.Immutable;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using GeneratedCode;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
using File = GeneratedCode.File;

namespace TestProject.Helpers;

public class ConnectServerFixture : IAsyncLifetime, IServerFixture
{
    private IContainer _connectApi;
    private IContainer _connectSync;
    private HashSet<string> _existingItems;
    public string TemporaryDirectory { get; private set; } = "";
    public string Vault { get; private set; } = "";
    public I1PasswordConnect Connect => pulumi_resource_one_password_native_unofficial.Helpers.CreateConnectClient(ConnectHost.ToString(), ConnectToken);

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

        TemporaryDirectory = Path.Combine(Path.GetTempPath(), "connect", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(TemporaryDirectory);
        var volume = new VolumeBuilder().WithName("data").WithCleanUp(true).Build();
        
        _connectApi = new ContainerBuilder()
            .WithImage("1password/connect-api:latest")
            .WithConnectJson(Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_JSON"))
            .WithPortBinding(8080, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(x => x.ForPath("/heartbeat").ForPort(8080)))
            .WithVolumeMount(volume, "/home/opuser/.op/data")
            .Build();

        _connectSync = new ContainerBuilder()
            .WithImage("1password/connect-sync:latest")
            .WithConnectJson(Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_JSON"))
            .WithPortBinding(8080, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(x => x.ForPath("/heartbeat").ForPort(8080)))
            .WithVolumeMount(volume, "/home/opuser/.op/data")
            .Build();

        await Task.WhenAll(_connectApi.StartAsync(), _connectSync.StartAsync());

        ConnectHost = new UriBuilder()
        {
            Host = _connectApi.Hostname,
            Port = _connectApi.GetMappedPublicPort(8080),
        }.Uri;

        var health = await Connect.GetServerHealth();

        var vault = await Connect.GetVaults("")
            .ToAsyncEnumerable()
            .SelectMany(z => z.ToAsyncEnumerable())
            .FirstAsync(z => z.Name == "testing-pulumi");

        _existingItems = await Connect.GetVaultItems(vault.Id, "")
            .ToAsyncEnumerable()
            .SelectMany(z => z.ToAsyncEnumerable())
            .Select(z => z.Id)
            .ToHashSetAsync(StringComparer.OrdinalIgnoreCase);
        Vault = vault.Id;
    }

    public Uri ConnectHost { get; private set; }
    public string ConnectToken => Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_CONNECT_TOKEN") ?? "";

    public async Task DisposeAsync()
    {
        // await foreach (var item in Connect.GetVaultItems(Vault, "")
        //                    .ToAsyncEnumerable()
        //                    .SelectMany(z => z.ToAsyncEnumerable())
        //                    // .Where(z => !_existingItems.Contains(z.Id))
        //               )
        // {
        //     await Connect.DeleteVaultItem(Vault, item.Id);
        // }

        Directory.Delete(TemporaryDirectory, true);
        await _connectApi.DisposeAsync();
        await _connectSync.DisposeAsync();
    }

    private static readonly PropertyValueSerializer Serializer = new();
    public async Task ConfigureProvider(OnePasswordProvider provider, ImmutableDictionary<string, PropertyValue>? additionalConfig = default, CancellationToken cancellationToken = default)
    {
        var config = new
        {
            connectHost = new PropertyValue(new PropertyValue(ConnectHost.ToString())), 
            connectToken = new PropertyValue(new PropertyValue(ConnectToken))
        };
        var values = await Serializer.Serialize(config);
        values.TryGetObject(out var configObject);
        configObject = configObject!.AddRange(additionalConfig ?? ImmutableDictionary<string, PropertyValue>.Empty);

        var response = await provider.CheckConfig(new CheckRequest(ItemType.LoginItem, configObject, configObject, ImmutableArray<byte>.Empty),
            cancellationToken);
        var configureResponse = await provider.Configure(new ConfigureRequest(ImmutableDictionary<string, string>.Empty, configObject, true, true),
            cancellationToken);
    }

    [CollectionDefinition("Connect collection")]
    public class Collection : ICollectionFixture<ConnectServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
    
}

public static class ExtensionMethods
{
    public static ContainerBuilder WithConnectJson(this ContainerBuilder builder, string json)
    {
        if (System.IO.File.Exists(json))
        {
            return builder.WithResourceMapping(new FileInfo(json),
                new FileInfo("/home/opuser/.op/1password-credentials.json"));
        }
        else
        {
            return builder.WithResourceMapping(Encoding.UTF8.GetBytes(json), "/home/opuser/.op/1password-credentials.json");
        }
    }
} 
