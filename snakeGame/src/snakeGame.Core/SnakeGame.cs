using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

namespace snakeGame.Core;

using static Helpers.ArgumentHelper;
using static Helpers.ChainingHelper;
using static Utility;

public class SnakeGame
{
    private readonly IGenerate generator = GetGenerator();

    private readonly IDisplay display = GetDisplay();

    public void Run(string[] args)
    {
        Result<(bool, int, int)> argumentsValidationResult = ValidateArguments(args, Console.BufferHeight, Console.BufferWidth);
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
            Actors = new Actor[argumentsValidationResult.Data.Item2][],
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
