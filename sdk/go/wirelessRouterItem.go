// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unofficial

import (
	"context"
	"reflect"

	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type WirelessRouterItem struct {
	pulumi.CustomResourceState

	AirPortId               pulumi.StringPtrOutput    `pulumi:"airPortId"`
	AttachedStoragePassword pulumi.StringPtrOutput    `pulumi:"attachedStoragePassword"`
	Attachments             OutputAttachmentMapOutput `pulumi:"attachments"`
	BaseStationName         pulumi.StringPtrOutput    `pulumi:"baseStationName"`
	BaseStationPassword     pulumi.StringPtrOutput    `pulumi:"baseStationPassword"`
	Category                pulumi.StringOutput       `pulumi:"category"`
	Fields                  OutputFieldMapOutput      `pulumi:"fields"`
	// The UUID of the item to retrieve. This field will be populated with the UUID of the item if the item it looked up by its title.
	Id              pulumi.StringOutput        `pulumi:"id"`
	NetworkName     pulumi.StringPtrOutput     `pulumi:"networkName"`
	Notes           pulumi.StringPtrOutput     `pulumi:"notes"`
	References      OutputReferenceArrayOutput `pulumi:"references"`
	Sections        OutputSectionMapOutput     `pulumi:"sections"`
	ServerIpAddress pulumi.StringPtrOutput     `pulumi:"serverIpAddress"`
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayOutput `pulumi:"tags"`
	// The title of the item.
	Title                   pulumi.StringOutput    `pulumi:"title"`
	Urls                    OutputUrlArrayOutput   `pulumi:"urls"`
	Vault                   pulumi.StringMapOutput `pulumi:"vault"`
	WirelessNetworkPassword pulumi.StringPtrOutput `pulumi:"wirelessNetworkPassword"`
	WirelessSecurity        pulumi.StringPtrOutput `pulumi:"wirelessSecurity"`
}

// NewWirelessRouterItem registers a new resource with the given unique name, arguments, and options.
func NewWirelessRouterItem(ctx *pulumi.Context,
	name string, args *WirelessRouterItemArgs, opts ...pulumi.ResourceOption) (*WirelessRouterItem, error) {
	if args == nil {
		args = &WirelessRouterItemArgs{}
	}

	args.Category = pulumi.StringPtr("Wireless Router")
	if args.AttachedStoragePassword != nil {
		args.AttachedStoragePassword = pulumi.ToSecret(args.AttachedStoragePassword).(pulumi.StringPtrOutput)
	}
	if args.BaseStationPassword != nil {
		args.BaseStationPassword = pulumi.ToSecret(args.BaseStationPassword).(pulumi.StringPtrOutput)
	}
	if args.WirelessNetworkPassword != nil {
		args.WirelessNetworkPassword = pulumi.ToSecret(args.WirelessNetworkPassword).(pulumi.StringPtrOutput)
	}
	secrets := pulumi.AdditionalSecretOutputs([]string{
		"attachedStoragePassword",
		"attachments",
		"baseStationPassword",
		"fields",
		"sections",
		"wirelessNetworkPassword",
	})
	opts = append(opts, secrets)
	opts = pkgResourceDefaultOpts(opts)
	var resource WirelessRouterItem
	err := ctx.RegisterResource("one-password-native-unofficial:index:WirelessRouterItem", name, args, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// GetWirelessRouterItem gets an existing WirelessRouterItem resource's state with the given name, ID, and optional
// state properties that are used to uniquely qualify the lookup (nil if not required).
func GetWirelessRouterItem(ctx *pulumi.Context,
	name string, id pulumi.IDInput, state *WirelessRouterItemState, opts ...pulumi.ResourceOption) (*WirelessRouterItem, error) {
	var resource WirelessRouterItem
	err := ctx.ReadResource("one-password-native-unofficial:index:WirelessRouterItem", name, id, state, &resource, opts...)
	if err != nil {
		return nil, err
	}
	return &resource, nil
}

// Input properties used for looking up and filtering WirelessRouterItem resources.
type wirelessRouterItemState struct {
	// The UUID of the vault the item is in.
	Vault *string `pulumi:"vault"`
}

type WirelessRouterItemState struct {
	// The UUID of the vault the item is in.
	Vault pulumi.StringInput
}

func (WirelessRouterItemState) ElementType() reflect.Type {
	return reflect.TypeOf((*wirelessRouterItemState)(nil)).Elem()
}

type wirelessRouterItemArgs struct {
	AirPortId               *string                          `pulumi:"airPortId"`
	AttachedStoragePassword *string                          `pulumi:"attachedStoragePassword"`
	Attachments             map[string]pulumi.AssetOrArchive `pulumi:"attachments"`
	BaseStationName         *string                          `pulumi:"baseStationName"`
	BaseStationPassword     *string                          `pulumi:"baseStationPassword"`
	// The category of the vault the item is in.
	Category        *string            `pulumi:"category"`
	Fields          map[string]Field   `pulumi:"fields"`
	NetworkName     *string            `pulumi:"networkName"`
	Notes           *string            `pulumi:"notes"`
	References      []string           `pulumi:"references"`
	Sections        map[string]Section `pulumi:"sections"`
	ServerIpAddress *string            `pulumi:"serverIpAddress"`
	// An array of strings of the tags assigned to the item.
	Tags []string `pulumi:"tags"`
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title *string       `pulumi:"title"`
	Urls  []interface{} `pulumi:"urls"`
	// The UUID of the vault the item is in.
	Vault                   *string `pulumi:"vault"`
	WirelessNetworkPassword *string `pulumi:"wirelessNetworkPassword"`
	WirelessSecurity        *string `pulumi:"wirelessSecurity"`
}

// The set of arguments for constructing a WirelessRouterItem resource.
type WirelessRouterItemArgs struct {
	AirPortId               pulumi.StringPtrInput
	AttachedStoragePassword pulumi.StringPtrInput
	Attachments             pulumi.AssetOrArchiveMapInput
	BaseStationName         pulumi.StringPtrInput
	BaseStationPassword     pulumi.StringPtrInput
	// The category of the vault the item is in.
	Category        pulumi.StringPtrInput
	Fields          FieldMapInput
	NetworkName     pulumi.StringPtrInput
	Notes           pulumi.StringPtrInput
	References      pulumi.StringArrayInput
	Sections        SectionMapInput
	ServerIpAddress pulumi.StringPtrInput
	// An array of strings of the tags assigned to the item.
	Tags pulumi.StringArrayInput
	// The title of the item to retrieve. This field will be populated with the title of the item if the item it looked up by its UUID.
	Title pulumi.StringPtrInput
	Urls  pulumi.ArrayInput
	// The UUID of the vault the item is in.
	Vault                   pulumi.StringPtrInput
	WirelessNetworkPassword pulumi.StringPtrInput
	WirelessSecurity        pulumi.StringPtrInput
}

func (WirelessRouterItemArgs) ElementType() reflect.Type {
	return reflect.TypeOf((*wirelessRouterItemArgs)(nil)).Elem()
}

type WirelessRouterItemInput interface {
	pulumi.Input

	ToWirelessRouterItemOutput() WirelessRouterItemOutput
	ToWirelessRouterItemOutputWithContext(ctx context.Context) WirelessRouterItemOutput
}

func (*WirelessRouterItem) ElementType() reflect.Type {
	return reflect.TypeOf((**WirelessRouterItem)(nil)).Elem()
}

func (i *WirelessRouterItem) ToWirelessRouterItemOutput() WirelessRouterItemOutput {
	return i.ToWirelessRouterItemOutputWithContext(context.Background())
}

func (i *WirelessRouterItem) ToWirelessRouterItemOutputWithContext(ctx context.Context) WirelessRouterItemOutput {
	return pulumi.ToOutputWithContext(ctx, i).(WirelessRouterItemOutput)
}

// WirelessRouterItemArrayInput is an input type that accepts WirelessRouterItemArray and WirelessRouterItemArrayOutput values.
// You can construct a concrete instance of `WirelessRouterItemArrayInput` via:
//
//	WirelessRouterItemArray{ WirelessRouterItemArgs{...} }
type WirelessRouterItemArrayInput interface {
	pulumi.Input

	ToWirelessRouterItemArrayOutput() WirelessRouterItemArrayOutput
	ToWirelessRouterItemArrayOutputWithContext(context.Context) WirelessRouterItemArrayOutput
}

type WirelessRouterItemArray []WirelessRouterItemInput

func (WirelessRouterItemArray) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*WirelessRouterItem)(nil)).Elem()
}

func (i WirelessRouterItemArray) ToWirelessRouterItemArrayOutput() WirelessRouterItemArrayOutput {
	return i.ToWirelessRouterItemArrayOutputWithContext(context.Background())
}

func (i WirelessRouterItemArray) ToWirelessRouterItemArrayOutputWithContext(ctx context.Context) WirelessRouterItemArrayOutput {
	return pulumi.ToOutputWithContext(ctx, i).(WirelessRouterItemArrayOutput)
}

// WirelessRouterItemMapInput is an input type that accepts WirelessRouterItemMap and WirelessRouterItemMapOutput values.
// You can construct a concrete instance of `WirelessRouterItemMapInput` via:
//
//	WirelessRouterItemMap{ "key": WirelessRouterItemArgs{...} }
type WirelessRouterItemMapInput interface {
	pulumi.Input

	ToWirelessRouterItemMapOutput() WirelessRouterItemMapOutput
	ToWirelessRouterItemMapOutputWithContext(context.Context) WirelessRouterItemMapOutput
}

type WirelessRouterItemMap map[string]WirelessRouterItemInput

func (WirelessRouterItemMap) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*WirelessRouterItem)(nil)).Elem()
}

func (i WirelessRouterItemMap) ToWirelessRouterItemMapOutput() WirelessRouterItemMapOutput {
	return i.ToWirelessRouterItemMapOutputWithContext(context.Background())
}

func (i WirelessRouterItemMap) ToWirelessRouterItemMapOutputWithContext(ctx context.Context) WirelessRouterItemMapOutput {
	return pulumi.ToOutputWithContext(ctx, i).(WirelessRouterItemMapOutput)
}

type WirelessRouterItemOutput struct{ *pulumi.OutputState }

func (WirelessRouterItemOutput) ElementType() reflect.Type {
	return reflect.TypeOf((**WirelessRouterItem)(nil)).Elem()
}

func (o WirelessRouterItemOutput) ToWirelessRouterItemOutput() WirelessRouterItemOutput {
	return o
}

func (o WirelessRouterItemOutput) ToWirelessRouterItemOutputWithContext(ctx context.Context) WirelessRouterItemOutput {
	return o
}

type WirelessRouterItemArrayOutput struct{ *pulumi.OutputState }

func (WirelessRouterItemArrayOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*[]*WirelessRouterItem)(nil)).Elem()
}

func (o WirelessRouterItemArrayOutput) ToWirelessRouterItemArrayOutput() WirelessRouterItemArrayOutput {
	return o
}

func (o WirelessRouterItemArrayOutput) ToWirelessRouterItemArrayOutputWithContext(ctx context.Context) WirelessRouterItemArrayOutput {
	return o
}

func (o WirelessRouterItemArrayOutput) Index(i pulumi.IntInput) WirelessRouterItemOutput {
	return pulumi.All(o, i).ApplyT(func(vs []interface{}) *WirelessRouterItem {
		return vs[0].([]*WirelessRouterItem)[vs[1].(int)]
	}).(WirelessRouterItemOutput)
}

type WirelessRouterItemMapOutput struct{ *pulumi.OutputState }

func (WirelessRouterItemMapOutput) ElementType() reflect.Type {
	return reflect.TypeOf((*map[string]*WirelessRouterItem)(nil)).Elem()
}

func (o WirelessRouterItemMapOutput) ToWirelessRouterItemMapOutput() WirelessRouterItemMapOutput {
	return o
}

func (o WirelessRouterItemMapOutput) ToWirelessRouterItemMapOutputWithContext(ctx context.Context) WirelessRouterItemMapOutput {
	return o
}

func (o WirelessRouterItemMapOutput) MapIndex(k pulumi.StringInput) WirelessRouterItemOutput {
	return pulumi.All(o, k).ApplyT(func(vs []interface{}) *WirelessRouterItem {
		return vs[0].(map[string]*WirelessRouterItem)[vs[1].(string)]
	}).(WirelessRouterItemOutput)
}

func init() {
	pulumi.RegisterInputType(reflect.TypeOf((*WirelessRouterItemInput)(nil)).Elem(), &WirelessRouterItem{})
	pulumi.RegisterInputType(reflect.TypeOf((*WirelessRouterItemArrayInput)(nil)).Elem(), WirelessRouterItemArray{})
	pulumi.RegisterInputType(reflect.TypeOf((*WirelessRouterItemMapInput)(nil)).Elem(), WirelessRouterItemMap{})
	pulumi.RegisterOutputType(WirelessRouterItemOutput{})
	pulumi.RegisterOutputType(WirelessRouterItemArrayOutput{})
	pulumi.RegisterOutputType(WirelessRouterItemMapOutput{})
}
