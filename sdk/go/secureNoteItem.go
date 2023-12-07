// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pkg/errors"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type SecureNoteItem struct {
	pulumi.CustomResourceState

	Attachments OutputAttachmentMapOutput  `pulumi:"attachments"`
	Category    pulumi.StringOutput        `pulumi:"category"`
	Fields      OutputFieldMapOutput       `pulumi:"fields"`
	Notes       pulumi.StringPtrOutput     `pulumi:"notes"`
	References  OutputReferenceArrayOutput `pulumi:"references"`
	Sections    OutputSectionMapOutput     `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayOutput `pulumi:"tags"`
	// The title of the item.
	Title pulumi.StringOutput  `pulumi:"title"`
	Urls  OutputUrlArrayOutput `pulumi:"urls"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid  pulumi.StringOutput    `pulumi:"uuid"`
	Vault pulumi.StringMapOutput `pulumi:"vault"`
}

// NewSecureNoteItem registers a new resource with the given unique name, arguments, and options.
func NewSecureNoteItem(ctx *pulumi.Context,
	name string, args *SecureNoteItemArgs, opts ...pulumi.ResourceOption) (*SecureNoteItem, error) {
	if args == nil {
		return nil, errors.New("missing one or more required arguments")
	}

	if args.Vault == nil {
		return nil, errors.New("invalid value for required argument 'Vault'")
	}
	args.Category = pulumi.StringPtr("Secure Note")
	secrets := pulumi.AdditionalSecretOutputs([]string{
		"attachments",
		"fields",
		"sections",
	})
	opts = append(opts, secrets)
	opts = pkgResourceDefaultOpts(opts)
	var resource SecureNoteItem
	err := ctx.RegisterResource("one-password-native-unofficial:index:SecureNoteItem", name, args, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// GetSecureNoteItem gets an existing SecureNoteItem resource's state with the given name, ID, and optional
// state properties that are used to uniquely qualify the lookup (nil if not required).
func GetSecureNoteItem(ctx *pulumi.Context,
	name string, id pulumi.IDInput, state *SecureNoteItemState, opts ...pulumi.ResourceOption) (*SecureNoteItem, error) {
	var resource SecureNoteItem
	err := ctx.ReadResource("one-password-native-unofficial:index:SecureNoteItem", name, id, state, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// Input properties used for looking up and filtering SecureNoteItem resources.
type secureNoteItemState struct {
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

type SecureNoteItemState struct {
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (SecureNoteItemState) ElementType() reflect.Type {
	return reflect.TypeOf((*secureNoteItemState)(nil)).Elem()
}

type secureNoteItemArgs struct {
	Attachments map[string]pulumi.AssetOrArchive `pulumi:"attachments"`
	// The category of the vault the item is in.
	Category *string            `pulumi:"category"`
	Fields   map[string]Field   `pulumi:"fields"`
	Notes    *string            `pulumi:"notes"`
	Sections map[string]Section `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string  `pulumi:"title"`
	Urls  []string `pulumi:"urls"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

// The set of arguments for constructing a SecureNoteItem resource.
type SecureNoteItemArgs struct {
	Attachments pulumi.AssetOrArchiveMapInput
	// The category of the vault the item is in.
	Category pulumi.StringPtrInput
	Fields   FieldMapInput
	Notes    pulumi.StringPtrInput
	Sections SectionMapInput
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayInput
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput
	Urls  pulumi.StringArrayInput
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (SecureNoteItemArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*secureNoteItemArgs)(nil)).Elem()
}

type SecureNoteItemInput interface {
	pulumi.Input

	ToSecureNoteItemOutput() SecureNoteItemOutput
	ToSecureNoteItemOutputWithContext(ctx context.Context) SecureNoteItemOutput
}

func (*SecureNoteItem) ElementType() reflect.Type {
	return reflect.TypeOf((**SecureNoteItem)(nil)).Elem()
}

func (i *SecureNoteItem) ToSecureNoteItemOutput() SecureNoteItemOutput {
	return i.ToSecureNoteItemOutputWithContext(context.Background())
}

func (i *SecureNoteItem) ToSecureNoteItemOutputWithContext(ctx context.Context) SecureNoteItemOutput {
	return pulumi.ToOutputWithContext(ctx, i).(SecureNoteItemOutput)
}

// SecureNoteItemArrayInput is an input type that accepts SecureNoteItemArray and SecureNoteItemArrayOutput values.
// You can construct a concrete instance of `SecureNoteItemArrayInput` via:
//
//	SecureNoteItemArray{ SecureNoteItemArgs{...} }
type SecureNoteItemArrayInput interface {
	pulumi.Input

	ToSecureNoteItemArrayOutput() SecureNoteItemArrayOutput
	ToSecureNoteItemArrayOutputWithContext(context.Context) SecureNoteItemArrayOutput
}

type SecureNoteItemArray []SecureNoteItemInput

func (SecureNoteItemArray) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*SecureNoteItem)(nil)).Elem()
}

func (i SecureNoteItemArray) ToSecureNoteItemArrayOutput() SecureNoteItemArrayOutput {
	return i.ToSecureNoteItemArrayOutputWithContext(context.Background())
}

func (i SecureNoteItemArray) ToSecureNoteItemArrayOutputWithContext(ctx context.Context) SecureNoteItemArrayOutput {
	return pulumi.ToOutputWithContext(ctx, i).(SecureNoteItemArrayOutput)
}

// SecureNoteItemMapInput is an input type that accepts SecureNoteItemMap and SecureNoteItemMapOutput values.
// You can construct a concrete instance of `SecureNoteItemMapInput` via:
//
//	SecureNoteItemMap{ "key": SecureNoteItemArgs{...} }
type SecureNoteItemMapInput interface {
	pulumi.Input

	ToSecureNoteItemMapOutput() SecureNoteItemMapOutput
	ToSecureNoteItemMapOutputWithContext(context.Context) SecureNoteItemMapOutput
}

type SecureNoteItemMap map[string]SecureNoteItemInput

func (SecureNoteItemMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*SecureNoteItem)(nil)).Elem()
}

func (i SecureNoteItemMap) ToSecureNoteItemMapOutput() SecureNoteItemMapOutput {
	return i.ToSecureNoteItemMapOutputWithContext(context.Background())
}

func (i SecureNoteItemMap) ToSecureNoteItemMapOutputWithContext(ctx context.Context) SecureNoteItemMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(SecureNoteItemMapOutput)
}

type SecureNoteItemOutput struct{ *pulumi.OutputState }

func (SecureNoteItemOutput) ElementType() reflect.Type {
	return reflect.TypeOf((**SecureNoteItem)(nil)).Elem()
}

func (o SecureNoteItemOutput) ToSecureNoteItemOutput() SecureNoteItemOutput {
	return o
}

func (o SecureNoteItemOutput) ToSecureNoteItemOutputWithContext(ctx context.Context) SecureNoteItemOutput {
	return o
}

type SecureNoteItemArrayOutput struct{ *pulumi.OutputState }

func (SecureNoteItemArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*SecureNoteItem)(nil)).Elem()
}

func (o SecureNoteItemArrayOutput) ToSecureNoteItemArrayOutput() SecureNoteItemArrayOutput {
	return o
}

func (o SecureNoteItemArrayOutput) ToSecureNoteItemArrayOutputWithContext(ctx context.Context) SecureNoteItemArrayOutput {
	return o
}

func (o SecureNoteItemArrayOutput) Index(i pulumi.IntInput) SecureNoteItemOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) *SecureNoteItem {
		return vs[0].([]*SecureNoteItem)[vs[1].(int)]
	}).(SecureNoteItemOutput)
}

type SecureNoteItemMapOutput struct{ *pulumi.OutputState }

func (SecureNoteItemMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*SecureNoteItem)(nil)).Elem()
}

func (o SecureNoteItemMapOutput) ToSecureNoteItemMapOutput() SecureNoteItemMapOutput {
	return o
}

func (o SecureNoteItemMapOutput) ToSecureNoteItemMapOutputWithContext(ctx context.Context) SecureNoteItemMapOutput {
	return o
}

func (o SecureNoteItemMapOutput) MapIndex(k pulumi.StringInput) SecureNoteItemOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) *SecureNoteItem {
		return vs[0].(map[string]*SecureNoteItem)[vs[1].(string)]
	}).(SecureNoteItemOutput)
}

func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*SecureNoteItemInput)(nil)).Elem(), &SecureNoteItem{})
	pulumi.RegisterInputType(reflect.TypeOf((*SecureNoteItemArrayInput)(nil)).Elem(), SecureNoteItemArray{})
	pulumi.RegisterInputType(reflect.TypeOf((*SecureNoteItemMapInput)(nil)).Elem(), SecureNoteItemMap{})
	pulumi.RegisterOutputType(SecureNoteItemOutput{})
	pulumi.RegisterOutputType(SecureNoteItemArrayOutput{})
	pulumi.RegisterOutputType(SecureNoteItemMapOutput{})
}
