namespace snakeGame.Core;

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

    internal static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}