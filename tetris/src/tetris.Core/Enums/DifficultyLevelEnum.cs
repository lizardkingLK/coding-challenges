using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument(Prefix = "-d", Name = "--difficulty", Default = Medium, Type = ArgumentTypeEnum.Difficulty)]
public enum DifficultyLevelEnum
{
    Easy,
    Medium,
    Hard,
}