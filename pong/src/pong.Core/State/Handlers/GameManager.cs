using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.Notifications;
using pong.Core.State.Misc;

namespace pong.Core.State.Handlers;

public record GameManager : Publisher
{
    private readonly GameRoundEndNotification _gameRoundEndNotification = new();

    public override HashMap<Type, DynamicallyAllocatedArray<Subscriber>> Subscribers { get; set; } = new();
    public Difficulty Difficulty { get; set; } = new();
    public int PointsToWin { get; set; }
    public GameModeEnum GameMode { get; internal set; }

    public bool gamePaused = true;
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

    public override void Publish(Notification notification)
    {
        foreach (Subscriber? subscriber in Subscribers[notification.GetType()].Values)
        {
            subscriber?.Listen(notification);
        }
    }
}