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

        manager.Publisher.Publish(new(Enums.GameStateEnum.CreateBoard, null, manager.Height, manager.Width));

        return new(true, null);
    }
}