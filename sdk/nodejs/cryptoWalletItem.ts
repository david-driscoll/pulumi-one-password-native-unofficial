// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export class CryptoWalletItem extends pulumi.CustomResource {
    /**
     * Get an existing CryptoWalletItem resource's state with the given name, ID, and optional extra
     * properties used to qualify the lookup.
     *
     * @param name The _unique_ name of the resulting resource.
     * @param id The _unique_ provider ID of the resource to lookup.
     * @param state Any extra arguments used during the lookup.
     * @param opts Optional settings to control the behavior of the CustomResource.
     */
    public static get(name: string, id: pulumi.Input<pulumi.ID>, state?: CryptoWalletItemState, opts?: pulumi.CustomResourceOptions): CryptoWalletItem {
        return new CryptoWalletItem(name, <any>state, { ...opts, id: id });
    }

    /** @internal */
    public static readonly __pulumiType = 'one-password-native-unofficial:index:CryptoWalletItem';

    /**
     * Returns true if the given object is an instance of CryptoWalletItem.  This is designed to work even
     * when multiple copies of the Pulumi SDK have been loaded into the same process.
     */
    public static isInstance(obj: any): obj is CryptoWalletItem {
        if (obj === undefined || obj === null) {
            return false;
        }
        return obj['__pulumiType'] === CryptoWalletItem.__pulumiType;
    }

    public readonly attachments!: pulumi.Output<{[key: string]: outputs.OutputAttachment}>;
    public readonly category!: pulumi.Output<enums.Category | string>;
    public readonly fields!: pulumi.Output<{[key: string]: outputs.OutputField}>;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    public /*out*/ readonly id!: pulumi.Output<string>;
    public readonly notes!: pulumi.Output<string | undefined>;
    public readonly password!: pulumi.Output<string | undefined>;
    public readonly recoveryPhrase!: pulumi.Output<string | undefined>;
    public readonly references!: pulumi.Output<outputs.OutputReference[]>;
    public readonly sections!: pulumi.Output<{[key: string]: outputs.OutputSection}>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    public readonly tags!: pulumi.Output<string[]>;
    /**
     * The title of the item.
     */
    public readonly title!: pulumi.Output<string>;
    public readonly urls!: pulumi.Output<outputs.OutputUrl[] | undefined>;
    public readonly vault!: pulumi.Output<outputs.OutputVault>;
    public readonly wallet!: pulumi.Output<outputs.cryptoWallet.WalletSection | undefined>;

    /**
     * Create a CryptoWalletItem resource with the given unique name, arguments, and options.
     *
     * @param name The _unique_ name of the resource.
     * @param args The arguments to use to populate this resource's properties.
     * @param opts A bag of options that control this resource's behavior.
     */
    constructor(name: string, args?: CryptoWalletItemArgs, opts?: pulumi.CustomResourceOptions)
    constructor(name: string, argsOrState?: CryptoWalletItemArgs | CryptoWalletItemState, opts?: pulumi.CustomResourceOptions) {
        let resourceInputs: pulumi.Inputs = {};
        opts = opts || {};
        if (opts.id) {
            const state = argsOrState as CryptoWalletItemState | undefined;
            resourceInputs["vault"] = state ? state.vault : undefined;
        } else {
            const args = argsOrState as CryptoWalletItemArgs | undefined;
            resourceInputs["attachments"] = args ? args.attachments : undefined;
            resourceInputs["category"] = "Crypto Wallet";
            resourceInputs["fields"] = args ? args.fields : undefined;
            resourceInputs["notes"] = args ? args.notes : undefined;
            resourceInputs["password"] = args?.password ? pulumi.secret(args.password) : undefined;
            resourceInputs["recoveryPhrase"] = args?.recoveryPhrase ? pulumi.secret(args.recoveryPhrase) : undefined;
            resourceInputs["references"] = args ? args.references : undefined;
            resourceInputs["sections"] = args ? args.sections : undefined;
            resourceInputs["tags"] = args ? args.tags : undefined;
            resourceInputs["title"] = args ? args.title : undefined;
            resourceInputs["urls"] = args ? args.urls : undefined;
            resourceInputs["vault"] = args ? args.vault : undefined;
            resourceInputs["wallet"] = args ? args.wallet : undefined;
            resourceInputs["id"] = undefined /*out*/;
        }
        opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
        const secretOpts = { additionalSecretOutputs: ["attachments", "fields", "password", "recoveryPhrase", "sections"] };
        opts = pulumi.mergeOptions(opts, secretOpts);
        super(CryptoWalletItem.__pulumiType, name, resourceInputs, opts);
    }
}

export interface CryptoWalletItemState {
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}

/**
 * The set of arguments for constructing a CryptoWalletItem resource.
 */
export interface CryptoWalletItemArgs {
    attachments?: pulumi.Input<{[key: string]: pulumi.Input<pulumi.asset.Asset | pulumi.asset.Archive>}>;
    /**
     * The category of the vault the item is in.
     */
    category?: pulumi.Input<"Crypto Wallet">;
    fields?: pulumi.Input<{[key: string]: pulumi.Input<inputs.FieldArgs>}>;
    notes?: pulumi.Input<string>;
    password?: pulumi.Input<string>;
    recoveryPhrase?: pulumi.Input<string>;
    references?: pulumi.Input<pulumi.Input<string>[]>;
    sections?: pulumi.Input<{[key: string]: pulumi.Input<inputs.SectionArgs>}>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    tags?: pulumi.Input<pulumi.Input<string>[]>;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    urls?: pulumi.Input<pulumi.Input<inputs.UrlArgs | string>[]>;
    /**
     * The UUID of the vault the item is in.
     */
    vault?: pulumi.Input<string>;
    wallet?: pulumi.Input<inputs.cryptoWallet.WalletSectionArgs>;
}
