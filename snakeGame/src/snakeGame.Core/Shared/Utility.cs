namespace snakeGame.Core.Shared;

public static class Utility
{
    public static bool IncludesInCollection<T, V>(T value, V collection)
        where V : IEnumerable<T>
        where T : class
    {
        int i;
        int length = collection.Count();
        for (i = 0; i < length; i++)
        {
            if (collection.ElementAt(i).Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteInfo(object message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteInfo(string format, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(string.Format(format, args));
        Console.ResetColor();
    }

    public static void WriteSuccess(string format, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(string.Format(format, args));
        Console.ResetColor();
    }
}