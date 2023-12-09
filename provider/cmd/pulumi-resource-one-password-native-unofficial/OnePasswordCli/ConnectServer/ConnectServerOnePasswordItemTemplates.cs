using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;

public class ConnectServerOnePasswordItemTemplates(OnePasswordOptions options, ILogger logger)
    : ConnectServerOnePasswordBase(options, logger), IOnePasswordItemTemplates;
