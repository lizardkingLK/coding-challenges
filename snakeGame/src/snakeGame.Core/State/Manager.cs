using snakeGame.Core.Enums;
using snakeGame.Core.Library;

namespace snakeGame.Core.State;

public class Manager
{
    public int Height { get; init; }

    public int Width { get; init; }

    public (int, int) Dimensions { get => (Height, Width); }

    public OutputTypeEnum OutputType { get; init; }

    public Block Enemy { get; set; }

    public required Deque<Block> Player { get; set; }

    public required Block[,] Map { get; init; }

    public required DynamicArray<Block> Spaces { get; init; }
}
