using snakeGame.Core.Abstractions;
using snakeGame.Core.Helpers;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core;

using static Constants;
using static ChainingHelper;
using static EventHelper;
using static GameStateHelper;
using static OutputHelper;
using static ConsoleHelper;

public static class SnakeGame
{
    public static void Run(string[] args)
    {
        Result<bool> validatorResult = GetValidator(args, args.Length, out Arguments arguments, out IValidate? validator);
        if (validatorResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, validatorResult.Error, ConsoleColor.Red);
            WriteContentToConsole(1, 0, INFO_HELP, ConsoleColor.Cyan);
            return;
        }

        Result<bool> validationResult = validator!.Validate(ref arguments);
        if (validationResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, validationResult.Error, ConsoleColor.Red);
            WriteContentToConsole(1, 0, INFO_HELP, ConsoleColor.Cyan);
            return;
        }

        Result<bool> getManagerResult = GetManager(arguments, out Manager manager);
        if (getManagerResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, getManagerResult.Error, ConsoleColor.Red);
            return;
        }

        Play(manager);
    }

    private static void Play(Manager manager)
    {
        Result<bool> outputContext = GetOutput(manager, out _);
        if (outputContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, outputContext.Error, ConsoleColor.Red);
            return;
        }

        Result<bool> getEventPublisherResult = GetPublisher(manager, out _);
        if (getEventPublisherResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, getEventPublisherResult.Error, ConsoleColor.Red);
            return;
        }

        Result<bool> generatedGameContext = GetGenerator(manager).Generate();
        if (generatedGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, generatedGameContext.Error, ConsoleColor.Red);
            return;
        }

        Result<bool> runnableGameContext = GetPlayable(manager, out IPlayable playable);
        if (runnableGameContext.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, runnableGameContext.Error, ConsoleColor.Red);
            return;
        }

        playable.Play();

        ReplayPrompt(manager);
    }

    private static void ReplayPrompt(Manager oldManager)
    {
        WriteContentToConsole(oldManager.Height + 1, 0, INFO_REPLAY_PROMPT, ConsoleColor.Cyan);
        ConsoleKeyInfo keyInput = Console.ReadKey();
        if (keyInput.Key != ConsoleKey.Y)
        {
            return;
        }

        Result<bool> getManagerResult = GetManager(oldManager, out Manager manager);
        if (getManagerResult.Error != null)
        {
            Environment.ExitCode = 1;
            WriteContentToConsoleClearFirst(0, 0, getManagerResult.Error, ConsoleColor.Red);
            return;
        }

        Play(manager);
    }
}
