// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import * as utilities from "./utilities";

// Export members:
export * from "./apicredentialItem";
export * from "./bankAccountItem";
export * from "./creditCardItem";
export * from "./cryptoWalletItem";
export * from "./databaseItem";
export * from "./documentItem";
export * from "./driverLicenseItem";
export * from "./emailAccountItem";
export * from "./getAPICredential";
export * from "./getAttachment";
export * from "./getBankAccount";
export * from "./getCreditCard";
export * from "./getCryptoWallet";
export * from "./getDatabase";
export * from "./getDocument";
export * from "./getDriverLicense";
export * from "./getEmailAccount";
export * from "./getIdentity";
export * from "./getItem";
export * from "./getLogin";
export * from "./getMedicalRecord";
export * from "./getMembership";
export * from "./getOutdoorLicense";
export * from "./getPassport";
export * from "./getPassword";
export * from "./getRewardProgram";
export * from "./getSSHKey";
export * from "./getSecretReference";
export * from "./getSecureNote";
export * from "./getServer";
export * from "./getSocialSecurityNumber";
export * from "./getSoftwareLicense";
export * from "./getVault";
export * from "./getWirelessRouter";
export * from "./identityItem";
export * from "./item";
export * from "./loginItem";
export * from "./medicalRecordItem";
export * from "./membershipItem";
export * from "./outdoorLicenseItem";
export * from "./passportItem";
export * from "./passwordItem";
export * from "./provider";
export * from "./rewardProgramItem";
export * from "./secureNoteItem";
export * from "./serverItem";
export * from "./socialSecurityNumberItem";
export * from "./softwareLicenseItem";
export * from "./sshkeyItem";
export * from "./wirelessRouterItem";

// Export enums:
export * from "./types/enums";

// Export sub-modules:
import * as config from "./config";
import * as types from "./types";

export {
    config,
    types,
};

// Import resources to register:
import { APICredentialItem } from "./apicredentialItem";
import { BankAccountItem } from "./bankAccountItem";
import { CreditCardItem } from "./creditCardItem";
import { CryptoWalletItem } from "./cryptoWalletItem";
import { DatabaseItem } from "./databaseItem";
import { DocumentItem } from "./documentItem";
import { DriverLicenseItem } from "./driverLicenseItem";
import { EmailAccountItem } from "./emailAccountItem";
import { IdentityItem } from "./identityItem";
import { Item } from "./item";
import { LoginItem } from "./loginItem";
import { MedicalRecordItem } from "./medicalRecordItem";
import { MembershipItem } from "./membershipItem";
import { OutdoorLicenseItem } from "./outdoorLicenseItem";
import { PassportItem } from "./passportItem";
import { PasswordItem } from "./passwordItem";
import { RewardProgramItem } from "./rewardProgramItem";
import { SSHKeyItem } from "./sshkeyItem";
import { SecureNoteItem } from "./secureNoteItem";
import { ServerItem } from "./serverItem";
import { SocialSecurityNumberItem } from "./socialSecurityNumberItem";
import { SoftwareLicenseItem } from "./softwareLicenseItem";
import { WirelessRouterItem } from "./wirelessRouterItem";

const _module = {
    version: utilities.getVersion(),
    construct: (name: string, type: string, urn: string): pulumi.Resource => {
        switch (type) {
            case "one-password-native-unofficial:index:APICredentialItem":
                return new APICredentialItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:BankAccountItem":
                return new BankAccountItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:CreditCardItem":
                return new CreditCardItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:CryptoWalletItem":
                return new CryptoWalletItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:DatabaseItem":
                return new DatabaseItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:DocumentItem":
                return new DocumentItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:DriverLicenseItem":
                return new DriverLicenseItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:EmailAccountItem":
                return new EmailAccountItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:IdentityItem":
                return new IdentityItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:Item":
                return new Item(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:LoginItem":
                return new LoginItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:MedicalRecordItem":
                return new MedicalRecordItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:MembershipItem":
                return new MembershipItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:OutdoorLicenseItem":
                return new OutdoorLicenseItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:PassportItem":
                return new PassportItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:PasswordItem":
                return new PasswordItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:RewardProgramItem":
                return new RewardProgramItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:SSHKeyItem":
                return new SSHKeyItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:SecureNoteItem":
                return new SecureNoteItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:ServerItem":
                return new ServerItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:SocialSecurityNumberItem":
                return new SocialSecurityNumberItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:SoftwareLicenseItem":
                return new SoftwareLicenseItem(name, <any>undefined, { urn })
            case "one-password-native-unofficial:index:WirelessRouterItem":
                return new WirelessRouterItem(name, <any>undefined, { urn })
            default:
                throw new Error(`unknown resource type ${type}`);
        }
    },
};
pulumi.runtime.registerResourceModule("one-password-native-unofficial", "index", _module)

import { Provider } from "./provider";

pulumi.runtime.registerResourcePackage("one-password-native-unofficial", {
    version: utilities.getVersion(),
    constructProvider: (name: string, type: string, urn: string): pulumi.ProviderResource => {
        if (type !== "pulumi:providers:one-password-native-unofficial") {
            throw new Error(`unknown provider type ${type}`);
        }
        return new Provider(name, <any>undefined, { urn });
    },
});
