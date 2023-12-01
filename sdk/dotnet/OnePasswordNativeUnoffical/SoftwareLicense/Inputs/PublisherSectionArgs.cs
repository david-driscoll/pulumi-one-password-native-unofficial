// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnoffical.SoftwareLicense.Inputs
{

    public sealed class PublisherSectionArgs : Pulumi.ResourceArgs
    {
        [Input("downloadPage")]
        public Input<string>? DownloadPage { get; set; }

        [Input("publisher")]
        public Input<string>? Publisher { get; set; }

        [Input("retailPrice")]
        public Input<string>? RetailPrice { get; set; }

        [Input("supportEmail")]
        public Input<string>? SupportEmail { get; set; }

        [Input("website")]
        public Input<string>? Website { get; set; }

        public PublisherSectionArgs()
        {
        }
    }
}