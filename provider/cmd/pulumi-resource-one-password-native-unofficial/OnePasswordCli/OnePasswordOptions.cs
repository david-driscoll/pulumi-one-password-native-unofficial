using System.Text;
using CliWrap.Builders;
using CliWrap.Exceptions;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

/*
  account     Manage your locally configured 1Password accounts
  connect     Manage Connect instances and Connect tokens in your 1Password account
  document    Perform CRUD operations on Document items in your vaults
  events-api  Manage Events API integrations in your 1Password account
  group       Manage the groups in your 1Password account
  item        Perform CRUD operations on the 1Password items in your vaults
  plugin      Manage the shell plugins you use to authenticate third-party CLIs
  user        Manage users within this 1Password account
  vault       Manage permissions and perform CRUD operations on your 1Password vaults


  completion  Generate shell completion information
  inject      Inject secrets into a config file
  read        Read a secret using the secrets reference syntax
  run         Pass secrets as environment variables to a process
  signin      Sign in to a 1Password account
  signout     Sign out of a 1Password account
  update      Check for and download updates.
  whoami      Get information about a signed-in account


  environment variables:
    OP_ACCOUNT
    OP_CONNECT_HOST
    OP_CONNECT_TOKEN
    OP_SERVICE_ACCOUNT_TOKEN
    OP_FORMAT
    OP_DEBUG
    OP_CACHE
    OP_ISO_TIMESTAMPS

    {
  "url": "https://my.1password.com",
  "URL": "https://my.1password.com",
  "user_uuid": "B6JBRNFXPJGHPGD6CCG76JFDEE",
  "account_uuid": "2RNPNUVXRRC2PBMSJVNUYNQSFE",
  "user_type": "SERVICE_ACCOUNT",
  "ServiceAccountType": "SERVICE_ACCOUNT"
}

*/

public record OnePasswordOptions
{
    public string? Vault { get; init; }
    public string? Account { get; init; }
    public string? ServiceAccountToken { get; init; }
    public string? ConnectHost { get; init; }
    public string? ConnectToken { get; init; }
    public bool IsServiceAccount => !string.IsNullOrWhiteSpace(ServiceAccountToken);
    public bool IsConnectServer => !string.IsNullOrWhiteSpace(ConnectHost) || !string.IsNullOrWhiteSpace(ConnectToken);

    public void Apply(EnvironmentVariablesBuilder builder)
    {
        builder.Set("OP_FORMAT", "json");
        builder.Set("OP_ISO_TIMESTAMPS", "1");

        if (!string.IsNullOrWhiteSpace(ServiceAccountToken))
        {
            builder.Set("OP_SERVICE_ACCOUNT_TOKEN", ServiceAccountToken);
        }

        if (!string.IsNullOrWhiteSpace(Account))
        {
            builder.Set("OP_ACCOUNT", Account);
        }

        if (!string.IsNullOrWhiteSpace(ConnectHost) && !string.IsNullOrWhiteSpace(ConnectToken))
        {
            builder.Set("OP_CONNECT_HOST", ConnectHost);
            builder.Set("OP_CONNECT_TOKEN", ConnectToken);
        }
    }
}

/*
  account     Manage your locally configured 1Password accounts
  connect     Manage Connect instances and Connect tokens in your 1Password account
  document    Perform CRUD operations on Document items in your vaults
  events-api  Manage Events API integrations in your 1Password account
  group       Manage the groups in your 1Password account
  item        Perform CRUD operations on the 1Password items in your vaults
  plugin      Manage the shell plugins you use to authenticate third-party CLIs
  user        Manage users within this 1Password account
  vault       Manage permissions and perform CRUD operations on your 1Password vaults


  completion  Generate shell completion information
  inject      Inject secrets into a config file
  read        Read a secret using the secrets reference syntax
  run         Pass secrets as environment variables to a process
  signin      Sign in to a 1Password account
  signout     Sign out of a 1Password account
  update      Check for and download updates.
  whoami      Get information about a signed-in account


  environment variables:
    OP_ACCOUNT
    OP_CONNECT_HOST
    OP_CONNECT_TOKEN
    OP_SERVICE_ACCOUNT_TOKEN
    OP_FORMAT
    OP_DEBUG
    OP_CACHE
    OP_ISO_TIMESTAMPS

    {
  "url": "https://my.1password.com",
  "URL": "https://my.1password.com",
  "user_uuid": "B6JBRNFXPJGHPGD6CCG76JFDEE",
  "account_uuid": "2RNPNUVXRRC2PBMSJVNUYNQSFE",
  "user_type": "SERVICE_ACCOUNT",
  "ServiceAccountType": "SERVICE_ACCOUNT"
}

*/
