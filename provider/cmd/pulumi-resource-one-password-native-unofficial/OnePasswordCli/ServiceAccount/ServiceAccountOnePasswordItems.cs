using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Text.Json;
using CliWrap;
using Pulumi;
using Serilog;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePasswordItems(Command command, ArgsBuilder argsBuilder, OnePasswordOptions options, ILogger logger, JsonSerializerOptions serializerOptions)
    : ServiceAccountOnePasswordBase(command, argsBuilder.Add("item"), options, logger, serializerOptions), IOnePasswordItems
{
    private readonly Lazy<ServiceAccountOnePasswordItemTemplates> _templates = new(() => new(command, argsBuilder, options, logger, serializerOptions));

    public async Task<Item.Response> Create(Item.CreateRequest request, TemplateMetadata.Template templateJson, CancellationToken cancellationToken = default)
    {
        var attachments = templateJson.Fields.OfType<TemplateMetadata.TemplateAttachment>().ToImmutableArray();
        templateJson = templateJson with { Fields = templateJson.Fields.Where(x => x is not TemplateMetadata.TemplateAttachment).ToImmutableArray() };

        var args = ArgsBuilder
                .Add("create")
                .Add("-")
                .Add("generate-password", request.GeneratePassword)
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("category", request.Category)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? options.Vault)
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
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? options.Vault)
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
                .Add("vault", request.Vault ?? options.Vault)
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
                .Add("vault", request.Vault ?? options.Vault)
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

    public IOnePasswordItemTemplates Templates => _templates.Value;
}
