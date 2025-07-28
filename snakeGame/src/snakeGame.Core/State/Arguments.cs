using snakeGame.Core.Enums;

namespace snakeGame.Core.State;

public struct Arguments
{
    public int Height { get; set; }

    public int Width { get; set; }

    public OutputTypeEnum OutputType { get; set; }

    public GameModeEnum GameMode { get; set; }

    public DifficultyLevelEnum DifficultyLevel { get; set; }

    public readonly void Deconstruct(out int height, out int width)
    {
        height = Height;
        width = Width;
    }

    public readonly void Deconstruct(
        out OutputTypeEnum outputType,
        out GameModeEnum gameMode,
        out DifficultyLevelEnum difficultyLevel)
    {
        outputType = OutputType;
        gameMode = GameMode;
        difficultyLevel = DifficultyLevel;
    }
}