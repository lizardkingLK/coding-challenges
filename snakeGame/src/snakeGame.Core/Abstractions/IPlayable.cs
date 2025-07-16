using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IPlay
{
    public Manager Manager { get; init; }

    public IOutput Output { get; init; }

    public IPlay? Next { get; init; }

    public void Play();
}