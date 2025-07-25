namespace snakeGame.Core.Helpers;

using snakeGame.Core.Enums;
using snakeGame.Core.Library;

using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Enums.DirectionEnum;

public static class ConsoleHelper
{
    private static readonly HashMap<ConsoleKey, DirectionEnum> keyMap = new();

    static ConsoleHelper()
    {
        keyMap.Insert(ConsoleKey.RightArrow, Right);
        keyMap.Insert(ConsoleKey.L, Right);
        keyMap.Insert(ConsoleKey.DownArrow, Down);
        keyMap.Insert(ConsoleKey.J, Down);
        keyMap.Insert(ConsoleKey.LeftArrow, Left);
        keyMap.Insert(ConsoleKey.H, Left);
        keyMap.Insert(ConsoleKey.UpArrow, Up);
        keyMap.Insert(ConsoleKey.K, Up);
    }

    public static bool ReadKeyPress(DirectionEnum? previous, out DirectionEnum? value)
    {
        value = default;

        ConsoleKeyInfo read = Console.ReadKey();
        if (!keyMap.TryGetValue(
            read.Key,
            out _,
            out DirectionEnum readDirection))
        {
            return false;
        }

        value = readDirection;
        if (previous == null)
        {
            return true;
        }

        DirectionEnum reversedDirection = GetReversedDirection(previous.Value);
        if (reversedDirection == value)
        {
            value = previous;
        }

        return true;
    }
}
