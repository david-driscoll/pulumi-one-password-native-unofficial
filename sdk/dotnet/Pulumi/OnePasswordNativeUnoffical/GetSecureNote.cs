// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.Pulumi.OnePasswordNativeUnoffical
{
    public static class GetSecureNote
    {
        public static Task<GetSecureNoteResult> InvokeAsync(GetSecureNoteArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetSecureNoteResult>("one-password-native-unoffical:index:GetSecureNote", args ?? new GetSecureNoteArgs(), options.WithDefaults());

        public static Output<GetSecureNoteResult> Invoke(GetSecureNoteInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetSecureNoteResult>("one-password-native-unoffical:index:GetSecureNote", args ?? new GetSecureNoteInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetSecureNoteArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("uuid")]
        public string? Uuid { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault", required: true)]
        public string Vault { get; set; } = null!;

        public GetSecureNoteArgs()
        {
        }
    }

    public sealed class GetSecureNoteInvokeArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public Input<string>? Title { get; set; }

        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("uuid")]
        public Input<string>? Uuid { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault", required: true)]
        public Input<string> Vault { get; set; } = null!;

        public GetSecureNoteInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetSecureNoteResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutField> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutField> Fields;
        public readonly string? Notes;
        public readonly ImmutableDictionary<string, Outputs.OutField> References;
        public readonly ImmutableDictionary<string, Outputs.OutSection> Sections;
        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public readonly ImmutableArray<string> Tags;
        /// <summary>
        /// The title of the item.
        /// </summary>
        public readonly string Title;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Uuid;
        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        public readonly string Vault;

        [OutputConstructor]
        private GetSecureNoteResult(
            ImmutableDictionary<string, Outputs.OutField> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutField> fields,

            string? notes,

            ImmutableDictionary<string, Outputs.OutField> references,

            ImmutableDictionary<string, Outputs.OutSection> sections,

            ImmutableArray<string> tags,

            string title,

            string uuid,

            string vault)
        {
            Attachments = attachments;
            Category = category;
            Fields = fields;
            Notes = notes;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Uuid = uuid;
            Vault = vault;
        }
    }
}