using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using Serilog;

namespace TestProject.Helpers;

public interface IServerFixture
{
    string TemporaryDirectory { get; }
    string Vault { get; }

    Task<Provider> ConfigureProvider(ILogger logger, ImmutableDictionary<string, PropertyValue>? additionalConfig = default,
        CancellationToken cancellationToken = default);
}
