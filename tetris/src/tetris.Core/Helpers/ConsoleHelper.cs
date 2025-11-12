namespace tetris.Core.Helpers;

using static System.Console;

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
        WriteLine(content);
        ResetColor();
    }
}