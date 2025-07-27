using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

namespace snakeGame.Core.Events;

public class GameStatePublisher : IPublish<GameState>
{
    private readonly DynamicArray<ISubscribe<GameState>> _subscribers = new();

    public void Attach(ISubscribe<GameState> subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Detach(ISubscribe<GameState> subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void Publish(GameState @event)
    {
        foreach (ISubscribe<GameState>? subscriber in _subscribers.ToArray())
        {
            subscriber?.Notify(@event);
        }
    }
}