using snakeGame.Core.Abstractions;
using snakeGame.Core.State;
using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Playables;

public class Enemy : IPlayable
{
    public IPlayable? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    private readonly Random _random = new();

    public void Play()
    {
        DynamicArray<Block> spaces = Manager.Spaces;
        Block[,] map = Manager.Map;

        Manager.Enemy = spaces.Remove(_random.Next(0, spaces.Size));
        UpdateMapBlock(map, Manager.Enemy.Cordinates, CharEnemy);
    }
}