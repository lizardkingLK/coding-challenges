using tetris.Core.Enums;

namespace tetris.Core.State;

public record Arguments
{
    public ControllerTypeEnum ControllerType { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; }
}