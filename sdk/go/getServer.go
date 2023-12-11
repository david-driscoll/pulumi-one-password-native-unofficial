// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi-onepassword/sdk/go/onepassword/server"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func GetServer(ctx *pulumi.Context, args *GetServerArgs, opts ...pulumi.InvokeOption) (*GetServerResult, error) {
	opts = pkgInvokeDefaultOpts(opts)
	var rv GetServerResult
	err := ctx.Invoke("one-password-native-unofficial:index:GetServer", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type GetServerArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id *string `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type GetServerResult struct {
	AdminConsole    *server.AdminConsoleSection    `pulumi:"adminConsole"`
	Attachments     map[string]OutputAttachment    `pulumi:"attachments"`
	Category        string                         `pulumi:"category"`
	Fields          map[string]OutputField         `pulumi:"fields"`
	HostingProvider *server.HostingProviderSection `pulumi:"hostingProvider"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id         string                   `pulumi:"id"`
	Notes      *string                  `pulumi:"notes"`
	Password   *string                  `pulumi:"password"`
	References []OutputReference        `pulumi:"references"`
	Sections   map[string]OutputSection `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item.
	Title    string            `pulumi:"title"`
	Url      *string           `pulumi:"url"`
	Urls     []OutputUrl       `pulumi:"urls"`
	Username *string           `pulumi:"username"`
	Vault    map[string]string `pulumi:"vault"`
}

func GetServerOutput(ctx *pulumi.Context, args GetServerOutputArgs, opts ...pulumi.InvokeOption) GetServerResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (GetServerResult, error) {
			args := v.(GetServerArgs)
			r, err := GetServer(ctx, &args, opts...)
			var s GetServerResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(GetServerResultOutput)
}

type GetServerOutputArgs struct {
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id pulumi.StringPtrInput `pulumi:"id"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (GetServerOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*GetServerArgs)(nil)).Elem()
}

type GetServerResultOutput struct{ *pulumi.OutputState }

func (GetServerResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*GetServerResult)(nil)).Elem()
}

func (o GetServerResultOutput) ToGetServerResultOutput() GetServerResultOutput {
	return o
}

func (o GetServerResultOutput) ToGetServerResultOutputWithContext(ctx context.Context) GetServerResultOutput {
	return o
}

func (o GetServerResultOutput) AdminConsole() server.AdminConsoleSectionPtrOutput {
	return o.ApplyT(func(v GetServerResult) *server.AdminConsoleSection { return v.AdminConsole }).(server.AdminConsoleSectionPtrOutput)
}

func (o GetServerResultOutput) Attachments() OutputAttachmentMapOutput {
	return o.ApplyT(func(v GetServerResult) map[string]OutputAttachment { return v.Attachments }).(OutputAttachmentMapOutput)
}

func (o GetServerResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v GetServerResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o GetServerResultOutput) Fields() OutputFieldMapOutput {
	return o.ApplyT(func(v GetServerResult) map[string]OutputField { return v.Fields }).(OutputFieldMapOutput)
}

func (o GetServerResultOutput) HostingProvider() server.HostingProviderSectionPtrOutput {
	return o.ApplyT(func(v GetServerResult) *server.HostingProviderSection { return v.HostingProvider }).(server.HostingProviderSectionPtrOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o GetServerResultOutput) Id() pulumi.StringOutput {
	return o.ApplyT(func(v GetServerResult) string { return v.Id }).(pulumi.StringOutput)
}

func (o GetServerResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetServerResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o GetServerResultOutput) Password() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetServerResult) *string { return v.Password }).(pulumi.StringPtrOutput)
}

func (o GetServerResultOutput) References() OutputReferenceArrayOutput {
	return o.ApplyT(func(v GetServerResult) []OutputReference { return v.References }).(OutputReferenceArrayOutput)
}

func (o GetServerResultOutput) Sections() OutputSectionMapOutput {
	return o.ApplyT(func(v GetServerResult) map[string]OutputSection { return v.Sections }).(OutputSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o GetServerResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v GetServerResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

// The title of the item.
func (o GetServerResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v GetServerResult) string { return v.Title }).(pulumi.StringOutput)
}

func (o GetServerResultOutput) Url() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetServerResult) *string { return v.Url }).(pulumi.StringPtrOutput)
}

func (o GetServerResultOutput) Urls() OutputUrlArrayOutput {
	return o.ApplyT(func(v GetServerResult) []OutputUrl { return v.Urls }).(OutputUrlArrayOutput)
}

func (o GetServerResultOutput) Username() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetServerResult) *string { return v.Username }).(pulumi.StringPtrOutput)
}

func (o GetServerResultOutput) Vault() pulumi.StringMapOutput {
	return o.ApplyT(func(v GetServerResult) map[string]string { return v.Vault }).(pulumi.StringMapOutput)
}

func init() {
	pulumi.RegisterOutputType(GetServerResultOutput{})
}