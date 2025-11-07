using pong.Core.Enums;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Misc;

public class Arguments
{
    public CommandTypeEnum CommandType { get; set; }
    public GameModeEnum GameMode { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; }
    public PlayerSideEnum PlayerSide { get; set; }
    public int PointsToWin { get; set; } = DefaultPointsToWin;
}