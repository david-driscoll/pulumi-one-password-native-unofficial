using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record Inputs
{
    public string? Title { get; init; }
    public required string Category { get; init; }
    public ImmutableDictionary<string, TemplateField> Fields { get; init; } = ImmutableDictionary<string, TemplateField>.Empty;
    public ImmutableArray<Item.Url> Urls { get; init; } = ImmutableArray<Item.Url>.Empty;
    public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
    public string? Vault { get; init; }
    public PasswordGeneratorRecipe? GeneratePassword { get; init; }

    public static implicit operator Template(Inputs inputs)
    {
        return new Template()
        {
            Fields = inputs.Fields.Values.ToImmutableArray(),
            Urls = inputs.Urls.Select(z => new TemplateUrl() { Href = z.Href, Label = z.Label, Primary = z.Primary }).ToImmutableArray(),
        };
    }
}