using snakeGame.Core.Abstractions;
using snakeGame.Core.State;
using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Updators;

public class EnemyUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    private readonly Random _random = new();

    public void Play()
    {
        DynamicArray<Block> spaces = Manager.Spaces;
        Block[,] map = Manager.Map;

        Block enemy = spaces.Remove(_random.Next(0, spaces.Size));
        Manager.Enemy = enemy;

        (int y, int x, _) = enemy;
        UpdateMapBlock(map, (y, x), CharEnemy);
    }
}