using tetris.Core.Enums.Arguments;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Helpers;

public static class ValueHelper
{
    // TODO: add scoring system with renderer based on difficulty level
    // with bonuses if n times cleaned with difficulty multiplier
    public static void SetDifficultyValues(DifficultyLevelEnum difficultyLevel)
    {
        if (difficultyLevel == DifficultyLevelEnum.Easy)
        {
            durationMoveInterval = DurationMoveInterval * 2;
        }
        else if (difficultyLevel == DifficultyLevelEnum.Medium)
        {
            durationMoveInterval = DurationMoveInterval;
        }
        else if (difficultyLevel == DifficultyLevelEnum.Hard)
        {
            durationMoveInterval = DurationMoveInterval / 2;
        }
    }
}