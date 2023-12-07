// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetPassword
    {
        public static Task<GetPasswordResult> InvokeAsync(GetPasswordArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetPasswordResult>("one-password-native-unofficial:index:GetPassword", args ?? new GetPasswordArgs(), options.WithDefaults());

        public static Output<GetPasswordResult> Invoke(GetPasswordInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetPasswordResult>("one-password-native-unofficial:index:GetPassword", args ?? new GetPasswordInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetPasswordArgs : Pulumi.InvokeArgs
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

        public GetPasswordArgs()
        {
        }
    }

    public sealed class GetPasswordInvokeArgs : Pulumi.InvokeArgs
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

        public GetPasswordInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetPasswordResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly string? Notes;
        public readonly string? Password;
        public readonly ImmutableArray<Outputs.OutputReference> References;
        public readonly ImmutableDictionary<string, Outputs.OutputSection> Sections;
        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public readonly ImmutableArray<string> Tags;
        /// <summary>
        /// The title of the item.
        /// </summary>
        public readonly string Title;
        public readonly ImmutableArray<Outputs.OutputUrl> Urls;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Uuid;
        public readonly ImmutableDictionary<string, string> Vault;

        [OutputConstructor]
        private GetPasswordResult(
            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string? notes,

            string? password,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            string uuid,

            ImmutableDictionary<string, string> vault)
        {
            Attachments = attachments;
            Category = category;
            Fields = fields;
            Notes = notes;
            Password = password;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Urls = urls;
            Uuid = uuid;
            Vault = vault;
        }
    }
}