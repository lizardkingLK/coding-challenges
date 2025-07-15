using snakeGame.Core.Abstractions;
using snakeGame.Core.Shared;
using snakeGame.Core.Helpers;
using snakeGame.Core.State;

namespace snakeGame.Core;

using static Constants;
using static ArgumentHelper;
using static ChainingHelper;
using static Utility;

public class SnakeGame
{
    private readonly IGenerate _generator = GetGenerator();

    private readonly IDisplay _display = GetFileDisplay();

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
            Map = new Block[argumentsValidationResult.Data.Item2, argumentsValidationResult.Data.Item3],
            Spaces = new Library.DynamicArray<Block>(),
        };

        Result<bool> generatedGameContext = _generator.Generate(manager);
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        // INFO: Just testing
        _display.Display(manager);

        // TODO: run loop
    }
}
