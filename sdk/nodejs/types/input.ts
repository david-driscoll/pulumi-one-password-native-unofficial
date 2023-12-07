// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "../types";

import * as utilities from "../utilities";

export interface FieldArgs {
    type?: pulumi.Input<enums.FieldType>;
    value: pulumi.Input<string>;
}
/**
 * fieldArgsProvideDefaults sets the appropriate defaults for FieldArgs
 */
export function fieldArgsProvideDefaults(val: FieldArgs): FieldArgs {
    return {
        ...val,
        type: (val.type) ?? "STRING",
    };
}

export interface PasswordRecipeArgs {
    digits?: pulumi.Input<boolean>;
    length: pulumi.Input<number>;
    letters?: pulumi.Input<boolean>;
    symbols?: pulumi.Input<boolean>;
}

export interface SectionArgs {
    attachments?: pulumi.Input<{[key: string]: pulumi.Input<pulumi.asset.Asset | pulumi.asset.Archive>}>;
    fields: pulumi.Input<{[key: string]: pulumi.Input<inputs.FieldArgs>}>;
}

export namespace bankAccount {
    export interface BranchInformationSectionArgs {
        address?: pulumi.Input<string>;
        phone?: pulumi.Input<string>;
    }
}

export namespace creditCard {
    export interface AdditionalDetailsSectionArgs {
        cashWithdrawalLimit?: pulumi.Input<string>;
        creditLimit?: pulumi.Input<string>;
        interestRate?: pulumi.Input<string>;
        issueNumber?: pulumi.Input<string>;
        pin?: pulumi.Input<string>;
    }

    export interface ContactInformationSectionArgs {
        issuingBank?: pulumi.Input<string>;
        phoneIntl?: pulumi.Input<string>;
        phoneLocal?: pulumi.Input<string>;
        phoneTollFree?: pulumi.Input<string>;
        website?: pulumi.Input<string>;
    }
}

export namespace cryptoWallet {
    export interface WalletSectionArgs {
        walletAddress?: pulumi.Input<string>;
    }
}

export namespace emailAccount {
    export interface ContactInformationSectionArgs {
        phoneLocal?: pulumi.Input<string>;
        phoneTollFree?: pulumi.Input<string>;
        provider?: pulumi.Input<string>;
        providersWebsite?: pulumi.Input<string>;
    }

    export interface SmtpSectionArgs {
        authMethod?: pulumi.Input<string>;
        password?: pulumi.Input<string>;
        portNumber?: pulumi.Input<string>;
        security?: pulumi.Input<string>;
        smtpServer?: pulumi.Input<string>;
        username?: pulumi.Input<string>;
    }
}

export namespace identity {
    export interface AddressSectionArgs {
        address?: pulumi.Input<string>;
        business?: pulumi.Input<string>;
        cell?: pulumi.Input<string>;
        defaultPhone?: pulumi.Input<string>;
        home?: pulumi.Input<string>;
    }

    export interface IdentificationSectionArgs {
        birthDate?: pulumi.Input<string>;
        company?: pulumi.Input<string>;
        department?: pulumi.Input<string>;
        firstName?: pulumi.Input<string>;
        gender?: pulumi.Input<string>;
        initial?: pulumi.Input<string>;
        jobTitle?: pulumi.Input<string>;
        lastName?: pulumi.Input<string>;
        occupation?: pulumi.Input<string>;
    }

    export interface InternetDetailsSectionArgs {
        aolAim?: pulumi.Input<string>;
        email?: pulumi.Input<string>;
        forumSignature?: pulumi.Input<string>;
        icq?: pulumi.Input<string>;
        msn?: pulumi.Input<string>;
        reminderAnswer?: pulumi.Input<string>;
        reminderQuestion?: pulumi.Input<string>;
        skype?: pulumi.Input<string>;
        username?: pulumi.Input<string>;
        website?: pulumi.Input<string>;
        yahoo?: pulumi.Input<string>;
    }
}

export namespace medicalRecord {
    export interface MedicationSectionArgs {
        dosage?: pulumi.Input<string>;
        medication?: pulumi.Input<string>;
        medicationNotes?: pulumi.Input<string>;
    }
}

export namespace rewardProgram {
    export interface MoreInformationSectionArgs {
        customerServicePhone?: pulumi.Input<string>;
        memberIdAdditional?: pulumi.Input<string>;
        memberSince?: pulumi.Input<string>;
        phoneForReservations?: pulumi.Input<string>;
        website?: pulumi.Input<string>;
    }
}

export namespace server {
    export interface AdminConsoleSectionArgs {
        adminConsoleUrl?: pulumi.Input<string>;
        adminConsoleUsername?: pulumi.Input<string>;
        consolePassword?: pulumi.Input<string>;
    }

    export interface HostingProviderSectionArgs {
        name?: pulumi.Input<string>;
        supportPhone?: pulumi.Input<string>;
        supportUrl?: pulumi.Input<string>;
        website?: pulumi.Input<string>;
    }
}

export namespace softwareLicense {
    export interface CustomerSectionArgs {
        company?: pulumi.Input<string>;
        licensedTo?: pulumi.Input<string>;
        registeredEmail?: pulumi.Input<string>;
    }

    export interface OrderSectionArgs {
        orderNumber?: pulumi.Input<string>;
        orderTotal?: pulumi.Input<string>;
        purchaseDate?: pulumi.Input<string>;
    }

    export interface PublisherSectionArgs {
        downloadPage?: pulumi.Input<string>;
        publisher?: pulumi.Input<string>;
        retailPrice?: pulumi.Input<string>;
        supportEmail?: pulumi.Input<string>;
        website?: pulumi.Input<string>;
    }
}
