// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetIdentity
    {
        public static Task<GetIdentityResult> InvokeAsync(GetIdentityArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetIdentityResult>("one-password-native-unofficial:index:GetIdentity", args ?? new GetIdentityArgs(), options.WithDefaults());

        public static Output<GetIdentityResult> Invoke(GetIdentityInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetIdentityResult>("one-password-native-unofficial:index:GetIdentity", args ?? new GetIdentityInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetIdentityArgs : Pulumi.InvokeArgs
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

        public GetIdentityArgs()
        {
        }
    }

    public sealed class GetIdentityInvokeArgs : Pulumi.InvokeArgs
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

        public GetIdentityInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetIdentityResult
    {
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.AddressSection? Address;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.IdentificationSection? Identification;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.InternetDetailsSection? InternetDetails;
        public readonly string? Notes;
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
        public readonly ImmutableDictionary<string, string> Vault;

        [OutputConstructor]
        private GetIdentityResult(
            Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.AddressSection? address,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string id,

            Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.IdentificationSection? identification,

            Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Outputs.InternetDetailsSection? internetDetails,

            string? notes,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            ImmutableDictionary<string, string> vault)
        {
            Address = address;
            Attachments = attachments;
            Category = category;
            Fields = fields;
            Id = id;
            Identification = identification;
            InternetDetails = internetDetails;
            Notes = notes;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Urls = urls;
            Vault = vault;
        }
    }
}
