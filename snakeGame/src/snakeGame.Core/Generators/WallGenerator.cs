using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class WallGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        Actor[][] actors = manager.Actors;

        int i;
        int j;
        Actor currentActor;
        for (i = 0; i < height; i++)
        {
            actors[i] = new Actor[width];
            for (j = 0; j < width; j++)
            {
                currentActor = new(new(i, j), null, CharWallBlock, WallColor);
                actors[i][j] = currentActor;
            }
        }

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}