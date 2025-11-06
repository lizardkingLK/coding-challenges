using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.Notifications;
using pong.Core.State.Assets;
using pong.Core.State.Handlers;
using pong.Core.State.Misc;
using pong.Core.State.Players;
using static pong.Core.Shared.Constants;

namespace pong.Core.Builder;

public class GameBuilder
{
    private readonly Arguments _arguments;

    private readonly GameManager _gameManager;

    private readonly StatusManager _statusManager;

    private LeftRacketManager? _leftRacketManager;
    private RightRacketManager? _rightRacketManager;

    public GameBuilder(Arguments arguments)
    {
        _arguments = arguments;

        _gameManager = new();
        _statusManager = new(_gameManager);
    }

    public GameManager Create()
    {
        return WithDifficulty()
        .WithGameMode()
        .WithSubscribers()
        .WithPointsToWin()
        .WithPlayerSide()
        .Build();
    }

    private GameBuilder WithGameMode()
    {
        GameModeEnum gameMode = _arguments.GameMode;
        int speed = _gameManager.Difficulty!.RacketSpeed;
        if (gameMode == GameModeEnum.Auto)
        {

        }
        else if (gameMode == GameModeEnum.Offline)
        {
            UserPlayer userPlayer = new(speed);
            _leftRacketManager = new(_statusManager, userPlayer);
            userPlayer.Racket = _leftRacketManager;
            _gameManager.PlayerLeft = userPlayer;

            EnemyPlayer enemyPlayer = new(speed);
            _rightRacketManager = new(_statusManager, enemyPlayer);
            enemyPlayer.Racket = _rightRacketManager;
            _gameManager.PlayerRight = enemyPlayer;
        }
        else if (gameMode == GameModeEnum.Online)
        {

        }

        return this;
    }

    private GameBuilder WithPlayerSide()
    {
        // TODO: add player side options

        return this;
    }

    private GameBuilder WithPointsToWin()
    {
        _gameManager.PointsToWin = _arguments.PointsToWin;

        return this;
    }

    private GameBuilder WithSubscribers()
    {
        if (_leftRacketManager == null || _rightRacketManager == null)
        {
            return this;
        }

        HashMap<Type, DynamicallyAllocatedArray<Subscriber>> subscribers = _gameManager.Subscribers;
        BoardManager boardManager = new(_statusManager);
        BallManager ballManager = new(_statusManager);

        subscribers.Add(typeof(GamePausedNotification), new(
            boardManager,
            _leftRacketManager,
            _rightRacketManager,
            ballManager,
            _statusManager));
        subscribers.Add(typeof(GameRoundEndNotification), new(
            ballManager));
        subscribers.Add(typeof(GameCreateNotification), new(
            boardManager,
            _leftRacketManager,
            _rightRacketManager,
            ballManager,
            _statusManager));
        subscribers.Add(typeof(BallMoveNotification), new(
            ballManager,
            _leftRacketManager,
            _rightRacketManager));
        subscribers.Add(typeof(RacketMoveNotification), new(
            _leftRacketManager,
            _rightRacketManager));

        return this;
    }

    private GameBuilder WithDifficulty()
    {
        _gameManager.Difficulty = _arguments.DifficultyLevel switch
        {
            DifficultyLevelEnum.Easy => new Difficulty
            {
                RacketSpeed = DefaultSpeed * 2,
                BallMoveInterval = DefaultBallMoveInterval * 2,
                BallSpawnTimeout = DefaultBallSpawnTimeout * 2,
                CPUWaitTimeout = DefaultCPUWaitTimeout * 2,
            },
            DifficultyLevelEnum.Medium => new Difficulty(),
            DifficultyLevelEnum.Hard => new Difficulty
            {
                RacketSpeed = DefaultSpeed / 2,
                BallMoveInterval = DefaultBallMoveInterval / 2,
                BallSpawnTimeout = DefaultBallSpawnTimeout / 2,
                CPUWaitTimeout = DefaultCPUWaitTimeout / 2,
            },
            _ => throw new NotImplementedException(),
        };

        return this;
    }

    private GameManager Build() => _gameManager;
}