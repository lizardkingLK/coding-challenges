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
        Actor[][] actors = manager.Actors;
        HashMap<string, Actor> spaceTracker = manager.SpaceTracker;

        int i;
        int j;
        Actor currentActor;
        for (i = 1; i < height - 1; i++)
        {
            for (j = 1; j < width - 1; j++)
            {
                currentActor = actors[i][j];
                currentActor.State = CharSpaceBlock;
                currentActor.ForegroundColor = ConsoleColor.White;

                actors[i][j] = currentActor;
                spaceTracker.Insert(currentActor.Id, currentActor);
            }
        }

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}