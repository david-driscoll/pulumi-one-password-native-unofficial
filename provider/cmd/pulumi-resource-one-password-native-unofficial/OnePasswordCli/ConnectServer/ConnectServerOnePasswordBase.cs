using System.Collections.Immutable;
using GeneratedCode;
using Serilog;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordBase(
    OnePasswordOptions options,
    ILogger logger)
{
    private protected readonly ILogger Logger = logger;
    private ImmutableDictionary<string, string> _vaultIds = ImmutableDictionary<string, string>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);

    private readonly Lazy<I1PasswordConnect> _connect = new(() => Helpers.CreateConnectClient(
        // ReSharper disable once NullableWarningSuppressionIsUsed
        options.ConnectHost!,
        // ReSharper disable once NullableWarningSuppressionIsUsed
        options.ConnectToken!
    )); 

    internal I1PasswordConnect Connect => _connect.Value;

    protected async Task<string> GetVaultUuid(string? name)
    {
        if (name is null)
        {
            throw new KeyNotFoundException("vault name is null");
        }
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

    internal static FullItem ConvertToItemRequest(string vaultId, ItemRequestBase request, TemplateMetadata.Template templateJson)
    {
        var (fields, _, sections) = templateJson.GetFieldsAndAttachments();
        return new FullItem()
        {
            Id = request is Item.EditRequest { Id: not null } editRequest
                ? editRequest.Id
                : null,
            Category = Enum.TryParse<ItemCategory>(request.Category, true, out var category)
                ? category
                : ItemCategory.SECURE_NOTE,
            Vault = new Vault2() { Id = vaultId },
            Title = request.Title,
            Urls = request.Urls.Select(x => new Urls()
            {
                Label = x.Label,
                Href = x.Href,
                Primary = x.Primary,
            }).ToList(),
            Tags = request.Tags,
            Fields = fields.Select(z => new Field()
            {
                Id = z.Id,
                Type = Enum.TryParse<FieldType>(z.Type, true, out var type)
                    ? type
                    : FieldType.STRING,
                Value = z.Value,
                Purpose = Enum.TryParse<FieldPurpose>(z.Purpose, true, out var purpose)
                    ? purpose
                    : FieldPurpose.Empty,
                Section = z.Section is not null
                    ? new()
                    {
                        Id = z.Section.Id,
                        AdditionalProperties = new Dictionary<string, object>() { { "label", z.Section.Label } }
                    }
                    : null,
                Label = z.Label,
                // Entropy = ,
                Generate = purpose == FieldPurpose.PASSWORD && request.GeneratePassword is not null,
                Recipe = purpose == FieldPurpose.PASSWORD && request is { GeneratePassword: { Length: > 0 } or { CharacterSets.Length: > 0 } }
                    ? new GeneratorRecipe()
                    {
                        Length = request.GeneratePassword?.Length ?? 32,
                        CharacterSets = request.GeneratePassword?.CharacterSets
                    }
                    : null,
            }).ToList(),
            Sections = sections.Select(z => new Sections()
            {
                Id = z.Id,
                AdditionalProperties = new Dictionary<string, object>() { { "label", z.Label } }
            }).ToList(),
        };
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
            Fields = result.Fields?.Select(CreateField).ToImmutableArray() ?? ImmutableArray<Item.Field>.Empty,
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
            }).ToImmutableArray() ?? ImmutableArray<Item.Section>.Empty,
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

        Item.Field CreateField(Field x)
        {
            var purpose = x.Purpose == FieldPurpose.Empty ? null : x.Purpose.ToString();
            var type = x.Type.ToString();
            return new Item.Field()
            {
                Id = x.Id is { Length: 26 } ? x.Label ?? x.Id : x.Id,
                Label = x.Label,
                Type = type,
                Purpose = purpose,
                Section = x.Section is not null
                    ? new Item.Section()
                    {
                        Id = x.Section.Id,
                        Label = x.Section.AdditionalProperties.GetValue<string>("label"),
                    }
                    : null,
                Value = x.Value,
            };
        }
    }
}
