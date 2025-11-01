using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Assets;
using pong.Core.State.Players;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher
{
    private readonly InputPlayer _inputPlayer;
    private readonly StatusManager _statusManager;

    private readonly GameRoundEndNotification _gameRoundEndNotification;
    private readonly BallManager.BallMoveNotification _ballMoveNotification;

    public record GamePausedNotification : INotification;
    public record GameCreateNotification : INotification;
    public record GameRoundEndNotification : INotification;
    public record GameEndNotification : INotification;

    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public bool gamePaused = false;
    public bool gameRoundEnd = false;

    public GameManager()
    {
        _statusManager = new(this);

        Subscribers.Add(new BoardManager(_statusManager));
        Subscribers.Add(new RacketManager(_statusManager));
        Subscribers.Add(new BallManager(_statusManager));
        Subscribers.Add(_statusManager);

        _inputPlayer = new(this);

        _gameRoundEndNotification = new();
        _ballMoveNotification = new();
    }

    public bool Play()
    {
        Publish(new GameCreateNotification());

        Task.Run(_inputPlayer.Play);
        while (true)
        {
            if (gamePaused)
            {
                continue;
            }

            if (gameRoundEnd)
            {
                Publish(_gameRoundEndNotification);
                gameRoundEnd = false;
                continue;
            }

            Publish(_ballMoveNotification);

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