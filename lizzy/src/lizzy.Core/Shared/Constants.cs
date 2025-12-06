namespace lizzy.Core.Shared;

public static class Constants
{
    public const int CodeVerifierLength = 64;
    public const char Ampersand = '&';
    public const char Equal = '=';
    public const string RandomStringInput = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    public const string ResponseType = "code";
    public const string Error = "error";
    public const string ClientIdKey = "client_id";
    public const string GrantTypeKey = "grant_type";
    public const string RedirectUriKey = "redirect_uri";
    public const string CodeChallengeKey = "code_challenge";
    public const string CodeVerifierKey = "code_verifier";
    public const string CodeKey = "code";
    public const string AccessTokenKey = "access_token";
    public const string RefreshTokenKey = "refresh_token";
    public const string TokenTypeKey = "token_type";
    public const string ScopeKey = "scope";
    public const string ExpiresInKey = "expires_in";
    public const string AuthorizationCodeKey = "authorization_code";
    public const string UrlAuthorize = "/authorize";
    public const string UrlAccessToken = "/api/token";
    public const string UrlHome = "/index.html";
    public const string QueryError = "?error=access_denied";
    public const string ClientId = "0ec6076d4098439483a2bdce8d426859";
    public const string CodeChallengeMethod = "S256";
    public const string Scope = "user-read-private user-read-email";
    public const string RedirectUrl = "https://localhost:5500";
}