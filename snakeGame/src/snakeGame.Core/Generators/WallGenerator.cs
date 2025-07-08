using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Library;
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
        DynamicArray<Actor> actors = manager.Actors;

        int i;
        int j;
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                actors.Add(new Actor(new(i, j), null, CharWallBlock));
            }
        }

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}