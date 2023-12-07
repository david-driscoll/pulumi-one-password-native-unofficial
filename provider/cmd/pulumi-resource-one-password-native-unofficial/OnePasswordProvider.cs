using System.Collections.Immutable;
using System.Text.Json;
using Json.Patch;
using Microsoft.AspNetCore.Server.HttpSys;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
using pulumi_resource_one_password_native_unofficial.Domain;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Serilog;
using static pulumi_resource_one_password_native_unofficial.TemplateMetadata;

namespace pulumi_resource_one_password_native_unofficial;

public class OnePasswordProvider : Provider
{
    private readonly ILogger _logger;
    private OnePassword _op;

    public OnePasswordProvider(ILogger logger)
    {
        _logger = logger;
    }

    public async override Task<CheckResponse> Check(CheckRequest request, CancellationToken ct)
    {
        List<CheckFailure> failures = new();
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");

        if (resourceType.Urn != ItemType.Item && request.NewInputs.TryGetValue("category", out var category) && category.TryGetString(out var c) &&
            c != resourceType.ItemName)
        {
            failures.Add(new CheckFailure("category", $"Category must be {resourceType.ItemName}"));
        }

        if (_op.Options.Vault is null && request.NewInputs.TryGetValue("vault", out var vault) && vault.TryGetString(out var v) && string.IsNullOrWhiteSpace(v))
        {
            failures.Add(new CheckFailure("vault", $"Vault must be set or a default provided to the provider"));
        }

        return new CheckResponse { Inputs = request.NewInputs, Failures = failures };
    }

    public override async Task<DiffResponse> Diff(DiffRequest request, CancellationToken ct)
    {
        await Task.Yield();
        List<CheckFailure> failures = new();
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");

        request.NewInputs.TryAdd("category", new(resourceType.Urn == ItemType.Item ? "Secure Note" : resourceType.ItemName));
        if (request.OldState.TryGetValue("title", out var property) && property.TryGetString(out var p))
        {
            request.NewInputs.TryAdd("title", new(p!));
        }

        DebugHelper.WaitForDebugger();

        var news = resourceType.TransformInputs(request.NewInputs);
        var olds = resourceType.TransformInputs(request.OldState);

        var diff = olds.CreatePatch(news, new JsonSerializerOptions()
            {
            })
            .Operations;

        var replaces = new List<string>();
        if (resourceType.ItemName == "Item" &&
            diff.Any(z => z.Path.Segments.Length == 1 && z.Op == OperationType.Replace && z.Path.Segments[0].Value.Equals("category")))
        {
            replaces.Add("category");
        }

        if (diff.Any(z => z.Path.Segments.Length == 1 && z.Op == OperationType.Replace && z.Path.Segments[0].Value.Equals("vault")))
        {
            replaces.Add("vault");
        }

        return new DiffResponse()
        {
            Changes = diff.Any(),
            DetailedDiff = diff.Aggregate(new Dictionary<string, PropertyDiff>(), (result, patchOperation) =>
            {
                var diff = result[patchOperation.Path.ToString()] = new PropertyDiff()
                {
                    InputDiff = true,
                    Kind = patchOperation.Op switch
                    {
                        OperationType.Add => PropertyDiffKind.Add,
                        OperationType.Remove => PropertyDiffKind.Delete,
                        OperationType.Replace => PropertyDiffKind.Update,
                        _ => PropertyDiffKind.Update,
                    },
                };
                if (replaces.Contains(patchOperation.Path.Segments[0].Value))
                {
                    diff.Kind = diff.Kind switch
                    {
                        PropertyDiffKind.Add => PropertyDiffKind.AddReplace,
                        PropertyDiffKind.Delete => PropertyDiffKind.DeleteReplace,
                        PropertyDiffKind.Update => PropertyDiffKind.UpdateReplace,
                        _ => diff.Kind
                    };
                }

                return result;
            }),
            DeleteBeforeReplace = true,
            Replaces = replaces,
            Stables = resourceType.ItemName == "Item" ? ["uuid"] : ["uuid", "category"],
            Diffs = diff.Select(z => z.Path.Segments[0].Value).ToList(),
        };
    }

    public override async Task<CreateResponse> Create(CreateRequest request, CancellationToken ct)
    {
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
        DebugHelper.WaitForDebugger();


        var news = resourceType.TransformInputs(request.Properties);

        var response = await _op.Items.Create(new(news.Category)
        {
            Title = news.Title,
            Vault = news.Vault,
            Tags = news.Tags,
            Url = news.Urls.FirstOrDefault(),
            // TODO
            // GeneratePassword =
        }, news, ct);

        return new CreateResponse()
        {
            Id = response.Id,
            Properties = resourceType.TransformOutputs(response, news),
        };
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, CancellationToken ct)
    {
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
        DebugHelper.WaitForDebugger();

        var news = resourceType.TransformInputs(request.News);

        var response = await _op.Items.Edit(new()
        {
            Id = request.Id,
            Title = news.Title,
            Vault = news.Vault,
            Tags = news.Tags,
            Url = news.Urls.FirstOrDefault(),
            // TODO
            // GeneratePassword =
        }, news, ct);
        return new()
        {
            Properties = resourceType.TransformOutputs(response, news),
        };
    }

    public override async Task Delete(DeleteRequest request, CancellationToken ct)
    {
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
        DebugHelper.WaitForDebugger();
        await _op.Items.Delete(new(request.Id), ct);
    }

    public override async Task<ConfigureResponse> Configure(ConfigureRequest request, CancellationToken ct)
    {
        DebugHelper.WaitForDebugger();
        await Task.Yield();


        var news = ConfigExtensions.ConvertToConfig(request.Args);
        var options = new OnePasswordOptions()
        {
            Vault = news.Vault,
            ServiceAccountToken = news.ServiceAccountToken,
            ConnectHost = news.ConnectHost,
            ConnectToken = news.ConnectToken,
        };
        _op = new OnePassword(options);
        return new()
        {
            AcceptOutputs = true,
            AcceptResources = true,
            AcceptSecrets = true,
            SupportsPreview = true,
        };
    }

    public override async Task<CheckResponse> CheckConfig(CheckRequest request, CancellationToken ct)
    {
        await Task.Yield();
        DebugHelper.WaitForDebugger();
        var failures = new List<CheckFailure>();

        var news = ConfigExtensions.ConvertToConfig(request.NewInputs);
        var outputs = request.NewInputs;
        if (request.NewInputs.TryGetValue("vault", out var vault) && vault.TryGetString(out var v) && !string.IsNullOrWhiteSpace(v))
        {
            outputs = outputs.Add("vault", new(v));
        }

        var pass = news is { ConnectHost.Length: > 0, ConnectToken.Length: > 0 } || news is { ServiceAccountToken.Length: > 0 };

        if (!pass)
        {
            failures.Add(new CheckFailure("serviceAccountToken", "Service Account Token or Connect Host/Token must be set"));
            failures.Add(new CheckFailure("connectToken", "Service Account Token or Connect Host/Token must be set"));
        }

        return new CheckResponse()
        {
            Inputs = outputs,
            Failures = pass ? new List<CheckFailure>() : failures
        };
    }

    public override async Task<DiffResponse> DiffConfig(DiffRequest request, CancellationToken ct)
    {
        await Task.Yield();
        DebugHelper.WaitForDebugger();

        var olds = ConfigExtensions.ConvertToConfig(request.OldState);
        var news = ConfigExtensions.ConvertToConfig(request.NewInputs);

        var diff = olds.CreatePatch(news, new JsonSerializerOptions()
            {
            })
            .Operations;

        var diffs = new List<string>();
        var detailedDiffs = new Dictionary<string, PropertyDiff>();

        foreach (var op in diff)
        {
            // to I need to transform these strings at all? probably.
            diffs.Add(op.Path.Segments[0].Value);
            if (op.Op == OperationType.Add)
            {
                detailedDiffs[op.Path.ToString()] = new PropertyDiff() { InputDiff = true, Kind = PropertyDiffKind.Add };
            }

            if (op.Op == OperationType.Remove)
            {
                detailedDiffs[op.Path.ToString()] = new PropertyDiff() { InputDiff = true, Kind = PropertyDiffKind.Delete };
            }

            if (op.Op == OperationType.Replace)
            {
                detailedDiffs[op.Path.ToString()] = new PropertyDiff() { InputDiff = true, Kind = PropertyDiffKind.Update };
            }
        }

        return new DiffResponse() { Changes = diff.Any(), Diffs = diffs, DetailedDiff = detailedDiffs };
    }
}
