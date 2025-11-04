using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.Notifications;
using pong.Core.State.Assets;
using pong.Core.State.Misc;
using static pong.Core.Shared.Values;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher
{
    private readonly GameRoundEndNotification _gameRoundEndNotification = new();

    public HashMap<Type, DynamicallyAllocatedArray<ISubscriber>> Subscribers { get; set; } = new();
    public Difficulty Difficulty { get; set; } = new();
    public GameModeEnum GameMode { get; set; }
    public int PointsToWin { get; set; }

    public bool gamePaused = false;
    public bool gameEnd = false;
    public bool gameRoundEnd = false;

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
                Thread.Sleep(Difficulty.BallSpawnTimeout);
                continue;
            }

            Publish(new BallMoveNotification());

            Thread.Sleep(Difficulty.BallMoveInterval);
        }

        return gameEnd;
    }

    public void Publish(INotification notification)
    {
        foreach (ISubscriber? subscriber in Subscribers[notification.GetType()].Values)
        {
            subscriber?.Listen(notification);
        }
    }

    public void Publish()
    {
    }
}