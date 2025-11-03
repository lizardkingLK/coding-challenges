using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Assets;
using pong.Core.State.Game;
using pong.Core.State.Misc;
using pong.Core.State.Outputs;
using pong.Core.State.Players;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public record StatusManager : Status, ISubscriber
{
    private readonly Output _output;

    private readonly GameManager _gameManager;
    private readonly Arguments _arguments;
    private readonly ScoreManager _scoreManager;

    private readonly EnemyPlayer _enemyPlayer;
    private readonly InputPlayer _inputPlayer;

    public Block? ball;

    public int Height => _output.Height;
    public int Width => _output.Width;

    public StatusManager(GameManager gameManager, Arguments arguments)
    {
        _gameManager = gameManager;
        _arguments = arguments;
        _output = new ConsoleOutput(gameManager);
        _scoreManager = new(_output);

        MapGrid = new(values:
        [.. Enumerable.Range(0, _output.Height).Select(_
            => new DynamicallyAllocatedArray<Block>(_output.Width))]);

        _enemyPlayer = new(gameManager);
        _inputPlayer = new(gameManager);
    }

    public void Update(Block block)
    {
        MapGrid[block.Top]![block.Left] = block;

        _output.Draw(block, MapGrid);
    }

    public void Subscribe()
    {
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GameCreateNotification:
                Output();
                break;
            case GameManager.GameEndNotification:
                Win((GameManager.GameEndNotification)notification);
                break;
            case BallManager.BallMoveNotification:
                _enemyPlayer.Move((BallManager.BallMoveNotification)notification);
                break;
            default:
                break;
        }
    }

    public MoveTypeEnum Validate(int y, int x, out PlayerSideEnum? playerSide)
    {
        playerSide = default;

        bool isAtSides = x == 0 || x == Width - 1;
        if (!isAtSides)
        {
            return MoveTypeEnum.BallMoving;
        }

        bool isTermination = MapGrid![y]![x]!.Symbol == SpaceBlockSymbol;
        if (!isTermination)
        {
            return MoveTypeEnum.RacketHits;
        }

        playerSide = PlayerSideEnum.PlayerLeft;
        if (x > Width / 2)
        {
            playerSide = PlayerSideEnum.PlayerRight;
        }

        return MoveTypeEnum.PointScored;
    }

    public void ScorePoint(PlayerSideEnum playerSide)
    {
        if (_scoreManager.Score(playerSide) == _arguments.PointsToWin)
        {
            _gameManager.Publish(new GameManager.GameEndNotification(playerSide));
        }
    }

    public void GetBlock(int x, out char symbol, out ConsoleColor color)
    {
        if (x == Width / 2)
        {
            symbol = NetBlockSymbol;
            color = ConsoleColor.Yellow;
        }
        else
        {
            symbol = SpaceBlockSymbol;
            color = ConsoleColor.White;
        }
    }

    public void EndRound() => _gameManager.gameRoundEnd = true;

    public override void Output()
    {
        _output.Draw(MapGrid);

        _scoreManager.Output();

        Task.Run(_enemyPlayer.Play);
        Task.Run(_inputPlayer.Play);
    }

    private void Win(GameManager.GameEndNotification notification)
    {
        string content = string.Format(
            FormatGameOver,
            notification.PlayerSide == PlayerSideEnum.PlayerLeft
            ? Player
            : CPU);
        Position position = new(Height / 2, Width / 2 - content.Length / 2);
        _output.Draw(position, content, ConsoleColor.Green);
        Thread.Sleep(GameEndTimeout);
        _gameManager.gameEnd = true;
    }
}