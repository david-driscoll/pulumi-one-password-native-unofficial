using System.Net;
using GeneratedCode;
using pulumi_resource_one_password_native_unofficial.Domain;
using Refit;
using Serilog;
using File = GeneratedCode.File;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordItems(OnePasswordOptions options, ILogger logger)
    : ConnectServerOnePasswordBase(options, logger), IOnePasswordItems
{
    private readonly Lazy<IOnePasswordItemTemplates> _templates = new(() => new ConnectServerOnePasswordItemTemplates(options, logger));

    public async Task<Item.Response> Create(Item.CreateRequest request, Template templateJson, CancellationToken cancellationToken = default)
    {
        var (_, attachments, _, _) = templateJson.PrepareFieldsAndAttachments();
        if (attachments.Any())
        {
            throw new NotSupportedException("Attachments are not supported when using the Connect Server API");
        }

        try
        {
            var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
            var result = await Connect.CreateVaultItem(vaultId, ConvertToItemRequest(vaultId, request, templateJson));

            result.Vault = new Vault2()
            {
                Id = vaultId,
                // ReSharper disable once NullableWarningSuppressionIsUsed
                AdditionalProperties = new Dictionary<string, object>() { { "name", request.Vault ?? options.Vault! } }
            };

            return ConvertToItemResponse(result);
        }
        catch (ApiException e)
        {
            Logger.Error(e, "Error editing item {Content}", e.Content);
            throw;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error creating item");
            throw;
        }
    }

    public async Task<Item.Response> Edit(Item.EditRequest request, Template templateJson, CancellationToken cancellationToken = default)
    {
        var (_, attachments, _, _) = templateJson.PrepareFieldsAndAttachments();
        if (attachments.Any())
        {
            throw new NotSupportedException("Attachments are not supported when using the Connect Server API");
        }

        try
        {
            var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
            // var requestInput = ConvertToItemRequest(vaultId, request, templateJson);
            var existingItem = await Connect.GetVaultItemById(vaultId, request.Id);
            existingItem.Sections ??= new List<Sections>();
            existingItem.Fields ??= new List<Field>();
            existingItem.Files ??= new List<File>();

            existingItem.Tags = request.Tags;
            existingItem.Title = request.Title;
            existingItem.Urls = request.Urls.Select(x => new Urls()
            {
                Label = x.Label,
                Href = x.Href,
                Primary = x.Primary,
            }).ToList();
            foreach (var (field, templateField) in existingItem.Fields.Join(templateJson.Fields, x => x.Id, x => x.Id, (x, y) => (x, y)))
            {
                field.Value = templateField.Value;
                field.Label = templateField.Label;
                field.Type = Enum.TryParse<FieldType>(templateField.Type, true, out var type)
                    ? type
                    : FieldType.STRING;
            }

            foreach (var (section, templateSection) in existingItem.Sections.Join(templateJson.Sections, x => x.Id, x => x.Id, (x, y) => (x, y)))
            {
                section.Label = templateSection.Label;
            }

            var result = await Connect.UpdateVaultItem(vaultId, request.Id, existingItem);

            result.Vault = new Vault2()
            {
                Id = vaultId,
                // ReSharper disable once NullableWarningSuppressionIsUsed
                AdditionalProperties = new Dictionary<string, object>() { { "name", request.Vault ?? options.Vault! } }
            };
            return ConvertToItemResponse(result);
        }
        catch (ApiException e)
        {
            Logger.Error(e, "Error editing item {Content}", e.Content);
            throw;
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

        result.Vault = new Vault2()
        {
            Id = vaultId,
            // ReSharper disable once NullableWarningSuppressionIsUsed
            AdditionalProperties = new Dictionary<string, object>() { { "name", request.Vault ?? options.Vault! } }
        };
        return ConvertToItemResponse(result);
    }

    public async Task Delete(Item.DeleteRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var vaultId = await GetVaultUuid(request.Vault ?? options.Vault);
            await Connect.DeleteVaultItem(vaultId, request.Id);
        }
        catch (ApiException e) when (e.StatusCode is HttpStatusCode.NotFound)
        {
            Logger.Warning(e, "Item {Id} not found", request.Id);
        }
        catch (ApiException e)
        {
            Logger.Error(e, "Error deleting item {Content}", e.Content);
            throw;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error deleting item");
            throw;
        }
    }

    public IOnePasswordItemTemplates Templates => _templates.Value;
}
