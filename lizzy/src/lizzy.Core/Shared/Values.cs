using System.Web;
using static lizzy.Core.Shared.Constants;

namespace lizzy.Core.Shared;

public static class Values
{
    public static readonly Dictionary<string, string> authorizationCodeParams = new()
    {
        { "response_type", ResponseType },
        { ClientIdKey, ClientId},
        { ScopeKey, HttpUtility.UrlEncode(Scope)},
        { "code_challenge_method", CodeChallengeMethod},
        { CodeChallengeKey, string.Empty},
        { RedirectUriKey, HttpUtility.UrlEncode(RedirectUrl)},
    };

    public static readonly Dictionary<string, string> accessTokenGrantParams = new()
    {
        { ClientIdKey, ClientId},
        { GrantTypeKey, AuthorizationCodeKey },
        { CodeKey, string.Empty },
        { RedirectUriKey, RedirectUrl },
        { CodeVerifierKey, string.Empty },
    };

    public static readonly Dictionary<string, string> refreshTokenGrantParams = new()
    {
        { GrantTypeKey, RefreshTokenKey },
        { ClientIdKey, ClientId },
        { RefreshTokenKey, string.Empty }
    };
}