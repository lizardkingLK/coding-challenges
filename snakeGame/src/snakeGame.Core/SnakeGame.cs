﻿using snakeGame.Core.Shared;
using snakeGame.Core.Helpers;
using snakeGame.Core.State;
using snakeGame.Core.Enums;
using snakeGame.Core.Abstractions;

namespace snakeGame.Core;

using static Constants;
using static GameStateHelper;
using static ArgumentHelper;
using static ChainingHelper;
using static OutputHelper;
using static Utility;

public class SnakeGame
{
    public static void Run(string[] args)
    {
        Result<(bool, int, int, OutputTypeEnum)> argumentsValidationResult = ValidateArguments
        (args, MaxHeight, MaxWidth);
        if (argumentsValidationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(argumentsValidationResult.Error);
            return;
        }

        Result<bool> getManagerResult = GetManager(argumentsValidationResult.Data, out Manager manager);
        if (getManagerResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(getManagerResult.Error);
            return;
        }

        Result<bool> generatedGameContext = GetGenerator().Generate(manager);
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(generatedGameContext.Error);
            return;
        }

        Result<bool> outputContext = GetOutput(manager.OutputType, out IOutput output);
        if (outputContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(outputContext.Error);
            return;
        }

        Result<bool> runnableGameContext = GetPlayable(manager, output, out IPlay playable);
        if (runnableGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteError(runnableGameContext.Error);
            return;
        }

        playable.Play();
    }
}
