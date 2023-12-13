using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Security.Cryptography;
using System.Text.Json;
using CliWrap;
using pulumi_resource_one_password_native_unofficial.Domain;
using Serilog;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

public class ServiceAccountOnePasswordItems(
    Command command,
    ArgsBuilder argsBuilder,
    OnePasswordOptions options,
    ILogger logger,
    JsonSerializerOptions serializerOptions)
    : ServiceAccountOnePasswordBase(command, argsBuilder.Add("item"), options, logger, serializerOptions), IOnePasswordItems
{
    private readonly Lazy<ServiceAccountOnePasswordItemTemplates> _templates = new(() => new(command, argsBuilder, options, logger, serializerOptions));

    public async Task<Item.Response> Create(Item.CreateRequest request, Template templateJson, CancellationToken cancellationToken = default)
    {
        (_, var attachments, _, templateJson) = templateJson.PrepareFieldsAndAttachments();

        var args = ArgsBuilder
                .Add("create")
                .Add("-")
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("category", request.Category)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? options.Vault)
                .Add("dry-run", request.DryRun)
            ;
        if (request is { GeneratePassword: { } recipe })
        {
            var r = recipe is { Length: > 0 } or { CharacterSets.Length: > 0 } ? "=" + recipe : "";
            args = args.Add($"--generate-password{r}");
        }
        (args, var disposable) = await AttachFiles(args, attachments, cancellationToken);
        try
        {
            var result = await ExecuteCommand(
                Command.WithArguments(args.Build()),
                templateJson,
                cancellationToken
            );

            // ReSharper disable once NullableWarningSuppressionIsUsed
            return JsonSerializer.Deserialize<Item.Response>(result.StandardOutput, SerializerOptions)!;
        }
        finally
        {
            disposable.Dispose();
        }
    }

    public async Task<Item.Response> Edit(Item.EditRequest request, Template templateJson, CancellationToken cancellationToken = default)
    {
        (_, var attachments, _, templateJson) = templateJson.PrepareFieldsAndAttachments();

        var args = ArgsBuilder
                .Add("edit")
                .Add(request.Id)
                .Add("favorite", request.Favorite)
                .Add("title", request.Title)
                .Add("tags", request.Tags)
                .Add("vault", request.Vault ?? options.Vault)
                .Add("dry-run", request.DryRun)
            ;

        if (request is { GeneratePassword: { } recipe })
        {
            var r = recipe is { Length: > 0 } or { CharacterSets.Length: > 0 } ? "=" + recipe : "";
            args = args.Add($"--generate-password{r}");
        }

        // TODO: Update this to use assignments for the differences

        (args, var disposable) = await AttachFiles(args, attachments, cancellationToken);

        try
        {
            var result = await ExecuteCommand(
                Command.WithArguments(args.Build()),
                templateJson,
                cancellationToken
            );
            disposable.Dispose();

            // ReSharper disable once NullableWarningSuppressionIsUsed
            return JsonSerializer.Deserialize<Item.Response>(result.StandardOutput, SerializerOptions)!;
        }
        finally
        {
            disposable.Dispose();
        }
    }

    private async Task<(ArgsBuilder args, IDisposable disposable)> AttachFiles(ArgsBuilder args,
        ImmutableArray<TemplateAttachment> attachments, CancellationToken cancellationToken = default)
    {
        var tempDirectory = Directory.CreateTempSubdirectory("p1p");
        foreach (var attachment in attachments)
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            var filePath = await attachment.Asset.ResolveAssetPath(tempDirectory.FullName, attachment.Id!, cancellationToken);
            // Logger.Information("Attaching file {Id} {Path} exists: {Exists}", attachment.Id, filePath, File.Exists(filePath));
            var id = attachment is { Section: { Id: { Length: > 0 } section } } ? $"{section}.{attachment.Id}" : attachment.Id;
            args = args.Add($"\"{id}[file]={filePath}\"");
        }

        return (args, Disposable.Create(tempDirectory.FullName, (s) => Directory.Delete(s, true)));
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
