using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Pulumi;
using Pulumi.OnePasswordNative;
using Pulumi.OnePasswordNative.Inputs;

return await Deployment.RunAsync(() =>
{
   var login = new LoginItem("my-password", new()
   {
      Vault = "testing-pulumi",
      Username = "me",
      Attachments = new()
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

   var member = new MembershipItem("random-membership", new()
   {
      Vault = "testing-pulumi",
      MemberId = login.Uuid,
      Pin = "12345"
   });

   login.GetAttachment(new() { Name = "my-attachment" }).Apply(z =>
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
