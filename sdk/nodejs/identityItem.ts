// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export class IdentityItem extends pulumi.CustomResource {
    /**
     * Get an existing IdentityItem resource's state with the given name, ID, and optional extra
     * properties used to qualify the lookup.
     *
     * @param name The _unique_ name of the resulting resource.
     * @param id The _unique_ provider ID of the resource to lookup.
     * @param state Any extra arguments used during the lookup.
     * @param opts Optional settings to control the behavior of the CustomResource.
     */
    public static get(name: string, id: pulumi.Input<pulumi.ID>, state?: IdentityItemState, opts?: pulumi.CustomResourceOptions): IdentityItem {
        return new IdentityItem(name, <any>state, { ...opts, id: id });
    }

    /** @internal */
    public static readonly __pulumiType = 'one-password-native-unofficial:index:IdentityItem';

    /**
     * Returns true if the given object is an instance of IdentityItem.  This is designed to work even
     * when multiple copies of the Pulumi SDK have been loaded into the same process.
     */
    public static isInstance(obj: any): obj is IdentityItem {
        if (obj === undefined || obj === null) {
            return false;
        }
        return obj['__pulumiType'] === IdentityItem.__pulumiType;
    }

    public readonly address!: pulumi.Output<outputs.identity.AddressSection | undefined>;
    public readonly attachments!: pulumi.Output<{[key: string]: outputs.OutputAttachment}>;
    public readonly category!: pulumi.Output<enums.Category | string>;
    public readonly fields!: pulumi.Output<{[key: string]: outputs.OutputField}>;
    public readonly identification!: pulumi.Output<outputs.identity.IdentificationSection | undefined>;
    public readonly internetDetails!: pulumi.Output<outputs.identity.InternetDetailsSection | undefined>;
    public readonly notes!: pulumi.Output<string | undefined>;
    public /*out*/ readonly references!: pulumi.Output<outputs.OutputReference[]>;
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
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    public /*out*/ readonly uuid!: pulumi.Output<string>;
    public readonly vault!: pulumi.Output<{[key: string]: string}>;

    /**
     * Create a IdentityItem resource with the given unique name, arguments, and options.
     *
     * @param name The _unique_ name of the resource.
     * @param args The arguments to use to populate this resource's properties.
     * @param opts A bag of options that control this resource's behavior.
     */
    constructor(name: string, args: IdentityItemArgs, opts?: pulumi.CustomResourceOptions)
    constructor(name: string, argsOrState?: IdentityItemArgs | IdentityItemState, opts?: pulumi.CustomResourceOptions) {
        let resourceInputs: pulumi.Inputs = {};
        opts = opts || {};
        if (opts.id) {
            const state = argsOrState as IdentityItemState | undefined;
            resourceInputs["vault"] = state ? state.vault : undefined;
        } else {
            const args = argsOrState as IdentityItemArgs | undefined;
            if ((!args || args.vault === undefined) && !opts.urn) {
                throw new Error("Missing required property 'vault'");
            }
            resourceInputs["address"] = args ? args.address : undefined;
            resourceInputs["attachments"] = args ? args.attachments : undefined;
            resourceInputs["category"] = "Identity";
            resourceInputs["fields"] = args ? args.fields : undefined;
            resourceInputs["identification"] = args ? args.identification : undefined;
            resourceInputs["internetDetails"] = args ? args.internetDetails : undefined;
            resourceInputs["notes"] = args ? args.notes : undefined;
            resourceInputs["sections"] = args ? args.sections : undefined;
            resourceInputs["tags"] = args ? args.tags : undefined;
            resourceInputs["title"] = args ? args.title : undefined;
            resourceInputs["urls"] = args ? args.urls : undefined;
            resourceInputs["vault"] = args ? args.vault : undefined;
            resourceInputs["references"] = undefined /*out*/;
            resourceInputs["uuid"] = undefined /*out*/;
        }
        opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
        const secretOpts = { additionalSecretOutputs: ["attachments", "fields", "sections"] };
        opts = pulumi.mergeOptions(opts, secretOpts);
        super(IdentityItem.__pulumiType, name, resourceInputs, opts);
    }
}

export interface IdentityItemState {
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}

/**
 * The set of arguments for constructing a IdentityItem resource.
 */
export interface IdentityItemArgs {
    address?: pulumi.Input<inputs.identity.AddressSectionArgs>;
    attachments?: pulumi.Input<{[key: string]: pulumi.Input<pulumi.asset.Asset | pulumi.asset.Archive>}>;
    /**
     * The category of the vault the item is in.
     */
    category?: pulumi.Input<"Identity">;
    fields?: pulumi.Input<{[key: string]: pulumi.Input<inputs.FieldArgs>}>;
    identification?: pulumi.Input<inputs.identity.IdentificationSectionArgs>;
    internetDetails?: pulumi.Input<inputs.identity.InternetDetailsSectionArgs>;
    notes?: pulumi.Input<string>;
    sections?: pulumi.Input<{[key: string]: pulumi.Input<inputs.SectionArgs>}>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    tags?: pulumi.Input<pulumi.Input<string>[]>;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    urls?: pulumi.Input<pulumi.Input<string>[]>;
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}
