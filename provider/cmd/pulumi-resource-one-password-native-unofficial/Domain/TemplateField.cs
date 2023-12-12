namespace pulumi_resource_one_password_native_unofficial.Domain;

public record TemplateField
{
    public string? Id { get; init; }
    public string? Label { get; init; }
    public string? Type { get; init; }
    public string? Purpose { get; init; }
    public TemplateSection? Section { get; init; }
    public string? Value { get; set; }
}
