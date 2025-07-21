using snakeGame.Core.Enums;
using snakeGame.Core.Shared;
using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;
using static snakeGame.Core.Shared.Values;

namespace snakeGame.Core.Helpers;

public static class ArgumentHelper
{
    public static
    Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> ValidateArguments(string[] args)
    {
        OutputTypeEnum outputType = default;
        GameModeEnum gameMode = default;
        int length = args.Length;
        if (length == 0)
        {
            return new((true, MinHeight, MinWidth, outputType, gameMode), null);
        }

        if (length % 2 != 0)
        {
            return ErrorInvalidArguments;
        }

        int i;
        string currentArgument;
        string currentValueString;
        int width = MinWidth;
        int height = MinHeight;
        for (i = 0; i < length; i += 2)
        {
            currentArgument = args[i].Trim().ToLower();
            currentValueString = args[i + 1];
            if (!int.TryParse(currentValueString, out int currentValue))
            {
                return ErrorInvalidArguments;
            }

            if (IncludesInCollection(currentArgument, heightFlags))
            {
                height = currentValue < MinHeight || currentValue > MaxHeight
                ? MinHeight
                : currentValue;
                continue;
            }

            if (IncludesInCollection(currentArgument, widthFlags))
            {
                width = currentValue < MinWidth || currentValue > MaxWidth
                ? MinWidth
                : currentValue;
                continue;
            }

            if (IncludesInCollection(currentArgument, outputFlags)
            && Enum.TryParse(currentValueString, out outputType))
            {
                continue;
            }

            if (IncludesInCollection(currentArgument, gameModeFlags)
            && Enum.TryParse(currentValueString, out gameMode))
            {
                continue;
            }

            return ErrorInvalidArguments;
        }

        return new((true, height, width, outputType, gameMode), null);
    }
}