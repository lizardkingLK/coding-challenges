namespace snakeGame.Core.Shared;

public static class Utility
{
    public static bool IncludesInValues<T, V>(T value, V values)
        where T : struct
        where V : IEnumerable<T>
    {
        int i;
        int length = values.Count();
        for (i = 0; i < length; i++)
        {
            if (values.ElementAt(i).Equals(value))
            {
                return true;
            }
        }

        return false;
    }
}