// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Inputs
{

    public sealed class AddressSectionArgs : Pulumi.ResourceArgs
    {
        [Input("address")]
        public Input<string>? Address { get; set; }

        [Input("business")]
        public Input<string>? Business { get; set; }

        [Input("cell")]
        public Input<string>? Cell { get; set; }

        [Input("defaultPhone")]
        public Input<string>? DefaultPhone { get; set; }

        [Input("home")]
        public Input<string>? Home { get; set; }

        public AddressSectionArgs()
        {
        }
    }
}
