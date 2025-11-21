using tetris.Core.Abstractions;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.ConsoleHelper;

namespace tetris.Core.Outputs;

public record ConsoleOutput : IOutput
{
    public void Clear() => Console.Clear();

    public void Flush(in int height, in int width, in Block[,] map)
    {
        foreach (((int y, int x), char symbol, ConsoleColor color) in map)
        {
            WriteAt(symbol, y, x, color);
        }
    }

    public void Stream(in Block block, in int height, in int width, in Block[,] map)
    => WriteAt(block.Symbol, block.Y, block.X, block.Color);
}