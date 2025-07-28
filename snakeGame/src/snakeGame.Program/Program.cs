namespace snakeGame.Program;

using snakeGame.Core;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.ConsoleHelper;

class Program
{
    static void Main(string[] args)
    {   
        try
        {
            SnakeGame.Run(args);
        }
        catch (IOException)
        {
            WriteContentToConsoleClearFirst(0, 0, ERROR_INVALID_TERMINAL, ConsoleColor.Red);
            throw;
        }
    }
}
