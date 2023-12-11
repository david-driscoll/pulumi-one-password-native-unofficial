using System.Collections.Immutable;
using System.Text.Json.Serialization;
using GeneratedCode;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public record ItemRequestBase
{
    public string? Category { get; init; }
    public string? Title { get; init; }
    public ImmutableArray<string> Tags { get; init; } = ImmutableArray<string>.Empty;
    public string? Vault { get; init; }
    public PasswordGeneratorRecipe? GeneratePassword { get; init; }
    public ImmutableArray<Item.Url> Urls { get; init; } = ImmutableArray<Item.Url>.Empty;
}

public record PasswordGeneratorRecipe
{
    public int? Length { get; init; }
    public ImmutableArray<CharacterSets>? CharacterSets { get; init; }

    public static implicit operator string(PasswordGeneratorRecipe recipe) =>
        string.Join(",", (recipe.CharacterSets ?? ImmutableArray<CharacterSets>.Empty).Select(z => z.ToString().ToLowerInvariant()).Concat(new[] { recipe.Length.ToString() }));
}
