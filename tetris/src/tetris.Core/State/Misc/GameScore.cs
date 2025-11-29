namespace tetris.Core.State.Misc;

public record GameScore
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public required string GameMode { get; init; }
    public required string PlayMode { get; init; }
    public required string Time { get; init; }
    public int Score { get; init; }
}