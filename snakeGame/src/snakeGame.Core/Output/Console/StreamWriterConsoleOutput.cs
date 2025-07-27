using snakeGame.Core.Abstractions;
using snakeGame.Core.State;
using System.Text;

using static System.Console;

namespace snakeGame.Core.Output.Console;

public class StreamWriterConsoleOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output(GameState? state = null)
    {
        int height = Manager!.Height;
        int width = Manager.Width;
        Block[,] map = Manager.Map;
        StringBuilder consoleOutputBuilder = new();
        using StreamWriter output = new(
            OpenStandardOutput(),
            bufferSize: Manager.Width * Manager.Height);

        char type;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                type = map[i, j].Type;

                consoleOutputBuilder.Append(type);
            }

            consoleOutputBuilder.AppendLine();
        }

        Clear();
        output.WriteLine(consoleOutputBuilder.ToString());
    }

    public void Stream(GameState state)
    {
    }
}