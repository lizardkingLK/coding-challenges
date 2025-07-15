using snakeGame.Core.Enums;
using snakeGame.Core.Library;

namespace snakeGame.Core.State;

public class Manager
{
    public int Height { get; init; }

    public int Width { get; init; }

    public OutputTypeEnum OutputType { get; init; }

    public Block? Enemy { get; set; }

    public Deque<Block>? Player { get; set; }

    public required Block[,] Map { get; init; }

    public required DynamicArray<Block> Spaces { get; init; }

}
