using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;
using snakeGame.Core.Helpers;

namespace snakeGame.Core;

using static Constants;
using static ArgumentHelper;
using static ChainingHelper;
using static Utility;

public class SnakeGame
{
    private readonly IGenerate generator = GetGenerator();

    private readonly IDisplay display = GetFileDisplay();

    public void Run(string[] args)
    {
        Result<(bool, int, int)> argumentsValidationResult = ValidateArguments(args, MaxHeight, MaxWidth);
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
        };

        Result<bool> generatedGameContext = generator.Generate(manager);
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        display.Display(manager);
    }
}
