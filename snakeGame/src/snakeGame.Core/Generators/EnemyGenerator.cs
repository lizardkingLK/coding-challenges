using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    private readonly Random _random = new();

    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        DynamicArray<Block> spaces = manager.Spaces;
        Block[,] map = manager.Map;

        Block enemy = spaces.Remove(_random.Next(0, spaces.Size));
        manager.Enemy = enemy;

        (int y, int x, _) = enemy;
        UpdateMapBlock(map, (y, x), CharEnemy);

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}