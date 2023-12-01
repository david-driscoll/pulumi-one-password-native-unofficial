// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unoffical

import (
	"context"
	"reflect"

	"github.com/pkg/errors"
	"github.com/pulumi/pulumi-onepassword/sdk/go/onepassword/creditcard"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type CreditCardItem struct {
	pulumi.CustomResourceState

	AdditionalDetails  creditcard.AdditionalDetailsSectionPtrOutput  `pulumi:"additionalDetails"`
	Attachments        OutFieldMapOutput                             `pulumi:"attachments"`
	CardholderName     pulumi.StringPtrOutput                        `pulumi:"cardholderName"`
	Category           pulumi.StringOutput                           `pulumi:"category"`
	ContactInformation creditcard.ContactInformationSectionPtrOutput `pulumi:"contactInformation"`
	ExpiryDate         pulumi.StringPtrOutput                        `pulumi:"expiryDate"`
	Fields             OutFieldMapOutput                             `pulumi:"fields"`
	Notes              pulumi.StringPtrOutput                        `pulumi:"notes"`
	Number             pulumi.StringPtrOutput                        `pulumi:"number"`
	References         OutFieldMapOutput                             `pulumi:"references"`
	Sections           OutSectionMapOutput                           `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayOutput `pulumi:"tags"`
	// The title of the item.
	Title pulumi.StringOutput    `pulumi:"title"`
	Type  pulumi.StringPtrOutput `pulumi:"type"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid      pulumi.StringOutput    `pulumi:"uuid"`
	ValidFrom pulumi.StringPtrOutput `pulumi:"validFrom"`
	// The UUID of the vault the item is in.
	Vault              pulumi.StringOutput    `pulumi:"vault"`
	VerificationNumber pulumi.StringPtrOutput `pulumi:"verificationNumber"`
}

// NewCreditCardItem registers a new resource with the given unique name, arguments, and options.
func NewCreditCardItem(ctx *pulumi.Context,
	name string, args *CreditCardItemArgs, opts ...pulumi.ResourceOption) (*CreditCardItem, error) {
	if args == nil {
		return nil, errors.New("missing one or more required arguments")
	}

	if args.Vault == nil {
		return nil, errors.New("invalid value for required argument 'Vault'")
	}
	args.Category = pulumi.StringPtr("Credit Card")
	if args.VerificationNumber != nil {
		args.VerificationNumber = pulumi.ToSecret(args.VerificationNumber).(pulumi.StringPtrOutput)
	}
	secrets := pulumi.AdditionalSecretOutputs([]string{
		"additionalDetails",
		"attachments",
		"fields",
		"references",
		"sections",
		"verificationNumber",
	})
	opts = append(opts, secrets)
	var resource CreditCardItem
	err := ctx.RegisterResource("one-password-native-unoffical:index:CreditCardItem", name, args, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// GetCreditCardItem gets an existing CreditCardItem resource's state with the given name, ID, and optional
// state properties that are used to uniquely qualify the lookup (nil if not required).
func GetCreditCardItem(ctx *pulumi.Context,
	name string, id pulumi.IDInput, state *CreditCardItemState, opts ...pulumi.ResourceOption) (*CreditCardItem, error) {
	var resource CreditCardItem
	err := ctx.ReadResource("one-password-native-unoffical:index:CreditCardItem", name, id, state, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// Input properties used for looking up and filtering CreditCardItem resources.
type creditCardItemState struct {
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

type CreditCardItemState struct {
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (CreditCardItemState) ElementType() reflect.Type {
	return reflect.TypeOf((*creditCardItemState)(nil)).Elem()
}

type creditCardItemArgs struct {
	AdditionalDetails *creditcard.AdditionalDetailsSection `pulumi:"additionalDetails"`
	Attachments       map[string]pulumi.AssetOrArchive     `pulumi:"attachments"`
	CardholderName    *string                              `pulumi:"cardholderName"`
	// The category of the vault the item is in.
	Category           *string                               `pulumi:"category"`
	ContactInformation *creditcard.ContactInformationSection `pulumi:"contactInformation"`
	ExpiryDate         *string                               `pulumi:"expiryDate"`
	Fields             map[string]Field                      `pulumi:"fields"`
	Notes              *string                               `pulumi:"notes"`
	Number             *string                               `pulumi:"number"`
	Sections           map[string]Section                    `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title     *string `pulumi:"title"`
	Type      *string `pulumi:"type"`
	ValidFrom *string `pulumi:"validFrom"`
	// The UUID of the vault the item is in.
	Vault              string  `pulumi:"vault"`
	VerificationNumber *string `pulumi:"verificationNumber"`
}

// The set of arguments for constructing a CreditCardItem resource.
type CreditCardItemArgs struct {
	AdditionalDetails creditcard.AdditionalDetailsSectionPtrInput
	Attachments       pulumi.AssetOrArchiveMapInput
	CardholderName    pulumi.StringPtrInput
	// The category of the vault the item is in.
	Category           pulumi.StringPtrInput
	ContactInformation creditcard.ContactInformationSectionPtrInput
	ExpiryDate         pulumi.StringPtrInput
	Fields             FieldMapInput
	Notes              pulumi.StringPtrInput
	Number             pulumi.StringPtrInput
	Sections           SectionMapInput
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayInput
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title     pulumi.StringPtrInput
	Type      pulumi.StringPtrInput
	ValidFrom pulumi.StringPtrInput
	// The UUID of the vault the item is in.
	Vault              pulumi.StringInput
	VerificationNumber pulumi.StringPtrInput
}

func (CreditCardItemArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*creditCardItemArgs)(nil)).Elem()
}

func (r *CreditCardItem) GetAttachment(ctx *pulumi.Context, args *CreditCardItemGetAttachmentArgs) (CreditCardItemGetAttachmentResultOutput, error) {
	out, err := ctx.Call("one-password-native-unoffical:index:CreditCardItem/attachment", args, CreditCardItemGetAttachmentResultOutput{}, r)
	if err != nil {
		return CreditCardItemGetAttachmentResultOutput{}, err
	}
	return out.(CreditCardItemGetAttachmentResultOutput), nil
}

type creditCardItemGetAttachmentArgs struct {
	// The name or uuid of the attachment to get
	Name string `pulumi:"name"`
}

// The set of arguments for the GetAttachment method of the CreditCardItem resource.
type CreditCardItemGetAttachmentArgs struct {
	// The name or uuid of the attachment to get
	Name pulumi.StringInput
}

func (CreditCardItemGetAttachmentArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*creditCardItemGetAttachmentArgs)(nil)).Elem()
}

// The resolved reference value
type CreditCardItemGetAttachmentResult struct {
	// the value of the attachment
	Value string `pulumi:"value"`
}

type CreditCardItemGetAttachmentResultOutput struct{ *pulumi.OutputState }

func (CreditCardItemGetAttachmentResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*CreditCardItemGetAttachmentResult)(nil)).Elem()
}

// the value of the attachment
func (o CreditCardItemGetAttachmentResultOutput) Value() pulumi.StringOutput {
	return o.ApplyT(func(v CreditCardItemGetAttachmentResult) string { return v.Value }).(pulumi.StringOutput)
}

type CreditCardItemInput interface {
	pulumi.Input

	ToCreditCardItemOutput() CreditCardItemOutput
	ToCreditCardItemOutputWithContext(ctx context.Context) CreditCardItemOutput
}

func (*CreditCardItem) ElementType() reflect.Type {
	return reflect.TypeOf((**CreditCardItem)(nil)).Elem()
}

func (i *CreditCardItem) ToCreditCardItemOutput() CreditCardItemOutput {
	return i.ToCreditCardItemOutputWithContext(context.Background())
}

func (i *CreditCardItem) ToCreditCardItemOutputWithContext(ctx context.Context) CreditCardItemOutput {
	return pulumi.ToOutputWithContext(ctx, i).(CreditCardItemOutput)
}

// CreditCardItemArrayInput is an input type that accepts CreditCardItemArray and CreditCardItemArrayOutput values.
// You can construct a concrete instance of `CreditCardItemArrayInput` via:
//
//	CreditCardItemArray{ CreditCardItemArgs{...} }
type CreditCardItemArrayInput interface {
	pulumi.Input

	ToCreditCardItemArrayOutput() CreditCardItemArrayOutput
	ToCreditCardItemArrayOutputWithContext(context.Context) CreditCardItemArrayOutput
}

type CreditCardItemArray []CreditCardItemInput

func (CreditCardItemArray) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*CreditCardItem)(nil)).Elem()
}

func (i CreditCardItemArray) ToCreditCardItemArrayOutput() CreditCardItemArrayOutput {
	return i.ToCreditCardItemArrayOutputWithContext(context.Background())
}

func (i CreditCardItemArray) ToCreditCardItemArrayOutputWithContext(ctx context.Context) CreditCardItemArrayOutput {
	return pulumi.ToOutputWithContext(ctx, i).(CreditCardItemArrayOutput)
}

// CreditCardItemMapInput is an input type that accepts CreditCardItemMap and CreditCardItemMapOutput values.
// You can construct a concrete instance of `CreditCardItemMapInput` via:
//
//	CreditCardItemMap{ "key": CreditCardItemArgs{...} }
type CreditCardItemMapInput interface {
	pulumi.Input

	ToCreditCardItemMapOutput() CreditCardItemMapOutput
	ToCreditCardItemMapOutputWithContext(context.Context) CreditCardItemMapOutput
}

type CreditCardItemMap map[string]CreditCardItemInput

func (CreditCardItemMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*CreditCardItem)(nil)).Elem()
}

func (i CreditCardItemMap) ToCreditCardItemMapOutput() CreditCardItemMapOutput {
	return i.ToCreditCardItemMapOutputWithContext(context.Background())
}

func (i CreditCardItemMap) ToCreditCardItemMapOutputWithContext(ctx context.Context) CreditCardItemMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(CreditCardItemMapOutput)
}

type CreditCardItemOutput struct{ *pulumi.OutputState }

func (CreditCardItemOutput) ElementType() reflect.Type {
	return reflect.TypeOf((**CreditCardItem)(nil)).Elem()
}

func (o CreditCardItemOutput) ToCreditCardItemOutput() CreditCardItemOutput {
	return o
}

func (o CreditCardItemOutput) ToCreditCardItemOutputWithContext(ctx context.Context) CreditCardItemOutput {
	return o
}

type CreditCardItemArrayOutput struct{ *pulumi.OutputState }

func (CreditCardItemArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*CreditCardItem)(nil)).Elem()
}

func (o CreditCardItemArrayOutput) ToCreditCardItemArrayOutput() CreditCardItemArrayOutput {
	return o
}

func (o CreditCardItemArrayOutput) ToCreditCardItemArrayOutputWithContext(ctx context.Context) CreditCardItemArrayOutput {
	return o
}

func (o CreditCardItemArrayOutput) Index(i pulumi.IntInput) CreditCardItemOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) *CreditCardItem {
		return vs[0].([]*CreditCardItem)[vs[1].(int)]
	}).(CreditCardItemOutput)
}

type CreditCardItemMapOutput struct{ *pulumi.OutputState }

func (CreditCardItemMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*CreditCardItem)(nil)).Elem()
}

func (o CreditCardItemMapOutput) ToCreditCardItemMapOutput() CreditCardItemMapOutput {
	return o
}

func (o CreditCardItemMapOutput) ToCreditCardItemMapOutputWithContext(ctx context.Context) CreditCardItemMapOutput {
	return o
}

func (o CreditCardItemMapOutput) MapIndex(k pulumi.StringInput) CreditCardItemOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) *CreditCardItem {
		return vs[0].(map[string]*CreditCardItem)[vs[1].(string)]
	}).(CreditCardItemOutput)
}

func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*CreditCardItemInput)(nil)).Elem(), &CreditCardItem{})
	pulumi.RegisterInputType(reflect.TypeOf((*CreditCardItemArrayInput)(nil)).Elem(), CreditCardItemArray{})
	pulumi.RegisterInputType(reflect.TypeOf((*CreditCardItemMapInput)(nil)).Elem(), CreditCardItemMap{})
	pulumi.RegisterOutputType(CreditCardItemOutput{})
	pulumi.RegisterOutputType(CreditCardItemGetAttachmentResultOutput{})
	pulumi.RegisterOutputType(CreditCardItemArrayOutput{})
	pulumi.RegisterOutputType(CreditCardItemMapOutput{})
}