// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export function getIdentity(args: GetIdentityArgs, opts?: pulumi.InvokeOptions): Promise<GetIdentityResult> {
    if (!opts) {
        opts = {}
    }

    opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
    return pulumi.runtime.invoke("one-password-native-unofficial:index:GetIdentity", {
        "id": args.id,
        "title": args.title,
        "vault": args.vault,
    }, opts);
}

export interface GetIdentityArgs {
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

export interface GetIdentityResult {
    readonly address?: outputs.identity.AddressSection;
    readonly attachments: {[key: string]: outputs.OutputAttachment};
    readonly category: enums.Category | string;
    readonly fields: {[key: string]: outputs.OutputField};
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    readonly id: string;
    readonly identification?: outputs.identity.IdentificationSection;
    readonly internetDetails?: outputs.identity.InternetDetailsSection;
    readonly notes?: string;
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

export function getIdentityOutput(args: GetIdentityOutputArgs, opts?: pulumi.InvokeOptions): pulumi.Output<GetIdentityResult> {
    return pulumi.output(args).apply(a => getIdentity(a, opts))
}

export interface GetIdentityOutputArgs {
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
