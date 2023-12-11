using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
using Serilog;

namespace TestProject.Helpers;

public class ServiceAccountFixture : IAsyncLifetime, IServerFixture
{
    private HashSet<DelegatingProvider> _delegatingProviders = new();
    public string TemporaryDirectory { get; private set; } = "";
    public string Vault { get; private set; } = "testing-pulumi";
    public string Token { get; set; }

    public Task InitializeAsync()
    {
        if (Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN") is not { Length: > 0 })
        {
            throw new Exception("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN is not set");
        }

        TemporaryDirectory = Path.Combine(Path.GetTempPath(), "service-account", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(TemporaryDirectory);
        Token = Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN")!;
        return Task.CompletedTask;
    }


    public async Task DisposeAsync()
    {
        foreach (var provider in _delegatingProviders)
        {
            foreach (var id in provider.CreatedIds)
            {
                await provider.Delete(new("", id, ImmutableDictionary<string, PropertyValue>.Empty.Add("vault", new(
                    ImmutableDictionary<string, PropertyValue>.Empty.Add("id", new(Vault))
                )), TimeSpan.MaxValue), CancellationToken.None);
                await Task.Delay(200);
            }
        }

        Directory.Delete(TemporaryDirectory, true);
    }

    private static readonly PropertyValueSerializer Serializer = new();

    public async Task<Provider> ConfigureProvider(ILogger logger, ImmutableDictionary<string, PropertyValue>? additionalConfig = default,
        CancellationToken cancellationToken = default)
    {
        var provider = new DelegatingProvider(new OnePasswordProvider(logger));
        _delegatingProviders.Add(provider);

        var config = new
        {
            serviceAccountToken = new PropertyValue(new PropertyValue(Token))
        };
        var values = await Serializer.Serialize(config);
        values.TryGetObject(out var configObject);
        configObject = configObject!.AddRange(additionalConfig ?? ImmutableDictionary<string, PropertyValue>.Empty);

        var response = await provider.CheckConfig(new CheckRequest(ItemType.LoginItem, configObject, configObject, ImmutableArray<byte>.Empty),
            cancellationToken);
        var configureResponse = await provider.Configure(new ConfigureRequest(ImmutableDictionary<string, string>.Empty, configObject, true, true),
            cancellationToken);

        return provider;
    }


    [CollectionDefinition("Service Account collection")]
    public class Collection : ICollectionFixture<ServiceAccountFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
