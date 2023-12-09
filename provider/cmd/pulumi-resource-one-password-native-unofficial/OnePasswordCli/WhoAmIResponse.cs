using System.Text.Json.Serialization;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public record WhoAmIResponse(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("account_uuid")]
    string AccountUuid,
    [property: JsonPropertyName("user_uuid")]
    string UserUuid,
    [property: JsonPropertyName("user_type")]
    string UserType
);