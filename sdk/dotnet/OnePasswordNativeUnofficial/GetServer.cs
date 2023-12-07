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
    public static class GetServer
    {
        public static Task<GetServerResult> InvokeAsync(GetServerArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetServerResult>("one-password-native-unofficial:index:GetServer", args ?? new GetServerArgs(), options.WithDefaults());

        public static Output<GetServerResult> Invoke(GetServerInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetServerResult>("one-password-native-unofficial:index:GetServer", args ?? new GetServerInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetServerArgs : Pulumi.InvokeArgs
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

        public GetServerArgs()
        {
        }
    }

    public sealed class GetServerInvokeArgs : Pulumi.InvokeArgs
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

        public GetServerInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetServerResult
    {
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.Server.Outputs.AdminConsoleSection? AdminConsole;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        public readonly Rocket.Surgery.OnePasswordNativeUnofficial.Server.Outputs.HostingProviderSection? HostingProvider;
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
        public readonly string? Url;
        public readonly ImmutableArray<Outputs.OutputUrl> Urls;
        public readonly string? Username;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Uuid;
        public readonly ImmutableDictionary<string, string> Vault;

        [OutputConstructor]
        private GetServerResult(
            Rocket.Surgery.OnePasswordNativeUnofficial.Server.Outputs.AdminConsoleSection? adminConsole,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            Rocket.Surgery.OnePasswordNativeUnofficial.Server.Outputs.HostingProviderSection? hostingProvider,

            string? notes,

            string? password,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            ImmutableArray<string> tags,

            string title,

            string? url,

            ImmutableArray<Outputs.OutputUrl> urls,

            string? username,

            string uuid,

            ImmutableDictionary<string, string> vault)
        {
            AdminConsole = adminConsole;
            Attachments = attachments;
            Category = category;
            Fields = fields;
            HostingProvider = hostingProvider;
            Notes = notes;
            Password = password;
            References = references;
            Sections = sections;
            Tags = tags;
            Title = title;
            Url = url;
            Urls = urls;
            Username = username;
            Uuid = uuid;
            Vault = vault;
        }
    }
}