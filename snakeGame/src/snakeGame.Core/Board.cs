using snakeGame.Core.Actors;

using static snakeGame.Core.Constants;

namespace snakeGame.Core;

public static class Board
{
    public static Result<Actor[][]> InitializeBoard(int height, int width)
    {
        int i;
        int j;
        Actor[][] actors = new Actor[height][];
        for (i = 0; i < height; i++)
        {
            actors[i] = new Actor[width];
            for (j = 0; j < width; j++)
            {
                actors[i][j] = new(new(i, j), null, CharWallBlock);
            }
        }

        return new(actors, null);
    }

    public static void RenderBoard(Actor[][] actors, int height, int width)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(actors[i][j].State);
            }

            Console.WriteLine();
        }
    }
}