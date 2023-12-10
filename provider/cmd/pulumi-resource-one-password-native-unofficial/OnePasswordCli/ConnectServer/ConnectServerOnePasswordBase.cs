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
    private ImmutableDictionary<string, string> _vaultIds = ImmutableDictionary<string, string>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);

    internal readonly I1PasswordConnect Connect = Helpers.CreateConnectClient(options.ConnectHost!, options.ConnectToken!);

    protected async Task<string> GetVaultUuid(string? name)
    {
        if (_vaultIds.TryGetValue(name, out var id))
        {
            return id;
        }

        var vaults = await Connect.GetVaults("");
        foreach (var vault in vaults)
        {
            _vaultIds = _vaultIds.Add(vault.Id, vault.Id);
            _vaultIds = _vaultIds.Add(vault.Name, vault.Id);
        }

        return _vaultIds.TryGetValue(name, out id) ? id : throw new KeyNotFoundException($"vault {name} not found");
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
