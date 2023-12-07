// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import * as utilities from "./utilities";

/**
 * Use this data source to get details of a vault by either its name or uuid.
 */
export function getVault(args: GetVaultArgs, opts?: pulumi.InvokeOptions): Promise<GetVaultResult> {
    if (!opts) {
        opts = {}
    }

    opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
    return pulumi.runtime.invoke("one-password-native-unofficial:index:GetVault", {
        "vault": args.vault,
    }, opts);
}

export interface GetVaultArgs {
    /**
     * The vault to get information of.  Can be either the name or the UUID.
     */
    vault: string;
}

export interface GetVaultResult {
    /**
     * The name of the vault to retrieve. This field will be populated with the name of the vault if the vault it looked up by its UUID.
     */
    readonly name?: string;
    /**
     * The UUID of the vault to retrieve. This field will be populated with the UUID of the vault if the vault it looked up by its name.
     */
    readonly uuid?: string;
}

export function getVaultOutput(args: GetVaultOutputArgs, opts?: pulumi.InvokeOptions): pulumi.Output<GetVaultResult> {
    return pulumi.output(args).apply(a => getVault(a, opts))
}

export interface GetVaultOutputArgs {
    /**
     * The vault to get information of.  Can be either the name or the UUID.
     */
    vault: pulumi.Input<string>;
}
