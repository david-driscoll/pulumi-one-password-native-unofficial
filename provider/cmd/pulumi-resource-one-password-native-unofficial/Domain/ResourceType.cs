using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using Item = pulumi_resource_one_password_native_unofficial.OnePasswordCli.Item;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public partial record ResourceType(
    string Urn,
    string InputCategory,
    string OutputCategory,
    TemplateMetadata.TransformInputs TransformInputsToTemplate,
    TemplateMetadata.TransformOutputs TransformItemToOutputs
) : IPulumiItemType
{

    public Inputs TransformInputs(ImmutableDictionary<string, PropertyValue> properties)
    {
        return TransformInputsToTemplate(this, properties);
    }
    
    // public ImmutableDictionary<string, PropertyValue> ReduceOutputs(Item.Response item, ImmutableDictionary<string, PropertyValue>? inputs)
    // {
    //     var outputs = TransformItemToOutputs(ImmutableDictionary.CreateBuilder<string, PropertyValue>(), this, item, inputs).ToBuilder();
    //     TemplateMetadata.AssignCommonOutputs(outputs, this, item, inputs);
    //     return outputs.ToImmutable();
    // }
    public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item, ImmutableDictionary<string, PropertyValue>? inputs, bool isPreview = false)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        TemplateMetadata.AssignCommonOutputs(outputs, this, item, inputs, isPreview);
        return TransformItemToOutputs(outputs, this, item, inputs);
    }

    public ImmutableDictionary<string, PropertyValue> TransformOutputs(Inputs item, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var response = new Item.Response()
        {
            Vault = new Item.VaultResponse()
            {
                Id = null,
                Name = item.Vault,
            },
            Category = item.Category,
            Title = item.Title,
            Urls = item.Urls.Select(z => new Item.Url()
            {
                Href = z.Href,
                Primary = z.Primary,
                Label = z.Label,
            }).ToImmutableArray(),
            Sections = item.Fields
                .Select(z => z.Value.Section)
                .Where(z => z is not null)
                .Distinct(z => z.Id)
                .Select(z => new Item.Section()
                {
                    Id = z!.Id,
                    Label = z.Label,
                }).ToImmutableArray(),
            Fields = item.Fields
                .Where(z => TemplateFieldType.File != z.Value.Type)
                .Select(z => new Item.Field()
                {
                    Id = z.Value.Id,
                    Label = z.Value.Label,
                    Type = (string)z.Value.Type,
                    Purpose = z.Value.Purpose,
                    Section = z.Value.Section is null
                        ? null
                        : new Item.Section()
                        {
                            Id = z.Value.Section.Id!,
                            Label = z.Value.Section.Label,
                        },
                    Value = z.Value.Value,
                }).ToImmutableArray(),
            Files = item.Fields
                .Where(z => z.Value.Type==TemplateFieldType.File)
                .Select(z => new Item.File()
                {
                    Id = z.Value.Id ?? z.Key,
                    Name = z.Value.Label ?? z.Key,
                    Section = z.Value.Section is null
                        ? null
                        : new Item.Section()
                        {
                            Id = z.Value.Section.Id!,
                            Label = z.Value.Section.Label,
                        },
                }).ToImmutableArray(),
        };

        var result = TransformOutputs(response, inputs, true);
        var vault = result.GetStringProperty("vault.name");
        result = result.SetItem("vault", new PropertyValue(ImmutableDictionary.Create<string, PropertyValue>()
            .Add("id", PropertyValue.Computed)
            .Add("name", vault is null ? PropertyValue.Computed : new PropertyValue(vault))
        ));

        if (result.GetStringProperty("title") is not { Length: > 0 })
        {
            result = result.SetItem("title", PropertyValue.Computed);
        }

        return result;
    }
    
    public virtual bool Equals(ResourceType? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Urn, other.Urn, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Urn);
    }
}
