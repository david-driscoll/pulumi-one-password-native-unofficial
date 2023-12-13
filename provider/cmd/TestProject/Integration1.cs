using System.Runtime.CompilerServices;
using Pulumi;
using pulumi_resource_one_password_native_unofficial.Domain;
using Pulumi.Automation;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;
using TestProject.Helpers;

namespace TestProject;

[Collection("Connect collection")]
[UsesVerify]
public class Integration1 : IClassFixture<PulumiFixture>
{
    private readonly PulumiFixture _fixture;
    private readonly ConnectServerFixture _serverFixture;

    public Integration1(PulumiFixture fixture, ConnectServerFixture serverFixture)
    {
        _fixture = fixture;
        _serverFixture = serverFixture;
        fixture.Connect(_serverFixture);
    }

    [Fact]
    public async Task Should_Recreate_Item_If_Category_Changes()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new Item("item", new()
            {
                Title = "Test Item",
                Category = ResourceType.Membership.InputCategory,
                Fields = new InputMap<FieldArgs>()
                {
                    ["user"] = new FieldArgs()
                    {
                        Value = "abcd"
                    }
                },
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note"
            });
        });
        var programUpdate = PulumiFn.Create(() =>
        {
            var login = new LoginItem("item", new()
            {
                Title = "Test Item",
                Category = ResourceType.DriverLicense.InputCategory,
                Fields = new InputMap<FieldArgs>()
                {
                    ["user"] = new FieldArgs()
                    {
                        Value = "abcd"
                    }
                },
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note"
            });
        });

        var name = Guid.NewGuid().ToString("N");

        var yaml = $"""
                    name: {name}
                    runtime: dotnet
                    description: A minimal C# Pulumi program
                    plugins:
                      providers:
                        - name: one-password-native-unofficial
                          path: {GetPulumiPluginExeLocation()}
                    """;


        var workDir = Path.Combine(_serverFixture.TemporaryDirectory, name, "workdir");
        Directory.CreateDirectory(workDir);
        await File.WriteAllTextAsync(Path.Combine(workDir, "Pulumi.yaml"), yaml);

        {
            var stack = await LocalWorkspace.CreateStackAsync(
                new LocalProgramArgs("csharp", workDir)
                {
                    EnvironmentVariables = _fixture.EnvironmentVariables,
                    Program = program,
                });
            await stack.SetConfigAsync("one-password-native-unofficial:connectHost", new(_serverFixture.ConnectHost.ToString(), false));
            await stack.SetConfigAsync("one-password-native-unofficial:connectToken", new(_serverFixture.ConnectToken, true));
            var config = await stack.GetAllConfigAsync();
            // var refreshConfig = await stack.RefreshConfigAsync();
            var up = await stack.UpAsync();
        }
        {
            var stack = await LocalWorkspace.CreateOrSelectStackAsync(
                new LocalProgramArgs("csharp", workDir)
                {
                    EnvironmentVariables = _fixture.EnvironmentVariables,
                    Program = programUpdate,
                });
            var up = await stack.UpAsync(new UpOptions()
            {
            });
        }
    }

    [Fact]
    public async Task Should_Create_Login_Item()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new LoginItem("login", new()
            {
                Title = "Test Login",
                Username = "myusername",
                Password = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note"
            });
        });

        var name = Guid.NewGuid().ToString("N");

        var yaml = $"""
                    name: {name}
                    runtime: dotnet
                    description: A minimal C# Pulumi program
                    plugins:
                      providers:
                        - name: one-password-native-unofficial
                          path: {GetPulumiPluginExeLocation()}
                    """;


        var workDir = Path.Combine(_serverFixture.TemporaryDirectory, name, "workdir");
        Directory.CreateDirectory(workDir);
        await File.WriteAllTextAsync(Path.Combine(workDir, "Pulumi.yaml"), yaml);

        var stack = await LocalWorkspace.CreateOrSelectStackAsync(
            new LocalProgramArgs("csharp", workDir)
            {
                EnvironmentVariables = _fixture.EnvironmentVariables,
                Program = program,
            });
        await stack.SetConfigAsync("one-password-native-unofficial:connectHost", new(_serverFixture.ConnectHost.ToString(), false));
        await stack.SetConfigAsync("one-password-native-unofficial:connectToken", new(_serverFixture.ConnectToken, true));
        var config = await stack.GetAllConfigAsync();
        var up = await stack.UpAsync();
        var u2 = await stack.UpAsync();
        await stack.DestroyAsync();
    }

    [Fact]
    public async Task Should_Update_Login_Item()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new LoginItem("login", new()
            {
                Title = "Test Login",
                Username = "myusername",
                Password = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note"
            });
        });
        var programUpdate = PulumiFn.Create(() =>
        {
            var login = new LoginItem("login", new()
            {
                Title = "Test Login 2",
                Username = "myusername",
                Password = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                    "http://nothome.com",
                },
                Notes = "this was a note"
            });
        });

        var name = Guid.NewGuid().ToString("N");

        var yaml = $"""
                    name: {name}
                    runtime: dotnet
                    description: A minimal C# Pulumi program
                    plugins:
                      providers:
                        - name: one-password-native-unofficial
                          path: {GetPulumiPluginExeLocation()}
                    """;


        var workDir = Path.Combine(_serverFixture.TemporaryDirectory, name, "workdir");
        Directory.CreateDirectory(workDir);
        await File.WriteAllTextAsync(Path.Combine(workDir, "Pulumi.yaml"), yaml);

        {
            var stack = await LocalWorkspace.CreateStackAsync(
                new LocalProgramArgs("csharp", workDir)
                {
                    EnvironmentVariables = _fixture.EnvironmentVariables,
                    Program = program,
                });
            await stack.SetConfigAsync("one-password-native-unofficial:connectHost", new(_serverFixture.ConnectHost.ToString(), false));
            await stack.SetConfigAsync("one-password-native-unofficial:connectToken", new(_serverFixture.ConnectToken, true));
            var config = await stack.GetAllConfigAsync();
            // var refreshConfig = await stack.RefreshConfigAsync();
            var up = await stack.UpAsync();
        }
        {
            var stack = await LocalWorkspace.CreateOrSelectStackAsync(
                new LocalProgramArgs("csharp", workDir)
                {
                    EnvironmentVariables = _fixture.EnvironmentVariables,
                    Program = programUpdate,
                });
            var up = await stack.UpAsync(new UpOptions()
            {
            });
        }
        // await stack.DestroyAsync(new ()
        // {
        //     Debug = true,
        //     Tracing = "7"
        // });
    }

    private static string GetPulumiPluginExeLocation([CallerFilePath] string callerFilePath = null!)
    {
        var prefix = callerFilePath.Substring(0, callerFilePath.IndexOf("TestProject", StringComparison.OrdinalIgnoreCase));
        return Path.Combine(prefix, "pulumi-resource-one-password-native-unofficial", "bin", "Debug", "net8.0");
    }
}
