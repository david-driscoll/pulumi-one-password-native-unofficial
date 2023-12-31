﻿using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial;
using pulumi_resource_one_password_native_unofficial.Domain;
using Pulumi.Experimental.Provider;
using Serilog;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

public class ServiceAccountFixture : IAsyncLifetime, IServerFixture
{
    private HashSet<DelegatingProvider> _delegatingProviders = new();
    public string TemporaryDirectory { get; private set; } = "";
    public string Vault { get; private set; } = "testing-pulumi";
    public string Token { get; set; } = "";

    public Task InitializeAsync()
    {
        if (Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN") is not { Length: > 0 })
        {
            throw new Exception("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN is not set");
        }

        DirectoryName = Guid.NewGuid().ToString("N");
        TemporaryDirectory = Path.Combine(Path.GetTempPath(), "service-account", DirectoryName);
        Directory.CreateDirectory(TemporaryDirectory);
        Token = Environment.GetEnvironmentVariable("PULUMI_ONEPASSWORD_SERVICE_ACCOUNT_TOKEN")!;
        return Task.CompletedTask;
    }

    public string DirectoryName { get; set; }


    public async Task DisposeAsync()
    {
        // deleting is too clostly with these tests on a family account :D
        // foreach (var provider in _delegatingProviders)
        // {
        //     foreach (var id in provider.CreatedIds)
        //     {
        //         try
        //         {
        //             await provider.Delete(new("", id, ImmutableDictionary<string, PropertyValue>.Empty.Add("vault", new(
        //                 ImmutableDictionary<string, PropertyValue>.Empty.Add("id", new(Vault))
        //             )), TimeSpan.MaxValue), CancellationToken.None);
        //         }
        //         catch
        //         {
        //             // ignored to not break test
        //         }
        //     }
        // }

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

        _ = await provider.CheckConfig(new CheckRequest(ResourceType.Login.Urn, configObject, configObject, ImmutableArray<byte>.Empty), cancellationToken);
        _ = await provider.Configure(new ConfigureRequest(ImmutableDictionary<string, string>.Empty, configObject, true, true), cancellationToken);

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
