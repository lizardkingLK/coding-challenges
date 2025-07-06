namespace snakeGame.Program;

using snakeGame.Core;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Snake.Run(args);
        }
        catch (IOException)
        {
            Utility.WriteError(Constants.ERROR_INVALID_TERMINAL);
        }
    }
}
