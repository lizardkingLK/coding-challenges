namespace ccct.Core.Helpers;

public static class ApplicationHelper
{
    public static void HandleSuccess(object content)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(content);
        Environment.Exit(0);
    }

    public static void HandleError(object content)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(content);
        Environment.Exit(1);
    }
}