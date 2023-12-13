namespace pulumi_resource_one_password_native_unofficial.Domain;

public record TemplateUrl
{
    public string? Label { get; init; }
    public bool Primary { get; init; }
    public string Href { get; init; } = "";
}