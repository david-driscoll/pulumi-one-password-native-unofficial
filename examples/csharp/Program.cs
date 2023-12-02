using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Pulumi;
using Rocket.Surgery.OnePasswordNativeUnoffical;
using Rocket.Surgery.OnePasswordNativeUnoffical.Inputs;

return await Deployment.RunAsync(() =>
{
   var login = new LoginItem("my-password", new()
   {
      Vault = "testing-pulumi",
      Username = "me",
      InputAttachments = new()
      {
         ["my-attachment"] = new StringAsset("this is an attachment"),
         ["package.json"] = new FileAsset("./Pulumi.yaml")
      },
      Fields = new()
      {
         ["password"] = new FieldArgs()
         {
            Value = "secret1234",
            Type = FieldAssignmentType.Concealed
         }
      },
      Sections = new()
      {
         ["mysection"] = new SectionArgs()
         {
            Fields = new()
            {
               ["password"] = new FieldArgs()
               {
                  Value = "secret1235!",
                  Type = FieldAssignmentType.Concealed
               }
            }
         }
      }
   });

   // var api = new APICredentialItem("my-api", new()
   // {

   //    Vault = "testing-pulumi",
   //    Credential = "abcdf",
   //    Hostname = "hostname",
   //    Fields = new()
   //    {
   //       ["name"] = new FieldArgs { Type = FieldAssignmentType.Text, Value = "thename" },
   //    },
   //    Category = "APICredential",
   //    Filename = "abcd",
   //    Expires = "",
   //    ValidFrom = "",
   //    Type = "1234",
   //    Notes = "",
   //    Username = "",
   //    Title = "mytitles"
   // });

   // var ssn = new SocialSecurityNumberItem("my-api", new()
   // {
   //    Vault = "testing-pulumi",
   //    Fields = new()
   //    {
   //       ["afasdfasdf"] = new FieldArgs { Type = FieldAssignmentType.Text, Value = "thename" },
   //    },
   //    Notes = "",
   //    Title = "mytitles",
   // });

   // var member = new MembershipItem("random-membership", new()
   // {
   //    Vault = "testing-pulumi",
   //    MemberId = login.Uuid,
   //    Pin = "12345"
   // });

   login.Attachments
   .Apply(z => z["my-attachment"].Reference)
   .Apply(z => GetAttachment.Invoke(new() { Reference = z }))
   .Apply(z =>
   {
      Log.Info(z.Value);
      return z.Value;
   });


   // Add your resources here
   // e.g. var resource = new Resource("name", new ResourceArgs { });

   // Export outputs here
   return new Dictionary<string, object?>
   {
      ["outputKey"] = "outputValue"
   };
});
