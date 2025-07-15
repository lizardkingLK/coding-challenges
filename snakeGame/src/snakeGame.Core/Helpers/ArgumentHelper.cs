namespace snakeGame.Core.Helpers;

using snakeGame.Core.Enums;
using snakeGame.Core.Shared;

using static snakeGame.Core.Shared.Utility;
using static snakeGame.Core.Shared.Values;

public static class ArgumentHelper
{
    public static
    Result<(bool, int, int, OutputTypeEnum)> ValidateArguments(
        string[] args,
        int maxHeight,
        int maxWidth)
    {
        OutputTypeEnum outputType = default;
        int length = args.Length;
        if (length == 0)
        {
            return new((true, maxHeight, maxWidth, outputType), null);
        }

        if (length % 2 != 0)
        {
            return ErrorInvalidArguments;
        }

        int i;
        string currentArgument;
        string currentValueString;
        int width = 0;
        int height = 0;
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
                height = currentValue;
                continue;
            }

            if (IncludesInCollection(currentArgument, widthFlags))
            {
                width = currentValue;
                continue;
            }

            if (IncludesInCollection(currentArgument, outputFlags)
            && Enum.TryParse(currentValueString, out outputType))
            {
                continue;
            }

            return ErrorInvalidArguments;
        }

        if (height < 10 || height > maxHeight)
        {
            height = maxHeight;
        }

        if (width < 10 || width > maxWidth)
        {
            width = maxWidth;
        }

        return new((true, height, width, outputType), null);
    }
}