using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Helpers;

public static class CommandHelper
{
    public static void HandleError(string content)
    {
        WriteAt(content, 0, 0, ColorError);
        Environment.Exit(1);
    }

    public static void HandleSuccess(string content)
    {
        WriteAt(content, 0, 0, ColorSuccess);
        Environment.Exit(0);
    }
}