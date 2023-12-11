using System.Collections;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Pulumi;
using pulumi_resource_one_password_native_unofficial;
using pulumi_resource_one_password_native_unofficial.OnePasswordCli;
using Pulumi.Experimental.Provider;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using TestProject.Helpers;
using Xunit.Abstractions;
using YamlDotNet.Serialization;
using FieldType = Rocket.Surgery.OnePasswordNativeUnofficial.FieldType;

namespace TestProject;

[Collection("Connect collection")]
[UsesVerify]
public class ConnectServerItemTests : IClassFixture<PulumiFixture>
{
    private readonly PulumiFixture _fixture;
    private readonly ConnectServerFixture _serverFixture;
    private readonly Logger _logger;

    public ConnectServerItemTests(PulumiFixture fixture, ConnectServerFixture serverFixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _serverFixture = serverFixture;
        _logger = new LoggerConfiguration()
            .WriteTo.TestOutput(output, LogEventLevel.Verbose)
            .CreateLogger();
        fixture.Connect(serverFixture);
    }

    [Fact]
    public async Task Should_Create_Login_Item()
    {
        var provider = new OnePasswordProvider(_logger);

        await _serverFixture.ConfigureProvider(provider);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            // Password = "secret1234",
            Fields = new()
            {
                ["password"] = new FieldArgs()
                {
                    Value = "secret1234",
                    Type = FieldType.Concealed
                }
            },
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secret1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"));
    }


    [Fact]
    public async Task Should_Update_Login_Item()
    {
        var provider = new OnePasswordProvider(_logger);

        await _serverFixture.ConfigureProvider(provider);

        var createInput = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            Password = "secret1234",
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secret1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });
        
        var updateInput = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me2",
            Password = "secret12344",
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secrtet1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag", "another tag" }
        });

        var create = await provider.Create(new CreateRequest(createInput.Urn, createInput.Request, TimeSpan.MaxValue, false), CancellationToken.None);
        await Task.Delay(5000);
        
        var update = await provider.Update(new UpdateRequest(updateInput.Urn, create.Id!,create.Properties!.ToImmutableDictionary(), updateInput.Request.ToImmutableDictionary(), TimeSpan.MaxValue, ImmutableArray<string>.Empty, false), CancellationToken.None);

        await Verify(new { create, update })
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"));
    }

    [Fact]
    public async Task Should_Diff_Login_Item()
    {
        var provider = new OnePasswordProvider(_logger);
        var serializer = new PropertyValueSerializer();

        await _serverFixture.ConfigureProvider(provider);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            // Password = "secret1234",
            Fields = new()
            {
                ["password"] = new FieldArgs()
                {
                    Value = "secret1234",
                    Type = FieldType.Concealed
                }
            },
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secret1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);
        
        var diff = await provider.Diff(new DiffRequest(data.Urn, create.Id, create.Properties.ToImmutableDictionary(), create.Properties.ToImmutableDictionary(),  ImmutableArray<string>.Empty), CancellationToken.None);


        await Verify(new { create, diff });
    }

    [Fact]
    public async Task Should_Diff_Login_Item_With_Differences()
    {
        var provider = new OnePasswordProvider(_logger);
        var serializer = new PropertyValueSerializer();

        await _serverFixture.ConfigureProvider(provider);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            Password = "secret1234",
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secret1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });

        var data2 = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me2",
            Password = "secret1234",
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Fields = new()
                    {
                        ["password2"] = new FieldArgs()
                        {
                            Value = "secret1235!",
                            Type = FieldType.Concealed
                        }
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);
        
        var diff = await provider.Diff(new DiffRequest(data.Urn, create.Id, create.Properties.ToImmutableDictionary(), data2.Request,  ImmutableArray<string>.Empty), CancellationToken.None);
        await Verify(new { create, diff });
    }
    

    [Fact]
    public async Task Should_Generate_Password()
    {
        var provider = new OnePasswordProvider(_logger);

        await _serverFixture.ConfigureProvider(provider);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            GeneratePassword = new PasswordRecipeArgs()
            {
                Digits = true,
                Length = 40,
                Symbols = true,
                Letters = true
            },
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"))
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"), "[redacted,server-generated]"));
    }
    

    [Fact]
    public async Task Should_Generate_Random_Password()
    {
        var provider = new OnePasswordProvider(_logger);

        await _serverFixture.ConfigureProvider(provider);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            GeneratePassword = true,
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"))
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"), "[redacted,server-generated]"));
    }
}
