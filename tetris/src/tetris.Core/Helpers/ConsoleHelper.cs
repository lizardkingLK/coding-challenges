namespace tetris.Core.Helpers;

using static System.Console;

public static class ConsoleHelper
{
    public static void WriteError(
        string content,
        int y,
        int x,
        ConsoleColor color)
    {
        ForegroundColor = color;
        SetCursorPosition(y, x);
        WriteLine(content);
        ResetColor();
    }
}