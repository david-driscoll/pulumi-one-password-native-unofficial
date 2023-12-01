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
    public static class GetOutdoorLicense
    {
        public static Task<GetOutdoorLicenseResult> InvokeAsync(GetOutdoorLicenseArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetOutdoorLicenseResult>("one-password-native-unoffical:index:GetOutdoorLicense", args ?? new GetOutdoorLicenseArgs(), options.WithDefaults());

        public static Output<GetOutdoorLicenseResult> Invoke(GetOutdoorLicenseInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetOutdoorLicenseResult>("one-password-native-unoffical:index:GetOutdoorLicense", args ?? new GetOutdoorLicenseInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetOutdoorLicenseArgs : Pulumi.InvokeArgs
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

        public GetOutdoorLicenseArgs()
        {
        }
    }

    public sealed class GetOutdoorLicenseInvokeArgs : Pulumi.InvokeArgs
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

        public GetOutdoorLicenseInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetOutdoorLicenseResult
    {
        public readonly string? ApprovedWildlife;
        public readonly ImmutableDictionary<string, Outputs.OutField> Attachments;
        public readonly string Category;
        public readonly string? Country;
        public readonly string? Expires;
        public readonly ImmutableDictionary<string, Outputs.OutField> Fields;
        public readonly string? FullName;
        public readonly string? MaximumQuota;
        public readonly string? Notes;
        public readonly ImmutableDictionary<string, Outputs.OutField> References;
        public readonly ImmutableDictionary<string, Outputs.OutSection> Sections;
        public readonly string? State;
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
        public readonly string? ValidFrom;
        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        public readonly string Vault;

        [OutputConstructor]
        private GetOutdoorLicenseResult(
            string? approvedWildlife,

            ImmutableDictionary<string, Outputs.OutField> attachments,

            string category,

            string? country,

            string? expires,

            ImmutableDictionary<string, Outputs.OutField> fields,

            string? fullName,

            string? maximumQuota,

            string? notes,

            ImmutableDictionary<string, Outputs.OutField> references,

            ImmutableDictionary<string, Outputs.OutSection> sections,

            string? state,

            ImmutableArray<string> tags,

            string title,

            string uuid,

            string? validFrom,

            string vault)
        {
            ApprovedWildlife = approvedWildlife;
            Attachments = attachments;
            Category = category;
            Country = country;
            Expires = expires;
            Fields = fields;
            FullName = fullName;
            MaximumQuota = maximumQuota;
            Notes = notes;
            References = references;
            Sections = sections;
            State = state;
            Tags = tags;
            Title = title;
            Uuid = uuid;
            ValidFrom = validFrom;
            Vault = vault;
        }
    }
}