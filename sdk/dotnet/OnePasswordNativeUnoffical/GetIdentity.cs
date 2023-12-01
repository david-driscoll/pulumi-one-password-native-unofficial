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
        public readonly ImmutableDictionary<string, Outputs.OutField> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutField> Fields;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.IdentificationSection? Identification;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.InternetDetailsSection? InternetDetails;
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
        private GetIdentityResult(
            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.AddressSection? address,

            ImmutableDictionary<string, Outputs.OutField> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutField> fields,

            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.IdentificationSection? identification,

            Rocket.Surgery.OnePasswordNativeUnoffical.Identity.Outputs.InternetDetailsSection? internetDetails,

            string? notes,

            ImmutableDictionary<string, Outputs.OutField> references,

            ImmutableDictionary<string, Outputs.OutSection> sections,

            ImmutableArray<string> tags,

            string title,

            string uuid,

            string vault)
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
            Uuid = uuid;
            Vault = vault;
        }
    }
}