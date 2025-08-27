using System.Text.Json;
using lizzy.Core.Helpers;
using lizzy.Core.State;
using static lizzy.Core.Shared.Constants;
using static lizzy.Core.Shared.Values;

namespace lizzy.Core.Services;

public class AuthorizationService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public string GetAuthorizationCodeURL()
    {
        (string codeVerifierValue, string codeVerifierBase64)
            = CryptographyHelper.GetCodeVerifierValues();

        Environment.SetEnvironmentVariable(CodeVerifierKey, codeVerifierValue);

        authorizationCodeParams[CodeChallengeKey] = codeVerifierBase64;

        UriBuilder uriBuilder = new(_httpClient.BaseAddress!)
        {
            Path = UrlAuthorize,
            Query = string.Join(
                Ampersand,
                authorizationCodeParams
                .AsEnumerable()
                .Select(item => $"{item.Key}={item.Value}")),
        };

        return uriBuilder.ToString();
    }

    public static bool ValidateAuthorizationCode(string value)
    {
        if (value.Contains(ResponseType))
        {
            Environment.SetEnvironmentVariable(
                AuthorizationCodeKey,
                value.Split(
                    ResponseType + Equal,
                    StringSplitOptions.RemoveEmptyEntries)
                    .ElementAtOrDefault(1));

            return true;
        }

        return false;
    }

    public bool TryGetAccessToken(out AccessTokenDTO? accessToken)
    {
        accessToken = default;

        string? refreshToken = Environment.GetEnvironmentVariable(RefreshTokenKey);
        if (refreshToken != null)
        {
            refreshTokenGrantParams[RefreshTokenKey] = refreshToken;
            return GetAccessToken(refreshTokenGrantParams, out accessToken);
        }

        string? codeVerifier = Environment.GetEnvironmentVariable(CodeVerifierKey);
        if (codeVerifier == null)
        {
            return false;
        }

        accessTokenGrantParams[CodeVerifierKey] = codeVerifier;

        string? authorizationCode = Environment.GetEnvironmentVariable(AuthorizationCodeKey);
        if (authorizationCode == null)
        {
            return false;
        }

        accessTokenGrantParams[CodeKey] = authorizationCode;

        return GetAccessToken(accessTokenGrantParams, out accessToken);
    }

    private bool GetAccessToken(Dictionary<string, string> parameters, out AccessTokenDTO? accessToken)
    {
        accessToken = default;

        FormUrlEncodedContent httpContent = new(parameters);
        UriBuilder uriBuilder = new(_httpClient.BaseAddress!)
        {
            Path = UrlAccessToken,
        };

        Task<HttpResponseMessage> requestTask = Task
        .Run(() => _httpClient.PostAsync(uriBuilder.ToString(), httpContent));
        requestTask.Wait();

        HttpResponseMessage response = requestTask.Result;
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        HttpContent responseContent = response.Content;
        Task<string> responseTask = Task.Run(responseContent.ReadAsStringAsync);
        responseTask.Wait();

        string responseString = responseTask.Result;
        if (string.IsNullOrEmpty(responseString))
        {
            return false;
        }

        accessToken = JsonSerializer.Deserialize<AccessTokenDTO>(responseString);
        if (accessToken == null)
        {
            return false;
        }

        Environment.SetEnvironmentVariable(AccessTokenKey, accessToken.AccessToken);
        Environment.SetEnvironmentVariable(RefreshTokenKey, accessToken.RefreshToken);

        return true;
    }

    public static AuthorizationDTO? CollectVariables()
    {
        string? accessToken = Environment.GetEnvironmentVariable(AccessTokenKey);
        string? refreshToken = Environment.GetEnvironmentVariable(RefreshTokenKey);
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            return null;
        }

        return new AuthorizationDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }
}