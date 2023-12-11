// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.Server.Inputs
{

    public sealed class AdminConsoleSectionArgs : Pulumi.ResourceArgs
    {
        [Input("adminConsoleUrl")]
        public Input<string>? AdminConsoleUrl { get; set; }

        [Input("adminConsoleUsername")]
        public Input<string>? AdminConsoleUsername { get; set; }

        [Input("consolePassword")]
        private Input<string>? _consolePassword;
        public Input<string>? ConsolePassword
        {
            get => _consolePassword;
            set
            {
                var emptySecret = Output.CreateSecret(0);
                _consolePassword = Output.Tuple<Input<string>?, int>(value, emptySecret).Apply(t => t.Item1);
            }
        }

        public AdminConsoleSectionArgs()
        {
        }
    }
}
