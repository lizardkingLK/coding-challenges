using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Events;
using snakeGame.Core.Library;

namespace snakeGame.Core.State;

public class Manager
{
    public int Height { get; init; }

    public int Width { get; init; }

    public (int, int) Dimensions { get => (Height, Width); }

    public OutputTypeEnum OutputType { get; init; }

    public IOutput? Output { get; set; }

    public GameModeEnum GameMode { get; init; }

    public DifficultyLevelEnum DifficultyLevel { get; init; }

    public Block Enemy { get; set; }

    public required Deque<Block> Player { get; set; }

    public required Block[,] Map { get; init; }

    public required DynamicArray<Block> Spaces { get; init; }

    public GameStatePublisher? Publisher { get; set; }

    public void Deconstruct(out int height, out int width)
    {
        height = Height;
        width = Width;
    }
    
    public void Deconstruct(out OutputTypeEnum outputType, out GameModeEnum gameMode, out DifficultyLevelEnum difficultyLevel)
    {
        outputType = OutputType;
        gameMode = GameMode;
        difficultyLevel = DifficultyLevel;
    }
}
