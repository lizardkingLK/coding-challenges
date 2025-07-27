namespace snakeGame.Core.Helpers;

using snakeGame.Core.Enums;
using snakeGame.Core.Library;

using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Enums.DirectionEnum;

public static class ConsoleHelper
{
    private static readonly HashMap<ConsoleKey, DirectionEnum> _keyMap = new();

    static ConsoleHelper()
    {
        _keyMap.Insert(ConsoleKey.RightArrow, Right);
        _keyMap.Insert(ConsoleKey.L, Right);
        _keyMap.Insert(ConsoleKey.DownArrow, Down);
        _keyMap.Insert(ConsoleKey.J, Down);
        _keyMap.Insert(ConsoleKey.LeftArrow, Left);
        _keyMap.Insert(ConsoleKey.H, Left);
        _keyMap.Insert(ConsoleKey.UpArrow, Up);
        _keyMap.Insert(ConsoleKey.K, Up);
    }

    public static bool ReadKeyPress(DirectionEnum? previous, out DirectionEnum? value)
    {
        value = default;

        ConsoleKeyInfo read = Console.ReadKey();
        if (!_keyMap.TryGetValue(read.Key, out DirectionEnum readDirection))
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
