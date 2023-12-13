
using System.Collections.Immutable;
using pulumi_resource_one_password_native_unofficial;

// ReSharper disable once CheckNamespace
namespace pulumi_resource_one_password_native_unofficial.Domain;

public partial record ResourceType
{
    public static ResourceType APICredential { get; } = new("one-password-native-unofficial:index:APICredentialItem", "API Credential", "API_CREDENTIAL", TemplateMetadata.TransformInputsToAPICredential, TemplateMetadata.TransformOutputsToAPICredential);
public static ResourceType BankAccount { get; } = new("one-password-native-unofficial:index:BankAccountItem", "Bank Account", "BANK_ACCOUNT", TemplateMetadata.TransformInputsToBankAccount, TemplateMetadata.TransformOutputsToBankAccount);
public static ResourceType CreditCard { get; } = new("one-password-native-unofficial:index:CreditCardItem", "Credit Card", "CREDIT_CARD", TemplateMetadata.TransformInputsToCreditCard, TemplateMetadata.TransformOutputsToCreditCard);
public static ResourceType CryptoWallet { get; } = new("one-password-native-unofficial:index:CryptoWalletItem", "Crypto Wallet", "CUSTOM", TemplateMetadata.TransformInputsToCryptoWallet, TemplateMetadata.TransformOutputsToCryptoWallet);
public static ResourceType Database { get; } = new("one-password-native-unofficial:index:DatabaseItem", "Database", "DATABASE", TemplateMetadata.TransformInputsToDatabase, TemplateMetadata.TransformOutputsToDatabase);
public static ResourceType Document { get; } = new("one-password-native-unofficial:index:DocumentItem", "Document", "DOCUMENT", TemplateMetadata.TransformInputsToDocument, TemplateMetadata.TransformOutputsToDocument);
public static ResourceType DriverLicense { get; } = new("one-password-native-unofficial:index:DriverLicenseItem", "Driver License", "DRIVER_LICENSE", TemplateMetadata.TransformInputsToDriverLicense, TemplateMetadata.TransformOutputsToDriverLicense);
public static ResourceType EmailAccount { get; } = new("one-password-native-unofficial:index:EmailAccountItem", "Email Account", "EMAIL_ACCOUNT", TemplateMetadata.TransformInputsToEmailAccount, TemplateMetadata.TransformOutputsToEmailAccount);
public static ResourceType Identity { get; } = new("one-password-native-unofficial:index:IdentityItem", "Identity", "IDENTITY", TemplateMetadata.TransformInputsToIdentity, TemplateMetadata.TransformOutputsToIdentity);
public static ResourceType Item { get; } = new("one-password-native-unofficial:index:Item", "Secure Note", "SECURE_NOTE", TemplateMetadata.TransformInputsToItem, TemplateMetadata.TransformOutputsToItem);
public static ResourceType Login { get; } = new("one-password-native-unofficial:index:LoginItem", "Login", "LOGIN", TemplateMetadata.TransformInputsToLogin, TemplateMetadata.TransformOutputsToLogin);
public static ResourceType MedicalRecord { get; } = new("one-password-native-unofficial:index:MedicalRecordItem", "Medical Record", "MEDICAL_RECORD", TemplateMetadata.TransformInputsToMedicalRecord, TemplateMetadata.TransformOutputsToMedicalRecord);
public static ResourceType Membership { get; } = new("one-password-native-unofficial:index:MembershipItem", "Membership", "MEMBERSHIP", TemplateMetadata.TransformInputsToMembership, TemplateMetadata.TransformOutputsToMembership);
public static ResourceType OutdoorLicense { get; } = new("one-password-native-unofficial:index:OutdoorLicenseItem", "Outdoor License", "OUTDOOR_LICENSE", TemplateMetadata.TransformInputsToOutdoorLicense, TemplateMetadata.TransformOutputsToOutdoorLicense);
public static ResourceType Passport { get; } = new("one-password-native-unofficial:index:PassportItem", "Passport", "PASSPORT", TemplateMetadata.TransformInputsToPassport, TemplateMetadata.TransformOutputsToPassport);
public static ResourceType Password { get; } = new("one-password-native-unofficial:index:PasswordItem", "Password", "PASSWORD", TemplateMetadata.TransformInputsToPassword, TemplateMetadata.TransformOutputsToPassword);
public static ResourceType RewardProgram { get; } = new("one-password-native-unofficial:index:RewardProgramItem", "Reward Program", "REWARD_PROGRAM", TemplateMetadata.TransformInputsToRewardProgram, TemplateMetadata.TransformOutputsToRewardProgram);
public static ResourceType SSHKey { get; } = new("one-password-native-unofficial:index:SSHKeyItem", "SSH Key", "SSH_KEY", TemplateMetadata.TransformInputsToSSHKey, TemplateMetadata.TransformOutputsToSSHKey);
public static ResourceType SecureNote { get; } = new("one-password-native-unofficial:index:SecureNoteItem", "Secure Note", "SECURE_NOTE", TemplateMetadata.TransformInputsToSecureNote, TemplateMetadata.TransformOutputsToSecureNote);
public static ResourceType Server { get; } = new("one-password-native-unofficial:index:ServerItem", "Server", "SERVER", TemplateMetadata.TransformInputsToServer, TemplateMetadata.TransformOutputsToServer);
public static ResourceType SocialSecurityNumber { get; } = new("one-password-native-unofficial:index:SocialSecurityNumberItem", "Social Security Number", "SOCIAL_SECURITY_NUMBER", TemplateMetadata.TransformInputsToSocialSecurityNumber, TemplateMetadata.TransformOutputsToSocialSecurityNumber);
public static ResourceType SoftwareLicense { get; } = new("one-password-native-unofficial:index:SoftwareLicenseItem", "Software License", "SOFTWARE_LICENSE", TemplateMetadata.TransformInputsToSoftwareLicense, TemplateMetadata.TransformOutputsToSoftwareLicense);
public static ResourceType WirelessRouter { get; } = new("one-password-native-unofficial:index:WirelessRouterItem", "Wireless Router", "WIRELESS_ROUTER", TemplateMetadata.TransformInputsToWirelessRouter, TemplateMetadata.TransformOutputsToWirelessRouter);
}

public partial record FunctionType
{
    public static FunctionType GetAPICredential { get; } = new("one-password-native-unofficial:index:GetAPICredential", "API Credential", "API_CREDENTIAL", TemplateMetadata.TransformOutputsToAPICredential);
public static FunctionType GetBankAccount { get; } = new("one-password-native-unofficial:index:GetBankAccount", "Bank Account", "BANK_ACCOUNT", TemplateMetadata.TransformOutputsToBankAccount);
public static FunctionType GetCreditCard { get; } = new("one-password-native-unofficial:index:GetCreditCard", "Credit Card", "CREDIT_CARD", TemplateMetadata.TransformOutputsToCreditCard);
public static FunctionType GetCryptoWallet { get; } = new("one-password-native-unofficial:index:GetCryptoWallet", "Crypto Wallet", "CUSTOM", TemplateMetadata.TransformOutputsToCryptoWallet);
public static FunctionType GetDatabase { get; } = new("one-password-native-unofficial:index:GetDatabase", "Database", "DATABASE", TemplateMetadata.TransformOutputsToDatabase);
public static FunctionType GetDocument { get; } = new("one-password-native-unofficial:index:GetDocument", "Document", "DOCUMENT", TemplateMetadata.TransformOutputsToDocument);
public static FunctionType GetDriverLicense { get; } = new("one-password-native-unofficial:index:GetDriverLicense", "Driver License", "DRIVER_LICENSE", TemplateMetadata.TransformOutputsToDriverLicense);
public static FunctionType GetEmailAccount { get; } = new("one-password-native-unofficial:index:GetEmailAccount", "Email Account", "EMAIL_ACCOUNT", TemplateMetadata.TransformOutputsToEmailAccount);
public static FunctionType GetIdentity { get; } = new("one-password-native-unofficial:index:GetIdentity", "Identity", "IDENTITY", TemplateMetadata.TransformOutputsToIdentity);
public static FunctionType GetItem { get; } = new("one-password-native-unofficial:index:GetItem", "Secure Note", "SECURE_NOTE", TemplateMetadata.TransformOutputsToItem);
public static FunctionType GetLogin { get; } = new("one-password-native-unofficial:index:GetLogin", "Login", "LOGIN", TemplateMetadata.TransformOutputsToLogin);
public static FunctionType GetMedicalRecord { get; } = new("one-password-native-unofficial:index:GetMedicalRecord", "Medical Record", "MEDICAL_RECORD", TemplateMetadata.TransformOutputsToMedicalRecord);
public static FunctionType GetMembership { get; } = new("one-password-native-unofficial:index:GetMembership", "Membership", "MEMBERSHIP", TemplateMetadata.TransformOutputsToMembership);
public static FunctionType GetOutdoorLicense { get; } = new("one-password-native-unofficial:index:GetOutdoorLicense", "Outdoor License", "OUTDOOR_LICENSE", TemplateMetadata.TransformOutputsToOutdoorLicense);
public static FunctionType GetPassport { get; } = new("one-password-native-unofficial:index:GetPassport", "Passport", "PASSPORT", TemplateMetadata.TransformOutputsToPassport);
public static FunctionType GetPassword { get; } = new("one-password-native-unofficial:index:GetPassword", "Password", "PASSWORD", TemplateMetadata.TransformOutputsToPassword);
public static FunctionType GetRewardProgram { get; } = new("one-password-native-unofficial:index:GetRewardProgram", "Reward Program", "REWARD_PROGRAM", TemplateMetadata.TransformOutputsToRewardProgram);
public static FunctionType GetSSHKey { get; } = new("one-password-native-unofficial:index:GetSSHKey", "SSH Key", "SSH_KEY", TemplateMetadata.TransformOutputsToSSHKey);
public static FunctionType GetSecureNote { get; } = new("one-password-native-unofficial:index:GetSecureNote", "Secure Note", "SECURE_NOTE", TemplateMetadata.TransformOutputsToSecureNote);
public static FunctionType GetServer { get; } = new("one-password-native-unofficial:index:GetServer", "Server", "SERVER", TemplateMetadata.TransformOutputsToServer);
public static FunctionType GetSocialSecurityNumber { get; } = new("one-password-native-unofficial:index:GetSocialSecurityNumber", "Social Security Number", "SOCIAL_SECURITY_NUMBER", TemplateMetadata.TransformOutputsToSocialSecurityNumber);
public static FunctionType GetSoftwareLicense { get; } = new("one-password-native-unofficial:index:GetSoftwareLicense", "Software License", "SOFTWARE_LICENSE", TemplateMetadata.TransformOutputsToSoftwareLicense);
public static FunctionType GetWirelessRouter { get; } = new("one-password-native-unofficial:index:GetWirelessRouter", "Wireless Router", "WIRELESS_ROUTER", TemplateMetadata.TransformOutputsToWirelessRouter);
}


public static partial class TemplateMetadata
{
    private static ImmutableArray<ResourceType> ResourceTypes = [
        ResourceType.APICredential,
ResourceType.BankAccount,
ResourceType.CreditCard,
ResourceType.CryptoWallet,
ResourceType.Database,
ResourceType.Document,
ResourceType.DriverLicense,
ResourceType.EmailAccount,
ResourceType.Identity,
ResourceType.Item,
ResourceType.Login,
ResourceType.MedicalRecord,
ResourceType.Membership,
ResourceType.OutdoorLicense,
ResourceType.Passport,
ResourceType.Password,
ResourceType.RewardProgram,
ResourceType.SSHKey,
ResourceType.SecureNote,
ResourceType.Server,
ResourceType.SocialSecurityNumber,
ResourceType.SoftwareLicense,
ResourceType.WirelessRouter];
    private static ImmutableArray<FunctionType> FunctionTypes = [
        FunctionType.GetAPICredential,
FunctionType.GetBankAccount,
FunctionType.GetCreditCard,
FunctionType.GetCryptoWallet,
FunctionType.GetDatabase,
FunctionType.GetDocument,
FunctionType.GetDriverLicense,
FunctionType.GetEmailAccount,
FunctionType.GetIdentity,
FunctionType.GetItem,
FunctionType.GetLogin,
FunctionType.GetMedicalRecord,
FunctionType.GetMembership,
FunctionType.GetOutdoorLicense,
FunctionType.GetPassport,
FunctionType.GetPassword,
FunctionType.GetRewardProgram,
FunctionType.GetSSHKey,
FunctionType.GetSecureNote,
FunctionType.GetServer,
FunctionType.GetSocialSecurityNumber,
FunctionType.GetSoftwareLicense,
FunctionType.GetWirelessRouter,
    FunctionType.GetVault,
    FunctionType.GetSecretReference,
    FunctionType.Read,
    FunctionType.Inject,
    FunctionType.GetAttachment,
    ];
}
