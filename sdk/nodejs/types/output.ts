// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "../types";

import * as utilities from "../utilities";

export interface OutputAttachment {
    name: string;
    reference: string;
    size: number;
    uuid: string;
}

export interface OutputField {
    data: {[key: string]: any};
    label: string;
    reference: string;
    type: enums.FieldType;
    uuid: string;
    value: string;
}

export interface OutputReference {
    itemId: string;
    label: string;
    reference: string;
    uuid: string;
}

export interface OutputSection {
    attachments?: {[key: string]: outputs.OutputAttachment};
    fields: {[key: string]: outputs.OutputField};
    label: string;
    uuid: string;
}

export interface OutputUrl {
    href: string;
    label?: string;
    primary: boolean;
}

export namespace bankAccount {
    export interface BranchInformationSection {
        address?: string;
        phone?: string;
    }

}

export namespace creditCard {
    export interface AdditionalDetailsSection {
        cashWithdrawalLimit?: string;
        creditLimit?: string;
        interestRate?: string;
        issueNumber?: string;
        pin?: string;
    }

    export interface ContactInformationSection {
        issuingBank?: string;
        phoneIntl?: string;
        phoneLocal?: string;
        phoneTollFree?: string;
        website?: string;
    }

}

export namespace cryptoWallet {
    export interface WalletSection {
        walletAddress?: string;
    }

}

export namespace emailAccount {
    export interface ContactInformationSection {
        phoneLocal?: string;
        phoneTollFree?: string;
        provider?: string;
        providersWebsite?: string;
    }

    export interface SmtpSection {
        authMethod?: string;
        password?: string;
        portNumber?: string;
        security?: string;
        smtpServer?: string;
        username?: string;
    }

}

export namespace identity {
    export interface AddressSection {
        address?: string;
        business?: string;
        cell?: string;
        defaultPhone?: string;
        home?: string;
    }

    export interface IdentificationSection {
        birthDate?: string;
        company?: string;
        department?: string;
        firstName?: string;
        gender?: string;
        initial?: string;
        jobTitle?: string;
        lastName?: string;
        occupation?: string;
    }

    export interface InternetDetailsSection {
        aolAim?: string;
        email?: string;
        forumSignature?: string;
        icq?: string;
        msn?: string;
        reminderAnswer?: string;
        reminderQuestion?: string;
        skype?: string;
        username?: string;
        website?: string;
        yahoo?: string;
    }

}

export namespace medicalRecord {
    export interface MedicationSection {
        dosage?: string;
        medication?: string;
        medicationNotes?: string;
    }

}

export namespace rewardProgram {
    export interface MoreInformationSection {
        customerServicePhone?: string;
        memberIdAdditional?: string;
        memberSince?: string;
        phoneForReservations?: string;
        website?: string;
    }

}

export namespace server {
    export interface AdminConsoleSection {
        adminConsoleUrl?: string;
        adminConsoleUsername?: string;
        consolePassword?: string;
    }

    export interface HostingProviderSection {
        name?: string;
        supportPhone?: string;
        supportUrl?: string;
        website?: string;
    }

}

export namespace softwareLicense {
    export interface CustomerSection {
        company?: string;
        licensedTo?: string;
        registeredEmail?: string;
    }

    export interface OrderSection {
        orderNumber?: string;
        orderTotal?: string;
        purchaseDate?: string;
    }

    export interface PublisherSection {
        downloadPage?: string;
        publisher?: string;
        retailPrice?: string;
        supportEmail?: string;
        website?: string;
    }

}
