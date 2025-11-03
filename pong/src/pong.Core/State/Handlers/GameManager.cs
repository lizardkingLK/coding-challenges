using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Assets;
using pong.Core.State.Misc;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher
{
    private readonly StatusManager _statusManager;

    private readonly GameRoundEndNotification _gameRoundEndNotification;

    public record GamePausedNotification : INotification;
    public record GameCreateNotification : INotification;
    public record GameRoundEndNotification : INotification;
    public record GameEndNotification(PlayerSideEnum PlayerSide) : INotification;

    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public bool gamePaused = false;
    public bool gameEnd = false;
    public bool gameRoundEnd = false;

    public GameManager(Arguments arguments)
    {
        _statusManager = new(this, arguments);

        Subscribers.Add(new BoardManager(_statusManager));
        Subscribers.Add(new RacketManager(_statusManager));
        Subscribers.Add(new BallManager(_statusManager));
        Subscribers.Add(_statusManager);

        _gameRoundEndNotification = new();
    }

    public bool Play()
    {
        Publish(new GameCreateNotification());
        while (!gameEnd)
        {
            if (gamePaused)
            {
                continue;
            }

            if (gameRoundEnd)
            {
                Publish(_gameRoundEndNotification);
                gameRoundEnd = false;
                Thread.Sleep(BallSpawnTimeout);
                continue;
            }

            Publish(new BallManager.BallMoveNotification());

            Thread.Sleep(BallMoveInterval);
        }

        return gameEnd;
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