using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;

namespace TestProject.Helpers;

public interface IServerFixture
{
    string TemporaryDirectory { get; }
    string Vault { get; }

    Task ConfigureProvider(OnePasswordProvider provider, ImmutableDictionary<string, PropertyValue>? additionalConfig = default,
        CancellationToken cancellationToken = default);
}
