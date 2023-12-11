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
    public static class GetLogin
    {
        public static Task<GetLoginResult> InvokeAsync(GetLoginArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetLoginResult>("one-password-native-unofficial:index:GetLogin", args ?? new GetLoginArgs(), options.WithDefaults());

        public static Output<GetLoginResult> Invoke(GetLoginInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetLoginResult>("one-password-native-unofficial:index:GetLogin", args ?? new GetLoginInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetLoginArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("id")]
        public string? Id { get; set; }

        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault")]
        public string Vault { get; set; } = null!;

        public GetLoginArgs()
        {
        }
    }

    public sealed class GetLoginInvokeArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Input("id")]
        public Input<string>? Id { get; set; }

        /// <summary>
        /// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
        /// </summary>
        [Input("title")]
        public Input<string>? Title { get; set; }

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault")]
        public Input<string> Vault { get; set; } = null!;

        public GetLoginInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetLoginResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
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
        public readonly string? Username;
        public readonly ImmutableDictionary<string, string> Vault;

        [OutputConstructor]
        private GetLoginResult(
            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string id,

            string? notes,

            string? password,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            string? username,

            ImmutableDictionary<string, string> vault)
        {
            Attachments = attachments;
            Category = category;
            Fields = fields;
            Id = id;
            Notes = notes;
            Password = password;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Urls = urls;
            Username = username;
            Vault = vault;
        }
    }
}
