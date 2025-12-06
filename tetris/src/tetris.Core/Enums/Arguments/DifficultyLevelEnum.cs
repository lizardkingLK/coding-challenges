using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-d", Name = "--difficulty", Type = ArgumentTypeEnum.DifficultyLevel)]
public enum DifficultyLevelEnum
{
    Easy = -1,
    Medium = 0,
    Hard = 1,
}