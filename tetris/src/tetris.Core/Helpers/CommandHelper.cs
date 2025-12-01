using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Helpers;

public static class CommandHelper
{
    public static void HandleError(string content)
    {
        WriteLine(content, ColorError);
        Environment.Exit(1);
    }
}