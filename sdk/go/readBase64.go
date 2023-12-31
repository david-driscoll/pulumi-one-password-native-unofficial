// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func ReadBase64(ctx *pulumi.Context, args *ReadBase64Args, opts ...pulumi.InvokeOption) (*ReadBase64Result, error) {
	opts = pkgInvokeDefaultOpts(opts)
	var rv ReadBase64Result
	err := ctx.Invoke("one-password-native-unofficial:index:ReadBase64", args, &rv, opts...)
	if err != nil {
		return nil, err
	}
	return &rv, nil
}

type ReadBase64Args struct {
	// The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file
	Reference string `pulumi:"reference"`
}

type ReadBase64Result struct {
	// The read value as a base64 encoded string
	Base64 string `pulumi:"base64"`
}

func ReadBase64Output(ctx *pulumi.Context, args ReadBase64OutputArgs, opts ...pulumi.InvokeOption) ReadBase64ResultOutput {
	return pulumi.ToOutputWithContext(context.Background(), args).
		ApplyT(func(v interface{}) (ReadBase64Result, error) {
			args := v.(ReadBase64Args)
			r, err := ReadBase64(ctx, &args, opts...)
			var s ReadBase64Result
			if r != nil {
				s = *r
			}
			return s, err
		}).(ReadBase64ResultOutput)
}

type ReadBase64OutputArgs struct {
	// The 1Password secret reference path to the attachment.  eg: op://vault/item/[section]/file
	Reference pulumi.StringInput `pulumi:"reference"`
}

func (ReadBase64OutputArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*ReadBase64Args)(nil)).Elem()
}

type ReadBase64ResultOutput struct{ *pulumi.OutputState }

func (ReadBase64ResultOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*ReadBase64Result)(nil)).Elem()
}

func (o ReadBase64ResultOutput) ToReadBase64ResultOutput() ReadBase64ResultOutput {
	return o
}

func (o ReadBase64ResultOutput) ToReadBase64ResultOutputWithContext(ctx context.Context) ReadBase64ResultOutput {
	return o
}

// The read value as a base64 encoded string
func (o ReadBase64ResultOutput) Base64() pulumi.StringOutput {
	return o.ApplyT(func(v ReadBase64Result) string { return v.Base64 }).(pulumi.StringOutput)
}

func init() {
	pulumi.RegisterOutputType(ReadBase64ResultOutput{})
}
