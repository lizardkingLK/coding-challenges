namespace snakeGame.Core.Helpers;

using snakeGame.Core.Enums;
using snakeGame.Core.Library;

using static System.Console;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Enums.DirectionEnum;
using static snakeGame.Core.Shared.Constants;

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

    public static void InitializeConsole()
    {
        Clear();
        CursorVisible = false;
    }

    public static bool ReadKeyPress(DirectionEnum? previous, out DirectionEnum? value)
    {
        value = default;

        ConsoleKeyInfo read = ReadKey();
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

    public static void WriteContentToConsoleClearFirst(
        in int y,
        in int x,
        in string content,
        in ConsoleColor textColor = ConsoleColor.White)
    {
        Clear();
        SetConsoleCordinate(y, x);
        ForegroundColor = textColor;
        WriteLine(content);
        ResetColor();
    }

    public static void WriteContentToConsoleClearLineFirst(
        in int y,
        in int _,
        in string content,
        in ConsoleColor textColor = ConsoleColor.White)
    {
        SetConsoleCordinate(y, 0);
        WriteLine(new string(CharSpaceBlock, BufferWidth));

        SetConsoleCordinate(y, 0);
        ForegroundColor = textColor;
        WriteLine(content);
        ResetColor();
    }

    public static void WriteContentToConsole(
        in int y,
        in int x,
        in string content,
        in ConsoleColor textColor = ConsoleColor.White)
    {
        SetConsoleCordinate(y, x);
        ForegroundColor = textColor;
        WriteLine(content);
        ResetColor();
    }

    public static void WriteContentToConsole(
        in int y,
        in int x,
        in char content,
        in ConsoleColor textColor = ConsoleColor.White)
    {
        SetConsoleCordinate(y, x);
        ForegroundColor = textColor;
        Write(content);
        ResetColor();
    }

    public static void SetConsoleCordinate(in int y, in int x)
    {
        SetCursorPosition(x, y);
    }

    public static ConsoleColor GetColorForCharacter(in char type)
    {
        return type switch
        {
            CharWallBlock => ConsoleColor.DarkYellow,
            CharEnemy => ConsoleColor.Red,
            CharPlayerHead => ConsoleColor.Green,
            CharPlayerBody => ConsoleColor.DarkGreen,
            _ => ConsoleColor.White,
        };
    }
}
