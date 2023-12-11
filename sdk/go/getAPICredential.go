// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func GetAPICredential(ctx *pulumi.Context, args *GetAPICredentialArgs, opts ...pulumi.InvokeOption) (*GetAPICredentialResult, error) {
	opts = pkgInvokeDefaultOpts(opts)
	var rv GetAPICredentialResult
	err := ctx.Invoke("one-password-native-unofficial:index:GetAPICredential", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type GetAPICredentialArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id *string `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type GetAPICredentialResult struct {
	Attachments map[string]OutputAttachment `pulumi:"attachments"`
	Category    string                      `pulumi:"category"`
	Credential  *string                     `pulumi:"credential"`
	Expires     *string                     `pulumi:"expires"`
	Fields      map[string]OutputField      `pulumi:"fields"`
	Filename    *string                     `pulumi:"filename"`
	Hostname    *string                     `pulumi:"hostname"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id         string                   `pulumi:"id"`
	Notes      *string                  `pulumi:"notes"`
	References []OutputReference        `pulumi:"references"`
	Sections   map[string]OutputSection `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item.
	Title     string            `pulumi:"title"`
	Type      *string           `pulumi:"type"`
	Urls      []OutputUrl       `pulumi:"urls"`
	Username  *string           `pulumi:"username"`
	ValidFrom *string           `pulumi:"validFrom"`
	Vault     map[string]string `pulumi:"vault"`
}

func GetAPICredentialOutput(ctx *pulumi.Context, args GetAPICredentialOutputArgs, opts ...pulumi.InvokeOption) GetAPICredentialResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (GetAPICredentialResult, error) {
			args := v.(GetAPICredentialArgs)
			r, err := GetAPICredential(ctx, &args, opts...)
			var s GetAPICredentialResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(GetAPICredentialResultOutput)
}

type GetAPICredentialOutputArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id pulumi.StringPtrInput `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (GetAPICredentialOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*GetAPICredentialArgs)(nil)).Elem()
}

type GetAPICredentialResultOutput struct{ *pulumi.OutputState }

func (GetAPICredentialResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*GetAPICredentialResult)(nil)).Elem()
}

func (o GetAPICredentialResultOutput) ToGetAPICredentialResultOutput() GetAPICredentialResultOutput {
	return o
}

func (o GetAPICredentialResultOutput) ToGetAPICredentialResultOutputWithContext(ctx context.Context) GetAPICredentialResultOutput {
	return o
}

func (o GetAPICredentialResultOutput) Attachments() OutputAttachmentMapOutput {
	return o.ApplyT(func(v GetAPICredentialResult) map[string]OutputAttachment { return v.Attachments }).(OutputAttachmentMapOutput)
}

func (o GetAPICredentialResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v GetAPICredentialResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o GetAPICredentialResultOutput) Credential() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Credential }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) Expires() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Expires }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) Fields() OutputFieldMapOutput {
	return o.ApplyT(func(v GetAPICredentialResult) map[string]OutputField { return v.Fields }).(OutputFieldMapOutput)
}

func (o GetAPICredentialResultOutput) Filename() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Filename }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) Hostname() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Hostname }).(pulumi.StringPtrOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o GetAPICredentialResultOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v GetAPICredentialResult) string { return v.Id }).(pulumi.StringOutput)
}

func (o GetAPICredentialResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) References() OutputReferenceArrayOutput {
	return o.ApplyT(func(v GetAPICredentialResult) []OutputReference { return v.References }).(OutputReferenceArrayOutput)
}

func (o GetAPICredentialResultOutput) Sections() OutputSectionMapOutput {
	return o.ApplyT(func(v GetAPICredentialResult) map[string]OutputSection { return v.Sections }).(OutputSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o GetAPICredentialResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v GetAPICredentialResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

// The title of the item.
func (o GetAPICredentialResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v GetAPICredentialResult) string { return v.Title }).(pulumi.StringOutput)
}

func (o GetAPICredentialResultOutput) Type() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Type }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) Urls() OutputUrlArrayOutput {
	return o.ApplyT(func(v GetAPICredentialResult) []OutputUrl { return v.Urls }).(OutputUrlArrayOutput)
}

func (o GetAPICredentialResultOutput) Username() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.Username }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) ValidFrom() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetAPICredentialResult) *string { return v.ValidFrom }).(pulumi.StringPtrOutput)
}

func (o GetAPICredentialResultOutput) Vault() pulumi.StringMapOutput {
	return o.ApplyT(func(v GetAPICredentialResult) map[string]string { return v.Vault }).(pulumi.StringMapOutput)
}

func init() {
	pulumi.RegisterOutputType(GetAPICredentialResultOutput{})
}