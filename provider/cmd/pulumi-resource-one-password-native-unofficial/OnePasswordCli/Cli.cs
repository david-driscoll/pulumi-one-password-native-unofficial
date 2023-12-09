using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.Builders;
using CliWrap.Exceptions;
using Pulumi;
using Serilog;

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

public abstract class OnePasswordBase(
    Command command,
    ArgsBuilder argsBuilder,
    OnePasswordOptions options,
    ILogger logger,
    JsonSerializerOptions serializerOptions)
{
    private protected JsonSerializerOptions SerializerOptions = serializerOptions;
    private protected readonly OnePasswordOptions Options = options;
    private protected readonly Command Command = command;
    private protected readonly ArgsBuilder ArgsBuilder = argsBuilder;
    private protected readonly ILogger Logger = logger;

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, CancellationToken cancellationToken)
    {
        Logger.Information("Executing command: {Command} {Input}", command.Arguments);
        return command
            .WithEnvironmentVariables(Options.Apply)
            .ExecuteBufferedAsync(cancellationToken);
    }

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, string standardInput, CancellationToken cancellationToken)
    {
        Logger.Information("Executing command: {Command} - {Input}", command.Arguments, standardInput);
        return command
            .WithEnvironmentVariables(Options.Apply)
            .WithStandardInputPipe(PipeSource.FromString(standardInput))
            .ExecuteBufferedAsync(cancellationToken);
    }

    protected Task<BufferedCommandResult> ExecuteCommand(Command command, object standardInput, CancellationToken cancellationToken)
    {
        return ExecuteCommand(command, JsonSerializer.Serialize(standardInput, SerializerOptions), cancellationToken);
    }
}

public class OnePassword : OnePasswordBase
{
    public OnePasswordOptions Options { get; }
    private readonly Lazy<OnePasswordItems> _items;
    private readonly Lazy<OnePasswordVaults> _vaults;

    public OnePassword(OnePasswordOptions options, ILogger logger) : base(Cli.Wrap("op"), new(), options, logger,
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        })
    {
        Options = options;
        _items = new Lazy<OnePasswordItems>(() => new(Command, ArgsBuilder, Options, Logger, SerializerOptions));
        _vaults = new Lazy<OnePasswordVaults>(() => new(Command, ArgsBuilder, Options, Logger, SerializerOptions));
    }

    public OnePasswordItems Items => _items.Value;
    public OnePasswordVaults Vaults => _vaults.Value;

    public async Task<WhoAmIResponse> WhoAmI(CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("whoami").Build()),
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);

        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<WhoAmIResponse>(result.StandardOutput)!;
    }

    public async Task<string> Read(string reference, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("read").Add(reference).Build()),
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);
        return result.StandardOutput.TrimEnd();
    }

    public async Task<string> Inject(string template, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("inject").Build()),
            template,
            cancellationToken
        );
        if (result.ExitCode != 0) throw new Exception(result.StandardError);
        return result.StandardOutput.TrimEnd();
    }
}

public class OnePasswordItemTemplates(
    Command command,
    ArgsBuilder argsBuilder,
    OnePasswordOptions options,
    ILogger logger,
    JsonSerializerOptions serializerOptions)
    : OnePasswordBase(command, argsBuilder.Add("template"), options, logger, serializerOptions);

public class OnePasswordVaults(Command command, ArgsBuilder argsBuilder, OnePasswordOptions options, ILogger logger, JsonSerializerOptions serializerOptions)
    : OnePasswordBase(command, argsBuilder.Add("vault"), options, logger, serializerOptions)
{
    public async Task<VaultResponse> Get(string? vault = null, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommand(
            Command.WithArguments(ArgsBuilder.Add("get").Add(vault ?? Options.Vault).Build()),
            cancellationToken
        );
        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<VaultResponse>(result.StandardOutput, SerializerOptions)!;
    }
}

public class OnePasswordItems(Command command, ArgsBuilder argsBuilder, OnePasswordOptions options, ILogger logger, JsonSerializerOptions serializerOptions)
    : OnePasswordBase(command, argsBuilder.Add("item"), options, logger, serializerOptions)
{
    private readonly Lazy<OnePasswordItemTemplates> _templates = new(() => new(command, argsBuilder, options, logger, serializerOptions));

    public async Task<Item.Response> Create(Item.CreateRequest request, TemplateMetadata.Template templateJson, CancellationToken cancellationToken = default)
    {
        var attachments = templateJson.Fields.OfType<TemplateMetadata.TemplateAttachment>().ToImmutableArray();
        templateJson = templateJson with { Fields = templateJson.Fields.Where(x => x is not TemplateMetadata.TemplateAttachment).ToImmutableArray() };

        var args = ArgsBuilder
                .Add("create")
                .Add("-")
                .Add("generate-password", request.GeneratePassword)
                .Add("url", request.Url)
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("category", request.Category)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? Options.Vault)
                .Add("dry-run", request.DryRun)
            ;
        
        (args, var disposable) = await AttachFiles(args, attachments, cancellationToken);

        var result = await ExecuteCommand(
            Command.WithArguments(args.Build()),
            templateJson,
            cancellationToken
        );

        return JsonSerializer.Deserialize<Item.Response>(result.StandardOutput, SerializerOptions)!;
    }

    public async Task<Item.Response> Edit(Item.EditRequest request, TemplateMetadata.Template templateJson, CancellationToken cancellationToken = default)
    {
        var attachments = templateJson.Fields.OfType<TemplateMetadata.TemplateAttachment>().ToImmutableArray();
        templateJson = templateJson with { Fields = templateJson.Fields.Where(x => x is not TemplateMetadata.TemplateAttachment).ToImmutableArray() };


        var args = ArgsBuilder
                .Add("edit")
                .Add(request.Id)
                .Add("-")
                .Add("generate-password", request.GeneratePassword)
                .Add("url", request.Url)
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? Options.Vault)
                .Add("dry-run", request.DryRun)
            ;
        
        (args, var disposable) = await AttachFiles(args, attachments, cancellationToken);

        var result = await ExecuteCommand(
            Command.WithArguments(args.Build()),
            templateJson,
            cancellationToken
        );

        return JsonSerializer.Deserialize<Item.Response>(result.StandardOutput, SerializerOptions)!;
    }

    private async Task<(ArgsBuilder args, IDisposable disposable)> AttachFiles(ArgsBuilder args,
        ImmutableArray<TemplateMetadata.TemplateAttachment> attachments, CancellationToken cancellationToken = default)
    {
        
        var tempDirectory = Directory.CreateTempSubdirectory("p1p");
        foreach (var attachment in attachments)
        {
            var filePath = await ResolveAsset(tempDirectory.FullName, attachment.Id!, attachment.Asset, cancellationToken);
            Logger.Information("Attaching file {Id} {Path} exists: {Exists}", attachment.Id, filePath, File.Exists(filePath));
            args = args.Add($"'{attachment.Id}[file]={filePath}'");
        }

        return (args, Disposable.Empty);

        static async Task<string> ResolveAsset(string tempDirectory, string fileName, AssetOrArchive asset, CancellationToken cancellationToken)
        {
            if (asset is FileAsset fileAsset)
            {
                return Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileAsset));
            }

            if (asset is FileArchive fileArchive)
            {
                return Path.GetFullPath(AssetOrArchiveExtensions.GetAssetValue(fileArchive));
            }

            if (asset is StringAsset stringAsset)
            {
                var tempFilePath = Path.Combine(tempDirectory, fileName);
                await File.WriteAllTextAsync(tempFilePath, AssetOrArchiveExtensions.GetAssetValue(stringAsset), cancellationToken);
                return tempFilePath;
            }

            throw new("Asset not supported!");
        }
    }
    
    public async Task<Item.Response> Get(Item.GetRequest request, CancellationToken cancellationToken = default)
    {
        var args = ArgsBuilder
                .Add("get")
                .Add(request.Id)
                .Add("vault", request.Vault ?? Options.Vault)
            ;

        var result = await ExecuteCommand(
            Command.WithArguments(args.Build()),
            cancellationToken
        );
        // ReSharper disable once NullableWarningSuppressionIsUsed
        return JsonSerializer.Deserialize<Item.Response>(result.StandardOutput, SerializerOptions)!;
    }

    public async Task Delete(Item.DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var args = ArgsBuilder
                .Add("delete")
                .Add(request.Id)
                .Add("vault", request.Vault ?? Options.Vault)
            ;

        await ExecuteCommand(
            Command.WithArguments(args.Build()),
            cancellationToken
        );
    }

    // public Task<Item.CreateResponse> List()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<Item.CreateResponse> Move()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<Item.CreateResponse> Share()
    // {
    //     throw new NotImplementedException();
    // }

    public OnePasswordItemTemplates Templates => _templates.Value;
}

public record WhoAmIResponse(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("account_uuid")]
    string AccountUuid,
    [property: JsonPropertyName("user_uuid")]
    string UserUuid,
    [property: JsonPropertyName("user_type")]
    string UserType
);

public record VaultResponse(string Id, string Name);

public record ItemRequestBase
{
    public string? Title { get; init; }
    public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
    public string? Vault { get; init; }
}

public record ItemResponseBase(string Title, string Vault, ImmutableArray<string> Tags);

public static class Document
{
    public record CreateRequest(string FilePath)
    {
        public string? FileName { get; init; }
        public Stream? StandardInput { get; init; }
        public string? Title { get; init; }
        public ImmutableArray<string> Tags { get; init; }
        public string? Vault { get; init; }
    }

    public record CreateResponse(string Uuid, DateTimeOffset UpdatedAt, DateTimeOffset CreatedAt, string VaultUuid);
}

public static class Item
{
    public record CreateRequest(string Category) : ItemRequestBase
    {
        public bool DryRun { get; init; }
        public bool Favorite { get; init; }
        public string? Url { get; init; }
        public string? GeneratePassword { get; init; }
    }

    public record EditRequest : ItemRequestBase
    {
        public required string Id { get; init; }
        public bool DryRun { get; init; }
        public bool Favorite { get; init; }
        public string? Url { get; init; }
        public string? GeneratePassword { get; init; }
    }

    public record DeleteRequest(string Id)
    {
        public string? Vault { get; init; }
        public bool Archive { get; init; }
    }

    public record Response()
    {
        public string Id { get; init; } = "";
        public string Title { get; init; } = "";
        public int? Version { get; init; }
        public required VaultResponse Vault { get; init; }
        public string Category { get; init; }
        public string LastEditedBy { get; init; } = "";
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }
        public string AdditionalInformation { get; init; } = "";

        public ImmutableArray<Section> Sections { get; init; } = ImmutableArray<Section>.Empty;
        public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
        public ImmutableArray<Field> Fields { get; init; } = ImmutableArray<Field>.Empty;
        public ImmutableArray<File> Files { get; init; } = ImmutableArray<File>.Empty;
        public ImmutableArray<Url> Urls { get; init; } = ImmutableArray<Url>.Empty;
    }

    public record Section
    {
        public string Id { get; init; }
        public string? Label { get; init; }
    }

    public record Field
    {
        public string? Id { get; init; }
        public string? Label { get; init; }
        public required string Type { get; init; }
        public string? Purpose { get; init; }
        public Section? Section { get; init; }
        public string? Value { get; init; }
        [JsonExtensionData] public Dictionary<string, JsonElement>? ExtensionData { get; init; }
    }

    public record File
    {
        public string Id { get; init; } = "";
        public string Name { get; init; } = "";
        public int Size { get; init; }
        public string ContentPath { get; init; } = "";
        public Section? Section { get; init; }
    }

    public record Url
    {
        public string? Label { get; init; }
        public bool Primary { get; init; }
        public string Href { get; init; } = "";
    }


    public record VaultResponse
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        public required string Id { get; init; } = null!;

        // ReSharper disable once NullableWarningSuppressionIsUsed
        public required string Name { get; init; } = null!;
    }

    public record GetRequest()
    {
        public required string Id { get; init; }
        public string? Vault { get; init; }
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
