// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export class SoftwareLicenseItem extends pulumi.CustomResource {
    /**
     * Get an existing SoftwareLicenseItem resource's state with the given name, ID, and optional extra
     * properties used to qualify the lookup.
     *
     * @param name The _unique_ name of the resulting resource.
     * @param id The _unique_ provider ID of the resource to lookup.
     * @param state Any extra arguments used during the lookup.
     * @param opts Optional settings to control the behavior of the CustomResource.
     */
    public static get(name: string, id: pulumi.Input<pulumi.ID>, state?: SoftwareLicenseItemState, opts?: pulumi.CustomResourceOptions): SoftwareLicenseItem {
        return new SoftwareLicenseItem(name, <any>state, { ...opts, id: id });
    }

    /** @internal */
    public static readonly __pulumiType = 'one-password-native-unofficial:index:SoftwareLicenseItem';

    /**
     * Returns true if the given object is an instance of SoftwareLicenseItem.  This is designed to work even
     * when multiple copies of the Pulumi SDK have been loaded into the same process.
     */
    public static isInstance(obj: any): obj is SoftwareLicenseItem {
        if (obj === undefined || obj === null) {
            return false;
        }
        return obj['__pulumiType'] === SoftwareLicenseItem.__pulumiType;
    }

    public readonly attachments!: pulumi.Output<{[key: string]: outputs.OutputAttachment}>;
    public readonly category!: pulumi.Output<enums.Category | string>;
    public readonly customer!: pulumi.Output<outputs.softwareLicense.CustomerSection | undefined>;
    public readonly fields!: pulumi.Output<{[key: string]: outputs.OutputField}>;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    public /*out*/ readonly id!: pulumi.Output<string>;
    public readonly licenseKey!: pulumi.Output<string | undefined>;
    public readonly notes!: pulumi.Output<string | undefined>;
    public readonly order!: pulumi.Output<outputs.softwareLicense.OrderSection | undefined>;
    public readonly publisher!: pulumi.Output<outputs.softwareLicense.PublisherSection | undefined>;
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
    public readonly vault!: pulumi.Output<{[key: string]: string}>;
    public readonly version!: pulumi.Output<string | undefined>;

    /**
     * Create a SoftwareLicenseItem resource with the given unique name, arguments, and options.
     *
     * @param name The _unique_ name of the resource.
     * @param args The arguments to use to populate this resource's properties.
     * @param opts A bag of options that control this resource's behavior.
     */
    constructor(name: string, args: SoftwareLicenseItemArgs, opts?: pulumi.CustomResourceOptions)
    constructor(name: string, argsOrState?: SoftwareLicenseItemArgs | SoftwareLicenseItemState, opts?: pulumi.CustomResourceOptions) {
        let resourceInputs: pulumi.Inputs = {};
        opts = opts || {};
        if (opts.id) {
            const state = argsOrState as SoftwareLicenseItemState | undefined;
            resourceInputs["vault"] = state ? state.vault : undefined;
        } else {
            const args = argsOrState as SoftwareLicenseItemArgs | undefined;
            if ((!args || args.vault === undefined) && !opts.urn) {
                throw new Error("Missing required property 'vault'");
            }
            resourceInputs["attachments"] = args ? args.attachments : undefined;
            resourceInputs["category"] = "Software License";
            resourceInputs["customer"] = args ? args.customer : undefined;
            resourceInputs["fields"] = args ? args.fields : undefined;
            resourceInputs["licenseKey"] = args ? args.licenseKey : undefined;
            resourceInputs["notes"] = args ? args.notes : undefined;
            resourceInputs["order"] = args ? args.order : undefined;
            resourceInputs["publisher"] = args ? args.publisher : undefined;
            resourceInputs["sections"] = args ? args.sections : undefined;
            resourceInputs["tags"] = args ? args.tags : undefined;
            resourceInputs["title"] = args ? args.title : undefined;
            resourceInputs["urls"] = args ? args.urls : undefined;
            resourceInputs["vault"] = args ? args.vault : undefined;
            resourceInputs["version"] = args ? args.version : undefined;
            resourceInputs["id"] = undefined /*out*/;
            resourceInputs["references"] = undefined /*out*/;
        }
        opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
        const secretOpts = { additionalSecretOutputs: ["attachments", "fields", "sections"] };
        opts = pulumi.mergeOptions(opts, secretOpts);
        super(SoftwareLicenseItem.__pulumiType, name, resourceInputs, opts);
    }
}

export interface SoftwareLicenseItemState {
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}

/**
 * The set of arguments for constructing a SoftwareLicenseItem resource.
 */
export interface SoftwareLicenseItemArgs {
    attachments?: pulumi.Input<{[key: string]: pulumi.Input<pulumi.asset.Asset | pulumi.asset.Archive>}>;
    /**
     * The category of the vault the item is in.
     */
    category?: pulumi.Input<"Software License">;
    customer?: pulumi.Input<inputs.softwareLicense.CustomerSectionArgs>;
    fields?: pulumi.Input<{[key: string]: pulumi.Input<inputs.FieldArgs>}>;
    licenseKey?: pulumi.Input<string>;
    notes?: pulumi.Input<string>;
    order?: pulumi.Input<inputs.softwareLicense.OrderSectionArgs>;
    publisher?: pulumi.Input<inputs.softwareLicense.PublisherSectionArgs>;
    sections?: pulumi.Input<{[key: string]: pulumi.Input<inputs.SectionArgs>}>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    tags?: pulumi.Input<pulumi.Input<string>[]>;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    urls?: pulumi.Input<pulumi.Input<inputs.UrlArgs>[]>;
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
    version?: pulumi.Input<string>;
}
