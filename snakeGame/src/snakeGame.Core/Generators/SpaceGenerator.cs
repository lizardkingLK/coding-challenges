using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class SpaceGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        DynamicArray<Actor> actors = manager.Actors;

        int i;
        int j;
        for (i = 1; i < height - 1; i++)
        {
            for (j = 1; j < width - 1; j++)
            {
                if (!actors.Search(
                    actor => actor.Position.Item1 == i && actor.Position.Item2 == j,
                    out Actor currentActor))
                {
                    throw new NullReferenceException(nameof(currentActor));
                }

                currentActor.State = CharSpaceBlock;
            }
        }

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}