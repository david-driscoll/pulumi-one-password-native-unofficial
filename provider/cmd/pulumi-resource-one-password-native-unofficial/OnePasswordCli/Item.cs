using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public static class Item
{
    public record CreateRequest : ItemRequestBase
    {
        public bool DryRun { get; init; }
        public bool Favorite { get; init; }
    }

    public record EditRequest : ItemRequestBase
    {
        public required string Id { get; init; }
        public bool DryRun { get; init; }
        public bool Favorite { get; init; }
    }

    public record DeleteRequest(string Id)
    {
        public string? Vault { get; init; }
        public bool Archive { get; init; }
    }

    public record Response
    {
        public string Id { get; init; } = "";
        public string Title { get; init; } = "";
        public int? Version { get; init; }
        public required VaultResponse Vault { get; init; }
        public string Category { get; init; } = "";
        public string LastEditedBy { get; init; } = "";
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }
        // public string AdditionalInformation { get; init; } = "";

        public ImmutableArray<Section> Sections { get; init; } = ImmutableArray<Section>.Empty;
        public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
        public ImmutableArray<Field> Fields { get; init; } = ImmutableArray<Field>.Empty;
        public ImmutableArray<File> Files { get; init; } = ImmutableArray<File>.Empty;
        public ImmutableArray<Url> Urls { get; init; } = ImmutableArray<Url>.Empty;
    }

    public record Section
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        public string Id { get; init; } = null!;
        public string? Label { get; init; }
    }

    public record Field
    {
        public string? Id { get; init; }
        public string? Label { get; init; }
        public required string Type { get; init; }
        public string? Purpose { get; init; }
        public Section? Section { get; init; }
        public string? Value { get; init; }
        [JsonExtensionData] public Dictionary<string, JsonElement>? ExtensionData { get; init; }
    }

    public record File
    {
        public string Id { get; init; } = "";
        public string Name { get; init; } = "";
        public int Size { get; init; }
        public string ContentPath { get; init; } = "";
        public Section? Section { get; init; }
    }

    public record Url
    {
        public string? Label { get; init; }
        public bool Primary { get; init; }
        public string Href { get; init; } = "";
    }


    public record VaultResponse
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        public required string Id { get; init; } = null!;

        // ReSharper disable once NullableWarningSuppressionIsUsed
        public string Name { get; init; } = null!;
    }

    public record GetRequest
    {
        public required string Id { get; init; }
        public string? Vault { get; init; }
    }
}
