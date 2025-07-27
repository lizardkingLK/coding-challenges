using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Events.CreateBoard;

public class CreateBoardSubscriber(Manager manager) : ISubscribe<GameState>
{
    public Manager _manager = manager;

    public void Notify(GameState state)
    {
        _manager.Output!.Stream(state);
    }
}