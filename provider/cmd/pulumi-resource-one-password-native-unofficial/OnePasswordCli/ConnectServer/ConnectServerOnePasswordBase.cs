using System.Collections.Immutable;
using System.Net.Http.Headers;
using System.Text.Json;
using GeneratedCode;
using Refit;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordBase(
    OnePasswordOptions options,
    ILogger logger)
{
    private protected readonly ILogger Logger = logger;
    private ImmutableDictionary<string, string> VaultIds = ImmutableDictionary<string, string>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);

    private protected readonly I1PasswordConnect Connect = RestService.For<I1PasswordConnect>(options.ConnectHost!, new RefitSettings()
    {
        HttpMessageHandlerFactory = () => new AuthHeaderHandler(options) { InnerHandler = new HttpClientHandler() }
    });

    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly OnePasswordOptions _options;

        public AuthHeaderHandler(OnePasswordOptions options)
        {
            _options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ConnectToken);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

    protected async Task<string> GetVaultUuid(string? name)
    {
        if (VaultIds.TryGetValue(name, out var id))
        {
            return id;
        }

        var vaults = await Connect.GetVaults("");
        foreach (var vault in vaults)
        {
            VaultIds = VaultIds.Add(vault.Id, vault.Id);
            VaultIds = VaultIds.Add(vault.Name, vault.Id);
        }

        return VaultIds.TryGetValue(name, out id) ? id : throw new KeyNotFoundException($"vault {name} not found");
    }

    internal static Item.Response ConvertToItemResponse(FullItem result)
    {
        return new Item.Response()
        {
            Vault = new Item.VaultResponse()
            {
                Id = result.Vault.Id,
                Name = result.Vault.AdditionalProperties.GetValue<string>("name") ?? result.Vault.Id,
            },
            Id = result.Id,
            Title = result.Title,
            Version = result.Version,
            Category = result.Category.ToString(),
            LastEditedBy = result.LastEditedBy,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
            Fields = result.Fields?.Select(x => new Item.Field()
            {
                Id = x.Id,
                Label = x.Label,
                Type = x.Type.ToString(),
                Purpose = x.Purpose.ToString(),
                Section = x.Section is not null
                    ? new Item.Section()
                    {
                        Id = x.Section.Id,
                        Label = x.Section.AdditionalProperties.GetValue<string>("label"),
                    }
                    : null,
                Value = x.Value,
            }).ToImmutableArray() ?? ImmutableArray<Item.Field>.Empty,
            Tags = result.Tags?.ToImmutableArray() ?? ImmutableArray<string>.Empty,
            Urls = result.Urls?.Select(x => new Item.Url()
            {
                Label = x.Label,
                Primary = x.Primary,
                Href = x.Href,
            }).ToImmutableArray() ?? ImmutableArray<Item.Url>.Empty,
            Sections = result.Sections?.Select(x => new Item.Section()
            {
                Id = x.Id,
                Label = x.Label,
            }).ToImmutableArray()?? ImmutableArray<Item.Section>.Empty,
            Files = result.Files?.Select(x => new Item.File()
            {
                Id = x.Id,
                Name = x.Name,
                Size = x.Size,
                Section = x.Section is not null
                    ? new Item.Section()
                    {
                        Id = x.Section.Id,
                        Label = x.Section.AdditionalProperties.GetValue<string>("label"),
                    }
                    : null,
            }).ToImmutableArray() ?? ImmutableArray<Item.File>.Empty,
        };
    }
}
