// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.Outputs
{

    [OutputType]
    public sealed class OutputReference
    {
        public readonly string Id;
        public readonly string ItemId;
        public readonly string Label;
        public readonly string Reference;

        [OutputConstructor]
        private OutputReference(
            string id,

            string itemId,

            string label,

            string reference)
        {
            Id = id;
            ItemId = itemId;
            Label = label;
            Reference = reference;
        }
    }
}
