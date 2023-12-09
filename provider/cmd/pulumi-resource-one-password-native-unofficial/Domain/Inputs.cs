// using System.Collections.Immutable;
// using System.Reflection;
// using System.Security.Cryptography;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Pulumi;
// using Standart.Hash.xxHash;
//
// namespace pulumi_resource_one_password_native_unofficial.Domain;
//
//
// public record InputField(string? Value, OnePassword.Items.FieldPurpose? Purpose, OnePassword.Items.FieldType? Type);
//
// public record InputSection(ImmutableDictionary<string, InputField> Fields, ImmutableDictionary<string, InputAttachment> Attachments);
//
// public record InputReference(string ItemId);
// public record InputUrl(string? Label, bool Primary, string Href);
//
// public record Inputs(
//     ImmutableDictionary<string, InputField> Fields,
//     ImmutableDictionary<string, InputAttachment> Attachments,
//     ImmutableDictionary<string, InputSection> Sections,
//     ImmutableArray<InputReference> References,
//     ImmutableArray<InputUrl> Urls,
//     ImmutableArray<string> Tags,
//     string Title,
//     string Category,
//     string Vault,
//     string Notes
// );
//
// public record InputAttachment(string Hash);
//

using System.Collections.Immutable;
using System.Reflection;
using Pulumi;
using Pulumi.Experimental.Provider;
using Standart.Hash.xxHash;

public static class AssetOrArchiveExtensions
{
    private static PropertyInfo GetValueMethod = typeof(AssetOrArchive).GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public static string? HashAssetOrArchive(PropertyValue propertyValue) => HashAssetOrArchive(GetAssetOrArchive(propertyValue));

    public static AssetOrArchive? GetAssetOrArchive(PropertyValue assetOrArchive) =>
        assetOrArchive.TryGetAsset(out var asset) ? asset : assetOrArchive.TryGetArchive(out var archive) ? archive : null;

    public static string GetAssetValue(AssetOrArchive assetOrArchive)
    {
        if (assetOrArchive is FileAsset or FileAsset or StringAsset)
        {
            return (string)GetValueMethod.GetValue(assetOrArchive)!;
        }

        throw new("Asset not supported!");
    }

    public static string HashAssetOrArchive(AssetOrArchive? assetOrArchive)
    {
        if (assetOrArchive is FileAsset or RemoteAsset or StringAsset or FileArchive or RemoteArchive)
        {
            var value = (string)GetValueMethod.GetValue(assetOrArchive)!;
            return xxHash128.ComputeHash(value).ToGuid().ToString("N");
        }

        if (assetOrArchive is AssetArchive assetArchive)
        {
            var value = (ImmutableDictionary<string, AssetOrArchive>)GetValueMethod.GetValue(assetOrArchive)!;
            var hashes = value.OrderBy(z => z.Key).Select(z => HashAssetOrArchive(z.Value)).ToImmutableArray();
            return xxHash128.ComputeHash(string.Join(":", hashes)).ToGuid().ToString("N");
        }

        throw new Exception("Unknown asset or archive type");
    }
}
