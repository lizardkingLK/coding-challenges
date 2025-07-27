using snakeGame.Core.Events;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Helpers;

public static class EventHelper
{
    public static Result<bool> GetPublisher(Manager manager, out GameStatePublisher publisher)
    {
        publisher = new();

        publisher.Attach(new GameStateSubscriber(manager));

        manager.Publisher = publisher;

        return new(true, null);
    }
}