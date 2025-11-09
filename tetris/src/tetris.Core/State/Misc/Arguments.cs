using tetris.Core.Enums.Arguments;

namespace tetris.Core.State.Misc;

public record Arguments
{
    public ControllerTypeEnum ControllerType { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; }
    public GameModeEnum GameMode { get; set; }
}