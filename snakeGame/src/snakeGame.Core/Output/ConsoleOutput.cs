using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Output;

public class ConsoleOutput : IOutput
{
    public void Output(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        Block[,] map = manager.Map;

        char type;
        Console.Clear();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                type = map[i, j].Type;
                SetColor(type);

                Console.Write(type);
                Console.ResetColor();
            }

            Console.Write(CharNewLine);
        }
    }

    private static void SetColor(char type)
    {
        if (type == CharWallBlock)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        if (type == CharEnemy)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        if (type == CharPlayerHead)
        { 
            Console.ForegroundColor = ConsoleColor.Green;
        }

        if (type == CharPlayerBody)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
    }
}