using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Output;

public class ConsoleOutput: IOutput
{
    public void Output(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        Block[,] map = manager.Map;

        Console.Clear();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(map[i, j].Type);
            }

            Console.Write('\n');
        }
    }
}