using snakeGame.Core.Library;

namespace snakeGame.Core.State;

public class Manager
{
    public int Height { get; set; }

    public int Width { get; set; }

    public Block? Enemy { get; set; }

    public Deque<Block>? Player { get; set; }

    public required Block[,] Map { get; set; }

    public required DynamicArray<Block> Spaces { get; set; }
}
