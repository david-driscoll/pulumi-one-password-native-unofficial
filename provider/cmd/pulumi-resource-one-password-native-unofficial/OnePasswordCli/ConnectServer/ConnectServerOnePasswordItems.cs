using System.Collections.Immutable;
using System.Security.Cryptography;
using GeneratedCode;
using Serilog;
using File = GeneratedCode.File;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordItems(OnePasswordOptions options, ILogger logger)
    : ConnectServerOnePasswordBase(options, logger), IOnePasswordItems
{
    private readonly Lazy<IOnePasswordItemTemplates> _templates = new(() => new ConnectServerOnePasswordItemTemplates(options, logger));

    public async Task<Item.Response> Create(Item.CreateRequest request, TemplateMetadata.Template templateJson, CancellationToken cancellationToken = default)
    {
        var (fields, attachments, sections) = templateJson.GetFieldsAndAttachments();
        if (attachments.Any())
        {
            throw new NotSupportedException("Attachments are not supported when using the Connect Server API");
        }

        try
        {
            var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
            var result = await Connect.CreateVaultItem(vaultId, new FullItem()
            {
                Category = Enum.TryParse<ItemCategory>(request.Category, true, out var category)
                    ? category
                    : throw new ArgumentException($"Could not find category {request.Category}"),
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
                        // Generate = ,
                        Generate = purpose == FieldPurpose.PASSWORD && request.GeneratePassword is not null,
                        Recipe = purpose == FieldPurpose.PASSWORD && request is { GeneratePassword: { Length: > 0 } or { CharacterSets.Length: > 0 } }
                            ? new GeneratorRecipe()
                            {
                                Length = request.GeneratePassword?.Length ?? 32,
                                CharacterSets = request.GeneratePassword?.CharacterSets
                            }
                            : null,
                    })
                    .ToList(),
                Sections = sections.Select(z => new Sections()
                {
                    Id = z.Id,
                    AdditionalProperties = new Dictionary<string, object>() { { "label", z.Label } }
                }).ToList(),
            });

            return ConvertToItemResponse(result);
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error creating item");
            throw;
        }
    }

    public async Task<Item.Response> Edit(Item.EditRequest request, TemplateMetadata.Template templateJson, CancellationToken cancellationToken = default)
    {
        var (fields, attachments, sections) = templateJson.GetFieldsAndAttachments();
        if (attachments.Any())
        {
            throw new NotSupportedException("Attachments are not supported when using the Connect Server API");
        }

        try
        {
            var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
            var result = await Connect.UpdateVaultItem(vaultId, request.Id, new FullItem()
            {
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
                    // Generate = ,
                    // Recipe = ,
                }).ToList(),
                Sections = sections.Select(z => new Sections()
                {
                    Id = z.Id,
                    AdditionalProperties = new Dictionary<string, object>() { { "label", z.Label } }
                }).ToList(),
            });

            return ConvertToItemResponse(result);
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error editing item");
            throw;
        }
    }

    public async Task<Item.Response> Get(Item.GetRequest request, CancellationToken cancellationToken = default)
    {
        var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
        var result = await Connect.GetVaultItemById(vaultId, request.Id);
        return ConvertToItemResponse(result!);
    }

    public async Task Delete(Item.DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
        await Connect.DeleteVaultItem(vaultId, request.Id);
    }

    public IOnePasswordItemTemplates Templates => _templates.Value;
}
