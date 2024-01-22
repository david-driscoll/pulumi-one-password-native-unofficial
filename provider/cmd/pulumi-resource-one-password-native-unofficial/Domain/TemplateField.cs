using System.ComponentModel;

namespace pulumi_resource_one_password_native_unofficial.Domain;

public record TemplateField
{
    public string? Id { get; init; }
    public string? Label { get; init; }
    public TemplateFieldType Type { get; init; } = TemplateFieldType.String;
    public string? Purpose { get; init; }
    public TemplateSection? Section { get; init; }
    public string? Value { get; set; }
}


    public readonly struct TemplateFieldType : IEquatable<TemplateFieldType>
    {
        private readonly string _value;

        private TemplateFieldType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static TemplateFieldType Unknown { get; } = new TemplateFieldType("UNKNOWN");
        public static TemplateFieldType Address { get; } = new TemplateFieldType("ADDRESS");
        public static TemplateFieldType Concealed { get; } = new TemplateFieldType("CONCEALED");
        public static TemplateFieldType CreditCardNumber { get; } = new TemplateFieldType("CREDIT_CARD_NUMBER");
        public static TemplateFieldType CreditCardType { get; } = new TemplateFieldType("CREDIT_CARD_TYPE");
        public static TemplateFieldType Date { get; } = new TemplateFieldType("DATE");
        public static TemplateFieldType Email { get; } = new TemplateFieldType("EMAIL");
        public static TemplateFieldType Gender { get; } = new TemplateFieldType("GENDER");
        public static TemplateFieldType Menu { get; } = new TemplateFieldType("MENU");
        public static TemplateFieldType MonthYear { get; } = new TemplateFieldType("MONTH_YEAR");
        public static TemplateFieldType Otp { get; } = new TemplateFieldType("OTP");
        public static TemplateFieldType Phone { get; } = new TemplateFieldType("PHONE");
        public static TemplateFieldType Reference { get; } = new TemplateFieldType("REFERENCE");
        public static TemplateFieldType String { get; } = new TemplateFieldType("STRING");
        public static TemplateFieldType Url { get; } = new TemplateFieldType("URL");
        public static TemplateFieldType File { get; } = new TemplateFieldType("FILE");
        public static TemplateFieldType SshKey { get; } = new TemplateFieldType("SSHKEY");

        public static bool operator ==(TemplateFieldType left, TemplateFieldType right) => left.Equals(right);
        public static bool operator !=(TemplateFieldType left, TemplateFieldType right) => !left.Equals(right);

        public static explicit operator string(TemplateFieldType value) => value._value;
        public static implicit operator TemplateFieldType(string? value) => value is null ? String : new (value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is TemplateFieldType other && Equals(other);
        public bool Equals(TemplateFieldType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value;
    }
