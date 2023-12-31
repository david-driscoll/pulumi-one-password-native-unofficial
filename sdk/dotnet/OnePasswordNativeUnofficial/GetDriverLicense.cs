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
    public static class GetDriverLicense
    {
        public static Task<GetDriverLicenseResult> InvokeAsync(GetDriverLicenseArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetDriverLicenseResult>("one-password-native-unofficial:index:GetDriverLicense", args ?? new GetDriverLicenseArgs(), options.WithDefaults());

        public static Output<GetDriverLicenseResult> Invoke(GetDriverLicenseInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetDriverLicenseResult>("one-password-native-unofficial:index:GetDriverLicense", args ?? new GetDriverLicenseInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetDriverLicenseArgs : Pulumi.InvokeArgs
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
        [Input("vault", required: true)]
        public string Vault { get; set; } = null!;

        public GetDriverLicenseArgs()
        {
        }
    }

    public sealed class GetDriverLicenseInvokeArgs : Pulumi.InvokeArgs
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
        [Input("vault", required: true)]
        public Input<string> Vault { get; set; } = null!;

        public GetDriverLicenseInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetDriverLicenseResult
    {
        public readonly string? Address;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly string? ConditionsRestrictions;
        public readonly string? Country;
        public readonly string? DateOfBirth;
        public readonly string? ExpiryDate;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly string? FullName;
        public readonly string? Gender;
        public readonly string? Height;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly string? LicenseClass;
        public readonly string? Notes;
        public readonly string? Number;
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
        public readonly Outputs.OutputVault Vault;

        [OutputConstructor]
        private GetDriverLicenseResult(
            string? address,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            string? conditionsRestrictions,

            string? country,

            string? dateOfBirth,

            string? expiryDate,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string? fullName,

            string? gender,

            string? height,

            string id,

            string? licenseClass,

            string? notes,

            string? number,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            string? state,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            Outputs.OutputVault vault)
        {
            Address = address;
            Attachments = attachments;
            Category = category;
            ConditionsRestrictions = conditionsRestrictions;
            Country = country;
            DateOfBirth = dateOfBirth;
            ExpiryDate = expiryDate;
            Fields = fields;
            FullName = fullName;
            Gender = gender;
            Height = height;
            Id = id;
            LicenseClass = licenseClass;
            Notes = notes;
            Number = number;
            References = references;
            Sections = sections;
            State = state;
            Tags = tags;
            Title = title;
            Urls = urls;
            Vault = vault;
        }
    }
}
