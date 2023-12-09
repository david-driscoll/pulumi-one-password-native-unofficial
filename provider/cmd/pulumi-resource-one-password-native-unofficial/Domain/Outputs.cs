// using System.Collections.Immutable;
// using System.Reflection;
// using System.Security.Cryptography;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Pulumi;
// using Pulumi.Experimental.Provider;
// using Standart.Hash.xxHash;
// using static pulumi_resource_one_password_native_unofficial.TemplateMetadata;
//
// namespace pulumi_resource_one_password_native_unofficial.Domain;
//
// public record OutputAttachment(string Uuid, string Name, int Size, string Reference, string Hash);
//
// public record OutputReference(string ItemId, string Uuid, string Label, string Reference);
//
//
// public record OutputField
// (
//     string? Value,
//     ServiceAccountOnePassword.Items.FieldPurpose? Purpose,
//     ServiceAccountOnePassword.Items.FieldType? Type,
//     string Uuid,
//     string Label,
//     string Reference
// );
//
// public record OutputSection(ImmutableDictionary<string, OutputField> Fields, ImmutableDictionary<string, OutputAttachment> Attachments, string Uuid, string Label);
//
// public record OutputUrl(string Label, bool Primary, string Href);
//
// public record Outputs
// (
//     ImmutableDictionary<string, OutputAttachment> Attachments,
//     ImmutableDictionary<string, OutputField> Fields,
//     ImmutableDictionary<string, OutputSection> Sections,
//     ImmutableArray<OutputReference> References,
//     ImmutableArray<OutputUrl> Urls,
//     string Category,
//     string Uuid,
//     string Title,
//     string Notes,
//     OutputVault Vault,
//     ImmutableArray<string> Tags
// );
//
// public record OutputVault(string Uuid, string Name);
