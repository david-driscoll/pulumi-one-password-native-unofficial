// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.EmailAccount.Inputs
{

    public sealed class ContactInformationSectionArgs : Pulumi.ResourceArgs
    {
        [Input("phoneLocal")]
        public Input<string>? PhoneLocal { get; set; }

        [Input("phoneTollFree")]
        public Input<string>? PhoneTollFree { get; set; }

        [Input("provider")]
        public Input<string>? Provider { get; set; }

        [Input("providersWebsite")]
        public Input<string>? ProvidersWebsite { get; set; }

        public ContactInformationSectionArgs()
        {
        }
    }
}
