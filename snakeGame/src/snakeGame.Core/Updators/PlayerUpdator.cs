using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Updators;

public class PlayerUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    private static readonly HashMap<ConsoleKey, DirectionEnum> keyMap = new();

    static PlayerUpdator()
    {
        SetKeyMapValues();
        SetCancelEvent();
    }

    private static void SetCancelEvent()
    {
        Console.CancelKeyPress += OnCancelKeyPress;
    }

    private static void SetKeyMapValues()
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
        Output.Output(Manager);

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
            }

            StepToDirection(direction);
        }
        while (true);
    }

    private void StepToDirection(DirectionEnum direction)
    {
        (int y, int x, _) = Manager.Player!.SeekFront();
        GetNextCordinate((y, x), direction, out int cordinateY, out int cordinateX);
        if (!IsValidCordinate(cordinateY, cordinateX, Manager.Dimensions, Manager.Map))
        {
            throw new Exception("error. game over");
        }

        UpdateMapForNewPosition(new Block()
        {
            CordinateY = cordinateY,
            CordinateX = cordinateX,
            Direction = direction,
            Type = CharPlayerHead,
        });

        Output.Output(Manager);
    }

    private void UpdateMapForNewPosition(Block newPlayerBlock)
    {
        (int y, int x, _) = Manager.Player!.SeekFront();
        UpdateMapBlock(Manager.Map, (y, x), CharPlayerBody);

        Manager.Player!.InsertToFront(newPlayerBlock);
        (int cordinateY, int cordinateX, _) = newPlayerBlock;
        UpdateMapBlock(Manager.Map, (cordinateY, cordinateX), CharPlayerHead);
        UpdateSpaceBlockOut(Manager.Spaces, block =>
            block.CordinateY == cordinateY && block.CordinateX == cordinateX);

        Block oldPlayerTailBlock = Manager.Player!.RemoveFromRear();
        (y, x, _) = oldPlayerTailBlock;
        UpdateMapBlock(Manager.Map, (y, x), CharSpaceBlock);
        UpdateSpaceBlockIn(Manager.Spaces, oldPlayerTailBlock);
    }

    private static bool ReadKeyPress(out DirectionEnum value)
    {
        value = default;

        ConsoleKeyInfo read = Console.ReadKey();
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