using System.Collections.Immutable;
using Pulumi.Experimental.Provider;
using Item = pulumi_resource_one_password_native_unofficial.OnePasswordCli.Item;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record ResourceType(
    string Urn,
    string ItemName,
    string ItemCategory,
    TemplateMetadata.TransformInputs TransformInputsToTemplate,
    TemplateMetadata.TransformOutputs TransformItemToOutputs,
    ImmutableArray<(string field, string? section)> Fields
) : IPulumiItemType
{
    public Inputs TransformInputs(ImmutableDictionary<string, PropertyValue> properties)
    {
        return TransformInputsToTemplate(this, properties);
    }

    public ImmutableDictionary<string, PropertyValue> TransformOutputs(Item.Response item, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        return TransformItemToOutputs(this, item, inputs);
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
                .Select(z => z.Value.Section).Where(z => z is not null).Distinct(z => z.Id).Select(z => new Item.Section()
                {
                    Id = z!.Id,
                    Label = z.Label,
                }).ToImmutableArray(),
            Fields = item.Fields
                .Where(z => !string.Equals(z.Value.Type, "FILE", StringComparison.OrdinalIgnoreCase))
                .Select(z => new Item.Field()
                {
                    Id = z.Value.Id,
                    Label = z.Value.Label,
                    Type = z.Value.Type!,
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
                .Where(z => string.Equals(z.Value.Type, "FILE", StringComparison.OrdinalIgnoreCase))
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

        var result = TransformItemToOutputs(this, response, inputs);
        var vault = result.GetStringProperty("vault.name");
        result = result.SetItem("vault", new PropertyValue(ImmutableDictionary.Create<string, PropertyValue>()
            .Add("id", PropertyValue.Computed)
            .Add("name", item.Vault is null ? PropertyValue.Computed : new PropertyValue(result.GetStringProperty("vault.name")))
        ));

        if (result.GetStringProperty("title") is not { Length: > 0 })
        {
            result = result.SetItem("title", PropertyValue.Computed);
        }

        return result;
    }
}
