using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class SpaceGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(int height, int width, Actor[][] actors)
    {
        for (int i = 1; i < height - 1; i++)
        {
            for (int j = 0; j < width - 1; j++)
            {
                actors[i][j].State = CharSpaceBlock;
            }
        }

        if (Next != null)
        {
            return Next.Generate(height, width, actors);
        }

        return new(true, null);
    }
}