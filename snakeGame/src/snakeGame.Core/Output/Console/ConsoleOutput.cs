using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

using static System.Console;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Output.Console;

public class ConsoleOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output(GameState? state = null)
    {
        int height = Manager!.Height;
        int width = Manager.Width;
        Block[,] map = Manager.Map;

        char type;
        Clear();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                type = map[i, j].Type;
                SetColor(type);

                Write(type);
                ResetColor();
            }

            Write(CharNewLine);
        }
    }

    private static void SetColor(char type)
    {
        if (type == CharWallBlock)
        {
            ForegroundColor = ConsoleColor.DarkYellow;
        }

        if (type == CharEnemy)
        {
            ForegroundColor = ConsoleColor.Red;
        }

        if (type == CharPlayerHead)
        {
            ForegroundColor = ConsoleColor.Green;
        }

        if (type == CharPlayerBody)
        {
            ForegroundColor = ConsoleColor.DarkGreen;
        }
    }

    public void Stream(GameState state)
    {
    }
}