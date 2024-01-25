using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using CliWrap;
using FluentAssertions;
using GeneratedCode;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pulumi;
using Pulumi.Automation;
using Pulumi.Automation.Commands.Exceptions;
using Pulumi.Automation.Events;
using Pulumi.Automation.Exceptions;
using Pulumi.Automation.Serialization;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.Identity.Inputs;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;
using Serilog;
using Serilog.Extensions.Logging;
using TestProject.Helpers;
using Xunit.Abstractions;
using FieldPurpose = GeneratedCode.FieldPurpose;
using File = System.IO.File;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Item = Rocket.Surgery.OnePasswordNativeUnofficial.Item;
using ResourceType = pulumi_resource_one_password_native_unofficial.Domain.ResourceType;

namespace TestProject;

[Collection("Connect collection")]
[UsesVerify]
public class Integration1 : IClassFixture<PulumiFixture>, IAsyncLifetime
{
    private readonly PulumiFixture _fixture;
    private readonly ConnectServerFixture _serverFixture;
    private readonly ILogger _logger;
    private readonly string _name;
    private readonly string _workDir;

    public Integration1(PulumiFixture fixture, ConnectServerFixture serverFixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _serverFixture = serverFixture;
        fixture.Connect(_serverFixture);
        var logger = Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.TestOutput(output)
            .CreateLogger();
        _logger = CreateLogger(logger);


        _name = Guid.NewGuid().ToString("N");
        _workDir = Path.Combine(_serverFixture.TemporaryDirectory, _name, "workdir");
    }

    public Task InitializeAsync()
    {
        var yaml = $"""
                    name: {_name}
                    runtime: dotnet
                    description: A minimal C# Pulumi program
                    plugins:
                      providers:
                        - name: one-password-native-unofficial
                          path: {GetPulumiPluginExeLocation()}
                    """;
        Directory.CreateDirectory(_workDir);
        return File.WriteAllTextAsync(Path.Combine(_workDir, "Pulumi.yaml"), yaml);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        {
            var stack = await CreateStack("csharp", program);
            await stack.UpAsync();
            await ScrubVerify(stack.UpAsync(programUpdate).WithVaultItem(_serverFixture));
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        var stack = await CreateStack("csharp", program);

        await stack.GetAllConfigAsync();
        await ScrubVerify(stack.UpAsync().WithVaultItem(_serverFixture));
        await stack.DestroyAsync();
    }


    [Fact]
    public async Task Should_Be_Able_To_Use_Username_Password_From_Login_Item()
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
                Notes = "this is a note",
                Sections = new InputMap<SectionArgs>()
                {
                    ["backup"] = new SectionArgs()
                    {
                        Label = "My Backup",
                        Fields = new InputMap<FieldArgs>()
                        {
                            ["myfield"] = new FieldArgs() { Value = "1234" }
                        }
                    }
                }
            });

            login.Username.Apply(z => z.Should().Be("myusername"));
            login.Fields.Apply(z => z["username"].Value.Should().Be("myusername"));
            login.Password.Apply(z => z.Should().Be("mypassword"));
            login.Fields.Apply(z => z["password"].Value.Should().Be("mypassword"));
            login.Sections.Apply(z => z["backup"].Fields["myfield"].Reference.Should().NotBeNull());

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        var stack = await CreateStack("csharp", program);

        await stack.GetAllConfigAsync();
        await ScrubVerify(stack.UpAsync().WithVaultItem(_serverFixture));
        await stack.DestroyAsync();
    }


    [Fact]
    public async Task Should_Be_Able_To_Use_Username_Password_From_Login_Item_During_Preview()
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
                Notes = "this is a note",
                Sections = new InputMap<SectionArgs>()
                {
                    ["backup"] = new SectionArgs()
                    {
                        Label = "My Backup",
                        Fields = new InputMap<FieldArgs>()
                        {
                            ["myfield"] = new FieldArgs() { Value = "1234" }
                        }
                    }
                }
            });

            login.Username.Apply(z => z.Should().Be("myusername"));
            login.Fields.Apply(z => z["username"].Value.Should().Be("myusername"));
            login.Password.Apply(z => z.Should().Be("mypassword"));
            login.Fields.Apply(z => z["password"].Value.Should().Be("mypassword"));
            login.Sections.Apply(z => z["backup"].Fields["myfield"].Reference.Should().NotBeNull());

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        var stack = await CreateStack("csharp", program);

        await stack.GetAllConfigAsync();
        await ScrubVerify(stack.PreviewAsync());
    }

    [Fact]
    public async Task Should_Be_Able_To_Use_Username_Password_From_ApiCredential()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new APICredentialItem("login", new()
            {
                Title = "Test Login",
                Username = "myusername",
                Credential = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note",
                Sections = new InputMap<SectionArgs>()
                {
                    ["backup"] = new SectionArgs()
                    {
                        Label = "My Backup",
                        Fields = new InputMap<FieldArgs>()
                        {
                            ["myfield"] = new FieldArgs() { Value = "1234" }
                        }
                    }
                }
            });

            login.Username.Apply(z => z.Should().Be("myusername"));
            login.Fields.Apply(z => z["username"].Value.Should().Be("myusername"));
            login.Credential.Apply(z => z.Should().Be("mypassword"));
            login.Fields.Apply(z => z["credential"].Value.Should().Be("mypassword"));
            login.Fields.Apply(z => z["credential"].Reference.Should().NotBeNull());
            login.Sections.Apply(z => z["backup"].Fields["myfield"].Reference.Should().NotBeNull());

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        var stack = await CreateStack("csharp", program);

        await stack.GetAllConfigAsync();
        await ScrubVerify( stack.UpAsync().WithVaultItem(_serverFixture));
        await stack.DestroyAsync();
    }


    [Fact]
    public async Task Should_Be_Able_To_Use_Username_Password_From_ApiCredential_During_Preview()
    {
        var program = PulumiFn.Create(() =>
        {
            var login = new APICredentialItem("login", new()
            {
                Title = "Test Login",
                Username = "myusername",
                Credential = "mypassword",
                Tags = new string[] { "Test Tag" },
                Vault = "testing-pulumi",
                Urls = new()
                {
                    "http://notlocalhost.com",
                },
                Notes = "this is a note",
                Sections = new InputMap<SectionArgs>()
                {
                    ["backup"] = new SectionArgs()
                    {
                        Label = "My Backup",
                        Fields = new InputMap<FieldArgs>()
                        {
                            ["myfield"] = new FieldArgs() { Value = "1234" }
                        }
                    }
                }
            });

            login.Username.Apply(z => z.Should().Be("myusername"));
            login.Fields.Apply(z => z["username"].Value.Should().Be("myusername"));
            login.Credential.Apply(z => z.Should().Be("mypassword"));
            login.Fields.Apply(z => z["credential"].Value.Should().Be("mypassword"));
            login.Fields.Apply(z => z["credential"].Reference.Should().NotBeNull());
            login.Sections.Apply(z => z["backup"].Fields["myfield"].Reference.Should().NotBeNull());

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        var stack = await CreateStack("csharp", program);

        await stack.GetAllConfigAsync();
        await ScrubVerify( stack.PreviewAsync());
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        {
            var stack = await CreateStack("csharp", program);
            await stack.UpAsync();
            await ScrubVerify( stack.UpAsync(programUpdate).WithVaultItem(_serverFixture));
        }
    }


    [Fact]
    public async Task Should_Update_Login_Item_Adding_A_New_Section()
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });
        var programUpdate = PulumiFn.Create(() =>
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
                Notes = "this is a note",
                Sections = new InputMap<SectionArgs>()
                {
                    ["backup"] = new SectionArgs()
                    {
                        Label = "My Backup",
                        Fields = new InputMap<FieldArgs>()
                        {
                            ["myfield"] = new FieldArgs() { Value = "1234" }
                        }
                    }
                }
            });

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        {
            var stack = await CreateStack("csharp", program);
            await stack.UpAsync();
            await ScrubVerify( stack.UpAsync(programUpdate).WithVaultItem(_serverFixture));
        }
    }

    [Fact]
    public async Task Should_Refresh_Items()
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

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        {
            var stack = await CreateStack("csharp", program);

            await stack.UpAsync();

            await ScrubVerify( stack.RefreshAsync());
        }
    }

    [Fact(Skip = "Import isn't working properly yet")]
    public async Task Should_Import_Items()
    {
        var importProgram = PulumiFn.Create(() =>
        {
            var login = new DatabaseItem("ImportItem", new()
            {
                Title = "ImportItem",
                Vault = "testing-pulumi",
                Notes = "this is a note",
                Database = "test",
                Port = "8888",
                Server = "localhost",
                Type = "SQLite",
                Username = "test"
            }, new CustomResourceOptions()
            {
                ImportId = $"op://testing-pulumi/5elecsqms2qxrd67ohyyek4xmy"
            });

            return new Dictionary<string, object?>()
            {
                ["id"] = login.Id,
                ["vaultId"] = login.Vault.Apply(z => z.Id)
            };
        });

        {
            var stack = await CreateStack("csharp", importProgram);
            await ScrubVerify(stack.UpAsync());
        }
    }


    private static string GetPulumiPluginExeLocation([CallerFilePath] string callerFilePath = null!)
    {
        var prefix = callerFilePath.Substring(0, callerFilePath.IndexOf("TestProject", StringComparison.OrdinalIgnoreCase));
        return Path.Combine(prefix, "pulumi-resource-one-password-native-unofficial", "bin", "Debug", "net8.0");
    }

    private static Microsoft.Extensions.Logging.ILogger CreateLogger(Serilog.ILogger logger) =>
        new Serilog.Extensions.Logging.SerilogLoggerFactory(logger).CreateLogger("Program");

    private async Task<MyWorkspaceStack> CreateStack(string name, PulumiFn program)
    {
        var stack = await LocalWorkspace.CreateOrSelectStackAsync(
            new LocalProgramArgs(name, _workDir)
            {
                EnvironmentVariables = _fixture.EnvironmentVariables,
                Program = program,
                Logger = _logger,
            });
        await stack.SetConfigAsync("one-password-native-unofficial:connectHost", new(_serverFixture.ConnectHost.ToString(), false));
        await stack.SetConfigAsync("one-password-native-unofficial:connectToken", new(_serverFixture.ConnectToken, true));

        return new(stack, _logger);
    }

    private Task ScrubVerify<T>(Task<T> result)
    {
        return Verify(result).AddIdScrubber(_name);
    }
}
