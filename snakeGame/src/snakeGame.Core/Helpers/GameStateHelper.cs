using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Helpers;

public static class GameStateHelper
{
    public static
    Result<bool> GetManager(
        (bool, int, int, OutputTypeEnum) managerData,
        out Manager manager)
    {
        (_, int height, int width, OutputTypeEnum outputType)
        = managerData;

        manager = new()
        {
            Height = height,
            Width = width,
            OutputType = outputType,
            Map = new Block[height, width],
            Spaces = new DynamicArray<Block>(),
        };

        return new(true, null);
    }
}