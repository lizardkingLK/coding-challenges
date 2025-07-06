namespace snakeGame.Program;

using snakeGame.Core;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            SnakeGame snakeGame = new();
            snakeGame.Run(args);
        }
        catch (IOException)
        {
            WriteError(ERROR_INVALID_TERMINAL);
        }
    }
}
