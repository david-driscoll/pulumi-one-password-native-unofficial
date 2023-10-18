using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Pulumi;
using Pulumi.Onepassword;
using Pulumi.Onepassword.Inputs;

return await Deployment.RunAsync(() =>
{
   var login = new LoginItem("my-password", new()
   {
      Vault = "testing-pulumi",
      Username = "me",
      Attachments = new Dictionary<string, AssetOrArchive>()
      {
         ["my-attachment"] = new StringAsset("this is an attachment"),
         ["package.json"] = new FileAsset("./Pulumi.yaml")
      },
      Fields = new Dictionary<string, FieldArgs>()
      {
         ["password"] = new FieldArgs()
         {
            Value = "secret1234",
            Type = FieldAssignmentType.Concealed
         }
      },
      Sections = new Dictionary<string, SectionArgs>()
      {
         ["mysection"] = new SectionArgs()
         {
            Fields = new InputMap<FieldArgs>()
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

   var member = new MembershipItem("random-membership", new()
   {
      Vault = "testing-pulumi",
      MemberId = login.Uuid,
      Pin = "12345"
   });

   login.Attachment(new() { Name = "my-attachment" }).Apply(z =>
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
