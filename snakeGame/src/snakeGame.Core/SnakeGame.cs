using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Events;
using snakeGame.Core.Helpers;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core;

using static ArgumentHelper;
using static ChainingHelper;
using static Constants;
using static EventHelper;
using static GameStateHelper;
using static OutputHelper;
using static Utility;

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

        Result<bool> outputContext = GetOutput(manager, out _);
        if (outputContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(outputContext.Error);
            return;
        }

        Result<bool> getEventPublisherResult = GetPublisher(manager, out _);
        if (getEventPublisherResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(getEventPublisherResult.Error);
            return;
        }

        Result<bool> generatedGameContext = GetGenerator(manager).Generate();
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        Result<bool> runnableGameContext = GetPlayable(manager, out IPlayable playable);
        if (runnableGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(runnableGameContext.Error);
            return;
        }

        playable.Play();
    }
}
