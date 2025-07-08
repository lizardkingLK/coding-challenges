using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Library;

namespace snakeGame.Core.Display;

public class FileDisplay : IDisplay
{
    public void Display(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        DynamicArray<Actor> actors = manager.Actors;
        int size = actors.Size;

        using FileStream fileStream = new("output.txt", FileMode.Create);

        Actor currentActor;
        foreach (var item in actors.Get)
        {
            
        }
        for (int i = 0; i < size; i++)
        {
            currentActor = actors.Search();

            fileStream.WriteByte((byte)currentActor.State);
            if (i > 0 && i % width == 0)
            {
                fileStream.WriteByte((byte)'\n');
            }
        }

        fileStream.Close();
    }
}