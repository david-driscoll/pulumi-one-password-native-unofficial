using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeneratedCode;
using Pulumi;
using Pulumi.Automation;
using Pulumi.Experimental.Provider;
using TemplateMetadata = pulumi_resource_one_password_native_unofficial.Domain.TemplateMetadata;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

public static class TestExtensions
{
    public static SettingsTask AddIdScrubber(this SettingsTask verifier, string? id)
    {
        if (id is not { Length: > 0 }) return verifier;
        return verifier.AddScrubber(z => z.Replace(id, "[server-generated]"));
    }

    public static SettingsTask AddPasswordScrubber(this SettingsTask verifier, IReadOnlyDictionary<string, PropertyValue>? properties)
    {
        if (properties is not null && TemplateMetadata.GetObjectStringValue(properties.ToImmutableDictionary(), "password") is { Length: > 0 } value)
        {
            return verifier.AddScrubber(x => x.Replace(value, "[redacted,server-generated]"));
        }

        return verifier;
    }

    public static SettingsTask AddPasswordScrubber(this SettingsTask verifier, IDictionary<string, PropertyValue>? properties)
    {
        return properties is null ? verifier : AddPasswordScrubber(verifier, (IReadOnlyDictionary<string, PropertyValue>?)properties.ToImmutableDictionary());
    }

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

    public static async Task<VaultResult> WithVaultItem(this Task<UpResult> result, ConnectServerFixture serverFixture)
    {
        var r = await result;
        return new(r, await serverFixture.Connect.GetVaultItemById((string)r.Outputs["vaultId"].Value, (string)r.Outputs["id"].Value));
    }

    public record VaultResult(UpResult Result, FullItem Item);
}
