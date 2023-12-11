// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetWirelessRouter
    {
        public static Task<GetWirelessRouterResult> InvokeAsync(GetWirelessRouterArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetWirelessRouterResult>("one-password-native-unofficial:index:GetWirelessRouter", args ?? new GetWirelessRouterArgs(), options.WithDefaults());

        public static Output<GetWirelessRouterResult> Invoke(GetWirelessRouterInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetWirelessRouterResult>("one-password-native-unofficial:index:GetWirelessRouter", args ?? new GetWirelessRouterInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetWirelessRouterArgs : Pulumi.InvokeArgs
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

        public GetWirelessRouterArgs()
        {
        }
    }

    public sealed class GetWirelessRouterInvokeArgs : Pulumi.InvokeArgs
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

        public GetWirelessRouterInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetWirelessRouterResult
    {
        public readonly string? AirPortId;
        public readonly string? AttachedStoragePassword;
        public readonly ImmutableDictionary<string, Outputs.OutputAttachment> Attachments;
        public readonly string? BaseStationName;
        public readonly string? BaseStationPassword;
        public readonly string Category;
        public readonly ImmutableDictionary<string, Outputs.OutputField> Fields;
        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        public readonly string Id;
        public readonly string? NetworkName;
        public readonly string? Notes;
        public readonly ImmutableArray<Outputs.OutputReference> References;
        public readonly ImmutableDictionary<string, Outputs.OutputSection> Sections;
        public readonly string? ServerIpAddress;
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
        public readonly string? WirelessNetworkPassword;
        public readonly string? WirelessSecurity;

        [OutputConstructor]
        private GetWirelessRouterResult(
            string? airPortId,

            string? attachedStoragePassword,

            ImmutableDictionary<string, Outputs.OutputAttachment> attachments,

            string? baseStationName,

            string? baseStationPassword,

            string category,

            ImmutableDictionary<string, Outputs.OutputField> fields,

            string id,

            string? networkName,

            string? notes,

            ImmutableArray<Outputs.OutputReference> references,

            ImmutableDictionary<string, Outputs.OutputSection> sections,

            string? serverIpAddress,

            ImmutableArray<string> tags,

            string title,

            ImmutableArray<Outputs.OutputUrl> urls,

            ImmutableDictionary<string, string> vault,

            string? wirelessNetworkPassword,

            string? wirelessSecurity)
        {
            AirPortId = airPortId;
            AttachedStoragePassword = attachedStoragePassword;
            Attachments = attachments;
            BaseStationName = baseStationName;
            BaseStationPassword = baseStationPassword;
            Category = category;
            Fields = fields;
            Id = id;
            NetworkName = networkName;
            Notes = notes;
            References = references;
            Sections = sections;
            ServerIpAddress = serverIpAddress;
            Tags = tags;
            Title = title;
            Urls = urls;
            Vault = vault;
            WirelessNetworkPassword = wirelessNetworkPassword;
            WirelessSecurity = wirelessSecurity;
        }
    }
}
