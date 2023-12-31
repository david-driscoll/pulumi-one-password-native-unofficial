// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import * as utilities from "./utilities";

export class Provider extends pulumi.ProviderResource {
    /** @internal */
    public static readonly __pulumiType = 'one-password-native-unofficial';

    /**
     * Returns true if the given object is an instance of Provider.  This is designed to work even
     * when multiple copies of the Pulumi SDK have been loaded into the same process.
     */
    public static isInstance(obj: any): obj is Provider {
        if (obj === undefined || obj === null) {
            return false;
        }
        return obj['__pulumiType'] === Provider.__pulumiType;
    }

    public readonly connectHost!: pulumi.Output<string | undefined>;
    public readonly connectToken!: pulumi.Output<string | undefined>;
    public readonly serviceAccountToken!: pulumi.Output<string | undefined>;
    /**
     * The UUID of the vault the item is in.
     */
    public readonly vault!: pulumi.Output<string | undefined>;

    /**
     * Create a Provider resource with the given unique name, arguments, and options.
     *
     * @param name The _unique_ name of the resource.
     * @param args The arguments to use to populate this resource's properties.
     * @param opts A bag of options that control this resource's behavior.
     */
    constructor(name: string, args?: ProviderArgs, opts?: pulumi.ResourceOptions) {
        let resourceInputs: pulumi.Inputs = {};
        opts = opts || {};
        {
            resourceInputs["connectHost"] = args ? args.connectHost : undefined;
            resourceInputs["connectToken"] = args ? args.connectToken : undefined;
            resourceInputs["serviceAccountToken"] = args ? args.serviceAccountToken : undefined;
            resourceInputs["vault"] = args ? args.vault : undefined;
        }
        opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
        super(Provider.__pulumiType, name, resourceInputs, opts);
    }
}

/**
 * The set of arguments for constructing a Provider resource.
 */
export interface ProviderArgs {
    connectHost?: pulumi.Input<string>;
    connectToken?: pulumi.Input<string>;
    serviceAccountToken?: pulumi.Input<string>;
    /**
     * The UUID of the vault the item is in.
     */
    vault?: pulumi.Input<string>;
}
