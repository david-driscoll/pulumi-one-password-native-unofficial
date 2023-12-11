// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnofficial.Server.Outputs
{

    [OutputType]
    public sealed class AdminConsoleSection
    {
        public readonly string? AdminConsoleUrl;
        public readonly string? AdminConsoleUsername;
        public readonly string? ConsolePassword;

        [OutputConstructor]
        private AdminConsoleSection(
            string? adminConsoleUrl,

            string? adminConsoleUsername,

            string? consolePassword)
        {
            AdminConsoleUrl = adminConsoleUrl;
            AdminConsoleUsername = adminConsoleUsername;
            ConsolePassword = consolePassword;
        }
    }
}
