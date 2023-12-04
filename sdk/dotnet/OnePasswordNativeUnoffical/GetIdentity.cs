// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnoffical
{
    public static class GetIdentity
    {
        public static Task<GetIdentityResult> InvokeAsync(GetIdentityArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetIdentityResult>("one-password-native-unoffical:index:GetIdentity", args ?? new GetIdentityArgs(), options.WithDefaults());

        public static Output<GetIdentityResult> Invoke(GetIdentityInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetIdentityResult>("one-password-native-unoffical:index:GetIdentity", args ?? new GetIdentityInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetIdentityArgs : Pulumi.InvokeArgs
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

        public GetIdentityArgs()
        {
        }
    }

    public sealed class GetIdentityInvokeArgs : Pulumi.InvokeArgs
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

        public GetIdentityInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetIdentityResult
    {
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.AddressSection? Address;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.IdentificationSection? Identification;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.InternetDetailsSection? InternetDetails;
        public readonly string? Notes;
        public readonly ImmutableDictionary<string, Outputs.OutputReference> References;
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
        private GetIdentityResult(
            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.AddressSection? address,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.IdentificationSection? identification,

            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.InternetDetailsSection? internetDetails,

            string? notes,

            ImmutableDictionary<string, Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            string uuid,

            ImmutableDictionary<string, string> vault)
        {
            Address = address;
            Attachments = attachments;
            Category = category;
            Fields = fields;
            Identification = identification;
            InternetDetails = internetDetails;
            Notes = notes;
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
