using System.Collections.Immutable;
using System.Text.Json;
using Json.Patch;
using Pulumi.Experimental.Provider;
using pulumi_resource_one_password_native_unofficial.Domain;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Serilog;
using static pulumi_resource_one_password_native_unofficial.TemplateMetadata;
using System.Text.Json.Serialization;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli.ConnectServer;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli.ServiceAccount;

namespace pulumi_resource_one_password_native_unofficial;

public class OnePasswordProvider : Provider
{
    private readonly ILogger _logger;
    private IOnePassword _op;

    public OnePasswordProvider(ILogger logger)
    {
        _logger = logger;
    }

    public async override Task<CheckResponse> Check(CheckRequest request, CancellationToken ct)
    {
        try
        {
            List<CheckFailure> failures = new();
            if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");

            if (resourceType.Urn != ItemType.Item && request.NewInputs.TryGetValue("category", out var category) && category.TryGetString(out var c) &&
                c != resourceType.ItemName)
            {
                failures.Add(new CheckFailure("category", $"Category must be {resourceType.ItemName}"));
            }

            if (_op?.Options?.Vault is null &&
                (!request.NewInputs.TryGetValue("vault", out var vault) || !vault.TryGetString(out var v) || string.IsNullOrWhiteSpace(v)))
            {
                failures.Add(new CheckFailure("vault", $"Vault must be set or a default provided to the provider"));
            }

            return new CheckResponse { Inputs = request.NewInputs, Failures = failures };
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error checking item {Message} {Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

    public override async Task<DiffResponse> Diff(DiffRequest request, CancellationToken ct)
    {
        try
        {
            await Task.Yield();
            List<CheckFailure> failures = new();
            if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");

            request.NewInputs.TryAdd("category", new(resourceType.Urn == ItemType.Item ? "Secure Note" : resourceType.ItemName));
            if (request.OldState.TryGetValue("title", out var property) && property.TryGetString(out var p))
            {
                request.NewInputs.TryAdd("title", new(p!));
            }

            // DebugHelper.WaitForDebugger();

            var news = resourceType.TransformInputs(request.NewInputs);
            var olds = resourceType.TransformInputs(request.OldState);

            var diff = olds.CreatePatch(news, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
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
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error diffing item {Message} {Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

    public override async Task<CreateResponse> Create(CreateRequest request, CancellationToken ct)
    {
        try
        {
            if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
            // DebugHelper.WaitForDebugger();


            var news = resourceType.TransformInputs(request.Properties);

            var response = await _op.Items.Create(new(news.Category)
            {
                Title = news.Title ?? Helpers.NewUniqueName(Helpers.GetNameFromUrn(request.Urn)),
                Vault = news.Vault,
                Tags = news.Tags,
                Urls = news.Urls,
                // TODO
                // GeneratePassword =
            }, news, ct);

            return new CreateResponse()
            {
                Id = response.Id,
                Properties = resourceType.TransformOutputs(response, request.Properties),
            };
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error creating item {Message} {Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, CancellationToken ct)
    {
        try
        {
            if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
            // DebugHelper.WaitForDebugger();

            var news = resourceType.TransformInputs(request.News);

            var response = await _op.Items.Edit(new()
            {
                Id = request.Id,
                Title = news.Title,
                Vault = news.Vault,
                Tags = news.Tags,
                Urls = news.Urls,
                // TODO
                // GeneratePassword =
            }, news, ct);
            return new()
            {
                Properties = resourceType.TransformOutputs(response, request.News),
            };
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error updating item {Message} {Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

    public override async Task Delete(DeleteRequest request, CancellationToken ct)
    {
        try
        {
            if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
            // DebugHelper.WaitForDebugger();
            var inputs = resourceType.TransformInputs(request.Properties);
            Log.Logger.Information("Deleting item {Id} {@Properties}", request.Id, request.Properties);
            await _op.Items.Delete(new(request.Id)
            {
                Vault = GetObjectStringValue(GetObjectValue(request.Properties, "vault") ?? PropertyValue.Null, "id")
            }, ct);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error deleting item {Message} {Stack}", e.Message, e.StackTrace);
            throw;
        }
    }

    public override Task<InvokeResponse> Invoke(InvokeRequest request, CancellationToken ct)
    {
        if (GetFunctionType(request.Tok) is not { } functionType) throw new Exception($"unknown resource type {request.Tok}");
        // DebugHelper.WaitForDebugger();

        return request.Tok switch
        {
            "one-password-native-unofficial:index:GetVault" => GetVault(functionType, request.Args, ct),
            "one-password-native-unofficial:index:Inject" => Inject(functionType, request.Args, ct),
            "one-password-native-unofficial:index:GetSecretReference" or "one-password-native-unofficial:index:Read"
                or "one-password-native-unofficial:index:GetAttachment" => Read(functionType, request.Args, ct),
            _ => GetItem(functionType, request.Args, ct)
        };
    }

    private async Task<InvokeResponse> GetVault(FunctionType functionType, ImmutableDictionary<string, PropertyValue> inputs, CancellationToken ct)
    {
        var failures = new List<CheckFailure>();
        if (!inputs.ContainsKey("vault") && _op.Options.Vault is null)
        {
            failures.Add(new CheckFailure("vault", "Must give a vault in order to get a Vault"));
        }

        if (failures.Count > 0)
        {
            return new() { Failures = failures, };
        }

        var result = await _op.Vaults.Get(GetStringValue(inputs, "vault"));

        return new() { Return = new Dictionary<string, PropertyValue> { { "name", new(result.Name) }, { "uuid", new(result.Id) } } };
    }

    private async Task<InvokeResponse> GetItem(FunctionType functionType, ImmutableDictionary<string, PropertyValue> inputs, CancellationToken ct)
    {
        var failures = new List<CheckFailure>();
        if (!inputs.ContainsKey("id"))
        {
            failures.Add(new CheckFailure("id", "Must give a id in order to get an Item"));
        }

        if (!inputs.ContainsKey("vault") && _op.Options.Vault is null)
        {
            failures.Add(new CheckFailure("vault", "Must give a vault in order to get an Item"));
        }

        if (failures.Count > 0)
        {
            return new() { Failures = failures, };
        }

        var response = await _op.Items.Get(new() { Vault = GetStringValue(inputs, "vault"), Id = GetStringValue(inputs, "id")! }, ct);

        return new() { Return = functionType.TransformOutputs(response) };
    }

    private async Task<InvokeResponse> Inject(FunctionType functionType, ImmutableDictionary<string, PropertyValue> inputs, CancellationToken ct)
    {
        var failures = new List<CheckFailure>();
        if (!inputs.ContainsKey("template"))
        {
            failures.Add(new CheckFailure("template", "Must give a template to inject values into"));
        }

        if (failures.Count > 0)
        {
            return new() { Failures = failures, };
        }

        var response = await _op.Inject(GetStringValue(inputs, "template")!, ct);

        return new() { Return = ImmutableDictionary.Create<string, PropertyValue>().Add("result", new(response)) };
    }

    private async Task<InvokeResponse> Read(FunctionType functionType, ImmutableDictionary<string, PropertyValue> inputs, CancellationToken ct)
    {
        var failures = new List<CheckFailure>();
        if (!inputs.ContainsKey("reference"))
        {
            failures.Add(new CheckFailure("reference", "Must give a secret reference to get"));
        }

        if (failures.Count > 0)
        {
            return new() { Failures = failures, };
        }

        var value = await _op.Read(GetStringValue(inputs, "reference")!, ct);

        return new() { Return = ImmutableDictionary.Create<string, PropertyValue>().Add("value", new(value)) };
    }

    public override async Task<ReadResponse> Read(ReadRequest request, CancellationToken ct)
    {
        if (GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
        // DebugHelper.WaitForDebugger();

        var response = await _op.Items.Get(new() { Id = GetStringValue(request.Inputs, "id"), Vault = GetStringValue(request.Inputs, "vault") }, ct);
        return new()
        {
            Id = response.Id,
            Properties = resourceType.TransformOutputs(response, null),
        };
    }

    // public override Task<GetSchemaResponse> GetSchema(GetSchemaRequest request, CancellationToken ct)
    // {
    //     return base.GetSchema(request, ct);
    // }

    public override async Task<ConfigureResponse> Configure(ConfigureRequest request, CancellationToken ct)
    {
        await Task.Yield();
        var options = ConfigExtensions.ConvertToConfig(request.Args);

        if (options.IsConnectServer)
        {
            _op = new ConnectServerOnePassword(options, _logger);
        }
        else
        {
            _op = new ServiceAccountOnePassword(options, _logger);
        }

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
        var failures = new List<CheckFailure>();

        var news = ConfigExtensions.ConvertToConfig(request.NewInputs);
        var outputs = request.NewInputs;
        if (request.NewInputs.TryGetValue("vault", out var vault) && vault.TryGetString(out var v) && !string.IsNullOrWhiteSpace(v))
        {
            outputs = outputs.Add("vault", new(v));
        }

        var pass = news is { ConnectHost.Length: > 0, ConnectToken.Length: > 0 } or { ServiceAccountToken.Length: > 0 };

        if (!pass)
        {
            failures.Add(new CheckFailure("serviceAccountToken", "Service Account Token or Connect Host/Token must be set"));
            failures.Add(new CheckFailure("connectToken", "Service Account Token or Connect Host/Token must be set"));
        }

        return new CheckResponse()
        {
            Inputs = outputs,
            Failures = failures
        };
    }

    public override async Task<DiffResponse> DiffConfig(DiffRequest request, CancellationToken ct)
    {
        await Task.Yield();

        var olds = ConfigExtensions.ConvertToConfig(request.OldState);
        var news = ConfigExtensions.ConvertToConfig(request.NewInputs);

        var diff = olds.CreatePatch(news, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
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
