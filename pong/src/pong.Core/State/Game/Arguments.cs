using pong.Core.Enums;

namespace pong.Core.State.Game;

public class Arguments
{
    public CommandTypeEnum CommandType { get; set; }
    public GameModeEnum GameMode { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; }
    public OutputTypeEnum OutputType { get; set; }
}