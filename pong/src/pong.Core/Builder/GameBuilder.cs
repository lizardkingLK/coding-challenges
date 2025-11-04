using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.Notifications;
using pong.Core.State.Assets;
using pong.Core.State.Handlers;
using pong.Core.State.Misc;
using static pong.Core.State.Assets.RacketManager;

namespace pong.Core.Builder;

public class GameBuilder(Arguments arguments)
{
    private readonly GameManager _gameManager = new();

    private readonly Arguments _arguments = arguments;

    public GameManager Create()
    {
        // TODO: when ball moves enemy racket should listen
        return WithDifficulty().WithSubscribers().WithPointsToWin().Build();
    }

    private GameBuilder WithPointsToWin()
    {
        _gameManager.PointsToWin = _arguments.PointsToWin;

        return this;
    }

    private GameBuilder WithSubscribers()
    {
        HashMap<Type, DynamicallyAllocatedArray<ISubscriber>> subscribers = new();
        StatusManager statusManager = new(_gameManager);
        BoardManager boardManager = new(statusManager);
        RacketManager racketManager = new(statusManager);
        BallManager ballManager = new(statusManager);

        _gameManager.Subscribers = subscribers;
        
        subscribers.Add(typeof(GamePausedNotification), new(
            boardManager,
            racketManager,
            ballManager,
            statusManager));
        subscribers.Add(typeof(GameRoundEndNotification), new(
            ballManager));
        subscribers.Add(typeof(GameCreateNotification), new(
            boardManager,
            racketManager,
            ballManager,
            statusManager));
        subscribers.Add(typeof(BallMoveNotification), new(
            ballManager,
            racketManager,
            statusManager));
        subscribers.Add(typeof(RacketMoveNotification), new(
            racketManager));

        _gameManager.Subscribers = subscribers;

        return this;
    }

    private GameBuilder WithDifficulty()
    {
        _gameManager.Difficulty = _arguments.DifficultyLevel switch
        {
            DifficultyLevelEnum.Easy => new Difficulty
            {
                RacketSpeed = Difficulty.DefaultSpeed * 2,
                BallMoveInterval = Difficulty.DefaultBallMoveInterval * 2,
                BallSpawnTimeout = Difficulty.DefaultBallSpawnTimeout * 2,
                CPUWaitTimeout = Difficulty.DefaultCPUWaitTimeout * 2,
            },
            DifficultyLevelEnum.Medium => new Difficulty(),
            DifficultyLevelEnum.Hard => new Difficulty
            {
                RacketSpeed = Difficulty.DefaultSpeed / 2,
                BallMoveInterval = Difficulty.DefaultBallMoveInterval / 2,
                BallSpawnTimeout = Difficulty.DefaultBallSpawnTimeout / 2,
                CPUWaitTimeout = Difficulty.DefaultCPUWaitTimeout / 2,
            },
            _ => throw new NotImplementedException(),
        };

        return this;
    }

    private GameManager Build() => _gameManager;
}