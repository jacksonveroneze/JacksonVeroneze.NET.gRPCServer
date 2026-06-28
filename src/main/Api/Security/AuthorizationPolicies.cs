namespace JacksonVeroneze.NET.GRPCServer.Api.Security;

public static class AuthorizationPolicies
{
    public const string JwtAccess = "JwtAccess";
    public const string ProfilesCreate = "ProfilesCreate";
    public const string ProfilesActivate = "ProfilesActivate";
    public const string ProfilesInactivate = "ProfilesInactivate";
    public const string ProfilesRead = "ProfilesRead";
}
