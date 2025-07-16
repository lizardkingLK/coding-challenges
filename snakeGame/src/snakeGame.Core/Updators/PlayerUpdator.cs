using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

namespace snakeGame.Core.Updators;

public class PlayerUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    public HashMap<ConsoleKey, DirectionEnum> keyMap = new();

    static PlayerUpdator()
    {
        Console.CancelKeyPress += OnCancelKeyPress;
    }

    public PlayerUpdator()
    {
        keyMap.Insert(ConsoleKey.RightArrow, DirectionEnum.Right);
        keyMap.Insert(ConsoleKey.L, DirectionEnum.Right);
        keyMap.Insert(ConsoleKey.DownArrow, DirectionEnum.Down);
        keyMap.Insert(ConsoleKey.J, DirectionEnum.Down);
        keyMap.Insert(ConsoleKey.LeftArrow, DirectionEnum.Left);
        keyMap.Insert(ConsoleKey.H, DirectionEnum.Left);
        keyMap.Insert(ConsoleKey.UpArrow, DirectionEnum.Up);
        keyMap.Insert(ConsoleKey.K, DirectionEnum.Up);
    }

    public void Play()
    {
        DirectionEnum? previousDirection = null;
        WriteInfo(INFO_AWAITING_KEY_PRESS);
        do
        {
            if (!ReadKeyPress(out DirectionEnum direction))
            {
                WriteError(ERROR_INVALID_KEY_PRESSED);
                continue;
            }

            if (previousDirection != direction)
            {
                previousDirection = direction;
                Output.Output(Manager);
            }

            StepToDirection(direction);
        } while (true);
    }

    private static void StepToDirection(DirectionEnum direction)
    {
        Console.WriteLine(direction);
    }

    private bool ReadKeyPress(out DirectionEnum value)
    {
        value = default;

        ConsoleKeyInfo read;
        read = Console.ReadKey();
        if (!keyMap.TryGetValue(
            read.Key,
            out _,
            out DirectionEnum direction))
        {
            return false;
        }

        value = direction;

        return true;
    }

    private static void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        Console.Clear();
        Console.WriteLine("info. game over. your score is {0}", 1000);
    }
}