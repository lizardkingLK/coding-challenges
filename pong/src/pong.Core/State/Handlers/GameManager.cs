using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.Notifications;
using pong.Core.State.Misc;

namespace pong.Core.State.Handlers;

public record GameManager : Publisher
{
    private readonly GameRoundEndNotification _gameRoundEndNotification = new();
    private readonly BallMoveNotification _ballMoveNotification = new();

    public override HashMap<Type, DynamicallyAllocatedArray<Subscriber>> Subscribers { get; set; } = new();
    public Difficulty? Difficulty { get; set; }

    public Input? PlayerLeft { get; set; }
    public Input? PlayerRight { get; set; }

    public bool gamePaused = false;
    public bool gameEnd = false;
    public bool gameRoundEnd = false;

    public int PointsToWin { get; set; }

    public void Create()
    {
        Publish(new GameCreateNotification());
    }

    public bool Play()
    {
        Task.Run(PlayerLeft!.Play);
        Task.Run(PlayerRight!.Play);

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
                Thread.Sleep(Difficulty!.BallSpawnTimeout);
                continue;
            }

            Publish(_ballMoveNotification);
            Thread.Sleep(Difficulty!.BallMoveInterval);
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