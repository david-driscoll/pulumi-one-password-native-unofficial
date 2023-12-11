using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;

namespace pulumi_resource_one_password_native_unofficial;

public static partial class TemplateMetadata
{

    public static Inputs TransformInputsToAPICredential(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("credential", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "credential",
                Type = "CONCEALED",
                Id = "credential",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "credential") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "credential",
                Type = "CONCEALED",
                Id = "credential",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("type", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "type",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "type") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "type",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("filename", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "filename",
                Type = "STRING",
                Id = "filename",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "filename") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "filename",
                Type = "STRING",
                Id = "filename",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("validFrom", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "valid from",
                Type = "DATE",
                Id = "validFrom",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "validFrom") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "valid from",
                Type = "DATE",
                Id = "validFrom",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expires", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expires",
                Type = "DATE",
                Id = "expires",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expires") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expires",
                Type = "DATE",
                Id = "expires",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("hostname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "hostname",
                Type = "STRING",
                Id = "hostname",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "hostname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "hostname",
                Type = "STRING",
                Id = "hostname",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToAPICredential(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "username") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("username", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "credential") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("credential", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "type") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("type", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "filename") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("filename", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "validFrom") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("validFrom", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expires") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expires", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "hostname") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("hostname", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToBankAccount(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("bankName", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "bank name",
                Type = "STRING",
                Id = "bankName",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "bankName") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "bank name",
                Type = "STRING",
                Id = "bankName",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("owner", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "name on account",
                Type = "STRING",
                Id = "owner",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "owner") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "name on account",
                Type = "STRING",
                Id = "owner",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("accountType", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "accountType",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "accountType") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "accountType",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("routingNo", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "routing number",
                Type = "STRING",
                Id = "routingNo",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "routingNo") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "routing number",
                Type = "STRING",
                Id = "routingNo",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("accountNo", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "account number",
                Type = "STRING",
                Id = "accountNo",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "accountNo") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "account number",
                Type = "STRING",
                Id = "accountNo",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("swift", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "SWIFT",
                Type = "STRING",
                Id = "swift",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "swift") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "SWIFT",
                Type = "STRING",
                Id = "swift",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("iban", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "IBAN",
                Type = "STRING",
                Id = "iban",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "iban") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "IBAN",
                Type = "STRING",
                Id = "iban",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("telephonePin", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "telephonePin",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "telephonePin") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "telephonePin",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("branchPhone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone",
                Type = "PHONE",
                Id = "branchPhone",
                Purpose = null,
                Section = new () { Id = "branchInfo", Label = "Branch Information" },
            });
        }
        else if (GetSection(values, "branchInformation") is {} section && GetField(section, "branchPhone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone",
                Type = "PHONE",
                Id = "branchPhone",
                Purpose = null,
                Section = new () { Id = "branchInfo", Label = "Branch Information" },
            });
        }
    }
    {
        if (values.TryGetValue("branchAddress", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "address",
                Type = "STRING",
                Id = "branchAddress",
                Purpose = null,
                Section = new () { Id = "branchInfo", Label = "Branch Information" },
            });
        }
        else if (GetSection(values, "branchInformation") is {} section && GetField(section, "branchAddress") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "address",
                Type = "STRING",
                Id = "branchAddress",
                Purpose = null,
                Section = new () { Id = "branchInfo", Label = "Branch Information" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToBankAccount(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "bankName") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("bankName", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "owner") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("owner", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "accountType") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("accountType", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "routingNo") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("routingNo", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "accountNo") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("accountNo", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "swift") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("swift", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "iban") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("iban", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "telephonePin") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("telephonePin", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "branchPhone", "branchInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "branchInfo", "branchPhone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "branchAddress", "branchInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "branchInfo", "branchAddress", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToCreditCard(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("cardholder", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "cardholder name",
                Type = "STRING",
                Id = "cardholder",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "cardholder") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "cardholder name",
                Type = "STRING",
                Id = "cardholder",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("type", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "CREDIT_CARD_TYPE",
                Id = "type",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "type") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "CREDIT_CARD_TYPE",
                Id = "type",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("ccnum", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "number",
                Type = "CREDIT_CARD_NUMBER",
                Id = "ccnum",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "ccnum") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "number",
                Type = "CREDIT_CARD_NUMBER",
                Id = "ccnum",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("cvv", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "verification number",
                Type = "CONCEALED",
                Id = "cvv",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "cvv") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "verification number",
                Type = "CONCEALED",
                Id = "cvv",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expiry", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expiry") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("validFrom", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "valid from",
                Type = "MONTH_YEAR",
                Id = "validFrom",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "validFrom") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "valid from",
                Type = "MONTH_YEAR",
                Id = "validFrom",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("bank", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "issuing bank",
                Type = "STRING",
                Id = "bank",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "bank") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "issuing bank",
                Type = "STRING",
                Id = "bank",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("phoneLocal", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone (local)",
                Type = "PHONE",
                Id = "phoneLocal",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "phoneLocal") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone (local)",
                Type = "PHONE",
                Id = "phoneLocal",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("phoneTollFree", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone (toll free)",
                Type = "PHONE",
                Id = "phoneTollFree",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "phoneTollFree") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone (toll free)",
                Type = "PHONE",
                Id = "phoneTollFree",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("phoneIntl", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone (intl)",
                Type = "PHONE",
                Id = "phoneIntl",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "phoneIntl") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone (intl)",
                Type = "PHONE",
                Id = "phoneIntl",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "contactInfo", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("pin", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
        else if (GetSection(values, "additionalDetails") is {} section && GetField(section, "pin") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
    }
    {
        if (values.TryGetValue("creditLimit", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "credit limit",
                Type = "STRING",
                Id = "creditLimit",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
        else if (GetSection(values, "additionalDetails") is {} section && GetField(section, "creditLimit") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "credit limit",
                Type = "STRING",
                Id = "creditLimit",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
    }
    {
        if (values.TryGetValue("cashLimit", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "cash withdrawal limit",
                Type = "STRING",
                Id = "cashLimit",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
        else if (GetSection(values, "additionalDetails") is {} section && GetField(section, "cashLimit") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "cash withdrawal limit",
                Type = "STRING",
                Id = "cashLimit",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
    }
    {
        if (values.TryGetValue("interest", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "interest rate",
                Type = "STRING",
                Id = "interest",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
        else if (GetSection(values, "additionalDetails") is {} section && GetField(section, "interest") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "interest rate",
                Type = "STRING",
                Id = "interest",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
    }
    {
        if (values.TryGetValue("issuenumber", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "issue number",
                Type = "STRING",
                Id = "issuenumber",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
        else if (GetSection(values, "additionalDetails") is {} section && GetField(section, "issuenumber") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "issue number",
                Type = "STRING",
                Id = "issuenumber",
                Purpose = null,
                Section = new () { Id = "details", Label = "Additional Details" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToCreditCard(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "cardholder") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("cardholder", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "type") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("type", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "ccnum") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("ccnum", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "cvv") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("cvv", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expiry") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expiry", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "validFrom") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("validFrom", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "bank", "contactInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "contactInfo", "bank", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "phoneLocal", "contactInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "contactInfo", "phoneLocal", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "phoneTollFree", "contactInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "contactInfo", "phoneTollFree", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "phoneIntl", "contactInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "contactInfo", "phoneIntl", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "website", "contactInfo") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "contactInfo", "website", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "pin", "details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "details", "pin", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "creditLimit", "details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "details", "creditLimit", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "cashLimit", "details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "details", "cashLimit", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "interest", "details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "details", "interest", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "issuenumber", "details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "details", "issuenumber", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToCryptoWallet(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("recoveryPhrase", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "recovery phrase",
                Type = "CONCEALED",
                Id = "recoveryPhrase",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "recoveryPhrase") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "recovery phrase",
                Type = "CONCEALED",
                Id = "recoveryPhrase",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("walletAddress", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "wallet address",
                Type = "STRING",
                Id = "walletAddress",
                Purpose = null,
                Section = new () { Id = "wallet", Label = "Wallet" },
            });
        }
        else if (GetSection(values, "wallet") is {} section && GetField(section, "walletAddress") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "wallet address",
                Type = "STRING",
                Id = "walletAddress",
                Purpose = null,
                Section = new () { Id = "wallet", Label = "Wallet" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToCryptoWallet(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "recoveryPhrase") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("recoveryPhrase", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "walletAddress", "wallet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "wallet", "walletAddress", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToDatabase(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("database_type", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "database_type",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "database_type") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "database_type",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("hostname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "server",
                Type = "STRING",
                Id = "hostname",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "hostname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "server",
                Type = "STRING",
                Id = "hostname",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("port", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "port",
                Type = "STRING",
                Id = "port",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "port") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "port",
                Type = "STRING",
                Id = "port",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("database", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "database",
                Type = "STRING",
                Id = "database",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "database") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "database",
                Type = "STRING",
                Id = "database",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("sid", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "SID",
                Type = "STRING",
                Id = "sid",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "sid") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "SID",
                Type = "STRING",
                Id = "sid",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("alias", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "alias",
                Type = "STRING",
                Id = "alias",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "alias") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "alias",
                Type = "STRING",
                Id = "alias",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("options", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "connection options",
                Type = "STRING",
                Id = "options",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "options") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "connection options",
                Type = "STRING",
                Id = "options",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToDatabase(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "database_type") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("database_type", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "hostname") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("hostname", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "port") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("port", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "database") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("database", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "username") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("username", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "sid") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("sid", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "alias") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("alias", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "options") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("options", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToDocument(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToDocument(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToDriverLicense(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("fullname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "fullname",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "fullname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "fullname",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("address", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "address",
                Type = "STRING",
                Id = "address",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "address") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "address",
                Type = "STRING",
                Id = "address",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("birthdate", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "date of birth",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "birthdate") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "date of birth",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("gender", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "gender") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("height", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "height",
                Type = "STRING",
                Id = "height",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "height") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "height",
                Type = "STRING",
                Id = "height",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("number", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "number",
                Type = "STRING",
                Id = "number",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "number") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "number",
                Type = "STRING",
                Id = "number",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("class", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "license class",
                Type = "STRING",
                Id = "class",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "class") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "license class",
                Type = "STRING",
                Id = "class",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("conditions", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "conditions / restrictions",
                Type = "STRING",
                Id = "conditions",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "conditions") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "conditions / restrictions",
                Type = "STRING",
                Id = "conditions",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("state", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "state",
                Type = "STRING",
                Id = "state",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "state") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "state",
                Type = "STRING",
                Id = "state",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("country", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "country",
                Type = "STRING",
                Id = "country",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "country") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "country",
                Type = "STRING",
                Id = "country",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expiry_date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expiry_date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToDriverLicense(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "fullname") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("fullname", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "address") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("address", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "birthdate") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("birthdate", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "gender") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("gender", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "height") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("height", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "number") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("number", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "class") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("class", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "conditions") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("conditions", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "state") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("state", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "country") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("country", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expiry_date") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expiry_date", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToEmailAccount(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_type", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "pop_type",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_type") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "MENU",
                Id = "pop_type",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "pop_username",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "pop_username",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_server", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "server",
                Type = "STRING",
                Id = "pop_server",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_server") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "server",
                Type = "STRING",
                Id = "pop_server",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_port", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "port number",
                Type = "STRING",
                Id = "pop_port",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_port") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "port number",
                Type = "STRING",
                Id = "pop_port",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "pop_password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "pop_password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_security", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "security",
                Type = "MENU",
                Id = "pop_security",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_security") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "security",
                Type = "MENU",
                Id = "pop_security",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pop_authentication", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "auth method",
                Type = "MENU",
                Id = "pop_authentication",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pop_authentication") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "auth method",
                Type = "MENU",
                Id = "pop_authentication",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("smtp_server", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "SMTP server",
                Type = "STRING",
                Id = "smtp_server",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_server") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "SMTP server",
                Type = "STRING",
                Id = "smtp_server",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("smtp_port", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "port number",
                Type = "STRING",
                Id = "smtp_port",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_port") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "port number",
                Type = "STRING",
                Id = "smtp_port",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("smtp_username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "smtp_username",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "smtp_username",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("smtp_password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "smtp_password",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "smtp_password",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("smtp_security", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "security",
                Type = "MENU",
                Id = "smtp_security",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_security") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "security",
                Type = "MENU",
                Id = "smtp_security",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("smtp_authentication", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "auth method",
                Type = "MENU",
                Id = "smtp_authentication",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
        else if (GetSection(values, "smtp") is {} section && GetField(section, "smtp_authentication") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "auth method",
                Type = "MENU",
                Id = "smtp_authentication",
                Purpose = null,
                Section = new () { Id = "SMTP", Label = "SMTP" },
            });
        }
    }
    {
        if (values.TryGetValue("provider", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "provider",
                Type = "STRING",
                Id = "provider",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "provider") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "provider",
                Type = "STRING",
                Id = "provider",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("provider_website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "provider's website",
                Type = "STRING",
                Id = "provider_website",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "provider_website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "provider's website",
                Type = "STRING",
                Id = "provider_website",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("phone_local", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone (local)",
                Type = "STRING",
                Id = "phone_local",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "phone_local") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone (local)",
                Type = "STRING",
                Id = "phone_local",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
    }
    {
        if (values.TryGetValue("phone_tollfree", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone (toll free)",
                Type = "STRING",
                Id = "phone_tollfree",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
        else if (GetSection(values, "contactInformation") is {} section && GetField(section, "phone_tollfree") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone (toll free)",
                Type = "STRING",
                Id = "phone_tollfree",
                Purpose = null,
                Section = new () { Id = "Contact Information", Label = "Contact Information" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToEmailAccount(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_type") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_type", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_username") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_username", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_server") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_server", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_port") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_port", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_security") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_security", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pop_authentication") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pop_authentication", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "smtp_server", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_server", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "smtp_port", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_port", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "smtp_username", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_username", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "smtp_password", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_password", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "smtp_security", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_security", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "smtp_authentication", "SMTP") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "SMTP", "smtp_authentication", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "provider", "Contact Information") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "Contact Information", "provider", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "provider_website", "Contact Information") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "Contact Information", "provider_website", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "phone_local", "Contact Information") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "Contact Information", "phone_local", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "phone_tollfree", "Contact Information") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "Contact Information", "phone_tollfree", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToIdentity(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("firstname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "first name",
                Type = "STRING",
                Id = "firstname",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "firstname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "first name",
                Type = "STRING",
                Id = "firstname",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("initial", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "initial",
                Type = "STRING",
                Id = "initial",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "initial") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "initial",
                Type = "STRING",
                Id = "initial",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("lastname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "last name",
                Type = "STRING",
                Id = "lastname",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "lastname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "last name",
                Type = "STRING",
                Id = "lastname",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("gender", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "gender") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("birthdate", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "birth date",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "birthdate") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "birth date",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("occupation", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "occupation",
                Type = "STRING",
                Id = "occupation",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "occupation") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "occupation",
                Type = "STRING",
                Id = "occupation",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("company", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "company",
                Type = "STRING",
                Id = "company",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "company") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "company",
                Type = "STRING",
                Id = "company",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("department", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "department",
                Type = "STRING",
                Id = "department",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "department") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "department",
                Type = "STRING",
                Id = "department",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("jobtitle", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "job title",
                Type = "STRING",
                Id = "jobtitle",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
        else if (GetSection(values, "identification") is {} section && GetField(section, "jobtitle") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "job title",
                Type = "STRING",
                Id = "jobtitle",
                Purpose = null,
                Section = new () { Id = "name", Label = "Identification" },
            });
        }
    }
    {
        if (values.TryGetValue("address", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "address",
                Type = "ADDRESS",
                Id = "address",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
        else if (GetSection(values, "address") is {} section && GetField(section, "address") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "address",
                Type = "ADDRESS",
                Id = "address",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
    }
    {
        if (values.TryGetValue("defphone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "default phone",
                Type = "PHONE",
                Id = "defphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
        else if (GetSection(values, "address") is {} section && GetField(section, "defphone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "default phone",
                Type = "PHONE",
                Id = "defphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
    }
    {
        if (values.TryGetValue("homephone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "home",
                Type = "PHONE",
                Id = "homephone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
        else if (GetSection(values, "address") is {} section && GetField(section, "homephone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "home",
                Type = "PHONE",
                Id = "homephone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
    }
    {
        if (values.TryGetValue("cellphone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "cell",
                Type = "PHONE",
                Id = "cellphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
        else if (GetSection(values, "address") is {} section && GetField(section, "cellphone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "cell",
                Type = "PHONE",
                Id = "cellphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
    }
    {
        if (values.TryGetValue("busphone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "business",
                Type = "PHONE",
                Id = "busphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
        else if (GetSection(values, "address") is {} section && GetField(section, "busphone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "business",
                Type = "PHONE",
                Id = "busphone",
                Purpose = null,
                Section = new () { Id = "address", Label = "Address" },
            });
        }
    }
    {
        if (values.TryGetValue("username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("reminderq", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "reminder question",
                Type = "STRING",
                Id = "reminderq",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "reminderq") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "reminder question",
                Type = "STRING",
                Id = "reminderq",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("remindera", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "reminder answer",
                Type = "STRING",
                Id = "remindera",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "remindera") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "reminder answer",
                Type = "STRING",
                Id = "remindera",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("email", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "email",
                Type = "STRING",
                Id = "email",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "email") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "email",
                Type = "STRING",
                Id = "email",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "STRING",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "STRING",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("icq", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "ICQ",
                Type = "STRING",
                Id = "icq",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "icq") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "ICQ",
                Type = "STRING",
                Id = "icq",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("skype", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "skype",
                Type = "STRING",
                Id = "skype",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "skype") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "skype",
                Type = "STRING",
                Id = "skype",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("aim", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "AOL/AIM",
                Type = "STRING",
                Id = "aim",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "aim") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "AOL/AIM",
                Type = "STRING",
                Id = "aim",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("yahoo", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "Yahoo",
                Type = "STRING",
                Id = "yahoo",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "yahoo") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "Yahoo",
                Type = "STRING",
                Id = "yahoo",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("msn", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "MSN",
                Type = "STRING",
                Id = "msn",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "msn") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "MSN",
                Type = "STRING",
                Id = "msn",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
    {
        if (values.TryGetValue("forumsig", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "forum signature",
                Type = "STRING",
                Id = "forumsig",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
        else if (GetSection(values, "internetDetails") is {} section && GetField(section, "forumsig") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "forum signature",
                Type = "STRING",
                Id = "forumsig",
                Purpose = null,
                Section = new () { Id = "internet", Label = "Internet Details" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToIdentity(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "firstname", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "firstname", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "initial", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "initial", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "lastname", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "lastname", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "gender", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "gender", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "birthdate", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "birthdate", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "occupation", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "occupation", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "company", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "company", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "department", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "department", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "jobtitle", "name") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "name", "jobtitle", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "address", "address") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "address", "address", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "defphone", "address") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "address", "defphone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "homephone", "address") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "address", "homephone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "cellphone", "address") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "address", "cellphone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "busphone", "address") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "address", "busphone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "username", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "username", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "reminderq", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "reminderq", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "remindera", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "remindera", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "email", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "email", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "website", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "website", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "icq", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "icq", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "skype", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "skype", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "aim", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "aim", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "yahoo", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "yahoo", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "msn", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "msn", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "forumsig", "internet") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "internet", "forumsig", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToItem(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = category,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToItem(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToLogin(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = "USERNAME",
                
            });
        }
        else if (GetField(values, "username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = "USERNAME",
                
            });
        }
    }
    {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = "PASSWORD",
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = "PASSWORD",
                
            });
        }
    }
    {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToLogin(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "username") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("username", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToMedicalRecord(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "date",
                Type = "DATE",
                Id = "date",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "date",
                Type = "DATE",
                Id = "date",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("location", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "location",
                Type = "STRING",
                Id = "location",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "location") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "location",
                Type = "STRING",
                Id = "location",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("healthcareprofessional", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "healthcare professional",
                Type = "STRING",
                Id = "healthcareprofessional",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "healthcareprofessional") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "healthcare professional",
                Type = "STRING",
                Id = "healthcareprofessional",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("patient", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "patient",
                Type = "STRING",
                Id = "patient",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "patient") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "patient",
                Type = "STRING",
                Id = "patient",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("reason", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "reason for visit",
                Type = "STRING",
                Id = "reason",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "reason") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "reason for visit",
                Type = "STRING",
                Id = "reason",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("medication", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "medication",
                Type = "STRING",
                Id = "medication",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
        else if (GetSection(values, "medication") is {} section && GetField(section, "medication") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "medication",
                Type = "STRING",
                Id = "medication",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
    }
    {
        if (values.TryGetValue("dosage", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "dosage",
                Type = "STRING",
                Id = "dosage",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
        else if (GetSection(values, "medication") is {} section && GetField(section, "dosage") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "dosage",
                Type = "STRING",
                Id = "dosage",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
    }
    {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "medication notes",
                Type = "STRING",
                Id = "notes",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
        else if (GetSection(values, "medication") is {} section && GetField(section, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "medication notes",
                Type = "STRING",
                Id = "notes",
                Purpose = null,
                Section = new () { Id = "medication", Label = "medication" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToMedicalRecord(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "date") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("date", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "location") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("location", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "healthcareprofessional") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("healthcareprofessional", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "patient") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("patient", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "reason") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("reason", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "medication", "medication") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "medication", "medication", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "dosage", "medication") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "medication", "dosage", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "notes", "medication") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "medication", "notes", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToMembership(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("org_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "group",
                Type = "STRING",
                Id = "org_name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "org_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "group",
                Type = "STRING",
                Id = "org_name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("phone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "telephone",
                Type = "PHONE",
                Id = "phone",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "phone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "telephone",
                Type = "PHONE",
                Id = "phone",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("member_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member name",
                Type = "STRING",
                Id = "member_name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "member_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member name",
                Type = "STRING",
                Id = "member_name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("member_since", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member since",
                Type = "MONTH_YEAR",
                Id = "member_since",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "member_since") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member since",
                Type = "MONTH_YEAR",
                Id = "member_since",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expiry_date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expiry_date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expiry date",
                Type = "MONTH_YEAR",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("membership_no", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member ID",
                Type = "STRING",
                Id = "membership_no",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "membership_no") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member ID",
                Type = "STRING",
                Id = "membership_no",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pin", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pin") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToMembership(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "org_name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("org_name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "website") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("website", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "phone") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("phone", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "member_name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("member_name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "member_since") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("member_since", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expiry_date") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expiry_date", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "membership_no") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("membership_no", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pin") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pin", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToOutdoorLicense(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("valid_from", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "valid from",
                Type = "DATE",
                Id = "valid_from",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "valid_from") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "valid from",
                Type = "DATE",
                Id = "valid_from",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expires", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expires",
                Type = "DATE",
                Id = "expires",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expires") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expires",
                Type = "DATE",
                Id = "expires",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("game", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "approved wildlife",
                Type = "STRING",
                Id = "game",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "game") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "approved wildlife",
                Type = "STRING",
                Id = "game",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("quota", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "maximum quota",
                Type = "STRING",
                Id = "quota",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "quota") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "maximum quota",
                Type = "STRING",
                Id = "quota",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("state", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "state",
                Type = "STRING",
                Id = "state",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "state") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "state",
                Type = "STRING",
                Id = "state",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("country", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "country",
                Type = "STRING",
                Id = "country",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "country") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "country",
                Type = "STRING",
                Id = "country",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToOutdoorLicense(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "valid_from") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("valid_from", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expires") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expires", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "game") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("game", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "quota") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("quota", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "state") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("state", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "country") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("country", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToPassport(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("type", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "type",
                Type = "STRING",
                Id = "type",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "type") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "type",
                Type = "STRING",
                Id = "type",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("issuing_country", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "issuing country",
                Type = "STRING",
                Id = "issuing_country",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "issuing_country") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "issuing country",
                Type = "STRING",
                Id = "issuing_country",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("number", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "number",
                Type = "STRING",
                Id = "number",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "number") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "number",
                Type = "STRING",
                Id = "number",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("fullname", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "fullname",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "fullname") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "full name",
                Type = "STRING",
                Id = "fullname",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("gender", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "gender") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "gender",
                Type = "STRING",
                Id = "gender",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("nationality", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "nationality",
                Type = "STRING",
                Id = "nationality",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "nationality") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "nationality",
                Type = "STRING",
                Id = "nationality",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("issuing_authority", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "issuing authority",
                Type = "STRING",
                Id = "issuing_authority",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "issuing_authority") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "issuing authority",
                Type = "STRING",
                Id = "issuing_authority",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("birthdate", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "date of birth",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "birthdate") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "date of birth",
                Type = "DATE",
                Id = "birthdate",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("birthplace", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "place of birth",
                Type = "STRING",
                Id = "birthplace",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "birthplace") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "place of birth",
                Type = "STRING",
                Id = "birthplace",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("issue_date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "issued on",
                Type = "DATE",
                Id = "issue_date",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "issue_date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "issued on",
                Type = "DATE",
                Id = "issue_date",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("expiry_date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "expiry date",
                Type = "DATE",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "expiry_date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "expiry date",
                Type = "DATE",
                Id = "expiry_date",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToPassport(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "type") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("type", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "issuing_country") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("issuing_country", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "number") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("number", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "fullname") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("fullname", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "gender") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("gender", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "nationality") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("nationality", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "issuing_authority") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("issuing_authority", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "birthdate") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("birthdate", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "birthplace") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("birthplace", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "issue_date") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("issue_date", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "expiry_date") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("expiry_date", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToPassword(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = "PASSWORD",
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = "PASSWORD",
                
            });
        }
    }
    {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToPassword(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToRewardProgram(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("company_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "company name",
                Type = "STRING",
                Id = "company_name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "company_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "company name",
                Type = "STRING",
                Id = "company_name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("member_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member name",
                Type = "STRING",
                Id = "member_name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "member_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member name",
                Type = "STRING",
                Id = "member_name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("membership_no", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member ID",
                Type = "STRING",
                Id = "membership_no",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "membership_no") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member ID",
                Type = "STRING",
                Id = "membership_no",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("pin", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "pin") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "PIN",
                Type = "CONCEALED",
                Id = "pin",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("additional_no", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member ID (additional)",
                Type = "STRING",
                Id = "additional_no",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
        else if (GetSection(values, "moreInformation") is {} section && GetField(section, "additional_no") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member ID (additional)",
                Type = "STRING",
                Id = "additional_no",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
    }
    {
        if (values.TryGetValue("member_since", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "member since",
                Type = "MONTH_YEAR",
                Id = "member_since",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
        else if (GetSection(values, "moreInformation") is {} section && GetField(section, "member_since") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "member since",
                Type = "MONTH_YEAR",
                Id = "member_since",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
    }
    {
        if (values.TryGetValue("customer_service_phone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "customer service phone",
                Type = "PHONE",
                Id = "customer_service_phone",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
        else if (GetSection(values, "moreInformation") is {} section && GetField(section, "customer_service_phone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "customer service phone",
                Type = "PHONE",
                Id = "customer_service_phone",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
    }
    {
        if (values.TryGetValue("reservations_phone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "phone for reservations",
                Type = "PHONE",
                Id = "reservations_phone",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
        else if (GetSection(values, "moreInformation") is {} section && GetField(section, "reservations_phone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "phone for reservations",
                Type = "PHONE",
                Id = "reservations_phone",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
    }
    {
        if (values.TryGetValue("website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
        else if (GetSection(values, "moreInformation") is {} section && GetField(section, "website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "extra", Label = "More Information" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToRewardProgram(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "company_name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("company_name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "member_name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("member_name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "membership_no") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("membership_no", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "pin") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("pin", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "additional_no", "extra") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "extra", "additional_no", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "member_since", "extra") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "extra", "member_since", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "customer_service_phone", "extra") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "extra", "customer_service_phone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "reservations_phone", "extra") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "extra", "reservations_phone", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "website", "extra") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "extra", "website", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToSSHKey(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("private_key", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "private key",
                Type = "SSHKEY",
                Id = "private_key",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "private_key") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "private key",
                Type = "SSHKEY",
                Id = "private_key",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToSSHKey(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "private_key") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("private_key", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToSecureNote(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToSecureNote(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToServer(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("url", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "URL",
                Type = "STRING",
                Id = "url",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "url") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "URL",
                Type = "STRING",
                Id = "url",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "username",
                Type = "STRING",
                Id = "username",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("admin_console_url", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "admin console URL",
                Type = "STRING",
                Id = "admin_console_url",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
        else if (GetSection(values, "adminConsole") is {} section && GetField(section, "admin_console_url") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "admin console URL",
                Type = "STRING",
                Id = "admin_console_url",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
    }
    {
        if (values.TryGetValue("admin_console_username", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "admin console username",
                Type = "STRING",
                Id = "admin_console_username",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
        else if (GetSection(values, "adminConsole") is {} section && GetField(section, "admin_console_username") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "admin console username",
                Type = "STRING",
                Id = "admin_console_username",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
    }
    {
        if (values.TryGetValue("admin_console_password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "console password",
                Type = "CONCEALED",
                Id = "admin_console_password",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
        else if (GetSection(values, "adminConsole") is {} section && GetField(section, "admin_console_password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "console password",
                Type = "CONCEALED",
                Id = "admin_console_password",
                Purpose = null,
                Section = new () { Id = "admin_console", Label = "Admin Console" },
            });
        }
    }
    {
        if (values.TryGetValue("name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
        else if (GetSection(values, "hostingProvider") is {} section && GetField(section, "name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
    }
    {
        if (values.TryGetValue("website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "STRING",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
        else if (GetSection(values, "hostingProvider") is {} section && GetField(section, "website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "STRING",
                Id = "website",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
    }
    {
        if (values.TryGetValue("support_contact_url", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "support URL",
                Type = "STRING",
                Id = "support_contact_url",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
        else if (GetSection(values, "hostingProvider") is {} section && GetField(section, "support_contact_url") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "support URL",
                Type = "STRING",
                Id = "support_contact_url",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
    }
    {
        if (values.TryGetValue("support_contact_phone", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "support phone",
                Type = "STRING",
                Id = "support_contact_phone",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
        else if (GetSection(values, "hostingProvider") is {} section && GetField(section, "support_contact_phone") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "support phone",
                Type = "STRING",
                Id = "support_contact_phone",
                Purpose = null,
                Section = new () { Id = "hosting_provider_details", Label = "Hosting Provider" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToServer(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "url") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("url", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "username") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("username", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "admin_console_url", "admin_console") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "admin_console", "admin_console_url", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "admin_console_username", "admin_console") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "admin_console", "admin_console_username", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "admin_console_password", "admin_console") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "admin_console", "admin_console_password", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "name", "hosting_provider_details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "hosting_provider_details", "name", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "website", "hosting_provider_details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "hosting_provider_details", "website", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "support_contact_url", "hosting_provider_details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "hosting_provider_details", "support_contact_url", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "support_contact_phone", "hosting_provider_details") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "hosting_provider_details", "support_contact_phone", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToSocialSecurityNumber(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("number", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "number",
                Type = "CONCEALED",
                Id = "number",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "number") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "number",
                Type = "CONCEALED",
                Id = "number",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToSocialSecurityNumber(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "number") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("number", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToSoftwareLicense(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("product_version", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "version",
                Type = "STRING",
                Id = "product_version",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "product_version") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "version",
                Type = "STRING",
                Id = "product_version",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("reg_code", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "license key",
                Type = "STRING",
                Id = "reg_code",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "reg_code") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "license key",
                Type = "STRING",
                Id = "reg_code",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("reg_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "licensed to",
                Type = "STRING",
                Id = "reg_name",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
        else if (GetSection(values, "customer") is {} section && GetField(section, "reg_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "licensed to",
                Type = "STRING",
                Id = "reg_name",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
    }
    {
        if (values.TryGetValue("reg_email", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "registered email",
                Type = "EMAIL",
                Id = "reg_email",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
        else if (GetSection(values, "customer") is {} section && GetField(section, "reg_email") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "registered email",
                Type = "EMAIL",
                Id = "reg_email",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
    }
    {
        if (values.TryGetValue("company", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "company",
                Type = "STRING",
                Id = "company",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
        else if (GetSection(values, "customer") is {} section && GetField(section, "company") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "company",
                Type = "STRING",
                Id = "company",
                Purpose = null,
                Section = new () { Id = "customer", Label = "Customer" },
            });
        }
    }
    {
        if (values.TryGetValue("download_link", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "download page",
                Type = "URL",
                Id = "download_link",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
        else if (GetSection(values, "publisher") is {} section && GetField(section, "download_link") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "download page",
                Type = "URL",
                Id = "download_link",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
    }
    {
        if (values.TryGetValue("publisher_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "publisher",
                Type = "STRING",
                Id = "publisher_name",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
        else if (GetSection(values, "publisher") is {} section && GetField(section, "publisher_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "publisher",
                Type = "STRING",
                Id = "publisher_name",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
    }
    {
        if (values.TryGetValue("publisher_website", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "publisher_website",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
        else if (GetSection(values, "publisher") is {} section && GetField(section, "publisher_website") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "website",
                Type = "URL",
                Id = "publisher_website",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
    }
    {
        if (values.TryGetValue("retail_price", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "retail price",
                Type = "STRING",
                Id = "retail_price",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
        else if (GetSection(values, "publisher") is {} section && GetField(section, "retail_price") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "retail price",
                Type = "STRING",
                Id = "retail_price",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
    }
    {
        if (values.TryGetValue("support_email", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "support email",
                Type = "EMAIL",
                Id = "support_email",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
        else if (GetSection(values, "publisher") is {} section && GetField(section, "support_email") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "support email",
                Type = "EMAIL",
                Id = "support_email",
                Purpose = null,
                Section = new () { Id = "publisher", Label = "Publisher" },
            });
        }
    }
    {
        if (values.TryGetValue("order_date", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "purchase date",
                Type = "DATE",
                Id = "order_date",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
        else if (GetSection(values, "order") is {} section && GetField(section, "order_date") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "purchase date",
                Type = "DATE",
                Id = "order_date",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
    }
    {
        if (values.TryGetValue("order_number", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "order number",
                Type = "STRING",
                Id = "order_number",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
        else if (GetSection(values, "order") is {} section && GetField(section, "order_number") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "order number",
                Type = "STRING",
                Id = "order_number",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
    }
    {
        if (values.TryGetValue("order_total", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "order total",
                Type = "STRING",
                Id = "order_total",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
        else if (GetSection(values, "order") is {} section && GetField(section, "order_total") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "order total",
                Type = "STRING",
                Id = "order_total",
                Purpose = null,
                Section = new () { Id = "order", Label = "Order" },
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToSoftwareLicense(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "product_version") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("product_version", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "reg_code") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("reg_code", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "reg_name", "customer") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "customer", "reg_name", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "reg_email", "customer") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "customer", "reg_email", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "company", "customer") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "customer", "company", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "download_link", "publisher") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "publisher", "download_link", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "publisher_name", "publisher") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "publisher", "publisher_name", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "publisher_website", "publisher") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "publisher", "publisher_website", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "retail_price", "publisher") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "publisher", "retail_price", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "support_email", "publisher") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "publisher", "support_email", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "order_date", "order") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "order", "order_date", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "order_number", "order") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "order", "order_number", new PropertyValue(field.Value));;
        }
    }
    {
        if (GetField(template, "order_total", "order") is { } field)
        {
            // TODO date properties, etc.
            AddFieldToSection(outputs, "order", "order_total", new PropertyValue(field.Value));;
        }
    }
        return outputs.ToImmutableDictionary();
    }
    

    public static Inputs TransformInputsToWirelessRouter(IPulumiItemType resourceType, ImmutableDictionary<string, PropertyValue> values)
    {
        var title = GetStringValue(values, "title");
        var category = GetStringValue(values, "category")!;
        var vault = GetVaultName(values);
        var urls = GetUrlValues(values, "urls");
        var tags = GetArrayValues(values, "tags");
        var fields = new List<TemplateField>();
            {
        if (values.TryGetValue("notes", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
        else if (GetField(values, "notes") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "notesPlain",
                Type = "STRING",
                Id = "notesPlain",
                Purpose = "NOTES",
                
            });
        }
    }
    {
        if (values.TryGetValue("name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "base station name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "base station name",
                Type = "STRING",
                Id = "name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "base station password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "base station password",
                Type = "CONCEALED",
                Id = "password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("server", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "server / IP address",
                Type = "STRING",
                Id = "server",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "server") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "server / IP address",
                Type = "STRING",
                Id = "server",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("airport_id", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "AirPort ID",
                Type = "STRING",
                Id = "airport_id",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "airport_id") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "AirPort ID",
                Type = "STRING",
                Id = "airport_id",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("network_name", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "network name",
                Type = "STRING",
                Id = "network_name",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "network_name") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "network name",
                Type = "STRING",
                Id = "network_name",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("wireless_security", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "wireless security",
                Type = "MENU",
                Id = "wireless_security",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "wireless_security") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "wireless security",
                Type = "MENU",
                Id = "wireless_security",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("wireless_password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "wireless network password",
                Type = "CONCEALED",
                Id = "wireless_password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "wireless_password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "wireless network password",
                Type = "CONCEALED",
                Id = "wireless_password",
                Purpose = null,
                
            });
        }
    }
    {
        if (values.TryGetValue("disk_password", out var templateField) && templateField.TryGetString(out var templateValue))
        {
            fields.Add(new TemplateField()
            {
                Value = templateValue ?? "",
                    Label = "attached storage password",
                Type = "CONCEALED",
                Id = "disk_password",
                Purpose = null,
                
            });
        }
        else if (GetField(values, "disk_password") is {} field  && field.TryGetValue("value", out var value) && value.TryGetString(out var stringValue))
        {
            fields.Add(new TemplateField()
            {
                Value = stringValue ?? "",
                    Label = "attached storage password",
                Type = "CONCEALED",
                Id = "disk_password",
                Purpose = null,
                
            });
        }
    }
        fields.AddRange(AssignGenericElements(values, fields));
        return AssignOtherInputs(values, fields, new Inputs()
        {
            Title = title,
            Category = resourceType.ItemName,
            Urls = urls,
            Tags = tags,
            Vault = vault,
            Fields = fields.ToImmutableDictionary(z => z.Id!, z => z)
        });
    }
    public static ImmutableDictionary<string, PropertyValue> TransformOutputsToWirelessRouter(IPulumiItemType resourceType, Item.Response template, ImmutableDictionary<string, PropertyValue>? inputs)
    {
        var outputs = ImmutableDictionary.CreateBuilder<string, PropertyValue>();
        AssignCommonOutputs(outputs, resourceType, template, inputs);
            {
        if (GetField(template, "notes") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("notes", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "server") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("server", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "airport_id") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("airport_id", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "network_name") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("network_name", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "wireless_security") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("wireless_security", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "wireless_password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("wireless_password", new PropertyValue(field.Value));
        }
    }
    {
        if (GetField(template, "disk_password") is { } field)
        {
            // TODO date properties, etc.
            outputs.Add("disk_password", new PropertyValue(field.Value));
        }
    }
        return outputs.ToImmutableDictionary();
    }
    
}
