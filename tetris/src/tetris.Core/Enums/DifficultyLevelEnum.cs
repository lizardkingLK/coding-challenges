using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument<DifficultyLevelEnum>(Prefix = "-d", Name = "--difficulty", Default = Medium)]
public enum DifficultyLevelEnum
{
    Easy,
    Medium,
    Hard,
}