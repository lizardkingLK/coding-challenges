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

    private Racket? _leftRacket;
    private Racket? _rightRacket;

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
            // TODO: rename player labels
            EnemyPlayer enemyPlayerLeft = new(speed);
            _leftRacket = new LeftRacketManager(_statusManager, enemyPlayerLeft);
            enemyPlayerLeft.Racket = _leftRacket;
            _gameManager.PlayerLeft = enemyPlayerLeft;

            EnemyPlayer enemyPlayerRight = new(speed);
            _rightRacket = new RightRacketManager(_statusManager, enemyPlayerRight);
            enemyPlayerRight.Racket = _rightRacket;
            _gameManager.PlayerRight = enemyPlayerRight;
        }
        else if (gameMode == GameModeEnum.Offline)
        {
            UserPlayer userPlayer = new(speed);
            _leftRacket = new LeftRacketManager(_statusManager, userPlayer);
            userPlayer.Racket = _leftRacket;
            _gameManager.PlayerLeft = userPlayer;

            EnemyPlayer enemyPlayer = new(speed);
            _rightRacket = new RightRacketManager(_statusManager, enemyPlayer);
            enemyPlayer.Racket = _rightRacket;
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
        if (_leftRacket == null || _rightRacket == null)
        {
            return this;
        }

        HashMap<Type, DynamicallyAllocatedArray<Subscriber>> subscribers = _gameManager.Subscribers;
        BoardManager boardManager = new(_statusManager);
        BallManager ballManager = new(_statusManager);

        subscribers.Add(typeof(GamePausedNotification), new(
            boardManager,
            _leftRacket,
            _rightRacket,
            ballManager,
            _statusManager));
        subscribers.Add(typeof(GameRoundEndNotification), new(
            ballManager));
        subscribers.Add(typeof(GameCreateNotification), new(
            boardManager,
            _leftRacket,
            _rightRacket,
            ballManager,
            _statusManager));
        subscribers.Add(typeof(BallMoveNotification), new(
            ballManager,
            _leftRacket,
            _rightRacket));
        subscribers.Add(typeof(RacketMoveNotification), new(
            _leftRacket,
            _rightRacket));

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