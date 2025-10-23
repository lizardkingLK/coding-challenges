using static System.Console;
using static System.ConsoleColor;

namespace pong.Core.Utilities;

public static class ConsoleUtility
{
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
}