// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pkg/errors"
	"github.com/pulumi/pulumi-onepassword/sdk/go/onepassword/bankaccount"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type BankAccountItem struct {
	pulumi.CustomResourceState

	AccountNumber     pulumi.StringPtrOutput                        `pulumi:"accountNumber"`
	Attachments       OutputAttachmentMapOutput                     `pulumi:"attachments"`
	BankName          pulumi.StringPtrOutput                        `pulumi:"bankName"`
	BranchInformation bankaccount.BranchInformationSectionPtrOutput `pulumi:"branchInformation"`
	Category          pulumi.StringOutput                           `pulumi:"category"`
	Fields            OutputFieldMapOutput                          `pulumi:"fields"`
	Iban              pulumi.StringPtrOutput                        `pulumi:"iban"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id            pulumi.StringOutput        `pulumi:"id"`
	NameOnAccount pulumi.StringPtrOutput     `pulumi:"nameOnAccount"`
	Notes         pulumi.StringPtrOutput     `pulumi:"notes"`
	Pin           pulumi.StringPtrOutput     `pulumi:"pin"`
	References    OutputReferenceArrayOutput `pulumi:"references"`
	RoutingNumber pulumi.StringPtrOutput     `pulumi:"routingNumber"`
	Sections      OutputSectionMapOutput     `pulumi:"sections"`
	Swift         pulumi.StringPtrOutput     `pulumi:"swift"`
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayOutput `pulumi:"tags"`
	// The title of the item.
	Title pulumi.StringOutput    `pulumi:"title"`
	Type  pulumi.StringPtrOutput `pulumi:"type"`
	Urls  OutputUrlArrayOutput   `pulumi:"urls"`
	Vault pulumi.StringMapOutput `pulumi:"vault"`
}

// NewBankAccountItem registers a new resource with the given unique name, arguments, and options.
func NewBankAccountItem(ctx *pulumi.Context,
	name string, args *BankAccountItemArgs, opts ...pulumi.ResourceOption) (*BankAccountItem, error) {
	if args == nil {
		return nil, errors.New("missing one or more required arguments")
	}

	if args.Vault == nil {
		return nil, errors.New("invalid value for required argument 'Vault'")
	}
	args.Category = pulumi.StringPtr("Bank Account")
	if args.Pin != nil {
		args.Pin = pulumi.ToSecret(args.Pin).(pulumi.StringPtrOutput)
	}
	secrets := pulumi.AdditionalSecretOutputs([]string{
		"attachments",
		"fields",
		"pin",
		"sections",
	})
	opts = append(opts, secrets)
	opts = pkgResourceDefaultOpts(opts)
	var resource BankAccountItem
	err := ctx.RegisterResource("one-password-native-unofficial:index:BankAccountItem", name, args, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// GetBankAccountItem gets an existing BankAccountItem resource's state with the given name, ID, and optional
// state properties that are used to uniquely qualify the lookup (nil if not required).
func GetBankAccountItem(ctx *pulumi.Context,
	name string, id pulumi.IDInput, state *BankAccountItemState, opts ...pulumi.ResourceOption) (*BankAccountItem, error) {
	var resource BankAccountItem
	err := ctx.ReadResource("one-password-native-unofficial:index:BankAccountItem", name, id, state, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// Input properties used for looking up and filtering BankAccountItem resources.
type bankAccountItemState struct {
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

type BankAccountItemState struct {
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (BankAccountItemState) ElementType() reflect.Type {
	return reflect.TypeOf((*bankAccountItemState)(nil)).Elem()
}

type bankAccountItemArgs struct {
	AccountNumber     *string                               `pulumi:"accountNumber"`
	Attachments       map[string]pulumi.AssetOrArchive      `pulumi:"attachments"`
	BankName          *string                               `pulumi:"bankName"`
	BranchInformation *bankaccount.BranchInformationSection `pulumi:"branchInformation"`
	// The category of the vault the item is in.
	Category      *string            `pulumi:"category"`
	Fields        map[string]Field   `pulumi:"fields"`
	Iban          *string            `pulumi:"iban"`
	NameOnAccount *string            `pulumi:"nameOnAccount"`
	Notes         *string            `pulumi:"notes"`
	Pin           *string            `pulumi:"pin"`
	References    []Reference        `pulumi:"references"`
	RoutingNumber *string            `pulumi:"routingNumber"`
	Sections      map[string]Section `pulumi:"sections"`
	Swift         *string            `pulumi:"swift"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	Type  *string `pulumi:"type"`
	Urls  []Url   `pulumi:"urls"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

// The set of arguments for constructing a BankAccountItem resource.
type BankAccountItemArgs struct {
	AccountNumber     pulumi.StringPtrInput
	Attachments       pulumi.AssetOrArchiveMapInput
	BankName          pulumi.StringPtrInput
	BranchInformation bankaccount.BranchInformationSectionPtrInput
	// The category of the vault the item is in.
	Category      pulumi.StringPtrInput
	Fields        FieldMapInput
	Iban          pulumi.StringPtrInput
	NameOnAccount pulumi.StringPtrInput
	Notes         pulumi.StringPtrInput
	Pin           pulumi.StringPtrInput
	References    ReferenceArrayInput
	RoutingNumber pulumi.StringPtrInput
	Sections      SectionMapInput
	Swift         pulumi.StringPtrInput
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayInput
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput
	Type  pulumi.StringPtrInput
	Urls  UrlArrayInput
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (BankAccountItemArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*bankAccountItemArgs)(nil)).Elem()
}

type BankAccountItemInput interface {
	pulumi.Input

	ToBankAccountItemOutput() BankAccountItemOutput
	ToBankAccountItemOutputWithContext(ctx context.Context) BankAccountItemOutput
}

func (*BankAccountItem) ElementType() reflect.Type {
	return reflect.TypeOf((**BankAccountItem)(nil)).Elem()
}

func (i *BankAccountItem) ToBankAccountItemOutput() BankAccountItemOutput {
	return i.ToBankAccountItemOutputWithContext(context.Background())
}

func (i *BankAccountItem) ToBankAccountItemOutputWithContext(ctx context.Context) BankAccountItemOutput {
	return pulumi.ToOutputWithContext(ctx, i).(BankAccountItemOutput)
}

// BankAccountItemArrayInput is an input type that accepts BankAccountItemArray and BankAccountItemArrayOutput values.
// You can construct a concrete instance of `BankAccountItemArrayInput` via:
//
//	BankAccountItemArray{ BankAccountItemArgs{...} }
type BankAccountItemArrayInput interface {
	pulumi.Input

	ToBankAccountItemArrayOutput() BankAccountItemArrayOutput
	ToBankAccountItemArrayOutputWithContext(context.Context) BankAccountItemArrayOutput
}

type BankAccountItemArray []BankAccountItemInput

func (BankAccountItemArray) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*BankAccountItem)(nil)).Elem()
}

func (i BankAccountItemArray) ToBankAccountItemArrayOutput() BankAccountItemArrayOutput {
	return i.ToBankAccountItemArrayOutputWithContext(context.Background())
}

func (i BankAccountItemArray) ToBankAccountItemArrayOutputWithContext(ctx context.Context) BankAccountItemArrayOutput {
	return pulumi.ToOutputWithContext(ctx, i).(BankAccountItemArrayOutput)
}

// BankAccountItemMapInput is an input type that accepts BankAccountItemMap and BankAccountItemMapOutput values.
// You can construct a concrete instance of `BankAccountItemMapInput` via:
//
//	BankAccountItemMap{ "key": BankAccountItemArgs{...} }
type BankAccountItemMapInput interface {
	pulumi.Input

	ToBankAccountItemMapOutput() BankAccountItemMapOutput
	ToBankAccountItemMapOutputWithContext(context.Context) BankAccountItemMapOutput
}

type BankAccountItemMap map[string]BankAccountItemInput

func (BankAccountItemMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*BankAccountItem)(nil)).Elem()
}

func (i BankAccountItemMap) ToBankAccountItemMapOutput() BankAccountItemMapOutput {
	return i.ToBankAccountItemMapOutputWithContext(context.Background())
}

func (i BankAccountItemMap) ToBankAccountItemMapOutputWithContext(ctx context.Context) BankAccountItemMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(BankAccountItemMapOutput)
}

type BankAccountItemOutput struct{ *pulumi.OutputState }

func (BankAccountItemOutput) ElementType() reflect.Type {
	return reflect.TypeOf((**BankAccountItem)(nil)).Elem()
}

func (o BankAccountItemOutput) ToBankAccountItemOutput() BankAccountItemOutput {
	return o
}

func (o BankAccountItemOutput) ToBankAccountItemOutputWithContext(ctx context.Context) BankAccountItemOutput {
	return o
}

type BankAccountItemArrayOutput struct{ *pulumi.OutputState }

func (BankAccountItemArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*BankAccountItem)(nil)).Elem()
}

func (o BankAccountItemArrayOutput) ToBankAccountItemArrayOutput() BankAccountItemArrayOutput {
	return o
}

func (o BankAccountItemArrayOutput) ToBankAccountItemArrayOutputWithContext(ctx context.Context) BankAccountItemArrayOutput {
	return o
}

func (o BankAccountItemArrayOutput) Index(i pulumi.IntInput) BankAccountItemOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) *BankAccountItem {
		return vs[0].([]*BankAccountItem)[vs[1].(int)]
	}).(BankAccountItemOutput)
}

type BankAccountItemMapOutput struct{ *pulumi.OutputState }

func (BankAccountItemMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*BankAccountItem)(nil)).Elem()
}

func (o BankAccountItemMapOutput) ToBankAccountItemMapOutput() BankAccountItemMapOutput {
	return o
}

func (o BankAccountItemMapOutput) ToBankAccountItemMapOutputWithContext(ctx context.Context) BankAccountItemMapOutput {
	return o
}

func (o BankAccountItemMapOutput) MapIndex(k pulumi.StringInput) BankAccountItemOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) *BankAccountItem {
		return vs[0].(map[string]*BankAccountItem)[vs[1].(string)]
	}).(BankAccountItemOutput)
}

func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*BankAccountItemInput)(nil)).Elem(), &BankAccountItem{})
	pulumi.RegisterInputType(reflect.TypeOf((*BankAccountItemArrayInput)(nil)).Elem(), BankAccountItemArray{})
	pulumi.RegisterInputType(reflect.TypeOf((*BankAccountItemMapInput)(nil)).Elem(), BankAccountItemMap{})
	pulumi.RegisterOutputType(BankAccountItemOutput{})
	pulumi.RegisterOutputType(BankAccountItemArrayOutput{})
	pulumi.RegisterOutputType(BankAccountItemMapOutput{})
}
