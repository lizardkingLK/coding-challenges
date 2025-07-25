using snakeGame.Core.Shared;
using snakeGame.Core.Helpers;
using snakeGame.Core.State;
using snakeGame.Core.Enums;
using snakeGame.Core.Abstractions;

namespace snakeGame.Core;

using static GameStateHelper;
using static ArgumentHelper;
using static ChainingHelper;
using static OutputHelper;
using static Utility;
using static Constants;

public static class SnakeGame
{
    public static void Run(string[] args)
    {
        Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> validationResult = ValidateArguments(args);
        if (validationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(validationResult.Error);
            WriteInfo(INFO_HELP);
            return;
        }

        Result<bool> getManagerResult = GetManager(validationResult.Data, out Manager manager);
        if (getManagerResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(getManagerResult.Error);
            return;
        }

        Result<bool> generatedGameContext = GetGenerator(manager).Generate();
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        Result<bool> outputContext = GetOutput(manager, out IOutput? output);
        if (outputContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(outputContext.Error);
            return;
        }

        Result<bool> runnableGameContext = GetPlayable(manager, output!, out IPlayable playable);
        if (runnableGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(runnableGameContext.Error);
            return;
        }

        playable.Play();
    }
}
