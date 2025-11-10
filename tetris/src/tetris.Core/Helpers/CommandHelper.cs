using static tetris.Core.Helpers.ConsoleHelper;

namespace tetris.Core.Helpers;

public static class CommandHelper
{
    public static void HandleError(string content)
    {
        WriteAt(content, 0, 0, ConsoleColor.Red);
        Environment.Exit(1);
    }

    public static void HandleSuccess(string content)
    {
        WriteAt(content, 0, 0, ConsoleColor.Green);
        Environment.Exit(0);
    }
}