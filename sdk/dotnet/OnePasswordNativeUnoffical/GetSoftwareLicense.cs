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
    public static class GetSoftwareLicense
    {
        public static Task<GetSoftwareLicenseResult> InvokeAsync(GetSoftwareLicenseArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetSoftwareLicenseResult>("one-password-native-unoffical:index:GetSoftwareLicense", args ?? new GetSoftwareLicenseArgs(), options.WithDefaults());

        public static Output<GetSoftwareLicenseResult> Invoke(GetSoftwareLicenseInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetSoftwareLicenseResult>("one-password-native-unoffical:index:GetSoftwareLicense", args ?? new GetSoftwareLicenseInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetSoftwareLicenseArgs : Pulumi.InvokeArgs
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

        public GetSoftwareLicenseArgs()
        {
        }
    }

    public sealed class GetSoftwareLicenseInvokeArgs : Pulumi.InvokeArgs
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

        public GetSoftwareLicenseInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetSoftwareLicenseResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.CustomerSection? Customer;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly string? LicenseKey;
        public readonly string? Notes;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.OrderSection? Order;
        public readonly Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.PublisherSection? Publisher;
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
        public readonly string? Version;

        [OutputConstructor]
        private GetSoftwareLicenseResult(
            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.CustomerSection? customer,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string? licenseKey,

            string? notes,

            Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.OrderSection? order,

            Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Outputs.PublisherSection? publisher,

            ImmutableDictionary<string, Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            string uuid,

            ImmutableDictionary<string, string> vault,

            string? version)
        {
            Attachments = attachments;
            Category = category;
            Customer = customer;
            Fields = fields;
            LicenseKey = licenseKey;
            Notes = notes;
            Order = order;
            Publisher = publisher;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Urls = urls;
            Uuid = uuid;
            Vault = vault;
            Version = version;
        }
    }
}
