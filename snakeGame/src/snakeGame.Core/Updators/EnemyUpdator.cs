using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Updators;

public class EnemyUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }
    
    public required IOutput Output { get; init; }

    public void Play()
    {
        throw new NotImplementedException();
    }
}