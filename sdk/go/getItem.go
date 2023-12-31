// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func LookupItem(ctx *pulumi.Context, args *LookupItemArgs, opts ...pulumi.InvokeOption) (*LookupItemResult, error) {
	opts = pkgInvokeDefaultOpts(opts)
	var rv LookupItemResult
	err := ctx.Invoke("one-password-native-unofficial:index:GetItem", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type LookupItemArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id *string `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type LookupItemResult struct {
	Attachments map[string]OutputAttachment `pulumi:"attachments"`
	Category    string                      `pulumi:"category"`
	Fields      map[string]OutputField      `pulumi:"fields"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id         string                   `pulumi:"id"`
	Notes      *string                  `pulumi:"notes"`
	References []OutputReference        `pulumi:"references"`
	Sections   map[string]OutputSection `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item.
	Title string      `pulumi:"title"`
	Urls  []OutputUrl `pulumi:"urls"`
	Vault OutputVault `pulumi:"vault"`
}

func LookupItemOutput(ctx *pulumi.Context, args LookupItemOutputArgs, opts ...pulumi.InvokeOption) LookupItemResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (LookupItemResult, error) {
			args := v.(LookupItemArgs)
			r, err := LookupItem(ctx, &args, opts...)
			var s LookupItemResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(LookupItemResultOutput)
}

type LookupItemOutputArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id pulumi.StringPtrInput `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (LookupItemOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*LookupItemArgs)(nil)).Elem()
}

type LookupItemResultOutput struct{ *pulumi.OutputState }

func (LookupItemResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*LookupItemResult)(nil)).Elem()
}

func (o LookupItemResultOutput) ToLookupItemResultOutput() LookupItemResultOutput {
	return o
}

func (o LookupItemResultOutput) ToLookupItemResultOutputWithContext(ctx context.Context) LookupItemResultOutput {
	return o
}

func (o LookupItemResultOutput) Attachments() OutputAttachmentMapOutput {
	return o.ApplyT(func(v LookupItemResult) map[string]OutputAttachment { return v.Attachments }).(OutputAttachmentMapOutput)
}

func (o LookupItemResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v LookupItemResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o LookupItemResultOutput) Fields() OutputFieldMapOutput {
	return o.ApplyT(func(v LookupItemResult) map[string]OutputField { return v.Fields }).(OutputFieldMapOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o LookupItemResultOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v LookupItemResult) string { return v.Id }).(pulumi.StringOutput)
}

func (o LookupItemResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v LookupItemResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o LookupItemResultOutput) References() OutputReferenceArrayOutput {
	return o.ApplyT(func(v LookupItemResult) []OutputReference { return v.References }).(OutputReferenceArrayOutput)
}

func (o LookupItemResultOutput) Sections() OutputSectionMapOutput {
	return o.ApplyT(func(v LookupItemResult) map[string]OutputSection { return v.Sections }).(OutputSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o LookupItemResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v LookupItemResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

// The title of the item.
func (o LookupItemResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v LookupItemResult) string { return v.Title }).(pulumi.StringOutput)
}

func (o LookupItemResultOutput) Urls() OutputUrlArrayOutput {
	return o.ApplyT(func(v LookupItemResult) []OutputUrl { return v.Urls }).(OutputUrlArrayOutput)
}

func (o LookupItemResultOutput) Vault() OutputVaultOutput {
	return o.ApplyT(func(v LookupItemResult) OutputVault { return v.Vault }).(OutputVaultOutput)
}

func init() {
	pulumi.RegisterOutputType(LookupItemResultOutput{})
}
