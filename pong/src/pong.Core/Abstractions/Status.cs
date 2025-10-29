using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Game;

namespace pong.Core.Abstractions;

public abstract record Status
{
    public DynamicallyAllocatedArray<DynamicallyAllocatedArray<Block>> MapGrid { get; set; } = new();

    public void Create(Block block)
    {
        MapGrid[block.Top]!.Insert(block.Left, block);
    }

    public void CreateRange(
        int top,
        (int, int) bounds,
        char symbol,
        ConsoleColor color)
    {
        (int start, int end) = bounds;
        Block block;
        for (int i = start; i <= end; i++)
        {
            block = new(top, i, symbol, color);
            MapGrid[top]![i] = block;
        }
    }

    public abstract void Output();
}