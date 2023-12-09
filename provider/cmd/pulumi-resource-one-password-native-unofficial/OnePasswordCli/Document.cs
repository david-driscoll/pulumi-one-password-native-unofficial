using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public static class Document
{
    public record CreateRequest(string FilePath)
    {
        public string? FileName { get; init; }
        public Stream? StandardInput { get; init; }
        public string? Title { get; init; }
        public ImmutableArray<string> Tags { get; init; }
        public string? Vault { get; init; }
    }

    public record CreateResponse(string Uuid, DateTimeOffset UpdatedAt, DateTimeOffset CreatedAt, string VaultUuid);
}