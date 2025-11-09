using tetris.Core.Attributes;
using tetris.Core.Enums.Game;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-d", Name = "--difficulty", Type = ArgumentTypeEnum.Difficulty)]
public enum DifficultyLevelEnum
{
    Easy = -1,
    Medium = 0,
    Hard = 1,
}