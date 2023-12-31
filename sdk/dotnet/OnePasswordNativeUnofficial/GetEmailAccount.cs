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
    public static class GetEmailAccount
    {
        public static Task<GetEmailAccountResult> InvokeAsync(GetEmailAccountArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetEmailAccountResult>("one-password-native-unofficial:index:GetEmailAccount", args ?? new GetEmailAccountArgs(), options.WithDefaults());

        public static Output<GetEmailAccountResult> Invoke(GetEmailAccountInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetEmailAccountResult>("one-password-native-unofficial:index:GetEmailAccount", args ?? new GetEmailAccountInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetEmailAccountArgs : Pulumi.InvokeArgs
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

        public GetEmailAccountArgs()
        {
        }
    }

    public sealed class GetEmailAccountInvokeArgs : Pulumi.InvokeArgs
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

        public GetEmailAccountInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetEmailAccountResult
    {
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string? AuthMethod;
        public readonly string Category;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.EmailAccount.Outputs.ContactInformationSection? ContactInformation;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly string? Notes;
        public readonly string? Password;
        public readonly string? PortNumber;
        public readonly ImmutableArray<Outputs.OutputReference> References;
        public readonly ImmutableDictionary<string, Outputs.OutputSection> Sections;
        public readonly string? Security;
        public readonly string? Server;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.EmailAccount.Outputs.SmtpSection? Smtp;
        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public readonly ImmutableArray<string> Tags;
        /// <summary>
        /// The title of the item.
        /// </summary>
        public readonly string Title;
        public readonly string? Type;
        public readonly ImmutableArray<Outputs.OutputUrl> Urls;
        public readonly string? Username;
        public readonly Outputs.OutputVault Vault;

        [OutputConstructor]
        private GetEmailAccountResult(
            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string? authMethod,

            string category,

            Rocket.Surgery.OnePasswordNativeUnofficial.EmailAccount.Outputs.ContactInformationSection? contactInformation,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string id,

            string? notes,

            string? password,

            string? portNumber,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            string? security,

            string? server,

            Rocket.Surgery.OnePasswordNativeUnofficial.EmailAccount.Outputs.SmtpSection? smtp,

            ImmutableArray<string> tags,

            string title,

            string? type,

            ImmutableArray<Outputs.OutputUrl> urls,

            string? username,

            Outputs.OutputVault vault)
        {
            Attachments = attachments;
            AuthMethod = authMethod;
            Category = category;
            ContactInformation = contactInformation;
            Fields = fields;
            Id = id;
            Notes = notes;
            Password = password;
            PortNumber = portNumber;
            References = references;
            Sections = sections;
            Security = security;
            Server = server;
            Smtp = smtp;
            Tags = tags;
            Title = title;
            Type = type;
            Urls = urls;
            Username = username;
            Vault = vault;
        }
    }
}
