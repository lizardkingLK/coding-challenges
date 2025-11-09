using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument(Prefix = "-d", Name = "--difficulty", Type = ArgumentTypeEnum.Difficulty)]
public enum DifficultyLevelEnum
{
    Easy = -1,
    Medium = 0,
    Hard = 1,
}