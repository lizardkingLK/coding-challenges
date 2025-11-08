using tetris.Core.Enums;

namespace tetris.Core.State;

public record Arguments
{
    public DifficultyLevelEnum DifficultyLevel { get; set; }
}