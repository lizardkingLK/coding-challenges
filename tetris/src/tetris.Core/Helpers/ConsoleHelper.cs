using static System.Console;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Helpers;

public static class ConsoleHelper
{
    public static void WriteAt(
        object content,
        int y,
        int x,
        ConsoleColor color)
    {
        ForegroundColor = color;
        SetCursorPosition(x, y);
        Console.WriteLine(content);
        ResetColor();
    }

    public static void WriteLine(
        object content,
        ConsoleColor color)
    {
        ForegroundColor = color;
        Console.WriteLine(content);
        ResetColor();
    }

    public static string? ReadInput() => ReadLine();

    public static void ClearConsole() => Clear();

    public static void WritePrompt() => Continue("> ", ColorWall);

    public static void Toggle(bool isOn)
    {
        CursorVisible = !isOn;
        SetCursorPosition(0, 0);
        Clear();
    }

    private static void Continue(
        object content,
        ConsoleColor color)
    {
        ForegroundColor = color;
        Write(content);
        ResetColor();
    }
}