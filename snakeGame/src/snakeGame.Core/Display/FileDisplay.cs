using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Library;

namespace snakeGame.Core.Display;

public class FileDisplay : IDisplay
{
    public void Display(Manager manager)
    {
        int width = manager.Width;
        DynamicArray<Actor> actors = manager.Actors;
        int count = 0;

        using FileStream fileStream = new("output.txt", FileMode.Create);

        foreach (Actor actor in actors.GetValues())
        {
            fileStream.WriteByte((byte)actor.State);
            count++;
            if (count % width == 0)
            {
                fileStream.WriteByte((byte)'\n');
            }
        }

        fileStream.Close();
    }
}