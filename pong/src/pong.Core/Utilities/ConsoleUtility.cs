using static System.Console;
using static System.ConsoleColor;
using static pong.Core.Shared.Constants;

namespace pong.Core.Utilities;

public static class ConsoleUtility
{
    private static readonly Lock _lock = new();

    static ConsoleUtility()
    {
        CursorVisible = false;
        CancelKeyPress += (sender, _) => Clear();
    }

    public static void ClearConsole() => Clear();

    public static (int, int) GetWindowDimensions()
    {
        return (WindowHeight, WindowWidth);
    }

    public static void WriteSymbolAt(int top, int left, char symbol, ConsoleColor color = White)
    {
        lock (_lock)
        {
            ForegroundColor = color;
            SetCursorPosition(left, top);
            Write(symbol);
            ResetColor();
        }
    }

    public static void WriteInformationLine(int top, int left, object content)
    {
        SetCursorPosition(left, top);
        WriteLine(new string(SpaceBlockSymbol, BufferWidth));

        SetCursorPosition(left, top);
        ForegroundColor = Cyan;
        WriteLine(content);
        ResetColor();
    }

    public static void WriteInformation(object content)
    {
        ForegroundColor = Cyan;
        WriteLine(content);
        ResetColor();
    }

    public static void WriteError(object content)
    {
        ForegroundColor = Red;
        WriteLine(content);
        ResetColor();
    }

    public static bool ListenToResize(out int height, out int width)
    {
        int previousHeight = WindowHeight;
        int previousWidth = WindowWidth;
        while (true)
        {
            height = WindowHeight;
            width = WindowWidth;
            if (height != previousHeight || width != previousWidth)
            {
                return true;
            }
        }
    }
}