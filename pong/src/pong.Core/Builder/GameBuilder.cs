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

    private PlayerSideEnum _playerSide;

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
        .WithPlayerSide()
        .WithGameMode()
        .WithSubscribers()
        .WithPointsToWin()
        .Build();
    }

    private GameBuilder WithGameMode()
    {
        GameModeEnum gameMode = _arguments.GameMode;
        int distance = _gameManager.Difficulty!.RacketSpeed;
        string[] labels = new string[2];
        if (gameMode == GameModeEnum.Auto)
        {
            EnemyPlayer enemyPlayerLeft = new(distance);
            _leftRacket = new LeftRacketManager(_statusManager, enemyPlayerLeft);
            enemyPlayerLeft.Racket = _leftRacket;
            _gameManager.PlayerLeft = enemyPlayerLeft;
            labels[0] = CPU1;

            EnemyPlayer enemyPlayerRight = new(distance);
            _rightRacket = new RightRacketManager(_statusManager, enemyPlayerRight);
            enemyPlayerRight.Racket = _rightRacket;
            _gameManager.PlayerRight = enemyPlayerRight;
            labels[1] = CPU2;
        }
        else if (gameMode == GameModeEnum.OfflineSingle)
        {
            UserPlayerPrimary userPlayer = new(distance);
            EnemyPlayer enemyPlayer = new(distance);
            if (_playerSide == PlayerSideEnum.PlayerLeft)
            {
                _leftRacket = new LeftRacketManager(_statusManager, userPlayer);
                userPlayer.Racket = _leftRacket;
                _gameManager.PlayerLeft = userPlayer;
                labels[0] = Player;

                _rightRacket = new RightRacketManager(_statusManager, enemyPlayer);
                enemyPlayer.Racket = _rightRacket;
                _gameManager.PlayerRight = enemyPlayer;
                labels[1] = CPU;
            }
            else
            {
                _leftRacket = new LeftRacketManager(_statusManager, enemyPlayer);
                enemyPlayer.Racket = _leftRacket;
                _gameManager.PlayerLeft = enemyPlayer;
                labels[0] = CPU;

                _rightRacket = new RightRacketManager(_statusManager, userPlayer);
                userPlayer.Racket = _rightRacket;
                _gameManager.PlayerRight = userPlayer;
                labels[1] = Player;
            }
        }
        else if (gameMode == GameModeEnum.OfflineMultiple)
        {
            UserPlayerPrimary userPlayerLeft = new(distance);
            _leftRacket = new LeftRacketManager(_statusManager, userPlayerLeft);
            userPlayerLeft.Racket = _leftRacket;
            _gameManager.PlayerLeft = userPlayerLeft;
            labels[0] = Player1;

            UserPlayerSecondary userPlayerRight = new(distance);
            _rightRacket = new RightRacketManager(_statusManager, userPlayerRight);
            userPlayerRight.Racket = _rightRacket;
            _gameManager.PlayerRight = userPlayerRight;
            labels[1] = Player2;
        }
        else if (gameMode == GameModeEnum.Online)
        {
            // TODO: add online multiplayer game mode
        }

        _statusManager.Label(labels);

        return this;
    }

    private GameBuilder WithPlayerSide()
    {
        _playerSide = _arguments.PlayerSide;

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
                DistanceThreshold = DefaultDistanceThreshold * 2,
            },
            DifficultyLevelEnum.Medium => new Difficulty(),
            DifficultyLevelEnum.Hard => new Difficulty
            {
                RacketSpeed = DefaultSpeed / 2,
                BallMoveInterval = DefaultBallMoveInterval / 2,
                BallSpawnTimeout = DefaultBallSpawnTimeout / 2,
                CPUWaitTimeout = DefaultCPUWaitTimeout / 2,
                DistanceThreshold = DefaultDistanceThreshold / 2,
            },
            _ => throw new NotImplementedException(),
        };

        return this;
    }

    private GameManager Build() => _gameManager;
}