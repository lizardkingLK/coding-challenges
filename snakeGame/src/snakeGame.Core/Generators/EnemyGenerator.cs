using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    public required Manager Manager { get; init; }

    private readonly Random _random = new();

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        DynamicArray<Block> spaces = Manager.Spaces;
        Block[,] map = Manager.Map;

        Manager.Enemy = spaces.Remove(_random.Next(0, spaces.Size));
        UpdateMapBlock(map, Manager.Enemy.Cordinates, CharEnemy);

        if (Next != null)
        {
            return Next.Generate();
        }

        return new(true, null);
    }
}