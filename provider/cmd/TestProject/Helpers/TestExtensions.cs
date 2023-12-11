using System.Runtime.CompilerServices;
using Pulumi;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;

namespace TestProject.Helpers;

public static class TestExtensions
{
    public static Task<T> GetValue<T>(this Output<T> output) => output.GetValue(arg => arg);

    public static TaskAwaiter<T> GetAwaiter<T>(this Output<T> output)
    {
        return output.GetValue().GetAwaiter();
    }

    public static Task<TResult> GetValue<T, TResult>(this Output<T> output, Func<T, TResult> valueResolver)
    {
        var tcs = new TaskCompletionSource<TResult>();
        output.Apply(
            arg =>
            {
                var result = valueResolver(arg);
                tcs.SetResult(result);
                return result;
            }
        );
        return tcs.Task;
    }

    public static Task<T> GetValue<T>(this Input<T> input) => input.GetValue(arg => arg);

    public static TaskAwaiter<T> GetAwaiter<T>(this Input<T> input)
    {
        return input.GetValue().GetAwaiter();
    }

    public static Task<TResult> GetValue<T, TResult>(this Input<T> input, Func<T, TResult> valueResolver)
    {
        var tcs = new TaskCompletionSource<TResult>();
        input.Apply(
            arg =>
            {
                var result = valueResolver(arg);
                tcs.SetResult(result);
                return result;
            }
        );
        return tcs.Task;
    }
}

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
        return provider.ToString();
    }

    public override bool Equals(object? obj)
    {
        return provider.Equals(obj);
    }
}
