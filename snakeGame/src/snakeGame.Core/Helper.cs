namespace snakeGame.Core;

using static Constants;
using static Values;
using static Utility;

public static class Helper
{
    public static Result<(bool, int, int)> ValidateArguments(string[] args, int maxHeight, int maxWidth)
    {
        int length = args.Length;
        if (length == 0)
        {
            return new((true, maxHeight, maxWidth), null);
        }

        if (length % 2 != 0)
        {
            return new((false, -1, -1), ERROR_INVALID_ARGUMENTS);
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
                return new((false, -1, -1), ERROR_INVALID_ARGUMENTS);
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

            return new((false, -1, -1), ERROR_INVALID_ARGUMENTS);
        }

        if (height > maxHeight)
        {
            height = maxHeight;
        }

        if (width > maxWidth)
        {
            width = maxWidth;
        }

        return new((true, height, width), null);
    }
}