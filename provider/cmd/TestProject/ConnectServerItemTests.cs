using System.Collections.Immutable;
using System.Net;
using FluentAssertions;
using GeneratedCode;
using Polly;
using Polly.Retry;
using Pulumi;
using pulumi_resource_one_password_native_unofficial;
using pulumi_resource_one_password_native_unofficial.Domain;
using Pulumi.Experimental.Provider;
using Refit;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.CreditCard.Inputs;
using Rocket.Surgery.OnePasswordNativeUnofficial.CreditCard.Outputs;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;
using Serilog;
using Serilog.Core;
using TestProject.Helpers;
using Xunit.Abstractions;
using FieldType = Rocket.Surgery.OnePasswordNativeUnofficial.FieldType;
using Item = Rocket.Surgery.OnePasswordNativeUnofficial.Item;
using ResourceType = pulumi_resource_one_password_native_unofficial.Domain.ResourceType;
using TemplateMetadata = pulumi_resource_one_password_native_unofficial.Domain.TemplateMetadata;

// ReSharper disable NullableWarningSuppressionIsUsed

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
            .WriteTo.TestOutput(output)
            .CreateLogger();
        fixture.Connect(serverFixture);
    }

    [Fact]
    public async Task Should_Default_The_Category_On_Item_To_Secure_Note()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<Item, ItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Fields = new ()
            {
                ["username"] = new FieldArgs()
                {
                    Type = FieldType.String,
                    Value = "myusername"
                },
                ["password"] = new FieldArgs()
                {
                    Type = FieldType.Concealed,
                    Value = "mypassword"
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
            Tags = new[] { "test-tag" }
        });
        
        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Allow_Any_Category_On_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<Item, ItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Category = ResourceType.DriverLicense.InputCategory,
            Fields = new ()
            {
                ["username"] = new FieldArgs()
                {
                    Type = FieldType.String,
                    Value = "myusername"
                },
                ["password"] = new FieldArgs()
                {
                    Type = FieldType.Concealed,
                    Value = "mypassword"
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
            Tags = new[] { "test-tag" }
        });
        
        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
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
            Tags = new[] { "test-tag" }
        });

        var check = await provider.Check(new(data.Urn, data.Request, data.Request, ImmutableArray<byte>.Empty), CancellationToken.None);

        await Verify(check);
    }

    [Fact]
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Throw_When_Attachments_Are_Used()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("testlogin", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            Attachments = new()
            {
                ["my-attachment"] = new StringAsset("this is an attachment"), // ["package.json"] = new FileAsset("./Pulumi.yaml")
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

        Func<Task> action = () => provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);
        await action.Should().ThrowAsync<NotSupportedException>();
    }

    [Fact]
    public async Task Should_Handle_Date_Fields()
    {
        var data = await _fixture.CreateRequestObject<APICredentialItem, APICredentialItemArgs>("myapi", new()
        {
            Vault = "testing-pulumi",
            Expires = "2021-10-10",
            Fields = new ()
            {
                ["customDate"] = new FieldArgs()
                {
                    Type = FieldType.Date,
                    Value = "2021-10-10"
                }
            }
        });
        var provider = await _serverFixture.ConfigureProvider(_logger);
        
        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Handle_MonthYear_Fields()
    {
        var data = await _fixture.CreateRequestObject<MembershipItem, MembershipItemArgs>("myapi", new()
        {
            Vault = "testing-pulumi",
            ExpiryDate = "2021-10",
            Fields = new InputMap<FieldArgs>()
            {
                ["customMonthYear"] = new FieldArgs()
                {
                    Type = FieldType.MonthYear,
                    Value = "2021-10"
                }
            }
        });
        var provider = await _serverFixture.ConfigureProvider(_logger);
        
        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Handle_Complex_Items()
    {
        var item = await _fixture.CreateRequestObject<Item, ItemArgs>("Infrastructure Collection", new()
            {
                Vault = "testing-pulumi",
                Fields = new()
                         {
                             ["resourceGroup"] = new FieldArgs()
                                                 {
                                                     Label = "Resource Group",
                                                     Type = FieldType.String,
                                                     Value = "my resource group",
                                                 },
                         },
                Sections = new()
                           {
                               ["acr"] = new SectionArgs()
                                         {
                                             Label = "Azure Container Registry",
                                             Fields = new()
                                                      {
                                                          ["name"] = new FieldArgs()
                                                                     {
                                                                         Label = "Name",
                                                                         Type = FieldType.String,
                                                                         Value = "my registry",
                                                                     },
                                                          ["hostname"] = new FieldArgs()
                                                                         {
                                                                             Label = "Hostname",
                                                                             Type = FieldType.String,
                                                                             Value = "login server",
                                                                         },
                                                      }
                                         },
                               ["acrPull"] = new SectionArgs()
                                             {
                                                 Label = "Acr Pull Token",
        
                                                 Fields = new()
                                                          {
                                                              ["username"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Name",
                                                                                 Type = FieldType.String,
                                                                                 Value = "username",
                                                                             },
                                                              ["password"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Password",
                                                                                 Type = FieldType.Concealed,
                                                                                 Value = "password",
                                                                             },
                                                              ["hostname"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Hostname",
                                                                                 Type = FieldType.String,
                                                                                 Value = "login server",
                                                                             },
                                                          }
                                             },
                               ["acrPush"] = new SectionArgs()
                                             {
                                                 Label = "Acr Push Token",
        
                                                 Fields = new()
                                                          {
                                                              ["username"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Name",
                                                                                 Type = FieldType.String,
                                                                                 Value = "username",
                                                                             },
                                                              ["password"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Password",
                                                                                 Type = FieldType.Concealed,
                                                                                 Value = "password",
                                                                             },
                                                              ["hostname"] = new FieldArgs()
                                                                             {
                                                                                 Label = "Hostname",
                                                                                 Type = FieldType.String,
                                                                                 Value = "login server",
                                                                             },
                                                          }
                                             }
                           }
                    }
        );
        var provider = await _serverFixture.ConfigureProvider(_logger);
        
        var create = await provider.Create(new CreateRequest(item.Urn, item.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Update_Login_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            Tags = new[] { "test-tag" }
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
            Tags = new[] { "test-tag", "another tag" }
        });

        var create = await provider.Create(new CreateRequest(createInput.Urn, createInput.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var pipeline = new ResiliencePipelineBuilder<FullItem>()
            .AddRetry(new RetryStrategyOptions<FullItem>()
            {
                BackoffType = DelayBackoffType.Linear,
                MaxRetryAttempts = 50,
                Delay = TimeSpan.FromMilliseconds(100),
                UseJitter = true,
                ShouldHandle = arguments => ValueTask.FromResult(arguments.Outcome.Exception is ApiException { StatusCode: HttpStatusCode.NotFound }),
            })
            .Build();

        await pipeline.ExecuteAsync(static async (c, ct) => await c.Connect.GetVaultItemById(
            TemplateMetadata.GetObjectStringValue(TemplateMetadata.GetObjectValue(c.create.Properties!.ToImmutableDictionary(), "vault")!, "id"),
            c.create.Id
        ), (_serverFixture.Connect, create), CancellationToken.None);

        var update = await provider.Update(
            new UpdateRequest(updateInput.Urn, create.Id!, create.Properties!.ToImmutableDictionary(), updateInput.Request.ToImmutableDictionary(),
                TimeSpan.MaxValue, ImmutableArray<string>.Empty, false), CancellationToken.None);

        await Verify(new { create, update })
            .AddIdScrubber(create.Id);
    }


    [Fact]
    public async Task Should_Support_Create_Preview_Login_Item()
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, true), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id);
    }

    [Fact]
    public async Task Should_Support_Update_Preview_Login_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            Tags = new[] { "test-tag" }
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
            Tags = new[] { "test-tag", "another tag" }
        });

        var create = await provider.Create(new CreateRequest(createInput.Urn, createInput.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Policy.Handle<ApiException>(exception => exception.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(50, _ => TimeSpan.FromMilliseconds(100))
            .ExecuteAsync(() => _serverFixture.Connect.GetVaultItemById(
                TemplateMetadata.GetObjectStringValue(TemplateMetadata.GetObjectValue(create.Properties!.ToImmutableDictionary(), "vault")!, "id"),
                create.Id
            ));

        var update = await provider.Update(
            new UpdateRequest(updateInput.Urn, create.Id!, create.Properties!.ToImmutableDictionary(), updateInput.Request.ToImmutableDictionary(),
                TimeSpan.MaxValue, ImmutableArray<string>.Empty, true), CancellationToken.None);

        await Verify(new { create, update })
            .AddIdScrubber(create.Id);
    }


    [Fact]
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var diff = await provider.Diff(
            new DiffRequest(data.Urn, create.Id!, create.Properties!.ToImmutableDictionary(), create.Properties!.ToImmutableDictionary(),
                ImmutableArray<string>.Empty), CancellationToken.None);


        await Verify(new { create, diff });
    }

    [Fact]
    public async Task Should_Diff_Login_Item_With_Differences()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

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
            Tags = new[] { "test-tag" }
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        var diff = await provider.Diff(
            new DiffRequest(data.Urn, create.Id!, create.Properties!.ToImmutableDictionary(), data2.Request, ImmutableArray<string>.Empty),
            CancellationToken.None);
        await Verify(new { create, diff });
    }


    [Fact]
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
            Tags = new[] { "test-tag" },
            Urls = new()
            {
                
                "http://notlocalhost.com",
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
            .AddIdScrubber(create.Id)
            .AddPasswordScrubber(create.Properties);
    }

    [Fact]
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
            Tags = new[] { "test-tag" },
            Urls = new()
            {
                "http://notlocalhost.com",
                new UrlArgs()
                {
                    Href = "http://notaplace.com",
                    Label = "Not as cool a place",
                    Primary = true
                }
            }
        });

        var loginItemResult = await provider.Create(new CreateRequest(loginItem.Urn, loginItem.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        _ = await _fixture.CreateRequestObject<PasswordItem, PasswordItemArgs>("mypassword", new()
        {
            Vault = "testing-pulumi",
            GeneratePassword = new PasswordRecipeArgs()
            {
                Digits = true,
                Length = 40,
                Symbols = true,
                Letters = true
            },
            Tags = new[] { "test-tag" },
            Urls = new()
            {
                "http://notlocalhost.com",
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
            Tags = new[] { "test-tag" },
            References = new[] { loginItemResult.Id! },
            Sections = new()
            {
                ["mysection"] = new SectionArgs()
                {
                    Label = "My Section",
                    References = new[] { passwordItemResult.Id! },
                }
            },
            Urls = new()
            {
                "http://notlocalhost.com",
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
            .AddIdScrubber(create.Id)
            .AddPasswordScrubber(create.Properties);
    }


    [Fact]
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
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id)
            .AddPasswordScrubber(create.Properties);
    }


    [Fact]
    public async Task Should_Generate_Random_Password()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var data = await _fixture.CreateRequestObject<LoginItem, LoginItemArgs>("myitem", new()
        {
            Vault = "testing-pulumi",
            Username = "me",
            GeneratePassword = true,
            Tags = new[] { "test-tag" }
        });

        var create = await provider.Create(new CreateRequest(data.Urn, data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id)
            .AddPasswordScrubber(create.Properties);
    }

    [Fact(Skip = "unreliable")]
    public async Task Should_Be_Able_To_Get_Attachments()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
            FunctionType.GetAttachment.Urn,
            ImmutableDictionary<string, PropertyValue>.Empty.Add("reference", new("op://testing-pulumi/TestItem/my-attachment")
            )), CancellationToken.None);

        await Verify(result);
    }

    [Fact]
    public async Task Should_Be_Able_To_Get_An_Item()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                FunctionType.GetItem.Urn,
                ImmutableDictionary<string, PropertyValue>.Empty
                    .Add("id", new("67gg5pap6mncp6h2wjvpukc3cu"))
                    .Add("vault", new("testing-pulumi"))
            ),
            CancellationToken.None);

        await Verify(result);
    }

    [Fact]
    public async Task Should_Handle_Add_More_Section_With_Some_Grace()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                FunctionType.GetAPICredential.Urn,
                ImmutableDictionary<string, PropertyValue>.Empty
                    .Add("id", new("yzxwebilecnb2w532bfwvmvvzq"))
                    .Add("vault", new("testing-pulumi"))
            ),
            CancellationToken.None);

        await Verify(result);
    }

    [Fact]
    public async Task Should_Be_Able_To_Read_A_Reference()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                FunctionType.Read.Urn,
                ImmutableDictionary<string, PropertyValue>.Empty.Add("reference", new("op://testing-pulumi/TestItem/password"))),
            CancellationToken.None);

        await Verify(result);
    }

    [Fact]
    public async Task Should_Be_Able_To_Inject_References()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var result = await provider.Invoke(new InvokeRequest(
                FunctionType.Inject.Urn,
                ImmutableDictionary<string, PropertyValue>.Empty.Add("template", new(
                    $"""
                     MyPassword: op://testing-pulumi/TestItem/password
                     MyConfigValue: op://testing-pulumi/TestItem/text
                     """))),
            CancellationToken.None);

        await Verify(result);
    }

    [Fact]
    public async Task Should_Work_With_ApiCredentialItems()
    {
        var provider = await _serverFixture.ConfigureProvider(_logger);

        var name = Output.Create("registryname");
        var username = Output.Create("myusername");
        var password = Output.CreateSecret("mypassword");
        var loginserver = Output.Create("loginserver.com");

        var data = await _fixture.CreateRequestObject<APICredentialItem, APICredentialItemArgs>("credential with spaces", new()
        {
            Vault = "testing-pulumi",
            Username = username,
            Credential = password,
            Hostname = loginserver,
            Fields = new()
            {
                ["name"] = new FieldArgs { Type = FieldType.String, Value = name },
            }
        });

        var create = await provider.Create(
            new CreateRequest(
                "urn:pulumi:dev::Syndicates.Infrastructure::resources$one-password-native-unofficial:index:APICredentialItem::credential with spaces",
                data.Request, TimeSpan.MaxValue, false), CancellationToken.None);

        await Verify(create)
            .AddIdScrubber(create.Id)
            .AddPasswordScrubber(create.Properties);
    }
}
