namespace snakeGame.Core;

using static Helper;
using static Utility;

public static class Snake
{
    public static void Initialize(string[] args)
    {

        Result<(bool, int, int)> argumentsValidationResult = ValidateArguments(args, Console.BufferHeight, Console.BufferWidth);
        if (argumentsValidationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(argumentsValidationResult.Error);
            return;
        }
    }
}
