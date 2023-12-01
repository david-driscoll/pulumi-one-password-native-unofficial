// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.ComponentModel;
using Pulumi;

namespace Rocket.Surgery.OnePasswordNativeUnoffical
{
    /// <summary>
    /// The category of the item. One of [ApiCredential, BankAccount, CreditCard, CryptoWallet, Database, Document, DriverLicense, EmailAccount, Identity, Item, Login, MedicalRecord, Membership, OutdoorLicense, Passport, Password, RewardProgram, SshKey, SecureNote, Server, SocialSecurityNumber, SoftwareLicense, WirelessRouter]
    /// </summary>
    [EnumType]
    public readonly struct Category : IEquatable<Category>
    {
        private readonly string _value;

        private Category(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static Category ApiCredential { get; } = new Category("API Credential");
        public static Category BankAccount { get; } = new Category("Bank Account");
        public static Category CreditCard { get; } = new Category("Credit Card");
        public static Category CryptoWallet { get; } = new Category("Crypto Wallet");
        public static Category Database { get; } = new Category("Database");
        public static Category Document { get; } = new Category("Document");
        public static Category DriverLicense { get; } = new Category("Driver License");
        public static Category EmailAccount { get; } = new Category("Email Account");
        public static Category Identity { get; } = new Category("Identity");
        public static Category Item { get; } = new Category("Item");
        public static Category Login { get; } = new Category("Login");
        public static Category MedicalRecord { get; } = new Category("Medical Record");
        public static Category Membership { get; } = new Category("Membership");
        public static Category OutdoorLicense { get; } = new Category("Outdoor License");
        public static Category Passport { get; } = new Category("Passport");
        public static Category Password { get; } = new Category("Password");
        public static Category RewardProgram { get; } = new Category("Reward Program");
        public static Category SshKey { get; } = new Category("SSH Key");
        public static Category SecureNote { get; } = new Category("Secure Note");
        public static Category Server { get; } = new Category("Server");
        public static Category SocialSecurityNumber { get; } = new Category("Social Security Number");
        public static Category SoftwareLicense { get; } = new Category("Software License");
        public static Category WirelessRouter { get; } = new Category("Wireless Router");

        public static bool operator ==(Category left, Category right) => left.Equals(right);
        public static bool operator !=(Category left, Category right) => !left.Equals(right);

        public static explicit operator string(Category value) => value._value;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is Category other && Equals(other);
        public bool Equals(Category other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value;
    }

    [EnumType]
    public readonly struct FieldAssignmentType : IEquatable<FieldAssignmentType>
    {
        private readonly string _value;

        private FieldAssignmentType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static FieldAssignmentType Concealed { get; } = new FieldAssignmentType("concealed");
        public static FieldAssignmentType Text { get; } = new FieldAssignmentType("text");
        public static FieldAssignmentType Email { get; } = new FieldAssignmentType("email");
        public static FieldAssignmentType Url { get; } = new FieldAssignmentType("url");
        public static FieldAssignmentType Date { get; } = new FieldAssignmentType("date");
        public static FieldAssignmentType MonthYear { get; } = new FieldAssignmentType("monthYear");
        public static FieldAssignmentType Phone { get; } = new FieldAssignmentType("phone");

        public static bool operator ==(FieldAssignmentType left, FieldAssignmentType right) => left.Equals(right);
        public static bool operator !=(FieldAssignmentType left, FieldAssignmentType right) => !left.Equals(right);

        public static explicit operator string(FieldAssignmentType value) => value._value;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is FieldAssignmentType other && Equals(other);
        public bool Equals(FieldAssignmentType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value;
    }

    [EnumType]
    public readonly struct FieldPurpose : IEquatable<FieldPurpose>
    {
        private readonly string _value;

        private FieldPurpose(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static FieldPurpose Username { get; } = new FieldPurpose("USERNAME");
        public static FieldPurpose Password { get; } = new FieldPurpose("PASSWORD");
        public static FieldPurpose Note { get; } = new FieldPurpose("NOTE");

        public static bool operator ==(FieldPurpose left, FieldPurpose right) => left.Equals(right);
        public static bool operator !=(FieldPurpose left, FieldPurpose right) => !left.Equals(right);

        public static explicit operator string(FieldPurpose value) => value._value;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is FieldPurpose other && Equals(other);
        public bool Equals(FieldPurpose other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value;
    }

    [EnumType]
    public readonly struct ResponseFieldType : IEquatable<ResponseFieldType>
    {
        private readonly string _value;

        private ResponseFieldType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static ResponseFieldType Unknown { get; } = new ResponseFieldType("UNKNOWN");
        public static ResponseFieldType Address { get; } = new ResponseFieldType("ADDRESS");
        public static ResponseFieldType Concealed { get; } = new ResponseFieldType("CONCEALED");
        public static ResponseFieldType CreditCardNumber { get; } = new ResponseFieldType("CREDIT_CARD_NUMBER");
        public static ResponseFieldType CreditCardType { get; } = new ResponseFieldType("CREDIT_CARD_TYPE");
        public static ResponseFieldType Date { get; } = new ResponseFieldType("Date");
        public static ResponseFieldType Email { get; } = new ResponseFieldType("EMAIL");
        public static ResponseFieldType Gender { get; } = new ResponseFieldType("GENDER");
        public static ResponseFieldType Menu { get; } = new ResponseFieldType("MENU");
        public static ResponseFieldType MonthYear { get; } = new ResponseFieldType("MONTH_YEAR");
        public static ResponseFieldType Otp { get; } = new ResponseFieldType("OTP");
        public static ResponseFieldType Phone { get; } = new ResponseFieldType("PHONE");
        public static ResponseFieldType Reference { get; } = new ResponseFieldType("REFERENCE");
        public static ResponseFieldType String { get; } = new ResponseFieldType("STRING");
        public static ResponseFieldType Url { get; } = new ResponseFieldType("URL");
        public static ResponseFieldType File { get; } = new ResponseFieldType("FILE");
        public static ResponseFieldType SshKey { get; } = new ResponseFieldType("SSHKEY");

        public static bool operator ==(ResponseFieldType left, ResponseFieldType right) => left.Equals(right);
        public static bool operator !=(ResponseFieldType left, ResponseFieldType right) => !left.Equals(right);

        public static explicit operator string(ResponseFieldType value) => value._value;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is ResponseFieldType other && Equals(other);
        public bool Equals(ResponseFieldType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value;
    }
}