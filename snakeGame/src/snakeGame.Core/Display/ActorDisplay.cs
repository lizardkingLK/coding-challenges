using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;

namespace snakeGame.Core.Display;

public class ActorDisplay : IDisplay
{
    public void Display(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        Actor[][]? actors = manager.Actors;
        ArgumentNullException.ThrowIfNull(actors, nameof(actors));

        Actor currentActor;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                currentActor = actors[i][j];
                Console.ForegroundColor = currentActor.ForegroundColor;
                Console.Write(currentActor.State);
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}