using System.Text.Json;
using CliWrap;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePasswordItemTemplates(
    Command command,
    ArgsBuilder argsBuilder,
    OnePasswordOptions options,
    ILogger logger,
    JsonSerializerOptions serializerOptions)
    : ServiceAccountOnePasswordBase(command, argsBuilder.Add("template"), options, logger, serializerOptions), IOnePasswordItemTemplates;
