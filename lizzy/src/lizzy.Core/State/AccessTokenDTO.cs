using System.Text.Json.Serialization;
using static lizzy.Core.Shared.Constants;

namespace lizzy.Core.State;

public record AccessTokenDTO
{
    [JsonPropertyName(AccessTokenKey)]
    public required string AccessToken { get; init; }
    [JsonPropertyName(TokenTypeKey)]
    public required string TokenType { get; init; }
    [JsonPropertyName(ScopeKey)]
    public required string Scope { get; init; }
    [JsonPropertyName(ExpiresInKey)]
    public required int ExpiresIn { get; init; }
    [JsonPropertyName(RefreshTokenKey)]
    public required string RefreshToken { get; init; }
}