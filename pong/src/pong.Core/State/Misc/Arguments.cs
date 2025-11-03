using pong.Core.Enums;

namespace pong.Core.State.Misc;

public class Arguments
{
    public const int DefaultPointsToWin = 10;

    public CommandTypeEnum CommandType { get; set; }
    public GameModeEnum GameMode { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; }
    public int PointsToWin { get; set; } = DefaultPointsToWin;
}