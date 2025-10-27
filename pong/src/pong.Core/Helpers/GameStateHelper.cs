using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Helpers;

public static class GameStateHelper
{
    public static void HandleInformation(string content)
    {
        WriteInformation(content);
        Environment.Exit(0);
    }

    public static void HandleError(string content)
    {
        WriteError(content);
        Environment.Exit(-1);
    }
}