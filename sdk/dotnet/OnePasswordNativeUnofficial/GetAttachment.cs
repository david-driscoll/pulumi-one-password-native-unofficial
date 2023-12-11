// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System.Threading.Tasks;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial
{
    public static class GetAttachment
    {
        public static Task<GetAttachmentResult> InvokeAsync(GetAttachmentArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.InvokeAsync<GetAttachmentResult>("one-password-native-unofficial:index:GetAttachment", args ?? new GetAttachmentArgs(), options.WithDefaults());

        public static Output<GetAttachmentResult> Invoke(GetAttachmentInvokeArgs args, InvokeOptions? options = null)
            => Pulumi.Deployment.Instance.Invoke<GetAttachmentResult>("one-password-native-unofficial:index:GetAttachment", args ?? new GetAttachmentInvokeArgs(), options.WithDefaults());
    }


    public sealed class GetAttachmentArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file 
        /// </summary>
        [Input("reference", required: true)]
        public string Reference { get; set; } = null!;

        public GetAttachmentArgs()
        {
        }
    }

    public sealed class GetAttachmentInvokeArgs : Pulumi.InvokeArgs
    {
        /// <summary>
        /// The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file 
        /// </summary>
        [Input("reference", required: true)]
        public Input<string> Reference { get; set; } = null!;

        public GetAttachmentInvokeArgs()
        {
        }
    }


    [OutputType]
    public sealed class GetAttachmentResult
    {
        public readonly string? Value;

        [OutputConstructor]
        private GetAttachmentResult(string? value)
        {
            Value = value;
        }
    }
}
