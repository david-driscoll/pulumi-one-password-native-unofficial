// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type Field struct {
	Label *string    `pulumi:"label"`
	Type  *FieldType `pulumi:"type"`
	Value string     `pulumi:"value"`
}

// Defaults sets the appropriate defaults for Field
func (val *Field) Defaults() *Field {
	if val == nil {
		return nil
	}
	tmp := *val
	if isZero(tmp.Type) {
		type_ := FieldType("STRING")
		tmp.Type = &type_
	}
	return &tmp
}

// FieldInput is an input type that accepts FieldArgs and FieldOutput values.
// You can construct a concrete instance of `FieldInput` via:
//
//	FieldArgs{...}
type FieldInput interface {
	pulumi.Input

	ToFieldOutput() FieldOutput
	ToFieldOutputWithContext(context.Context) FieldOutput
}

type FieldArgs struct {
	Label pulumi.StringPtrInput `pulumi:"label"`
	Type  FieldTypePtrInput     `pulumi:"type"`
	Value pulumi.StringInput    `pulumi:"value"`
}

func (FieldArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*Field)(nil)).Elem()
}

func (i FieldArgs) ToFieldOutput() FieldOutput {
	return i.ToFieldOutputWithContext(context.Background())
}

func (i FieldArgs) ToFieldOutputWithContext(ctx context.Context) FieldOutput {
	return pulumi.ToOutputWithContext(ctx, i).(FieldOutput)
}

// FieldMapInput is an input type that accepts FieldMap and FieldMapOutput values.
// You can construct a concrete instance of `FieldMapInput` via:
//
//	FieldMap{ "key": FieldArgs{...} }
type FieldMapInput interface {
	pulumi.Input

	ToFieldMapOutput() FieldMapOutput
	ToFieldMapOutputWithContext(context.Context) FieldMapOutput
}

type FieldMap map[string]FieldInput

func (FieldMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]Field)(nil)).Elem()
}

func (i FieldMap) ToFieldMapOutput() FieldMapOutput {
	return i.ToFieldMapOutputWithContext(context.Background())
}

func (i FieldMap) ToFieldMapOutputWithContext(ctx context.Context) FieldMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(FieldMapOutput)
}

type FieldOutput struct{ *pulumi.OutputState }

func (FieldOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*Field)(nil)).Elem()
}

func (o FieldOutput) ToFieldOutput() FieldOutput {
	return o
}

func (o FieldOutput) ToFieldOutputWithContext(ctx context.Context) FieldOutput {
	return o
}

func (o FieldOutput) Label() pulumi.StringPtrOutput {
	return o.ApplyT(func(v Field) *string { return v.Label }).(pulumi.StringPtrOutput)
}

func (o FieldOutput) Type() FieldTypePtrOutput {
	return o.ApplyT(func(v Field) *FieldType { return v.Type }).(FieldTypePtrOutput)
}

func (o FieldOutput) Value() pulumi.StringOutput {
	return o.ApplyT(func(v Field) string { return v.Value }).(pulumi.StringOutput)
}

type FieldMapOutput struct{ *pulumi.OutputState }

func (FieldMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]Field)(nil)).Elem()
}

func (o FieldMapOutput) ToFieldMapOutput() FieldMapOutput {
	return o
}

func (o FieldMapOutput) ToFieldMapOutputWithContext(ctx context.Context) FieldMapOutput {
	return o
}

func (o FieldMapOutput) MapIndex(k pulumi.StringInput) FieldOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) Field {
		return vs[0].(map[string]Field)[vs[1].(string)]
	}).(FieldOutput)
}

type OutputAttachment struct {
	Id        string `pulumi:"id"`
	Name      string `pulumi:"name"`
	Reference string `pulumi:"reference"`
	Size      int    `pulumi:"size"`
}

type OutputAttachmentOutput struct{ *pulumi.OutputState }

func (OutputAttachmentOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputAttachment)(nil)).Elem()
}

func (o OutputAttachmentOutput) ToOutputAttachmentOutput() OutputAttachmentOutput {
	return o
}

func (o OutputAttachmentOutput) ToOutputAttachmentOutputWithContext(ctx context.Context) OutputAttachmentOutput {
	return o
}

func (o OutputAttachmentOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v OutputAttachment) string { return v.Id }).(pulumi.StringOutput)
}

func (o OutputAttachmentOutput) Name() pulumi.StringOutput {
	return o.ApplyT(func(v OutputAttachment) string { return v.Name }).(pulumi.StringOutput)
}

func (o OutputAttachmentOutput) Reference() pulumi.StringOutput {
	return o.ApplyT(func(v OutputAttachment) string { return v.Reference }).(pulumi.StringOutput)
}

func (o OutputAttachmentOutput) Size() pulumi.IntOutput {
	return o.ApplyT(func(v OutputAttachment) int { return v.Size }).(pulumi.IntOutput)
}

type OutputAttachmentMapOutput struct{ *pulumi.OutputState }

func (OutputAttachmentMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]OutputAttachment)(nil)).Elem()
}

func (o OutputAttachmentMapOutput) ToOutputAttachmentMapOutput() OutputAttachmentMapOutput {
	return o
}

func (o OutputAttachmentMapOutput) ToOutputAttachmentMapOutputWithContext(ctx context.Context) OutputAttachmentMapOutput {
	return o
}

func (o OutputAttachmentMapOutput) MapIndex(k pulumi.StringInput) OutputAttachmentOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) OutputAttachment {
		return vs[0].(map[string]OutputAttachment)[vs[1].(string)]
	}).(OutputAttachmentOutput)
}

type OutputField struct {
	Data      map[string]interface{} `pulumi:"data"`
	Id        string                 `pulumi:"id"`
	Label     string                 `pulumi:"label"`
	Reference string                 `pulumi:"reference"`
	Type      FieldType              `pulumi:"type"`
	Value     string                 `pulumi:"value"`
}

type OutputFieldOutput struct{ *pulumi.OutputState }

func (OutputFieldOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputField)(nil)).Elem()
}

func (o OutputFieldOutput) ToOutputFieldOutput() OutputFieldOutput {
	return o
}

func (o OutputFieldOutput) ToOutputFieldOutputWithContext(ctx context.Context) OutputFieldOutput {
	return o
}

func (o OutputFieldOutput) Data() pulumi.MapOutput {
	return o.ApplyT(func(v OutputField) map[string]interface{} { return v.Data }).(pulumi.MapOutput)
}

func (o OutputFieldOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v OutputField) string { return v.Id }).(pulumi.StringOutput)
}

func (o OutputFieldOutput) Label() pulumi.StringOutput {
	return o.ApplyT(func(v OutputField) string { return v.Label }).(pulumi.StringOutput)
}

func (o OutputFieldOutput) Reference() pulumi.StringOutput {
	return o.ApplyT(func(v OutputField) string { return v.Reference }).(pulumi.StringOutput)
}

func (o OutputFieldOutput) Type() FieldTypeOutput {
	return o.ApplyT(func(v OutputField) FieldType { return v.Type }).(FieldTypeOutput)
}

func (o OutputFieldOutput) Value() pulumi.StringOutput {
	return o.ApplyT(func(v OutputField) string { return v.Value }).(pulumi.StringOutput)
}

type OutputFieldArrayOutput struct{ *pulumi.OutputState }

func (OutputFieldArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]OutputField)(nil)).Elem()
}

func (o OutputFieldArrayOutput) ToOutputFieldArrayOutput() OutputFieldArrayOutput {
	return o
}

func (o OutputFieldArrayOutput) ToOutputFieldArrayOutputWithContext(ctx context.Context) OutputFieldArrayOutput {
	return o
}

func (o OutputFieldArrayOutput) Index(i pulumi.IntInput) OutputFieldOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) OutputField {
		return vs[0].([]OutputField)[vs[1].(int)]
	}).(OutputFieldOutput)
}

type OutputFieldMapOutput struct{ *pulumi.OutputState }

func (OutputFieldMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]OutputField)(nil)).Elem()
}

func (o OutputFieldMapOutput) ToOutputFieldMapOutput() OutputFieldMapOutput {
	return o
}

func (o OutputFieldMapOutput) ToOutputFieldMapOutputWithContext(ctx context.Context) OutputFieldMapOutput {
	return o
}

func (o OutputFieldMapOutput) MapIndex(k pulumi.StringInput) OutputFieldOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) OutputField {
		return vs[0].(map[string]OutputField)[vs[1].(string)]
	}).(OutputFieldOutput)
}

type OutputReference struct {
	Id        string `pulumi:"id"`
	ItemId    string `pulumi:"itemId"`
	Label     string `pulumi:"label"`
	Reference string `pulumi:"reference"`
}

type OutputReferenceOutput struct{ *pulumi.OutputState }

func (OutputReferenceOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputReference)(nil)).Elem()
}

func (o OutputReferenceOutput) ToOutputReferenceOutput() OutputReferenceOutput {
	return o
}

func (o OutputReferenceOutput) ToOutputReferenceOutputWithContext(ctx context.Context) OutputReferenceOutput {
	return o
}

func (o OutputReferenceOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v OutputReference) string { return v.Id }).(pulumi.StringOutput)
}

func (o OutputReferenceOutput) ItemId() pulumi.StringOutput {
	return o.ApplyT(func(v OutputReference) string { return v.ItemId }).(pulumi.StringOutput)
}

func (o OutputReferenceOutput) Label() pulumi.StringOutput {
	return o.ApplyT(func(v OutputReference) string { return v.Label }).(pulumi.StringOutput)
}

func (o OutputReferenceOutput) Reference() pulumi.StringOutput {
	return o.ApplyT(func(v OutputReference) string { return v.Reference }).(pulumi.StringOutput)
}

type OutputReferenceArrayOutput struct{ *pulumi.OutputState }

func (OutputReferenceArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]OutputReference)(nil)).Elem()
}

func (o OutputReferenceArrayOutput) ToOutputReferenceArrayOutput() OutputReferenceArrayOutput {
	return o
}

func (o OutputReferenceArrayOutput) ToOutputReferenceArrayOutputWithContext(ctx context.Context) OutputReferenceArrayOutput {
	return o
}

func (o OutputReferenceArrayOutput) Index(i pulumi.IntInput) OutputReferenceOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) OutputReference {
		return vs[0].([]OutputReference)[vs[1].(int)]
	}).(OutputReferenceOutput)
}

type OutputSection struct {
	Attachments map[string]OutputAttachment `pulumi:"attachments"`
	Fields      map[string]OutputField      `pulumi:"fields"`
	Id          string                      `pulumi:"id"`
	Label       string                      `pulumi:"label"`
	References  []OutputField               `pulumi:"references"`
}

type OutputSectionOutput struct{ *pulumi.OutputState }

func (OutputSectionOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputSection)(nil)).Elem()
}

func (o OutputSectionOutput) ToOutputSectionOutput() OutputSectionOutput {
	return o
}

func (o OutputSectionOutput) ToOutputSectionOutputWithContext(ctx context.Context) OutputSectionOutput {
	return o
}

func (o OutputSectionOutput) Attachments() OutputAttachmentMapOutput {
	return o.ApplyT(func(v OutputSection) map[string]OutputAttachment { return v.Attachments }).(OutputAttachmentMapOutput)
}

func (o OutputSectionOutput) Fields() OutputFieldMapOutput {
	return o.ApplyT(func(v OutputSection) map[string]OutputField { return v.Fields }).(OutputFieldMapOutput)
}

func (o OutputSectionOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v OutputSection) string { return v.Id }).(pulumi.StringOutput)
}

func (o OutputSectionOutput) Label() pulumi.StringOutput {
	return o.ApplyT(func(v OutputSection) string { return v.Label }).(pulumi.StringOutput)
}

func (o OutputSectionOutput) References() OutputFieldArrayOutput {
	return o.ApplyT(func(v OutputSection) []OutputField { return v.References }).(OutputFieldArrayOutput)
}

type OutputSectionMapOutput struct{ *pulumi.OutputState }

func (OutputSectionMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]OutputSection)(nil)).Elem()
}

func (o OutputSectionMapOutput) ToOutputSectionMapOutput() OutputSectionMapOutput {
	return o
}

func (o OutputSectionMapOutput) ToOutputSectionMapOutputWithContext(ctx context.Context) OutputSectionMapOutput {
	return o
}

func (o OutputSectionMapOutput) MapIndex(k pulumi.StringInput) OutputSectionOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) OutputSection {
		return vs[0].(map[string]OutputSection)[vs[1].(string)]
	}).(OutputSectionOutput)
}

type OutputUrl struct {
	Href    string  `pulumi:"href"`
	Label   *string `pulumi:"label"`
	Primary bool    `pulumi:"primary"`
}

type OutputUrlOutput struct{ *pulumi.OutputState }

func (OutputUrlOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputUrl)(nil)).Elem()
}

func (o OutputUrlOutput) ToOutputUrlOutput() OutputUrlOutput {
	return o
}

func (o OutputUrlOutput) ToOutputUrlOutputWithContext(ctx context.Context) OutputUrlOutput {
	return o
}

func (o OutputUrlOutput) Href() pulumi.StringOutput {
	return o.ApplyT(func(v OutputUrl) string { return v.Href }).(pulumi.StringOutput)
}

func (o OutputUrlOutput) Label() pulumi.StringPtrOutput {
	return o.ApplyT(func(v OutputUrl) *string { return v.Label }).(pulumi.StringPtrOutput)
}

func (o OutputUrlOutput) Primary() pulumi.BoolOutput {
	return o.ApplyT(func(v OutputUrl) bool { return v.Primary }).(pulumi.BoolOutput)
}

type OutputUrlArrayOutput struct{ *pulumi.OutputState }

func (OutputUrlArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]OutputUrl)(nil)).Elem()
}

func (o OutputUrlArrayOutput) ToOutputUrlArrayOutput() OutputUrlArrayOutput {
	return o
}

func (o OutputUrlArrayOutput) ToOutputUrlArrayOutputWithContext(ctx context.Context) OutputUrlArrayOutput {
	return o
}

func (o OutputUrlArrayOutput) Index(i pulumi.IntInput) OutputUrlOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) OutputUrl {
		return vs[0].([]OutputUrl)[vs[1].(int)]
	}).(OutputUrlOutput)
}

type OutputVault struct {
	Id   string `pulumi:"id"`
	Name string `pulumi:"name"`
}

type OutputVaultOutput struct{ *pulumi.OutputState }

func (OutputVaultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*OutputVault)(nil)).Elem()
}

func (o OutputVaultOutput) ToOutputVaultOutput() OutputVaultOutput {
	return o
}

func (o OutputVaultOutput) ToOutputVaultOutputWithContext(ctx context.Context) OutputVaultOutput {
	return o
}

func (o OutputVaultOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v OutputVault) string { return v.Id }).(pulumi.StringOutput)
}

func (o OutputVaultOutput) Name() pulumi.StringOutput {
	return o.ApplyT(func(v OutputVault) string { return v.Name }).(pulumi.StringOutput)
}

type PasswordRecipe struct {
	Digits  *bool `pulumi:"digits"`
	Length  int   `pulumi:"length"`
	Letters *bool `pulumi:"letters"`
	Symbols *bool `pulumi:"symbols"`
}

type Reference struct {
	ItemId string `pulumi:"itemId"`
}

type Section struct {
	Attachments map[string]pulumi.AssetOrArchive `pulumi:"attachments"`
	Fields      map[string]Field                 `pulumi:"fields"`
	Label       *string                          `pulumi:"label"`
	References  []string                         `pulumi:"references"`
}

// SectionInput is an input type that accepts SectionArgs and SectionOutput values.
// You can construct a concrete instance of `SectionInput` via:
//
//	SectionArgs{...}
type SectionInput interface {
	pulumi.Input

	ToSectionOutput() SectionOutput
	ToSectionOutputWithContext(context.Context) SectionOutput
}

type SectionArgs struct {
	Attachments pulumi.AssetOrArchiveMapInput `pulumi:"attachments"`
	Fields      FieldMapInput                 `pulumi:"fields"`
	Label       pulumi.StringPtrInput         `pulumi:"label"`
	References  pulumi.StringArrayInput       `pulumi:"references"`
}

func (SectionArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*Section)(nil)).Elem()
}

func (i SectionArgs) ToSectionOutput() SectionOutput {
	return i.ToSectionOutputWithContext(context.Background())
}

func (i SectionArgs) ToSectionOutputWithContext(ctx context.Context) SectionOutput {
	return pulumi.ToOutputWithContext(ctx, i).(SectionOutput)
}

// SectionMapInput is an input type that accepts SectionMap and SectionMapOutput values.
// You can construct a concrete instance of `SectionMapInput` via:
//
//	SectionMap{ "key": SectionArgs{...} }
type SectionMapInput interface {
	pulumi.Input

	ToSectionMapOutput() SectionMapOutput
	ToSectionMapOutputWithContext(context.Context) SectionMapOutput
}

type SectionMap map[string]SectionInput

func (SectionMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]Section)(nil)).Elem()
}

func (i SectionMap) ToSectionMapOutput() SectionMapOutput {
	return i.ToSectionMapOutputWithContext(context.Background())
}

func (i SectionMap) ToSectionMapOutputWithContext(ctx context.Context) SectionMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(SectionMapOutput)
}

type SectionOutput struct{ *pulumi.OutputState }

func (SectionOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*Section)(nil)).Elem()
}

func (o SectionOutput) ToSectionOutput() SectionOutput {
	return o
}

func (o SectionOutput) ToSectionOutputWithContext(ctx context.Context) SectionOutput {
	return o
}

func (o SectionOutput) Attachments() pulumi.AssetOrArchiveMapOutput {
	return o.ApplyT(func(v Section) map[string]pulumi.AssetOrArchive { return v.Attachments }).(pulumi.AssetOrArchiveMapOutput)
}

func (o SectionOutput) Fields() FieldMapOutput {
	return o.ApplyT(func(v Section) map[string]Field { return v.Fields }).(FieldMapOutput)
}

func (o SectionOutput) Label() pulumi.StringPtrOutput {
	return o.ApplyT(func(v Section) *string { return v.Label }).(pulumi.StringPtrOutput)
}

func (o SectionOutput) References() pulumi.StringArrayOutput {
	return o.ApplyT(func(v Section) []string { return v.References }).(pulumi.StringArrayOutput)
}

type SectionMapOutput struct{ *pulumi.OutputState }

func (SectionMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]Section)(nil)).Elem()
}

func (o SectionMapOutput) ToSectionMapOutput() SectionMapOutput {
	return o
}

func (o SectionMapOutput) ToSectionMapOutputWithContext(ctx context.Context) SectionMapOutput {
	return o
}

func (o SectionMapOutput) MapIndex(k pulumi.StringInput) SectionOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) Section {
		return vs[0].(map[string]Section)[vs[1].(string)]
	}).(SectionOutput)
}

type Url struct {
	Href    string  `pulumi:"href"`
	Label   *string `pulumi:"label"`
	Primary bool    `pulumi:"primary"`
}

// Defaults sets the appropriate defaults for Url
func (val *Url) Defaults() *Url {
	if val == nil {
		return nil
	}
	tmp := *val
	if isZero(tmp.Primary) {
		tmp.Primary = false
	}
	return &tmp
}
func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*FieldInput)(nil)).Elem(), FieldArgs{})
	pulumi.RegisterInputType(reflect.TypeOf((*FieldMapInput)(nil)).Elem(), FieldMap{})
	pulumi.RegisterInputType(reflect.TypeOf((*SectionInput)(nil)).Elem(), SectionArgs{})
	pulumi.RegisterInputType(reflect.TypeOf((*SectionMapInput)(nil)).Elem(), SectionMap{})
	pulumi.RegisterOutputType(FieldOutput{})
	pulumi.RegisterOutputType(FieldMapOutput{})
	pulumi.RegisterOutputType(OutputAttachmentOutput{})
	pulumi.RegisterOutputType(OutputAttachmentMapOutput{})
	pulumi.RegisterOutputType(OutputFieldOutput{})
	pulumi.RegisterOutputType(OutputFieldArrayOutput{})
	pulumi.RegisterOutputType(OutputFieldMapOutput{})
	pulumi.RegisterOutputType(OutputReferenceOutput{})
	pulumi.RegisterOutputType(OutputReferenceArrayOutput{})
	pulumi.RegisterOutputType(OutputSectionOutput{})
	pulumi.RegisterOutputType(OutputSectionMapOutput{})
	pulumi.RegisterOutputType(OutputUrlOutput{})
	pulumi.RegisterOutputType(OutputUrlArrayOutput{})
	pulumi.RegisterOutputType(OutputVaultOutput{})
	pulumi.RegisterOutputType(SectionOutput{})
	pulumi.RegisterOutputType(SectionMapOutput{})
}
