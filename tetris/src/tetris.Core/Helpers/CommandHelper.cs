using static tetris.Core.Helpers.ConsoleHelper;

namespace tetris.Core.Helpers;

public static class CommandHelper
{
    public static void HandleError(string errors)
    {
        WriteError(errors, 0, 0, ConsoleColor.Red);
        Environment.Exit(1);
    }
}