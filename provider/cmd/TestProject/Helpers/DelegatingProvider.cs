using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

public class DelegatingProvider(OnePasswordProvider provider) : Provider
{
    private readonly HashSet<string> _createdItems = new(StringComparer.OrdinalIgnoreCase);

    public IEnumerable<string> CreatedIds => _createdItems;

    public override async Task<CreateResponse> Create(CreateRequest request, CancellationToken ct)
    {
        var result = await provider.Create(request, ct);
        _createdItems.Add(result.Id!);
        return result;
    }

    public override Task<CheckResponse> Check(CheckRequest request, CancellationToken ct)
    {
        return provider.Check(request, ct);
    }

    public override Task<ConfigureResponse> Configure(ConfigureRequest request, CancellationToken ct)
    {
        return provider.Configure(request, ct);
    }

    public override Task Delete(DeleteRequest request, CancellationToken ct)
    {
        return provider.Delete(request, ct);
    }

    public override Task<DiffResponse> Diff(DiffRequest request, CancellationToken ct)
    {
        return provider.Diff(request, ct);
    }

    public override Task<ReadResponse> Read(ReadRequest request, CancellationToken ct)
    {
        return provider.Read(request, ct);
    }

    public override Task<UpdateResponse> Update(UpdateRequest request, CancellationToken ct)
    {
        return provider.Update(request, ct);
    }

    public override Task<InvokeResponse> Invoke(InvokeRequest request, CancellationToken ct)
    {
        return provider.Invoke(request, ct);
    }

    public override Task<CheckResponse> CheckConfig(CheckRequest request, CancellationToken ct)
    {
        return provider.CheckConfig(request, ct);
    }

    public override Task<DiffResponse> DiffConfig(DiffRequest request, CancellationToken ct)
    {
        return provider.DiffConfig(request, ct);
    }

    public override Task<GetSchemaResponse> GetSchema(GetSchemaRequest request, CancellationToken ct)
    {
        return provider.GetSchema(request, ct);
    }

    public override int GetHashCode()
    {
        return provider.GetHashCode();
    }

    public override string ToString()
    {
        return provider.ToString()!;
    }

    public override bool Equals(object? obj)
    {
        return provider.Equals(obj);
    }
}
