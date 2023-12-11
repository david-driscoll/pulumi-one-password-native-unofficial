// *** WARNING: this file was generated by Pulumi SDK Generator. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

import * as pulumi from "@pulumi/pulumi";
import { input as inputs, output as outputs, enums } from "./types";
import * as utilities from "./utilities";

export class EmailAccountItem extends pulumi.CustomResource {
    /**
     * Get an existing EmailAccountItem resource's state with the given name, ID, and optional extra
     * properties used to qualify the lookup.
     *
     * @param name The _unique_ name of the resulting resource.
     * @param id The _unique_ provider ID of the resource to lookup.
     * @param state Any extra arguments used during the lookup.
     * @param opts Optional settings to control the behavior of the CustomResource.
     */
    public static get(name: string, id: pulumi.Input<pulumi.ID>, state?: EmailAccountItemState, opts?: pulumi.CustomResourceOptions): EmailAccountItem {
        return new EmailAccountItem(name, <any>state, { ...opts, id: id });
    }

    /** @internal */
    public static readonly __pulumiType = 'one-password-native-unofficial:index:EmailAccountItem';

    /**
     * Returns true if the given object is an instance of EmailAccountItem.  This is designed to work even
     * when multiple copies of the Pulumi SDK have been loaded into the same process.
     */
    public static isInstance(obj: any): obj is EmailAccountItem {
        if (obj === undefined || obj === null) {
            return false;
        }
        return obj['__pulumiType'] === EmailAccountItem.__pulumiType;
    }

    public readonly attachments!: pulumi.Output<{[key: string]: outputs.OutputAttachment}>;
    public readonly authMethod!: pulumi.Output<string | undefined>;
    public readonly category!: pulumi.Output<enums.Category | string>;
    public readonly contactInformation!: pulumi.Output<outputs.emailAccount.ContactInformationSection | undefined>;
    public readonly fields!: pulumi.Output<{[key: string]: outputs.OutputField}>;
    /**
     * The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
     */
    public /*out*/ readonly id!: pulumi.Output<string>;
    public readonly notes!: pulumi.Output<string | undefined>;
    public readonly password!: pulumi.Output<string | undefined>;
    public readonly portNumber!: pulumi.Output<string | undefined>;
    public readonly references!: pulumi.Output<outputs.OutputReference[]>;
    public readonly sections!: pulumi.Output<{[key: string]: outputs.OutputSection}>;
    public readonly security!: pulumi.Output<string | undefined>;
    public readonly server!: pulumi.Output<string | undefined>;
    public readonly smtp!: pulumi.Output<outputs.emailAccount.SmtpSection | undefined>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    public readonly tags!: pulumi.Output<string[]>;
    /**
     * The title of the item.
     */
    public readonly title!: pulumi.Output<string>;
    public readonly type!: pulumi.Output<string | undefined>;
    public readonly urls!: pulumi.Output<outputs.OutputUrl[] | undefined>;
    public readonly username!: pulumi.Output<string | undefined>;
    public readonly vault!: pulumi.Output<{[key: string]: string}>;

    /**
     * Create a EmailAccountItem resource with the given unique name, arguments, and options.
     *
     * @param name The _unique_ name of the resource.
     * @param args The arguments to use to populate this resource's properties.
     * @param opts A bag of options that control this resource's behavior.
     */
    constructor(name: string, args: EmailAccountItemArgs, opts?: pulumi.CustomResourceOptions)
    constructor(name: string, argsOrState?: EmailAccountItemArgs | EmailAccountItemState, opts?: pulumi.CustomResourceOptions) {
        let resourceInputs: pulumi.Inputs = {};
        opts = opts || {};
        if (opts.id) {
            const state = argsOrState as EmailAccountItemState | undefined;
            resourceInputs["vault"] = state ? state.vault : undefined;
        } else {
            const args = argsOrState as EmailAccountItemArgs | undefined;
            if ((!args || args.vault === undefined) && !opts.urn) {
                throw new Error("Missing required property 'vault'");
            }
            resourceInputs["attachments"] = args ? args.attachments : undefined;
            resourceInputs["authMethod"] = args ? args.authMethod : undefined;
            resourceInputs["category"] = "Email Account";
            resourceInputs["contactInformation"] = args ? args.contactInformation : undefined;
            resourceInputs["fields"] = args ? args.fields : undefined;
            resourceInputs["notes"] = args ? args.notes : undefined;
            resourceInputs["password"] = args?.password ? pulumi.secret(args.password) : undefined;
            resourceInputs["portNumber"] = args ? args.portNumber : undefined;
            resourceInputs["references"] = args ? args.references : undefined;
            resourceInputs["sections"] = args ? args.sections : undefined;
            resourceInputs["security"] = args ? args.security : undefined;
            resourceInputs["server"] = args ? args.server : undefined;
            resourceInputs["smtp"] = args ? args.smtp : undefined;
            resourceInputs["tags"] = args ? args.tags : undefined;
            resourceInputs["title"] = args ? args.title : undefined;
            resourceInputs["type"] = args ? args.type : undefined;
            resourceInputs["urls"] = args ? args.urls : undefined;
            resourceInputs["username"] = args ? args.username : undefined;
            resourceInputs["vault"] = args ? args.vault : undefined;
            resourceInputs["id"] = undefined /*out*/;
        }
        opts = pulumi.mergeOptions(utilities.resourceOptsDefaults(), opts);
        const secretOpts = { additionalSecretOutputs: ["attachments", "fields", "password", "sections", "smtp"] };
        opts = pulumi.mergeOptions(opts, secretOpts);
        super(EmailAccountItem.__pulumiType, name, resourceInputs, opts);
    }
}

export interface EmailAccountItemState {
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}

/**
 * The set of arguments for constructing a EmailAccountItem resource.
 */
export interface EmailAccountItemArgs {
    attachments?: pulumi.Input<{[key: string]: pulumi.Input<pulumi.asset.Asset | pulumi.asset.Archive>}>;
    authMethod?: pulumi.Input<string>;
    /**
     * The category of the vault the item is in.
     */
    category?: pulumi.Input<"Email Account">;
    contactInformation?: pulumi.Input<inputs.emailAccount.ContactInformationSectionArgs>;
    fields?: pulumi.Input<{[key: string]: pulumi.Input<inputs.FieldArgs>}>;
    notes?: pulumi.Input<string>;
    password?: pulumi.Input<string>;
    portNumber?: pulumi.Input<string>;
    references?: pulumi.Input<pulumi.Input<inputs.ReferenceArgs>[]>;
    sections?: pulumi.Input<{[key: string]: pulumi.Input<inputs.SectionArgs>}>;
    security?: pulumi.Input<string>;
    server?: pulumi.Input<string>;
    smtp?: pulumi.Input<inputs.emailAccount.SmtpSectionArgs>;
    /**
     * An array of strings of the tags assigned to the item.
     */
    tags?: pulumi.Input<pulumi.Input<string>[]>;
    /**
     * The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
     */
    title?: pulumi.Input<string>;
    type?: pulumi.Input<string>;
    urls?: pulumi.Input<pulumi.Input<inputs.UrlArgs>[]>;
    username?: pulumi.Input<string>;
    /**
     * The UUID of the vault the item is in.
     */
    vault: pulumi.Input<string>;
}
