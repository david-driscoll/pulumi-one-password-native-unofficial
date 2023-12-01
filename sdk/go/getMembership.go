// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unoffical

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func GetMembership(ctx *pulumi.Context, args *GetMembershipArgs, opts ...pulumi.InvokeOption) (*GetMembershipResult, error) {
	var rv GetMembershipResult
	err := ctx.Invoke("one-password-native-unoffical:index:GetMembership", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type GetMembershipArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid *string `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault string `pulumi:"vault"`
}

type GetMembershipResult struct {
	Attachments map[string]OutField   `pulumi:"attachments"`
	Category    string                `pulumi:"category"`
	ExpiryDate  *string               `pulumi:"expiryDate"`
	Fields      map[string]OutField   `pulumi:"fields"`
	Group       *string               `pulumi:"group"`
	MemberId    *string               `pulumi:"memberId"`
	MemberName  *string               `pulumi:"memberName"`
	MemberSince *string               `pulumi:"memberSince"`
	Notes       *string               `pulumi:"notes"`
	Pin         *string               `pulumi:"pin"`
	References  map[string]OutField   `pulumi:"references"`
	Sections    map[string]OutSection `pulumi:"sections"`
	// An array of strings of the tags assigned to the item.
	Tags      []string `pulumi:"tags"`
	Telephone *string  `pulumi:"telephone"`
	// The title of the item.
	Title string `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid string `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault   string  `pulumi:"vault"`
	Website *string `pulumi:"website"`
}

func GetMembershipOutput(ctx *pulumi.Context, args GetMembershipOutputArgs, opts ...pulumi.InvokeOption) GetMembershipResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (GetMembershipResult, error) {
			args := v.(GetMembershipArgs)
			r, err := GetMembership(ctx, &args, opts...)
			var s GetMembershipResult
			if r != nil {
				s = *r
			}
			return s, err
		}).(GetMembershipResultOutput)
}

type GetMembershipOutputArgs struct {
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput `pulumi:"title"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Uuid pulumi.StringPtrInput `pulumi:"uuid"`
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput `pulumi:"vault"`
}

func (GetMembershipOutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*GetMembershipArgs)(nil)).Elem()
}

type GetMembershipResultOutput struct{ *pulumi.OutputState }

func (GetMembershipResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*GetMembershipResult)(nil)).Elem()
}

func (o GetMembershipResultOutput) ToGetMembershipResultOutput() GetMembershipResultOutput {
	return o
}

func (o GetMembershipResultOutput) ToGetMembershipResultOutputWithContext(ctx context.Context) GetMembershipResultOutput {
	return o
}

func (o GetMembershipResultOutput) Attachments() OutFieldMapOutput {
	return o.ApplyT(func(v GetMembershipResult) map[string]OutField { return v.Attachments }).(OutFieldMapOutput)
}

func (o GetMembershipResultOutput) Category() pulumi.StringOutput {
	return o.ApplyT(func(v GetMembershipResult) string { return v.Category }).(pulumi.StringOutput)
}

func (o GetMembershipResultOutput) ExpiryDate() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.ExpiryDate }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) Fields() OutFieldMapOutput {
	return o.ApplyT(func(v GetMembershipResult) map[string]OutField { return v.Fields }).(OutFieldMapOutput)
}

func (o GetMembershipResultOutput) Group() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.Group }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) MemberId() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.MemberId }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) MemberName() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.MemberName }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) MemberSince() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.MemberSince }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) Notes() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.Notes }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) Pin() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.Pin }).(pulumi.StringPtrOutput)
}

func (o GetMembershipResultOutput) References() OutFieldMapOutput {
	return o.ApplyT(func(v GetMembershipResult) map[string]OutField { return v.References }).(OutFieldMapOutput)
}

func (o GetMembershipResultOutput) Sections() OutSectionMapOutput {
	return o.ApplyT(func(v GetMembershipResult) map[string]OutSection { return v.Sections }).(OutSectionMapOutput)
}

// An array of strings of the tags assigned to the item.
func (o GetMembershipResultOutput) Tags() pulumi.StringArrayOutput {
	return o.ApplyT(func(v GetMembershipResult) []string { return v.Tags }).(pulumi.StringArrayOutput)
}

func (o GetMembershipResultOutput) Telephone() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.Telephone }).(pulumi.StringPtrOutput)
}

// The title of the item.
func (o GetMembershipResultOutput) Title() pulumi.StringOutput {
	return o.ApplyT(func(v GetMembershipResult) string { return v.Title }).(pulumi.StringOutput)
}

// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
func (o GetMembershipResultOutput) Uuid() pulumi.StringOutput {
	return o.ApplyT(func(v GetMembershipResult) string { return v.Uuid }).(pulumi.StringOutput)
}

// The UUID of the vault the item is in.
func (o GetMembershipResultOutput) Vault() pulumi.StringOutput {
	return o.ApplyT(func(v GetMembershipResult) string { return v.Vault }).(pulumi.StringOutput)
}

func (o GetMembershipResultOutput) Website() pulumi.StringPtrOutput {
	return o.ApplyT(func(v GetMembershipResult) *string { return v.Website }).(pulumi.StringPtrOutput)
}

func init() {
	pulumi.RegisterOutputType(GetMembershipResultOutput{})
}