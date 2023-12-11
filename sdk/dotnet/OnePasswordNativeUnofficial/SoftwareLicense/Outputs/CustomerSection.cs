// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.SoftwareLicense.Outputs
{

    [OutputType]
    public sealed class CustomerSection
    {
        public readonly string? Company;
        public readonly string? LicensedTo;
        public readonly string? RegisteredEmail;

        [OutputConstructor]
        private CustomerSection(
            string? company,

            string? licensedTo,

            string? registeredEmail)
        {
            Company = company;
            LicensedTo = licensedTo;
            RegisteredEmail = registeredEmail;
        }
    }
}
