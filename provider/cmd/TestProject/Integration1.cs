// using Pulumi.Automation;
// using Rocket.Surgery.OnePasswordNativeUnofficial;
//
// namespace TestProject;

// [Collection("Connect collection")]
// public class Integration1 : IClassFixture<PulumiFixture>
// {
//     private readonly PulumiFixture _fixture;
//
//     public Integration1(PulumiFixture fixture, ConnectServerFixture connectServerFixture)
//     {
//         _fixture = fixture;
//         fixture.Connect(connectServerFixture);
//     }
//
//     [Fact]
//     public async Task Should_Create_Login_Item()
//     {
//         var program = PulumiFn.Create(() =>
//         {
//             var login = new LoginItem("login", new()
//             {
//                 Title = "Test Login",
//                 Username = "myusername",
//                 Password = "mypassword",
//                 Tags = new string[] { "Test Tag" },
//                 Vault = "pulumi-testing",
//                 Urls = "http://notlocalhost.com",
//                 Notes = "this is a note"
//             });
//         });
//
//         var stack = await LocalWorkspace.CreateStackAsync(
//             new LocalProgramArgs("csharp", @"C:\Development\david-driscoll\pulumi-one-password-native-unofficial\examples\csharp\")
//             {
//                 EnvironmentVariables = _fixture.EnvironmentVariables,
//             });
//         await stack.SetConfigAsync("one-password-native-unofficial:connectHost", new(_fixture.ConnectHost, false));
//         await stack.SetConfigAsync("one-password-native-unofficial:connectToken", new(_fixture.ConnectToken, true));
//         var config = await stack.GetAllConfigAsync();
//         var up = await stack.UpAsync();
//         // await stack.DestroyAsync(new ()
//         // {
//         //     Debug = true,
//         //     Tracing = "7"
//         // });
//     }
// }
