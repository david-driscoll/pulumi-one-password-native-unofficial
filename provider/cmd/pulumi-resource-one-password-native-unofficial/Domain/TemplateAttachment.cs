using System.Text.Json.Serialization;
using Pulumi;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record TemplateAttachment : TemplateField
{
    [JsonIgnore] public required AssetOrArchive Asset { get; init; }
}