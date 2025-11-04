namespace pong.Core.State.Misc;

public record Difficulty
{
    public const int DefaultSpeed = 4;
    public const int DefaultBallMoveInterval = 20;
    public const int DefaultBallSpawnTimeout = 500;
    public const int DefaultCPUWaitTimeout = 20;

    public int RacketSpeed { get; set; } = DefaultSpeed;
    public int BallMoveInterval { get; set; } = DefaultBallMoveInterval;
    public int BallSpawnTimeout { get; set; } = DefaultBallSpawnTimeout;
    public int CPUWaitTimeout { get; set; } = DefaultCPUWaitTimeout;
}