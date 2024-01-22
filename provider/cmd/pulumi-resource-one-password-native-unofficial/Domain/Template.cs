using System.Collections.Immutable;
using GeneratedCode;
using Rocket.Surgery.OnePasswordNativeUnofficial;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record Template
{
    public required ImmutableArray<TemplateField> Fields { get; init; } = ImmutableArray<TemplateField>.Empty;
    public required ImmutableArray<TemplateUrl> Urls { get; init; } = ImmutableArray<TemplateUrl>.Empty;
    // public required ImmutableArray<TemplateFile> Files { get; init; } = ImmutableArray<TemplateField>.Empty;

    public ImmutableArray<TemplateSection> Sections =>
        Fields.Where(z => z.Section is not null)
            .GroupBy(z => z.Section?.Id!)
            .Select(z => z.First().Section)
            .ToImmutableArray();

    public (
        ImmutableArray<TemplateField> fields,
        ImmutableArray<TemplateAttachment> attachments,
        ImmutableArray<TemplateSection> sections,
        Template template
        ) PrepareFieldsAndAttachments()
    {
        var fields = Fields
            .Where(x => x is not TemplateAttachment)
            .Select(z => z with { Value = TemplateMetadata.Get1PasswordUnixString(z) })
            .Concat(
                // This is so we can keep present the accurate value of the field in the 1password app
                Fields
                    .Where(z => (FieldType)z.Type == FieldType.Date || (FieldType)z.Type == FieldType.MonthYear)
                    .Where(z => TemplateMetadata.Get1PasswordDayOnly(z) is not null)
                    .SelectMany(EnsureDateFieldsHaveBackingValues)
            )
            .ToImmutableArray();
        var attachments = Fields.OfType<TemplateAttachment>().ToImmutableArray();
        return (fields, attachments, Sections, this with { Fields = fields });

        static IEnumerable<TemplateField> EnsureDateFieldsHaveBackingValues(TemplateField templateField)
        {
            if (((FieldType)templateField.Type == FieldType.Date || (FieldType)templateField.Type == FieldType.MonthYear) &&
                TemplateMetadata.Get1PasswordDayOnly(templateField) is { } dateOnly)
            {
                yield return new TemplateField()
                {
                    Id = templateField.Id + "_iso",
                    Label = templateField.Label + " (ISO)",
                    Type = (string)FieldType.String,
                    Purpose = templateField.Purpose,
                    Section = templateField.Section,
                    Value = dateOnly.ToString((FieldType)templateField.Type == FieldType.Date ? "yyyy-MM-dd" : "yyyy-MM")
                };
            }
        }
    }
}
