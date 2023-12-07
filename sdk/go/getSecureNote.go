// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func GetSecureNote(ctx *pulumi.Context, args *GetSecureNoteArgs, opts ...pulumi.InvokeOption) (*GetSecureNoteResult, error) {
	opts = pkgInvokeDefaultOpts(opts)
	var rv GetSecureNoteResult
	err := ctx.Invoke("one-password-native-unofficial:index:GetSecureNote", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type GetSecureNoteArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid *string `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type GetSecureNoteResult struct {
	Attachments map[string]OutputAttachment `pulumi:"attachments"`
	Category    string                      `pulumi:"category"`
	Fields      map[string]OutputField      `pulumi:"fields"`
	Notes       *string                     `pulumi:"notes"`
	References  []OutputReference           `pulumi:"references"`
	Sections    map[string]OutputSection    `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item.
	Title string      `pulumi:"title"`
	Urls  []OutputUrl `pulumi:"urls"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid  string            `pulumi:"uuid"`
	Vault map[string]string `pulumi:"vault"`
}

func GetSecureNoteOutput(ctx *pulumi.Context, args GetSecureNoteOutputArgs, opts ...pulumi.InvokeOption) GetSecureNoteResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (GetSecureNoteResult, error) {
			args := v.(GetSecureNoteArgs)
			r, err := GetSecureNote(ctx, &args, opts...)
			var s GetSecureNoteResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(GetSecureNoteResultOutput)
}

type GetSecureNoteOutputArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid pulumi.StringPtrInput `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (GetSecureNoteOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*GetSecureNoteArgs)(nil)).Elem()
}

type GetSecureNoteResultOutput struct{ *pulumi.OutputState }

func (GetSecureNoteResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*GetSecureNoteResult)(nil)).Elem()
}

func (o GetSecureNoteResultOutput) ToGetSecureNoteResultOutput() GetSecureNoteResultOutput {
	return o
}

func (o GetSecureNoteResultOutput) ToGetSecureNoteResultOutputWithContext(ctx context.Context) GetSecureNoteResultOutput {
	return o
}

func (o GetSecureNoteResultOutput) Attachments() OutputAttachmentMapOutput {
	return o.ApplyT(func(v GetSecureNoteResult) map[string]OutputAttachment { return v.Attachments }).(OutputAttachmentMapOutput)
}

func (o GetSecureNoteResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v GetSecureNoteResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o GetSecureNoteResultOutput) Fields() OutputFieldMapOutput {
	return o.ApplyT(func(v GetSecureNoteResult) map[string]OutputField { return v.Fields }).(OutputFieldMapOutput)
}

func (o GetSecureNoteResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetSecureNoteResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o GetSecureNoteResultOutput) References() OutputReferenceArrayOutput {
	return o.ApplyT(func(v GetSecureNoteResult) []OutputReference { return v.References }).(OutputReferenceArrayOutput)
}

func (o GetSecureNoteResultOutput) Sections() OutputSectionMapOutput {
	return o.ApplyT(func(v GetSecureNoteResult) map[string]OutputSection { return v.Sections }).(OutputSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o GetSecureNoteResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v GetSecureNoteResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

// The title of the item.
func (o GetSecureNoteResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v GetSecureNoteResult) string { return v.Title }).(pulumi.StringOutput)
}

func (o GetSecureNoteResultOutput) Urls() OutputUrlArrayOutput {
	return o.ApplyT(func(v GetSecureNoteResult) []OutputUrl { return v.Urls }).(OutputUrlArrayOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o GetSecureNoteResultOutput) Uuid() pulumi.StringOutput {
	return o.ApplyT(func(v GetSecureNoteResult) string { return v.Uuid }).(pulumi.StringOutput)
}

func (o GetSecureNoteResultOutput) Vault() pulumi.StringMapOutput {
	return o.ApplyT(func(v GetSecureNoteResult) map[string]string { return v.Vault }).(pulumi.StringMapOutput)
}

func init() {
	pulumi.RegisterOutputType(GetSecureNoteResultOutput{})
}
