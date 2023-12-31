// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export function getMedicalRecord(args: GetMedicalRecordArgs, opts?: pulumi.InvokeOptions): Promise<GetMedicalRecordResult> {
    if (!opts) {
        opts = {}
    }

    opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
    return pulumi.runtime.invoke("one-password-native-unofficial:index:GetMedicalRecord", {
        "id": args.id,
        "title": args.title,
        "vault": args.vault,
    }, opts);
}

export interface GetMedicalRecordArgs {
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    id?: string;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: string;
    /**
     * The UUID of the vault the item is in.
     */
    vault: string;
}

export interface GetMedicalRecordResult {
    readonly attachments: {[key: string]: outputs.OutputAttachment};
    readonly category: enums.Category | string;
    readonly date?: string;
    readonly fields: {[key: string]: outputs.OutputField};
    readonly healthcareProfessional?: string;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    readonly id: string;
    readonly location?: string;
    readonly medication?: outputs.medicalRecord.MedicationSection;
    readonly notes?: string;
    readonly patient?: string;
    readonly reasonForVisit?: string;
    readonly references: outputs.OutputReference[];
    readonly sections: {[key: string]: outputs.OutputSection};
    /**
     * An array of strings of the tags assigned to the item.
     */
    readonly tags: string[];
    /**
     * The title of the item.
     */
    readonly title: string;
    readonly urls?: outputs.OutputUrl[];
    readonly vault: outputs.OutputVault;
}

export function getMedicalRecordOutput(args: GetMedicalRecordOutputArgs, opts?: pulumi.InvokeOptions): pulumi.Output<GetMedicalRecordResult> {
    return pulumi.output(args).apply(a => getMedicalRecord(a, opts))
}

export interface GetMedicalRecordOutputArgs {
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    id?: pulumi.Input<string>;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}
