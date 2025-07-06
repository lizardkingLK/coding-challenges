namespace snakeGame.Core;

using static Constants;

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
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static char[] GetHorizontalWall(int width)
    {
        char[] mapLine = new char[width];
        int i = 0;
        while (i < width)
        {
            mapLine[i++] = CharWallBlock;
        }

        return mapLine;
    }

    public static char[] GetHorizontalPath(int width)
    {
        if (width < 3)
        {
            return [];
        }

        char[] mapLine = new char[width];
        int i = 0;
        mapLine[i++] = CharWallBlock;
        while (i < width - 1)
        {
            mapLine[i++] = CharSpaceBlock;
        }

        mapLine[i] = CharWallBlock;

        return mapLine;
    }
}