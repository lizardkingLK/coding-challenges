using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Events.UpdateGameComplete;

public class UpdateGameCompleteSubscriber(Manager manager) : ISubscribe<GameState>
{
    private readonly Manager _manager = manager;

    public void Notify(GameState state)
    {
        _manager.Output!.Stream(state);
    }
}