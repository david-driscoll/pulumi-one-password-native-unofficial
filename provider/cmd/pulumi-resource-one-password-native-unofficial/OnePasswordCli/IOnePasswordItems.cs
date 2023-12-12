using pulumi_resource_one_password_native_unofficial.Domain;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public interface IOnePasswordItems
{
    Task<Item.Response> Create(Item.CreateRequest request, Template templateJson, CancellationToken cancellationToken = default);
    Task<Item.Response> Edit(Item.EditRequest request, Template templateJson,
        CancellationToken cancellationToken = default);
    Task<Item.Response> Get(Item.GetRequest request, CancellationToken cancellationToken = default);
    Task Delete(Item.DeleteRequest request, CancellationToken cancellationToken = default);
    IOnePasswordItemTemplates Templates { get; }
}
public interface IOnePasswordItemTemplates;
