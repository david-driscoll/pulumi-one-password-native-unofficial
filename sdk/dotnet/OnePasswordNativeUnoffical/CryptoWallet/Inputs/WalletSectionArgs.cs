// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnoffical.CryptoWallet.Inputs
{

    public sealed class WalletSectionArgs : Pulumi.ResourceArgs
    {
        [Input("walletAddress")]
        public Input<string>? WalletAddress { get; set; }

        public WalletSectionArgs()
        {
        }
    }
}