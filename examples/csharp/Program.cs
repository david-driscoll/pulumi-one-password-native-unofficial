using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using Google.Protobuf.WellKnownTypes;
using Pulumi;
using Rocket.Surgery.OnePasswordNativeUnofficial;
using Rocket.Surgery.OnePasswordNativeUnofficial.Inputs;

return await Deployment.RunAsync(() =>
{
    var login = new LoginItem("my-password", new()
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
                // Attachments = new()
                // {
                //     ["my-different-attachment"] = new StringAsset("this is my different attachment"),
                //     // currently there is no way to have a period escaped via the cli
                //     // ["package.json"] = new FileAsset("./Pulumi.yaml")
                // },
            }
        },
        Tags = new string[] { "test-tag" }
    });

    // // TODO: Allow config values for the vault, tokens, etc.
    // // Check for valid environment variables if not configured
    // var item = new Item("my-test-item", new ItemArgs()
    // {
    //     Category = "Social Security Number",
    //     Notes = "this is a note",
    //     Vault = "testing-pulumi",
    // });

    // var api = new APICredentialItem("my-api", new()
    // {
    //     Attachments = new()
    //     {
    //         ["my-attachment"] = new StringAsset("this is an attachment"),
    //     },
    //     Vault = "testing-pulumi",
    //     Credential = "abcdf",
    //     Hostname = "hostname",
    //     Fields = new()
    //     {
    //         ["name"] = new FieldArgs { Type = FieldType.String, Value = "thename" },
    //     },
    //     Sections = new()
    //     {
    //         ["mysection"] = new SectionArgs()
    //         {
    //             Fields = new()
    //             {
    //                 ["name"] = new FieldArgs { Type = FieldType.String, Value = "thesectionname" },
    //             }
    //         }
    //     },
    //     Category = "APICredential",
    //     Filename = "abcd",
    //     // Expires = "",
    //     // ValidFrom = "",
    //     Type = "1234",
    //     Notes = "5543434",
    //     Username = "142",
    //     Title = "mytitle2"
    // });

    // var ssn = new SocialSecurityNumberItem("my-api", new()
    // {
    //     Vault = "testing-pulumi",
    //     Fields = new()
    //     {
    //         ["afasdfasdf"] = new FieldArgs { Type = FieldType.String, Value = "thename" },
    //     },
    //     Notes = "this is a different note",
    //     Title = "mytitlessn",
    // });

    // var member = new MembershipItem("random-membership", new()
    // {
    //     Vault = "testing-pulumi",
    //     MemberId = login.Id,
    //     Pin = "12345"
    // });

    // login.Attachments
    //     .Apply(z => z["my-attachment"].Reference)
    //     .Apply(z => GetAttachment.Invoke(new() { Reference = z }))
    //     .Apply(z =>
    //     {
    //         Log.Info(z.Value);
    //         return z.Value;
    //     });

    login.Fields
        .Apply(z =>
        {
            Log.Info(string.Join(", ", z.Keys) + string.Join(", ", z.Values.Select(z => z.Reference)));
            return z;
        })
        .Apply(z => z["username"].Reference)
        .Apply(z =>
        {
            Log.Info("Reference: " + z);
            return z;
        })
        ;


    // Add your resources here
    // e.g. var resource = new Resource("name", new ResourceArgs { });

    // Export outputs here
    return new Dictionary<string, object?>
    {
        ["outputKey"] = "outputValue"
    };
});
