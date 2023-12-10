using System.Runtime.CompilerServices;
using Pulumi;

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