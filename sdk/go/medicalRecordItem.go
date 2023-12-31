// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi-onepassword/sdk/go/onepassword/medicalrecord"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type MedicalRecordItem struct {
	pulumi.CustomResourceState

	Attachments            OutputAttachmentMapOutput `pulumi:"attachments"`
	Category               pulumi.StringOutput       `pulumi:"category"`
	Date                   pulumi.StringPtrOutput    `pulumi:"date"`
	Fields                 OutputFieldMapOutput      `pulumi:"fields"`
	HealthcareProfessional pulumi.StringPtrOutput    `pulumi:"healthcareProfessional"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id             pulumi.StringOutput                      `pulumi:"id"`
	Location       pulumi.StringPtrOutput                   `pulumi:"location"`
	Medication     medicalrecord.MedicationSectionPtrOutput `pulumi:"medication"`
	Notes          pulumi.StringPtrOutput                   `pulumi:"notes"`
	Patient        pulumi.StringPtrOutput                   `pulumi:"patient"`
	ReasonForVisit pulumi.StringPtrOutput                   `pulumi:"reasonForVisit"`
	References     OutputReferenceArrayOutput               `pulumi:"references"`
	Sections       OutputSectionMapOutput                   `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayOutput `pulumi:"tags"`
	// The title of the item.
	Title pulumi.StringOutput  `pulumi:"title"`
	Urls  OutputUrlArrayOutput `pulumi:"urls"`
	Vault OutputVaultOutput    `pulumi:"vault"`
}

// NewMedicalRecordItem registers a new resource with the given unique name, arguments, and options.
func NewMedicalRecordItem(ctx *pulumi.Context,
	name string, args *MedicalRecordItemArgs, opts ...pulumi.ResourceOption) (*MedicalRecordItem, error) {
	if args == nil {
		args = &MedicalRecordItemArgs{}
	}

	args.Category = pulumi.StringPtr("Medical Record")
	secrets := pulumi.AdditionalSecretOutputs([]string{
		"attachments",
		"fields",
		"sections",
	})
	opts = append(opts, secrets)
	opts = pkgResourceDefaultOpts(opts)
	var resource MedicalRecordItem
	err := ctx.RegisterResource("one-password-native-unofficial:index:MedicalRecordItem", name, args, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// GetMedicalRecordItem gets an existing MedicalRecordItem resource's state with the given name, ID, and optional
// state properties that are used to uniquely qualify the lookup (nil if not required).
func GetMedicalRecordItem(ctx *pulumi.Context,
	name string, id pulumi.IDInput, state *MedicalRecordItemState, opts ...pulumi.ResourceOption) (*MedicalRecordItem, error) {
	var resource MedicalRecordItem
	err := ctx.ReadResource("one-password-native-unofficial:index:MedicalRecordItem", name, id, state, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// Input properties used for looking up and filtering MedicalRecordItem resources.
type medicalRecordItemState struct {
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

type MedicalRecordItemState struct {
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (MedicalRecordItemState) ElementType() reflect.Type {
	return reflect.TypeOf((*medicalRecordItemState)(nil)).Elem()
}

type medicalRecordItemArgs struct {
	Attachments map[string]pulumi.AssetOrArchive `pulumi:"attachments"`
	// The category of the vault the item is in.
	Category               *string                          `pulumi:"category"`
	Date                   *string                          `pulumi:"date"`
	Fields                 map[string]Field                 `pulumi:"fields"`
	HealthcareProfessional *string                          `pulumi:"healthcareProfessional"`
	Location               *string                          `pulumi:"location"`
	Medication             *medicalrecord.MedicationSection `pulumi:"medication"`
	Notes                  *string                          `pulumi:"notes"`
	Patient                *string                          `pulumi:"patient"`
	ReasonForVisit         *string                          `pulumi:"reasonForVisit"`
	References             []string                         `pulumi:"references"`
	Sections               map[string]Section               `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string       `pulumi:"title"`
	Urls  []interface{} `pulumi:"urls"`
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

// The set of arguments for constructing a MedicalRecordItem resource.
type MedicalRecordItemArgs struct {
	Attachments pulumi.AssetOrArchiveMapInput
	// The category of the vault the item is in.
	Category               pulumi.StringPtrInput
	Date                   pulumi.StringPtrInput
	Fields                 FieldMapInput
	HealthcareProfessional pulumi.StringPtrInput
	Location               pulumi.StringPtrInput
	Medication             medicalrecord.MedicationSectionPtrInput
	Notes                  pulumi.StringPtrInput
	Patient                pulumi.StringPtrInput
	ReasonForVisit         pulumi.StringPtrInput
	References             pulumi.StringArrayInput
	Sections               SectionMapInput
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayInput
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput
	Urls  pulumi.ArrayInput
	// The UUID of the vault the item is in.
	Vault pulumi.StringPtrInput
}

func (MedicalRecordItemArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*medicalRecordItemArgs)(nil)).Elem()
}

type MedicalRecordItemInput interface {
	pulumi.Input

	ToMedicalRecordItemOutput() MedicalRecordItemOutput
	ToMedicalRecordItemOutputWithContext(ctx context.Context) MedicalRecordItemOutput
}

func (*MedicalRecordItem) ElementType() reflect.Type {
	return reflect.TypeOf((**MedicalRecordItem)(nil)).Elem()
}

func (i *MedicalRecordItem) ToMedicalRecordItemOutput() MedicalRecordItemOutput {
	return i.ToMedicalRecordItemOutputWithContext(context.Background())
}

func (i *MedicalRecordItem) ToMedicalRecordItemOutputWithContext(ctx context.Context) MedicalRecordItemOutput {
	return pulumi.ToOutputWithContext(ctx, i).(MedicalRecordItemOutput)
}

// MedicalRecordItemArrayInput is an input type that accepts MedicalRecordItemArray and MedicalRecordItemArrayOutput values.
// You can construct a concrete instance of `MedicalRecordItemArrayInput` via:
//
//	MedicalRecordItemArray{ MedicalRecordItemArgs{...} }
type MedicalRecordItemArrayInput interface {
	pulumi.Input

	ToMedicalRecordItemArrayOutput() MedicalRecordItemArrayOutput
	ToMedicalRecordItemArrayOutputWithContext(context.Context) MedicalRecordItemArrayOutput
}

type MedicalRecordItemArray []MedicalRecordItemInput

func (MedicalRecordItemArray) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*MedicalRecordItem)(nil)).Elem()
}

func (i MedicalRecordItemArray) ToMedicalRecordItemArrayOutput() MedicalRecordItemArrayOutput {
	return i.ToMedicalRecordItemArrayOutputWithContext(context.Background())
}

func (i MedicalRecordItemArray) ToMedicalRecordItemArrayOutputWithContext(ctx context.Context) MedicalRecordItemArrayOutput {
	return pulumi.ToOutputWithContext(ctx, i).(MedicalRecordItemArrayOutput)
}

// MedicalRecordItemMapInput is an input type that accepts MedicalRecordItemMap and MedicalRecordItemMapOutput values.
// You can construct a concrete instance of `MedicalRecordItemMapInput` via:
//
//	MedicalRecordItemMap{ "key": MedicalRecordItemArgs{...} }
type MedicalRecordItemMapInput interface {
	pulumi.Input

	ToMedicalRecordItemMapOutput() MedicalRecordItemMapOutput
	ToMedicalRecordItemMapOutputWithContext(context.Context) MedicalRecordItemMapOutput
}

type MedicalRecordItemMap map[string]MedicalRecordItemInput

func (MedicalRecordItemMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*MedicalRecordItem)(nil)).Elem()
}

func (i MedicalRecordItemMap) ToMedicalRecordItemMapOutput() MedicalRecordItemMapOutput {
	return i.ToMedicalRecordItemMapOutputWithContext(context.Background())
}

func (i MedicalRecordItemMap) ToMedicalRecordItemMapOutputWithContext(ctx context.Context) MedicalRecordItemMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(MedicalRecordItemMapOutput)
}

type MedicalRecordItemOutput struct{ *pulumi.OutputState }

func (MedicalRecordItemOutput) ElementType() reflect.Type {
	return reflect.TypeOf((**MedicalRecordItem)(nil)).Elem()
}

func (o MedicalRecordItemOutput) ToMedicalRecordItemOutput() MedicalRecordItemOutput {
	return o
}

func (o MedicalRecordItemOutput) ToMedicalRecordItemOutputWithContext(ctx context.Context) MedicalRecordItemOutput {
	return o
}

type MedicalRecordItemArrayOutput struct{ *pulumi.OutputState }

func (MedicalRecordItemArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*MedicalRecordItem)(nil)).Elem()
}

func (o MedicalRecordItemArrayOutput) ToMedicalRecordItemArrayOutput() MedicalRecordItemArrayOutput {
	return o
}

func (o MedicalRecordItemArrayOutput) ToMedicalRecordItemArrayOutputWithContext(ctx context.Context) MedicalRecordItemArrayOutput {
	return o
}

func (o MedicalRecordItemArrayOutput) Index(i pulumi.IntInput) MedicalRecordItemOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) *MedicalRecordItem {
		return vs[0].([]*MedicalRecordItem)[vs[1].(int)]
	}).(MedicalRecordItemOutput)
}

type MedicalRecordItemMapOutput struct{ *pulumi.OutputState }

func (MedicalRecordItemMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*MedicalRecordItem)(nil)).Elem()
}

func (o MedicalRecordItemMapOutput) ToMedicalRecordItemMapOutput() MedicalRecordItemMapOutput {
	return o
}

func (o MedicalRecordItemMapOutput) ToMedicalRecordItemMapOutputWithContext(ctx context.Context) MedicalRecordItemMapOutput {
	return o
}

func (o MedicalRecordItemMapOutput) MapIndex(k pulumi.StringInput) MedicalRecordItemOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) *MedicalRecordItem {
		return vs[0].(map[string]*MedicalRecordItem)[vs[1].(string)]
	}).(MedicalRecordItemOutput)
}

func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*MedicalRecordItemInput)(nil)).Elem(), &MedicalRecordItem{})
	pulumi.RegisterInputType(reflect.TypeOf((*MedicalRecordItemArrayInput)(nil)).Elem(), MedicalRecordItemArray{})
	pulumi.RegisterInputType(reflect.TypeOf((*MedicalRecordItemMapInput)(nil)).Elem(), MedicalRecordItemMap{})
	pulumi.RegisterOutputType(MedicalRecordItemOutput{})
	pulumi.RegisterOutputType(MedicalRecordItemArrayOutput{})
	pulumi.RegisterOutputType(MedicalRecordItemMapOutput{})
}
