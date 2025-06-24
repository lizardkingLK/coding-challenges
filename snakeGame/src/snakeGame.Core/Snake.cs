namespace snakeGame.Core;

using static Helper;
using static Utility;

public static class Snake
{
    public static void Initialize(string[] args)
    {
        Console.WriteLine("console\nheight is {0}\nwidth is {1}", Console.BufferHeight, Console.BufferWidth);

        Result<(bool, int, int)> argumentsValidationResult = ValidateArguments(args, Console.BufferHeight, Console.BufferWidth);
        if (argumentsValidationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(argumentsValidationResult.Error);
            return;
        }

        Console.WriteLine("finished");
    }
}
