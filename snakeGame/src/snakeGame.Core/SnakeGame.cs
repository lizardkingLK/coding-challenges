using snakeGame.Core.Abstractions;
using snakeGame.Core.Shared;
using snakeGame.Core.Helpers;
using snakeGame.Core.State;
using snakeGame.Core.Library;
using snakeGame.Core.Enums;

namespace snakeGame.Core;

using static Constants;
using static ArgumentHelper;
using static ChainingHelper;
using static OutputHelper;
using static Utility;

public class SnakeGame
{
    public static void Run(string[] args)
    {
        Result<(bool, int, int, OutputTypeEnum)> argumentsValidationResult
        = ValidateArguments(args, MaxHeight, MaxWidth);
        if (argumentsValidationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(argumentsValidationResult.Error);
            return;
        }

        Manager manager = new()
        {
            Height = argumentsValidationResult.Data.Item2,
            Width = argumentsValidationResult.Data.Item3,
            Map = new Block[argumentsValidationResult.Data.Item2, argumentsValidationResult.Data.Item3],
            Spaces = new DynamicArray<Block>(),
        };

        Result<bool> generatedGameContext = GetGenerator().Generate(manager);
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        // INFO: Just testing
        GetOutput(argumentsValidationResult.Data.Item4).Output(manager);

        // TODO: run loop
    }
}
