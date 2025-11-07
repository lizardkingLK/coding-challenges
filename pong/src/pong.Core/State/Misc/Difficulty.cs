using static pong.Core.Shared.Constants;

namespace pong.Core.State.Misc;

public record Difficulty
{
    public int RacketSpeed { get; set; } = DefaultSpeed;
    public int BallMoveInterval { get; set; } = DefaultBallMoveInterval;
    public int BallSpawnTimeout { get; set; } = DefaultBallSpawnTimeout;
    public int CPUWaitTimeout { get; set; } = DefaultCPUWaitTimeout;
    public int DistanceThreshold { get; set; } = DefaultDistanceThreshold;
}