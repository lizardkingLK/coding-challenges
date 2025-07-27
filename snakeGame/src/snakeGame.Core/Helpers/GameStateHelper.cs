using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Helpers;

public static class GameStateHelper
{
    public static
    Result<bool> GetManager(
        (bool, int, int, OutputTypeEnum, GameModeEnum) managerData,
        out Manager manager)
    {
        (_, int height, int width, OutputTypeEnum outputType, GameModeEnum gameMode)
        = managerData;

        manager = new()
        {
            Height = height,
            Width = width,
            OutputType = outputType,
            GameMode = gameMode,
            Map = new Block[height, width],
            Spaces = new DynamicArray<Block>(),
            Player = new Deque<Block>(),
        };

        return new(true, null);
    }
}