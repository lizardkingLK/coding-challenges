using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IPlayable
{
    public IPlayable? Next { get; init; }

    public Manager Manager { get; init; }

    public IOutput Output { get; init; }

    public void Play();
}