// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;

namespace Pulumi.Onepassword
{
    [OnepasswordResourceType("onepassword:index:WirelessRouterItem")]
    public partial class WirelessRouterItem : Pulumi.CustomResource
    {
        [Output("airPortId")]
        public Output<string?> AirPortId { get; private set; } = null!;

        [Output("attachedStoragePassword")]
        public Output<string?> AttachedStoragePassword { get; private set; } = null!;

        [Output("baseStationName")]
        public Output<string?> BaseStationName { get; private set; } = null!;

        [Output("baseStationPassword")]
        public Output<string?> BaseStationPassword { get; private set; } = null!;

        [Output("category")]
        public Output<string> Category { get; private set; } = null!;

        [Output("fields")]
        public Output<ImmutableDictionary<string, Outputs.GetField>?> Fields { get; private set; } = null!;

        [Output("networkName")]
        public Output<string?> NetworkName { get; private set; } = null!;

        [Output("notes")]
        public Output<string?> Notes { get; private set; } = null!;

        [Output("sections")]
        public Output<ImmutableDictionary<string, Outputs.GetSection>?> Sections { get; private set; } = null!;

        [Output("serverIpAddress")]
        public Output<string?> ServerIpAddress { get; private set; } = null!;

        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        [Output("tags")]
        public Output<ImmutableArray<string>> Tags { get; private set; } = null!;

        /// <summary>
        /// The title of the item.
        /// </summary>
        [Output("title")]
        public Output<string> Title { get; private set; } = null!;

        /// <summary>
        /// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
        /// </summary>
        [Output("uuid")]
        public Output<string> Uuid { get; private set; } = null!;

        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Output("vault")]
        public Output<string> Vault { get; private set; } = null!;

        [Output("wirelessNetworkPassword")]
        public Output<string?> WirelessNetworkPassword { get; private set; } = null!;

        [Output("wirelessSecurity")]
        public Output<string?> WirelessSecurity { get; private set; } = null!;


        /// <summary>
        /// Create a WirelessRouterItem resource with the given unique name, arguments, and options.
        /// </summary>
        ///
        /// <param name="name">The unique name of the resource</param>
        /// <param name="args">The arguments used to populate this resource's properties</param>
        /// <param name="options">A bag of options that control this resource's behavior</param>
        public WirelessRouterItem(string name, WirelessRouterItemArgs args, CustomResourceOptions? options = null)
            : base("onepassword:index:WirelessRouterItem", name, MakeArgs(args), MakeResourceOptions(options, ""))
        {
        }

        private WirelessRouterItem(string name, Input<string> id, WirelessRouterItemState? state = null, CustomResourceOptions? options = null)
            : base("onepassword:index:WirelessRouterItem", name, state, MakeResourceOptions(options, id))
        {
        }

        private static WirelessRouterItemArgs MakeArgs(WirelessRouterItemArgs args)
        {
            args ??= new WirelessRouterItemArgs();
            args.Category = "Wireless Router";
            return args;
        }

        private static CustomResourceOptions MakeResourceOptions(CustomResourceOptions? options, Input<string>? id)
        {
            var defaultOptions = new CustomResourceOptions
            {
                Version = Utilities.Version,
            };
            var merged = CustomResourceOptions.Merge(defaultOptions, options);
            // Override the ID if one was specified for consistency with other language SDKs.
            merged.Id = id ?? merged.Id;
            return merged;
        }
        /// <summary>
        /// Get an existing WirelessRouterItem resource's state with the given name, ID, and optional extra
        /// properties used to qualify the lookup.
        /// </summary>
        ///
        /// <param name="name">The unique name of the resulting resource.</param>
        /// <param name="id">The unique provider ID of the resource to lookup.</param>
        /// <param name="state">Any extra arguments used during the lookup.</param>
        /// <param name="options">A bag of options that control this resource's behavior</param>
        public static WirelessRouterItem Get(string name, Input<string> id, WirelessRouterItemState? state = null, CustomResourceOptions? options = null)
        {
            return new WirelessRouterItem(name, id, state, options);
        }
    }

    public sealed class WirelessRouterItemArgs : Pulumi.ResourceArgs
    {
        [Input("airPortId")]
        public Input<string>? AirPortId { get; set; }

        [Input("attachedStoragePassword")]
        public Input<string>? AttachedStoragePassword { get; set; }

        [Input("baseStationName")]
        public Input<string>? BaseStationName { get; set; }

        [Input("baseStationPassword")]
        public Input<string>? BaseStationPassword { get; set; }

        /// <summary>
        /// The category of the vault the item is in.
        /// </summary>
        [Input("category")]
        public Input<string>? Category { get; set; }

        [Input("fields")]
        private InputMap<Inputs.FieldArgs>? _fields;
        public InputMap<Inputs.FieldArgs> Fields
        {
            get => _fields ?? (_fields = new InputMap<Inputs.FieldArgs>());
            set => _fields = value;
        }

        [Input("networkName")]
        public Input<string>? NetworkName { get; set; }

        [Input("notes")]
        public Input<string>? Notes { get; set; }

        [Input("sections")]
        private InputMap<Inputs.SectionArgs>? _sections;
        public InputMap<Inputs.SectionArgs> Sections
        {
            get => _sections ?? (_sections = new InputMap<Inputs.SectionArgs>());
            set => _sections = value;
        }

        [Input("serverIpAddress")]
        public Input<string>? ServerIpAddress { get; set; }

        [Input("tags")]
        private InputList<string>? _tags;

        /// <summary>
        /// An array of strings of the tags assigned to the item.
        /// </summary>
        public InputList<string> Tags
        {
            get => _tags ?? (_tags = new InputList<string>());
            set => _tags = value;
        }

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

        [Input("wirelessNetworkPassword")]
        public Input<string>? WirelessNetworkPassword { get; set; }

        [Input("wirelessSecurity")]
        public Input<string>? WirelessSecurity { get; set; }

        public WirelessRouterItemArgs()
        {
        }
    }

    public sealed class WirelessRouterItemState : Pulumi.ResourceArgs
    {
        /// <summary>
        /// The UUID of the vault the item is in.
        /// </summary>
        [Input("vault", required: true)]
        public Input<string> Vault { get; set; } = null!;

        public WirelessRouterItemState()
        {
        }
    }
}
