namespace lizzy.Core.State;

public record AuthorizationDTO
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}