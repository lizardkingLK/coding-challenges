using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Helpers;

public static class GameStateHelper
{
    public static
    Result<bool> GetManager(Arguments arguments, out Manager manager)
    {
        (int height, int width)
        = arguments;
        (OutputTypeEnum outputType, GameModeEnum gameMode, DifficultyLevelEnum difficultyLevel)
        = arguments;

        manager = new()
        {
            Height = height,
            Width = width,
            OutputType = outputType,
            GameMode = gameMode,
            DifficultyLevel = difficultyLevel,
            Map = new Block[height, width],
            Spaces = new DynamicArray<Block>(),
            Player = new Deque<Block>(),
        };

        return new(true, null);
    }

    public static Result<bool> GetManager(Manager oldManager, out Manager manager)
    {
        (int height, int width)
        = oldManager;
        (OutputTypeEnum outputType, GameModeEnum gameMode, DifficultyLevelEnum difficultyLevel)
        = oldManager;

        manager = new()
        {
            Height = height,
            Width = width,
            OutputType = outputType,
            GameMode = gameMode,
            DifficultyLevel = difficultyLevel,
            Map = new Block[height, width],
            Spaces = new DynamicArray<Block>(),
            Player = new Deque<Block>(),
        };

        return new(true, null);
    }
}