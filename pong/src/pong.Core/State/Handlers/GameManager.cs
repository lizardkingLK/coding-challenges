using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Assets;
using pong.Core.State.Misc;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher
{
    private readonly InputManager _inputManager;
    private readonly StatusManager _statusManager;

    public record GamePausedNotification : INotification;
    public record GameCreateNotification : INotification;
    public record GameRoundEndNotification : INotification;
    public record GameEndNotification : INotification;

    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public bool gamePaused = false;

    public GameManager()
    {
        _statusManager = new(this);

        Subscribers.Add(new BoardManager(_statusManager));
        Subscribers.Add(new RacketManager(_statusManager));
        Subscribers.Add(new BallManager(_statusManager));
        Subscribers.Add(_statusManager);

        _inputManager = new(this);
    }

    public bool Play()
    {
        Publish(new GameCreateNotification());

        Task.Run(_inputManager.Play);
        while (true)
        {
            if (gamePaused)
            {
                continue;
            }

            Publish(new BallManager.BallMoveNotification());

            Thread.Sleep(20);
        }
    }

    public void Publish(INotification notification)
    {
        foreach (ISubscriber? subscriber in Subscribers.Values)
        {
            subscriber?.Listen(notification);
        }
    }

    public void Publish()
    {
    }
}