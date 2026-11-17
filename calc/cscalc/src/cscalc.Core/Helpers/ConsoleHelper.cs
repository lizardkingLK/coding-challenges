namespace cscalc.Core.Helpers;

public static class ConsoleHelper
{
    extension(Console)
    {
        public static void WriteError(string message)
        {
            ConsoleColor colorBefore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = colorBefore;
        }

        public static void WriteSuccess(string message)
        {
            ConsoleColor colorBefore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = colorBefore;
        }
    }
}