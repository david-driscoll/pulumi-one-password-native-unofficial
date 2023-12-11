using System.Collections.Immutable;
using Pulumi;
using pulumi_resource_one_password_native_unofficial;
using Pulumi.Experimental.Provider;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using TestProject.Helpers;
using Xunit.Abstractions;

namespace TestProject;

[Collection("Service Account collection")]
[UsesVerify]
public class ServiceAccountItemTests : IClassFixture<PulumiFixture>
{
    private readonly PulumiFixture _fixture;
    private readonly IServerFixture _serverFixture;
    private readonly Logger _logger;

    public ServiceAccountItemTests(PulumiFixture fixture, ServiceAccountFixture serviceAccountFixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _serverFixture = serviceAccountFixture;
        _logger = new LoggerConfiguration()
            .WriteTo.TestOutput(output, LogEventLevel.Verbose)
            .CreateLogger();
        fixture.ServiceAccount(serviceAccountFixture);
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Throw_If_No_Vault_Is_Provided()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
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

        var check = await provider.Check(new(data.Urn, data.Request, data.Request, ImmutableArray<byte>.Empty), CancellationToken.None);

        await Verify(check);
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Support_Default_Vault()
    {
        var provider = await _serverFixture.ConfigureProvider(
            _logger,
            additionalConfig: ImmutableDictionary.Create<string, PropertyValue>()
                .Add("vault", new("testing-pulumi"))
        );

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
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

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"));
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Create_Login_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Diff_Login_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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

        var diff = await provider.Diff(
            new DiffRequest(data.Urn, create.Id, create.Properties.ToImmutableDictionary(), create.Properties.ToImmutableDictionary(),
                ImmutableArray<string>.Empty), CancellationToken.None);


        await Verify(new { create, diff });
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Diff_Login_Item_With_Differences()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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

        var data2 = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me2",
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

        var diff = await provider.Diff(
            new DiffRequest(data.Urn, create.Id, create.Properties.ToImmutableDictionary(), data2.Request, ImmutableArray<string>.Empty),
            CancellationToken.None);
        await Verify(new { create, diff });
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Create_Login_Item_With_Attachments()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            Attachments = new()
            {
                ["my-attachment"] = new StringAsset("this is an attachment"),
                // currently there is no way to have a period escaped via the cli
                // ["package.json"] = new FileAsset("./Pulumi.yaml")
            },
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
                    Attachments = new()
                    {
                        ["my-different-attachment"] = new StringAsset("this is my different attachment"),
                        // currently there is no way to have a period escaped via the cli
                        // ["package.json"] = new FileAsset("./Pulumi.yaml")
                    },
                }
            },
            Tags = new string[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);


        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"));
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Update_Login_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var createInput = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
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

        var updateInput = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me2",
            // Password = "secret1234",
            Fields = new()
            {
                ["password"] = new FieldArgs()
                {
                    Value = "secret12344",
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
            Tags = new string[] { "test-tag", "another-tag" }
        });

        var create = await provider.Create(new CreateRequest(createInput.Urn, createInput.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var update = await provider.Update(
            new UpdateRequest(updateInput.Urn, create.Id!, create.Properties!.ToImmutableDictionary(), updateInput.Request.ToImmutableDictionary(),
                TimeSpan.MaxValue, ImmutableArray<string>.Empty, false), CancellationToken.None);

        await Verify(new { create, update })
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"));
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Set_Urls()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            Tags = new string[] { "test-tag" },
            Urls = new UrlArgs[]
            {
                new UrlArgs()
                {
                    Href = "http://notlocalhost.com",
                    Label = "Some really cool place",
                    Primary = false
                },
                new UrlArgs()
                {
                    Href = "http://notaplace.com",
                    Label = "Not as cool a place",
                    Primary = true
                }
            }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"))
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"),
                "[redacted,server-generated]"));
    }


    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Set_References()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var loginItem = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
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
            Tags = new string[] { "test-tag" },
            Urls = new UrlArgs[]
            {
                new UrlArgs()
                {
                    Href = "http://notlocalhost.com",
                    Label = "Some really cool place",
                    Primary = false
                },
                new UrlArgs()
                {
                    Href = "http://notaplace.com",
                    Label = "Not as cool a place",
                    Primary = true
                }
            }
        });

        var loginItemResult = await provider.Create(new CreateRequest(loginItem.Urn, loginItem.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var passwordItem = await _fixture.CreateRequestObject<PasswordItem, PasswordItemArgs>("mypassword", new()
        {
            Vault = "testing-pulumi",
            GeneratePassword = new PasswordRecipeArgs()
            {
                Digits = true,
                Length = 40,
                Symbols = true,
                Letters = true
            },
            Tags = new string[] { "test-tag" },
            Urls = new UrlArgs[]
            {
                new UrlArgs()
                {
                    Href = "http://notlocalhost.com",
                    Label = "Some really cool place",
                    Primary = false
                },
                new UrlArgs()
                {
                    Href = "http://notaplace.com",
                    Label = "Not as cool a place",
                    Primary = true
                }
            }
        });

        var passwordItemResult = await provider.Create(new CreateRequest(loginItem.Urn, loginItem.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myreference", new()
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
            Tags = new string[] { "test-tag" },
            References = new ReferenceArgs[] { new ReferenceArgs() { ItemId = loginItemResult.Id! } },
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Label = "My Section",
                    References = new ReferenceArgs[] { new ReferenceArgs() { ItemId = passwordItemResult.Id! } },
                }
            }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddScrubber(z => z.Replace(create.Id!, "[server-generated]"))
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"),
                "[redacted,server-generated]"));
    }


    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Generate_Password()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"),
                "[redacted,server-generated]"));
    }


    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Generate_Random_Password()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            .AddScrubber(x => x.Replace(TemplateMetadata.GetObjectStringValue(create.Properties as IReadOnlyDictionary<string, PropertyValue>, "password"),
                "[redacted,server-generated]"));
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Be_Able_To_Get_Attachments()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
            ItemType.GetAttachment,
            ImmutableDictionary<string, PropertyValue>.Empty.Add("reference", new("op://testing-pulumi/67gg5pap6mncp6h2wjvpukc3cu/add more/my-attachment")
            )), CancellationToken.None);

        await Verify(result);
    }

    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Be_Able_To_Get_An_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
            ItemType.GetItem,
            ImmutableDictionary<string, PropertyValue>.Empty
                .Add("id", new("67gg5pap6mncp6h2wjvpukc3cu"))
                .Add("vault", new("testing-pulumi"))
            ), 
            CancellationToken.None);

        await Verify(result);
    }
    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Be_Able_To_Read_A_Reference()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                ItemType.Read,
                ImmutableDictionary<string, PropertyValue>.Empty.Add("reference", new("op://testing-pulumi/TestItem/password"))), 
            CancellationToken.None);

        await Verify(result);
    }
    [SkippableFact(typeof(TimeoutException))]
    public async Task Should_Be_Able_To_Inject_References()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                ItemType.Inject,
                ImmutableDictionary<string, PropertyValue>.Empty.Add("template", new(
                    $"""
                        MyPassword: op://testing-pulumi/TestItem/password
                        MyConfigValue: op://testing-pulumi/TestItem/text
                        """))),
            CancellationToken.None);

        await Verify(result);
    }
}
