// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export function getCryptoWallet(args: GetCryptoWalletArgs, opts?: pulumi.InvokeOptions): Promise<GetCryptoWalletResult> {
    if (!opts) {
        opts = {}
    }

    opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
    return pulumi.runtime.invoke("one-password-native-unofficial:index:GetCryptoWallet", {
        "title": args.title,
        "uuid": args.uuid,
        "vault": args.vault,
    }, opts);
}

export interface GetCryptoWalletArgs {
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: string;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    uuid?: string;
    /**
     * The UUID of the vault the item is in.
     */
    vault: string;
}

export interface GetCryptoWalletResult {
    readonly attachments: {[key: string]: outputs.OutputAttachment};
    readonly category: enums.Category | string;
    readonly fields: {[key: string]: outputs.OutputField};
    readonly notes?: string;
    readonly password?: string;
    readonly recoveryPhrase?: string;
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
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    readonly uuid: string;
    readonly vault: {[key: string]: string};
    readonly wallet?: outputs.cryptoWallet.WalletSection;
}

export function getCryptoWalletOutput(args: GetCryptoWalletOutputArgs, opts?: pulumi.InvokeOptions): pulumi.Output<GetCryptoWalletResult> {
    return pulumi.output(args).apply(a => getCryptoWallet(a, opts))
}

export interface GetCryptoWalletOutputArgs {
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    uuid?: pulumi.Input<string>;
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}
