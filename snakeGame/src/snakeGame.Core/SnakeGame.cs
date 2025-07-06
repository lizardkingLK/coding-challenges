using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

namespace snakeGame.Core;

using static Helpers.ArgumentHelper;
using static Helpers.GeneratorHelper;
using static Utility;

public class SnakeGame
{
    private readonly IGenerate generator = GetGenerator();

    public void Run(string[] args)
    {
        Result<(bool, int, int)> argumentsValidationResult = ValidateArguments(args, Console.BufferHeight, Console.BufferWidth);
        if (argumentsValidationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(argumentsValidationResult.Error);
            return;
        }

        (_, int height, int width) = argumentsValidationResult.Data;
        Actor[][] actors = new Actor[height][];
        
        Result<bool> generatedGameContext = generator.Generate(height, width, actors);
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        _ = InitializeGame();
    }
}
