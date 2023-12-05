
using System.Collections.Immutable;

namespace pulumi_resource_one_password_native_unoffical;
public static class ItemType
{
    public static string APICredentialItem { get; } = "one-password-native-unoffical:index:APICredentialItem";
public static string BankAccountItem { get; } = "one-password-native-unoffical:index:BankAccountItem";
public static string CreditCardItem { get; } = "one-password-native-unoffical:index:CreditCardItem";
public static string CryptoWalletItem { get; } = "one-password-native-unoffical:index:CryptoWalletItem";
public static string DatabaseItem { get; } = "one-password-native-unoffical:index:DatabaseItem";
public static string DocumentItem { get; } = "one-password-native-unoffical:index:DocumentItem";
public static string DriverLicenseItem { get; } = "one-password-native-unoffical:index:DriverLicenseItem";
public static string EmailAccountItem { get; } = "one-password-native-unoffical:index:EmailAccountItem";
public static string IdentityItem { get; } = "one-password-native-unoffical:index:IdentityItem";
public static string Item { get; } = "one-password-native-unoffical:index:Item";
public static string LoginItem { get; } = "one-password-native-unoffical:index:LoginItem";
public static string MedicalRecordItem { get; } = "one-password-native-unoffical:index:MedicalRecordItem";
public static string MembershipItem { get; } = "one-password-native-unoffical:index:MembershipItem";
public static string OutdoorLicenseItem { get; } = "one-password-native-unoffical:index:OutdoorLicenseItem";
public static string PassportItem { get; } = "one-password-native-unoffical:index:PassportItem";
public static string PasswordItem { get; } = "one-password-native-unoffical:index:PasswordItem";
public static string RewardProgramItem { get; } = "one-password-native-unoffical:index:RewardProgramItem";
public static string SSHKeyItem { get; } = "one-password-native-unoffical:index:SSHKeyItem";
public static string SecureNoteItem { get; } = "one-password-native-unoffical:index:SecureNoteItem";
public static string ServerItem { get; } = "one-password-native-unoffical:index:ServerItem";
public static string SocialSecurityNumberItem { get; } = "one-password-native-unoffical:index:SocialSecurityNumberItem";
public static string SoftwareLicenseItem { get; } = "one-password-native-unoffical:index:SoftwareLicenseItem";
public static string WirelessRouterItem { get; } = "one-password-native-unoffical:index:WirelessRouterItem";
public static string GetItem { get; } = "one-password-native-unoffical:index:GetItem";
public static string GetVault { get; } = "one-password-native-unoffical:index:GetVault";
public static string GetSecretReference { get; } = "one-password-native-unoffical:index:GetSecretReference";
public static string GetAttachment { get; } = "one-password-native-unoffical:index:GetAttachment";
public static string GetAPICredential { get; } = "one-password-native-unoffical:index:GetAPICredential";
public static string GetBankAccount { get; } = "one-password-native-unoffical:index:GetBankAccount";
public static string GetCreditCard { get; } = "one-password-native-unoffical:index:GetCreditCard";
public static string GetCryptoWallet { get; } = "one-password-native-unoffical:index:GetCryptoWallet";
public static string GetDatabase { get; } = "one-password-native-unoffical:index:GetDatabase";
public static string GetDocument { get; } = "one-password-native-unoffical:index:GetDocument";
public static string GetDriverLicense { get; } = "one-password-native-unoffical:index:GetDriverLicense";
public static string GetEmailAccount { get; } = "one-password-native-unoffical:index:GetEmailAccount";
public static string GetIdentity { get; } = "one-password-native-unoffical:index:GetIdentity";
public static string GetLogin { get; } = "one-password-native-unoffical:index:GetLogin";
public static string GetMedicalRecord { get; } = "one-password-native-unoffical:index:GetMedicalRecord";
public static string GetMembership { get; } = "one-password-native-unoffical:index:GetMembership";
public static string GetOutdoorLicense { get; } = "one-password-native-unoffical:index:GetOutdoorLicense";
public static string GetPassport { get; } = "one-password-native-unoffical:index:GetPassport";
public static string GetPassword { get; } = "one-password-native-unoffical:index:GetPassword";
public static string GetRewardProgram { get; } = "one-password-native-unoffical:index:GetRewardProgram";
public static string GetSSHKey { get; } = "one-password-native-unoffical:index:GetSSHKey";
public static string GetSecureNote { get; } = "one-password-native-unoffical:index:GetSecureNote";
public static string GetServer { get; } = "one-password-native-unoffical:index:GetServer";
public static string GetSocialSecurityNumber { get; } = "one-password-native-unoffical:index:GetSocialSecurityNumber";
public static string GetSoftwareLicense { get; } = "one-password-native-unoffical:index:GetSoftwareLicense";
public static string GetWirelessRouter { get; } = "one-password-native-unoffical:index:GetWirelessRouter";
}
public static partial class TemplateMetadata
{
    private static ImmutableArray<_ResourceType> ResourceTypes = [
        new("one-password-native-unoffical:index:APICredentialItem", "API Credential", [("notes", null), ("username", null), ("credential", null), ("type", null), ("filename", null), ("validFrom", null), ("expires", null), ("hostname", null)]),
new("one-password-native-unoffical:index:BankAccountItem", "Bank Account", [("notes", null), ("bankName", null), ("nameOnAccount", null), ("type", null), ("routingNumber", null), ("accountNumber", null), ("swift", null), ("iban", null), ("pin", null), ("phone", "branchInformation"), ("address", "branchInformation")]),
new("one-password-native-unoffical:index:CreditCardItem", "Credit Card", [("notes", null), ("cardholderName", null), ("type", null), ("number", null), ("verificationNumber", null), ("expiryDate", null), ("validFrom", null), ("issuingBank", "contactInformation"), ("phoneLocal", "contactInformation"), ("phoneTollFree", "contactInformation"), ("phoneIntl", "contactInformation"), ("website", "contactInformation"), ("pin", "additionalDetails"), ("creditLimit", "additionalDetails"), ("cashWithdrawalLimit", "additionalDetails"), ("interestRate", "additionalDetails"), ("issueNumber", "additionalDetails")]),
new("one-password-native-unoffical:index:CryptoWalletItem", "Crypto Wallet", [("notes", null), ("recoveryPhrase", null), ("password", null), ("walletAddress", "wallet")]),
new("one-password-native-unoffical:index:DatabaseItem", "Database", [("notes", null), ("type", null), ("server", null), ("port", null), ("database", null), ("username", null), ("password", null), ("sid", null), ("alias", null), ("connectionOptions", null)]),
new("one-password-native-unoffical:index:DocumentItem", "Document", [("notes", null)]),
new("one-password-native-unoffical:index:DriverLicenseItem", "Driver License", [("notes", null), ("fullName", null), ("address", null), ("dateOfBirth", null), ("gender", null), ("height", null), ("number", null), ("licenseClass", null), ("conditionsRestrictions", null), ("state", null), ("country", null), ("expiryDate", null)]),
new("one-password-native-unoffical:index:EmailAccountItem", "Email Account", [("notes", null), ("type", null), ("username", null), ("server", null), ("portNumber", null), ("password", null), ("security", null), ("authMethod", null), ("smtpServer", "smtp"), ("portNumber", "smtp"), ("username", "smtp"), ("password", "smtp"), ("security", "smtp"), ("authMethod", "smtp"), ("provider", "contactInformation"), ("providersWebsite", "contactInformation"), ("phoneLocal", "contactInformation"), ("phoneTollFree", "contactInformation")]),
new("one-password-native-unoffical:index:IdentityItem", "Identity", [("notes", null), ("firstName", "identification"), ("initial", "identification"), ("lastName", "identification"), ("gender", "identification"), ("birthDate", "identification"), ("occupation", "identification"), ("company", "identification"), ("department", "identification"), ("jobTitle", "identification"), ("address", "address"), ("defaultPhone", "address"), ("home", "address"), ("cell", "address"), ("business", "address"), ("username", "internetDetails"), ("reminderQuestion", "internetDetails"), ("reminderAnswer", "internetDetails"), ("email", "internetDetails"), ("website", "internetDetails"), ("icq", "internetDetails"), ("skype", "internetDetails"), ("aolAim", "internetDetails"), ("yahoo", "internetDetails"), ("msn", "internetDetails"), ("forumSignature", "internetDetails")]),
new("one-password-native-unoffical:index:Item", "Item", []),
new("one-password-native-unoffical:index:LoginItem", "Login", [("username", null), ("password", null), ("notes", null)]),
new("one-password-native-unoffical:index:MedicalRecordItem", "Medical Record", [("notes", null), ("date", null), ("location", null), ("healthcareProfessional", null), ("patient", null), ("reasonForVisit", null), ("medication", "medication"), ("dosage", "medication"), ("medicationNotes", "medication")]),
new("one-password-native-unoffical:index:MembershipItem", "Membership", [("notes", null), ("group", null), ("website", null), ("telephone", null), ("memberName", null), ("memberSince", null), ("expiryDate", null), ("memberId", null), ("pin", null)]),
new("one-password-native-unoffical:index:OutdoorLicenseItem", "Outdoor License", [("notes", null), ("fullName", null), ("validFrom", null), ("expires", null), ("approvedWildlife", null), ("maximumQuota", null), ("state", null), ("country", null)]),
new("one-password-native-unoffical:index:PassportItem", "Passport", [("notes", null), ("type", null), ("issuingCountry", null), ("number", null), ("fullName", null), ("gender", null), ("nationality", null), ("issuingAuthority", null), ("dateOfBirth", null), ("placeOfBirth", null), ("issuedOn", null), ("expiryDate", null)]),
new("one-password-native-unoffical:index:PasswordItem", "Password", [("password", null), ("notes", null)]),
new("one-password-native-unoffical:index:RewardProgramItem", "Reward Program", [("notes", null), ("companyName", null), ("memberName", null), ("memberId", null), ("pin", null), ("memberIdAdditional", "moreInformation"), ("memberSince", "moreInformation"), ("customerServicePhone", "moreInformation"), ("phoneForReservations", "moreInformation"), ("website", "moreInformation")]),
new("one-password-native-unoffical:index:SSHKeyItem", "SSH Key", [("notes", null), ("privateKey", null)]),
new("one-password-native-unoffical:index:SecureNoteItem", "Secure Note", [("notes", null)]),
new("one-password-native-unoffical:index:ServerItem", "Server", [("notes", null), ("url", null), ("username", null), ("password", null), ("adminConsoleUrl", "adminConsole"), ("adminConsoleUsername", "adminConsole"), ("consolePassword", "adminConsole"), ("name", "hostingProvider"), ("website", "hostingProvider"), ("supportUrl", "hostingProvider"), ("supportPhone", "hostingProvider")]),
new("one-password-native-unoffical:index:SocialSecurityNumberItem", "Social Security Number", [("notes", null), ("name", null), ("number", null)]),
new("one-password-native-unoffical:index:SoftwareLicenseItem", "Software License", [("notes", null), ("version", null), ("licenseKey", null), ("licensedTo", "customer"), ("registeredEmail", "customer"), ("company", "customer"), ("downloadPage", "publisher"), ("publisher", "publisher"), ("website", "publisher"), ("retailPrice", "publisher"), ("supportEmail", "publisher"), ("purchaseDate", "order"), ("orderNumber", "order"), ("orderTotal", "order")]),
new("one-password-native-unoffical:index:WirelessRouterItem", "Wireless Router", [("notes", null), ("baseStationName", null), ("baseStationPassword", null), ("serverIpAddress", null), ("airPortId", null), ("networkName", null), ("wirelessSecurity", null), ("wirelessNetworkPassword", null), ("attachedStoragePassword", null)])];
    private static ImmutableArray<_FunctionType> FunctionTypes = [
        new("one-password-native-unoffical:index:GetAPICredential", "API Credential", [("notes", null), ("username", null), ("credential", null), ("type", null), ("filename", null), ("validFrom", null), ("expires", null), ("hostname", null)]),
new("one-password-native-unoffical:index:GetBankAccount", "Bank Account", [("notes", null), ("bankName", null), ("nameOnAccount", null), ("type", null), ("routingNumber", null), ("accountNumber", null), ("swift", null), ("iban", null), ("pin", null), ("phone", "branchInformation"), ("address", "branchInformation")]),
new("one-password-native-unoffical:index:GetCreditCard", "Credit Card", [("notes", null), ("cardholderName", null), ("type", null), ("number", null), ("verificationNumber", null), ("expiryDate", null), ("validFrom", null), ("issuingBank", "contactInformation"), ("phoneLocal", "contactInformation"), ("phoneTollFree", "contactInformation"), ("phoneIntl", "contactInformation"), ("website", "contactInformation"), ("pin", "additionalDetails"), ("creditLimit", "additionalDetails"), ("cashWithdrawalLimit", "additionalDetails"), ("interestRate", "additionalDetails"), ("issueNumber", "additionalDetails")]),
new("one-password-native-unoffical:index:GetCryptoWallet", "Crypto Wallet", [("notes", null), ("recoveryPhrase", null), ("password", null), ("walletAddress", "wallet")]),
new("one-password-native-unoffical:index:GetDatabase", "Database", [("notes", null), ("type", null), ("server", null), ("port", null), ("database", null), ("username", null), ("password", null), ("sid", null), ("alias", null), ("connectionOptions", null)]),
new("one-password-native-unoffical:index:GetDocument", "Document", [("notes", null)]),
new("one-password-native-unoffical:index:GetDriverLicense", "Driver License", [("notes", null), ("fullName", null), ("address", null), ("dateOfBirth", null), ("gender", null), ("height", null), ("number", null), ("licenseClass", null), ("conditionsRestrictions", null), ("state", null), ("country", null), ("expiryDate", null)]),
new("one-password-native-unoffical:index:GetEmailAccount", "Email Account", [("notes", null), ("type", null), ("username", null), ("server", null), ("portNumber", null), ("password", null), ("security", null), ("authMethod", null), ("smtpServer", "smtp"), ("portNumber", "smtp"), ("username", "smtp"), ("password", "smtp"), ("security", "smtp"), ("authMethod", "smtp"), ("provider", "contactInformation"), ("providersWebsite", "contactInformation"), ("phoneLocal", "contactInformation"), ("phoneTollFree", "contactInformation")]),
new("one-password-native-unoffical:index:GetIdentity", "Identity", [("notes", null), ("firstName", "identification"), ("initial", "identification"), ("lastName", "identification"), ("gender", "identification"), ("birthDate", "identification"), ("occupation", "identification"), ("company", "identification"), ("department", "identification"), ("jobTitle", "identification"), ("address", "address"), ("defaultPhone", "address"), ("home", "address"), ("cell", "address"), ("business", "address"), ("username", "internetDetails"), ("reminderQuestion", "internetDetails"), ("reminderAnswer", "internetDetails"), ("email", "internetDetails"), ("website", "internetDetails"), ("icq", "internetDetails"), ("skype", "internetDetails"), ("aolAim", "internetDetails"), ("yahoo", "internetDetails"), ("msn", "internetDetails"), ("forumSignature", "internetDetails")]),
new("one-password-native-unoffical:index:GetItem", "Item", []),
new("one-password-native-unoffical:index:GetLogin", "Login", [("username", null), ("password", null), ("notes", null)]),
new("one-password-native-unoffical:index:GetMedicalRecord", "Medical Record", [("notes", null), ("date", null), ("location", null), ("healthcareProfessional", null), ("patient", null), ("reasonForVisit", null), ("medication", "medication"), ("dosage", "medication"), ("medicationNotes", "medication")]),
new("one-password-native-unoffical:index:GetMembership", "Membership", [("notes", null), ("group", null), ("website", null), ("telephone", null), ("memberName", null), ("memberSince", null), ("expiryDate", null), ("memberId", null), ("pin", null)]),
new("one-password-native-unoffical:index:GetOutdoorLicense", "Outdoor License", [("notes", null), ("fullName", null), ("validFrom", null), ("expires", null), ("approvedWildlife", null), ("maximumQuota", null), ("state", null), ("country", null)]),
new("one-password-native-unoffical:index:GetPassport", "Passport", [("notes", null), ("type", null), ("issuingCountry", null), ("number", null), ("fullName", null), ("gender", null), ("nationality", null), ("issuingAuthority", null), ("dateOfBirth", null), ("placeOfBirth", null), ("issuedOn", null), ("expiryDate", null)]),
new("one-password-native-unoffical:index:GetPassword", "Password", [("password", null), ("notes", null)]),
new("one-password-native-unoffical:index:GetRewardProgram", "Reward Program", [("notes", null), ("companyName", null), ("memberName", null), ("memberId", null), ("pin", null), ("memberIdAdditional", "moreInformation"), ("memberSince", "moreInformation"), ("customerServicePhone", "moreInformation"), ("phoneForReservations", "moreInformation"), ("website", "moreInformation")]),
new("one-password-native-unoffical:index:GetSSHKey", "SSH Key", [("notes", null), ("privateKey", null)]),
new("one-password-native-unoffical:index:GetSecureNote", "Secure Note", [("notes", null)]),
new("one-password-native-unoffical:index:GetServer", "Server", [("notes", null), ("url", null), ("username", null), ("password", null), ("adminConsoleUrl", "adminConsole"), ("adminConsoleUsername", "adminConsole"), ("consolePassword", "adminConsole"), ("name", "hostingProvider"), ("website", "hostingProvider"), ("supportUrl", "hostingProvider"), ("supportPhone", "hostingProvider")]),
new("one-password-native-unoffical:index:GetSocialSecurityNumber", "Social Security Number", [("notes", null), ("name", null), ("number", null)]),
new("one-password-native-unoffical:index:GetSoftwareLicense", "Software License", [("notes", null), ("version", null), ("licenseKey", null), ("licensedTo", "customer"), ("registeredEmail", "customer"), ("company", "customer"), ("downloadPage", "publisher"), ("publisher", "publisher"), ("website", "publisher"), ("retailPrice", "publisher"), ("supportEmail", "publisher"), ("purchaseDate", "order"), ("orderNumber", "order"), ("orderTotal", "order")]),
new("one-password-native-unoffical:index:GetWirelessRouter", "Wireless Router", [("notes", null), ("baseStationName", null), ("baseStationPassword", null), ("serverIpAddress", null), ("airPortId", null), ("networkName", null), ("wirelessSecurity", null), ("wirelessNetworkPassword", null), ("attachedStoragePassword", null)])];
}
