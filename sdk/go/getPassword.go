// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unoffical

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func GetPassword(ctx *pulumi.Context, args *GetPasswordArgs, opts ...pulumi.InvokeOption) (*GetPasswordResult, error) {
	var rv GetPasswordResult
	err := ctx.Invoke("one-password-native-unoffical:index:GetPassword", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type GetPasswordArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid *string `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type GetPasswordResult struct {
	Attachments map[string]OutField   `pulumi:"attachments"`
	Category    string                `pulumi:"category"`
	Fields      map[string]OutField   `pulumi:"fields"`
	Notes       *string               `pulumi:"notes"`
	Password    *string               `pulumi:"password"`
	References  map[string]OutField   `pulumi:"references"`
	Sections    map[string]OutSection `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item.
	Title string `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid string `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

func GetPasswordOutput(ctx *pulumi.Context, args GetPasswordOutputArgs, opts ...pulumi.InvokeOption) GetPasswordResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (GetPasswordResult, error) {
			args := v.(GetPasswordArgs)
			r, err := GetPassword(ctx, &args, opts...)
			var s GetPasswordResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(GetPasswordResultOutput)
}

type GetPasswordOutputArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid pulumi.StringPtrInput `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (GetPasswordOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*GetPasswordArgs)(nil)).Elem()
}

type GetPasswordResultOutput struct{ *pulumi.OutputState }

func (GetPasswordResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*GetPasswordResult)(nil)).Elem()
}

func (o GetPasswordResultOutput) ToGetPasswordResultOutput() GetPasswordResultOutput {
	return o
}

func (o GetPasswordResultOutput) ToGetPasswordResultOutputWithContext(ctx context.Context) GetPasswordResultOutput {
	return o
}

func (o GetPasswordResultOutput) Attachments() OutFieldMapOutput {
	return o.ApplyT(func(v GetPasswordResult) map[string]OutField { return v.Attachments }).(OutFieldMapOutput)
}

func (o GetPasswordResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v GetPasswordResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o GetPasswordResultOutput) Fields() OutFieldMapOutput {
	return o.ApplyT(func(v GetPasswordResult) map[string]OutField { return v.Fields }).(OutFieldMapOutput)
}

func (o GetPasswordResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetPasswordResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o GetPasswordResultOutput) Password() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetPasswordResult) *string { return v.Password }).(pulumi.StringPtrOutput)
}

func (o GetPasswordResultOutput) References() OutFieldMapOutput {
	return o.ApplyT(func(v GetPasswordResult) map[string]OutField { return v.References }).(OutFieldMapOutput)
}

func (o GetPasswordResultOutput) Sections() OutSectionMapOutput {
	return o.ApplyT(func(v GetPasswordResult) map[string]OutSection { return v.Sections }).(OutSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o GetPasswordResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v GetPasswordResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

// The title of the item.
func (o GetPasswordResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v GetPasswordResult) string { return v.Title }).(pulumi.StringOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o GetPasswordResultOutput) Uuid() pulumi.StringOutput {
	return o.ApplyT(func(v GetPasswordResult) string { return v.Uuid }).(pulumi.StringOutput)
}

// The UUID of the vault the item is in.
func (o GetPasswordResultOutput) Vault() pulumi.StringOutput {
	return o.ApplyT(func(v GetPasswordResult) string { return v.Vault }).(pulumi.StringOutput)
}

func init() {
	pulumi.RegisterOutputType(GetPasswordResultOutput{})
}