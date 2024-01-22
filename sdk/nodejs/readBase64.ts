// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import * as utilities from "./utilities";

export function readBase64(args: ReadBase64Args, opts?: pulumi.InvokeOptions): Promise<ReadBase64Result> {
    if (!opts) {
        opts = {}
    }

    opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
    return pulumi.runtime.invoke("one-password-native-unofficial:index:ReadBase64", {
        "reference": args.reference,
    }, opts);
}

export interface ReadBase64Args {
    /**
     * The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file 
     */
    reference: string;
}

export interface ReadBase64Result {
    /**
     * The read value as a base64 encoded string
     */
    readonly base64: string;
}

export function readBase64Output(args: ReadBase64OutputArgs, opts?: pulumi.InvokeOptions): pulumi.Output<ReadBase64Result> {
    return pulumi.output(args).apply(a => readBase64(a, opts))
}

export interface ReadBase64OutputArgs {
    /**
     * The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file 
     */
    reference: pulumi.Input<string>;
}