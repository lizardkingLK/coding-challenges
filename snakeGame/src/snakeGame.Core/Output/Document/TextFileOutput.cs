using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Output.Document;

public class TextFileOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output(GameState? state = null)
    {
        int height = Manager!.Height;
        int width = Manager.Width;
        Block[,] map = Manager.Map;

        using FileStream fileStream = new(Path.Join(Directory.GetCurrentDirectory(), DefaultFileName), FileMode.Create);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                fileStream.WriteByte((byte)map[i, j].Type);
            }

            fileStream.WriteByte((byte)CharNewLine);
        }

        fileStream.Close();
    }

    public void Stream(GameState state)
    {
    }
}