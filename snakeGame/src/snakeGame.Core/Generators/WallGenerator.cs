using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class WallGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(int height, int width, Actor[][] actors)
    {
        int i;
        int j;
        for (i = 0; i < height; i++)
        {
            actors[i] = new Actor[width];
            for (j = 0; j < width; j++)
            {
                actors[i][j] = new(new(i, j), null, CharWallBlock);
            }
        }

        if (Next != null)
        {
            return Next.Generate(height, width, actors);
        }

        return new(true, null);
    }
}