// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.Inputs
{

    public sealed class FieldArgs : Pulumi.ResourceArgs
    {
        [Input("label")]
        public Input<string>? Label { get; set; }

        [Input("type")]
        public Input<Rocket.Surgery.OnePasswordNativeUnofficial.FieldType>? Type { get; set; }

        [Input("value", required: true)]
        private Input<string>? _value;
        public Input<string>? Value
        {
            get => _value;
            set
            {
                var emptySecret = Output.CreateSecret(0);
                _value = Output.Tuple<Input<string>?, int>(value, emptySecret).Apply(t => t.Item1);
            }
        }

        public FieldArgs()
        {
            Type = Rocket.Surgery.OnePasswordNativeUnofficial.FieldType.String;
        }
    }
}
