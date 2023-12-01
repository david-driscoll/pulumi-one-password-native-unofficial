// Code generated by Pulumi SDK Generator DO NOT EDIT.
// *** WARNING: Do not edit by hand unless you're certain you know what you are doing! ***

package pulumi_one_password_native_unoffical

import (
	"fmt"

	"github.com/blang/semver"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

type module struct {
	version semver.Version
}

func (m *module) Version() semver.Version {
	return m.version
}

func (m *module) Construct(ctx *pulumi.Context, name, typ, urn string) (r pulumi.Resource, err error) {
	switch typ {
	case "one-password-native-unoffical:index:APICredentialItem":
		r = &APICredentialItem{}
	case "one-password-native-unoffical:index:BankAccountItem":
		r = &BankAccountItem{}
	case "one-password-native-unoffical:index:CreditCardItem":
		r = &CreditCardItem{}
	case "one-password-native-unoffical:index:CryptoWalletItem":
		r = &CryptoWalletItem{}
	case "one-password-native-unoffical:index:DatabaseItem":
		r = &DatabaseItem{}
	case "one-password-native-unoffical:index:DocumentItem":
		r = &DocumentItem{}
	case "one-password-native-unoffical:index:DriverLicenseItem":
		r = &DriverLicenseItem{}
	case "one-password-native-unoffical:index:EmailAccountItem":
		r = &EmailAccountItem{}
	case "one-password-native-unoffical:index:IdentityItem":
		r = &IdentityItem{}
	case "one-password-native-unoffical:index:Item":
		r = &Item{}
	case "one-password-native-unoffical:index:LoginItem":
		r = &LoginItem{}
	case "one-password-native-unoffical:index:MedicalRecordItem":
		r = &MedicalRecordItem{}
	case "one-password-native-unoffical:index:MembershipItem":
		r = &MembershipItem{}
	case "one-password-native-unoffical:index:OutdoorLicenseItem":
		r = &OutdoorLicenseItem{}
	case "one-password-native-unoffical:index:PassportItem":
		r = &PassportItem{}
	case "one-password-native-unoffical:index:PasswordItem":
		r = &PasswordItem{}
	case "one-password-native-unoffical:index:RewardProgramItem":
		r = &RewardProgramItem{}
	case "one-password-native-unoffical:index:SSHKeyItem":
		r = &SSHKeyItem{}
	case "one-password-native-unoffical:index:SecureNoteItem":
		r = &SecureNoteItem{}
	case "one-password-native-unoffical:index:ServerItem":
		r = &ServerItem{}
	case "one-password-native-unoffical:index:SocialSecurityNumberItem":
		r = &SocialSecurityNumberItem{}
	case "one-password-native-unoffical:index:SoftwareLicenseItem":
		r = &SoftwareLicenseItem{}
	case "one-password-native-unoffical:index:WirelessRouterItem":
		r = &WirelessRouterItem{}
	default:
		return nil, fmt.Errorf("unknown resource type: %s", typ)
	}

	err = ctx.RegisterResource(typ, name, nil, r, pulumi.URN_(urn))
	return
}

type pkg struct {
	version semver.Version
}

func (p *pkg) Version() semver.Version {
	return p.version
}

func (p *pkg) ConstructProvider(ctx *pulumi.Context, name, typ, urn string) (pulumi.ProviderResource, error) {
	if typ != "pulumi:providers:one-password-native-unoffical" {
		return nil, fmt.Errorf("unknown provider type: %s", typ)
	}

	r := &Provider{}
	err := ctx.RegisterResource(typ, name, nil, r, pulumi.URN_(urn))
	return r, err
}

func init() {
	version, _ := PkgVersion()
	pulumi.RegisterResourceModule(
		"one-password-native-unoffical",
		"index",
		&module{version},
	)
	pulumi.RegisterResourcePackage(
		"one-password-native-unoffical",
		&pkg{version},
	)
}