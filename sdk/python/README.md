# Pulumi OnePassword Native Integration (Unofficial)

This is a pulumi providier that allows you to use the [OnePassword CLI](https://support.1password.com/command-line-getting-started/) to manage secrets in your pulumi stack.  As well as the OnePassword Connect Server.

This provider is unofficial and not supported by 1Password.

Also this providier is an example of a pulumi provider written in C#.

## Sample one-password-native-unofficial Component Provider


## Prerequisites

- Pulumi CLI
- OnePassword CLI
- Node.js
- Yarn
- Go 1.17 (to regenerate the SDKs)
- Python 3.6+ (to build the Python SDK)
- .NET Core SDK (to build the .NET SDK)

## Build and Test

```bash
# Build and install the provider
make install_provider

# Regenerate SDKs
make generate

# Ensure the pulumi-provider-one-password-native-unofficial script is on PATH
$ export PATH=$PATH:$PWD/bin

# Test Node.js SDK
$ make install_nodejs_sdk
$ cd examples/simple
$ yarn install
$ yarn link pulumi-one-password-native-unofficial
$ pulumi stack init test
$ pulumi config set aws:region us-east-1
$ pulumi up
```

## Example component

### Schema
