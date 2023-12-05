using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.JsonDiffPatch;
using Pulumi.Experimental.Provider;
using pulumi_resource_one_password_native_unoffical.Domain;
using Serilog;

namespace pulumi_resource_one_password_native_unoffical;

public class OnePasswordProvider : Provider
{
    private readonly ILogger _logger;

    public OnePasswordProvider(ILogger logger)
    {
        this._logger = logger;
    }

    public async override Task<ConfigureResponse> Configure(ConfigureRequest request, CancellationToken ct)
    {
        await Task.Yield();
        _logger.Information("Configure called with args {@Args}", request.Args.Keys);
        return new()
        {
            AcceptOutputs = true,
            AcceptResources = true,
            AcceptSecrets = true,
            SupportsPreview = true,
        };
    }

    public async override Task<CheckResponse> Check(CheckRequest request, CancellationToken ct)
    {
        List<CheckFailure> failures = new();
        if (TemplateMetadata.GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");
        // JsonDiffPatcher.Diff(request.OldInputs, request.NewInputs);

        if (resourceType.Urn == ItemType.Item && request.NewInputs.TryGetValue("category", out var category) && category.TryGetString(out var c) && c == resourceType.ItemName)
        {
            failures.Add(new CheckFailure("category", $"Category must be {resourceType.ItemName}"));
        }
        if (request.NewInputs.TryGetValue("fields", out var f) && f.TryGetObject(out var fields))
        {
            ValidateFields(failures, fields!);
        }
        if (request.NewInputs.TryGetValue("sections", out var s) && s.TryGetObject(out var sections))
        {
            foreach (var section in sections!)
            {
                if (section.Key.Contains('.') || section.Key.Contains('\\') || section.Key.Contains('='))
                {
                    failures.Add(new CheckFailure($"sections.{section.Key}", "Section labels cannot contain a period, equals sign or backslash"));
                }
                if (section.Value.TryGetObject(out var fs))
                {
                    ValidateFields(failures, fs!);
                }
            }
        }

        return new CheckResponse { Inputs = request.NewInputs, Failures = failures };
        static void ValidateFields(List<CheckFailure> failures, ImmutableDictionary<string, PropertyValue> fields)
        {
            foreach (var field in fields)
            {
                if (field.Key.Contains('.') || field.Key.Contains('\\') || field.Key.Contains('='))
                {
                    failures.Add(new CheckFailure($"fields.{field.Key}", "Field labels cannot contain a period, equals sign or backslash"));
                }
            }
        }
    }

    public async override Task<DiffResponse> DiffConfig(DiffRequest request, CancellationToken ct)
    {
        DebugHelper.WaitForDebugger();
        return new DiffResponse() { Changes = false, Diffs = new List<string>(), DetailedDiff = new Dictionary<string, PropertyDiff>() };
    }

    public async override Task<DiffResponse> Diff(DiffRequest request, CancellationToken ct)
    {
        DebugHelper.WaitForDebugger();
        List<CheckFailure> failures = new();
        if (TemplateMetadata.GetResourceTypeFromUrn(request.Urn) is not { } resourceType) throw new Exception($"unknown resource type {request.Urn}");

        request.NewInputs.TryAdd("category", new(resourceType.Urn == ItemType.Item ? "Secure Note" : resourceType.ItemName));
        if (request.OldState.TryGetValue("title", out var property) && property.TryGetString(out var p))
        {
            request.NewInputs.TryAdd("title", new(p!));
        }

        var olds = InputOutputExtensions.ConvertToOutputs(request.OldState).ConvertToInputs();
        var news = InputOutputExtensions.ConvertToInputs(request.NewInputs);

        // const delta = this.diffValues(resourceType, convertOutputsToInputs(resourceType, olds), news);

        // return {
        // changes: delta.length > 0,
        //     deleteBeforeReplace: true,
        //     replaces: delta.some(z => z.path.length === 1 && z.op === 'replace' && z.path.includes('category')) ? ['category'] : undefined,
        //     stables: ['uuid'],
        //     //deleteBeforeReplace ??
        //     // replaces ??
        // }
        return new DiffResponse() { Changes = false, Diffs = new List<string>(), DetailedDiff = new Dictionary<string, PropertyDiff>() };
    }

    public async override Task<CreateResponse> Create(CreateRequest request, CancellationToken ct)
    {
        _logger.Information("Create called with args {@Args}", request.Properties.Keys);
        return new CreateResponse()
        {
            Properties = request.Properties,
            Id = Guid.NewGuid().ToString(),
        };
    }

    public override Task<GetSchemaResponse> GetSchema(GetSchemaRequest request, CancellationToken ct)
    {
        return base.GetSchema(request, ct);
    }
}
