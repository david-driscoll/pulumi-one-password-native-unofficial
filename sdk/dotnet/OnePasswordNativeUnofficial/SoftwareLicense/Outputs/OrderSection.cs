// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.SoftwareLicense.Outputs
{

    [OutputType]
    public sealed class OrderSection
    {
        public readonly string? OrderNumber;
        public readonly string? OrderTotal;
        public readonly string? PurchaseDate;

        [OutputConstructor]
        private OrderSection(
            string? orderNumber,

            string? orderTotal,

            string? purchaseDate)
        {
            OrderNumber = orderNumber;
            OrderTotal = orderTotal;
            PurchaseDate = purchaseDate;
        }
    }
}
