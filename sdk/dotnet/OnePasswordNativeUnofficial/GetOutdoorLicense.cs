// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetOutdoorLicense
    {
        public static Task<GetOutdoorLicenseResult> InvokeAsync(GetOutdoorLicenseArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetOutdoorLicenseResult>("one-password-native-unofficial:index:GetOutdoorLicense", args ?? new GetOutdoorLicenseArgs(), options.WithDefaults());

        public static Output<GetOutdoorLicenseResult> Invoke(GetOutdoorLicenseInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetOutdoorLicenseResult>("one-password-native-unofficial:index:GetOutdoorLicense", args ?? new GetOutdoorLicenseInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetOutdoorLicenseArgs : Pulumi.InvokeArgs
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

        public GetOutdoorLicenseArgs()
        {
        }
    }

    public sealed class GetOutdoorLicenseInvokeArgs : Pulumi.InvokeArgs
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

        public GetOutdoorLicenseInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetOutdoorLicenseResult
    {
        public readonly string? ApprovedWildlife;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly string? Country;
        public readonly string? Expires;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly string? FullName;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly string? MaximumQuota;
        public readonly string? Notes;
        public readonly ImmutableArray<Outputs.OutputReference> References;
        public readonly ImmutableDictionary<string, Outputs.OutputSection> Sections;
        public readonly string? State;
        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public readonly ImmutableArray<string> Tags;
        /// <summary>
        /// The title of the item.
        /// </summary>
        public readonly string Title;
        public readonly ImmutableArray<Outputs.OutputUrl> Urls;
        public readonly string? ValidFrom;
        public readonly ImmutableDictionary<string, string> Vault;

        [OutputConstructor]
        private GetOutdoorLicenseResult(
            string? approvedWildlife,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            string? country,

            string? expires,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string? fullName,

            string id,

            string? maximumQuota,

            string? notes,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            string? state,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            string? validFrom,

            ImmutableDictionary<string, string> vault)
        {
            ApprovedWildlife = approvedWildlife;
            Attachments = attachments;
            Category = category;
            Country = country;
            Expires = expires;
            Fields = fields;
            FullName = fullName;
            Id = id;
            MaximumQuota = maximumQuota;
            Notes = notes;
            References = references;
            Sections = sections;
            State = state;
            Tags = tags;
            Title = title;
            Urls = urls;
            ValidFrom = validFrom;
            Vault = vault;
        }
    }
}
