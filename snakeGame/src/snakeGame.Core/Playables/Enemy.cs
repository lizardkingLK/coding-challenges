using snakeGame.Core.Abstractions;
using snakeGame.Core.State;
using snakeGame.Core.Enums;

namespace snakeGame.Core.Playables;

public class Enemy : IPlayable
{
    public IPlayable? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    public void Play()
    {
        Manager.Publisher!.Publish(new(GameStateEnum.CreateEnemy, null));
    }
}