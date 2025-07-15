using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Display;

public class FileDisplay : IDisplay
{
    public void Display(Manager manager)
    {
        int height = manager.Height;
        int width = manager.Width;
        Block[,] map = manager.Map;

        using FileStream fileStream = new(Path.Join(Directory.GetCurrentDirectory(), "output.txt"), FileMode.Create);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                fileStream.WriteByte((byte)map[i, j].Type);
            }

            fileStream.WriteByte((byte)'\n');
        }

        fileStream.Close();
    }
}