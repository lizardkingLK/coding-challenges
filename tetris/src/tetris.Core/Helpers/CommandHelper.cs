using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Helpers;

public static class CommandHelper
{
    public static void HandleError(string content)
    {
        WriteAt(content, 0, 0, ColorError);
        Toggle(isOn: false);
        Environment.Exit(1);
    }
}