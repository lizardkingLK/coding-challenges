using snakeGame.Core.Abstractions;
using snakeGame.Core.State;
using System.Text;

namespace snakeGame.Core.Output;

public class StringBuilderConsoleOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output()
    {
        int height = Manager!.Height;
        int width = Manager.Width;
        Block[,] map = Manager.Map;
        StringBuilder consoleOutputBuilder = new();
        using StreamWriter output = new(
            Console.OpenStandardOutput(),
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

        Console.Clear();
        Console.WriteLine(consoleOutputBuilder.ToString());
    }
}