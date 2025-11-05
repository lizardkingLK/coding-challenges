using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Notifications;
using pong.Core.State.Game;
using pong.Core.State.Outputs;
using pong.Core.State.Players;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public record StatusManager : Status
{
    private readonly Output _output;

    private readonly ScoreManager _scoreManager;

    private readonly GameManager _gameManager;

    private readonly UserPlayer _userPlayer;
    private readonly EnemyPlayer _enemyPlayer;

    public Block? ball;

    public int Height => _output.Height;
    public int Width => _output.Width;

    public StatusManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _output = new ConsoleOutput(_gameManager);
        _scoreManager = new(_output);

        _userPlayer = new(_gameManager);
        _enemyPlayer = new(_gameManager);

        MapGrid = new(values:
        [.. Enumerable.Range(0, _output.Height).Select(_
            => new DynamicallyAllocatedArray<Block>(_output.Width))]);
    }

    public void Map(Block block)
    {
        MapGrid[block.Top]![block.Left] = block;
    }

    public void Update(Block block)
    {
        MapGrid[block.Top]![block.Left] = block;

        _output.Draw(block, MapGrid);
    }

    public override void Listen(Notification notification)
    {
        switch (notification)
        {
            case GameCreateNotification:
                Output();
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

    public bool ScorePoint(PlayerSideEnum playerSide)
    {
        return _scoreManager.Score(playerSide) == _gameManager.PointsToWin;
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
    }

    public void Win(PlayerSideEnum playerSide)
    {
        string content = string.Format(
            FormatGameOver,
            playerSide == PlayerSideEnum.PlayerLeft
            ? Player
            : CPU);
        Position position = new(Height / 2, Width / 2 - content.Length / 2);
        _output.Draw(position, content, ConsoleColor.Green);
        Thread.Sleep(GameEndTimeout);

        _output.Clear();
        _gameManager.gameEnd = true;
    }

    public Input GetInput(PlayerSideEnum playerSide)
    {
        if (playerSide == PlayerSideEnum.PlayerLeft)
        {
            return _userPlayer;
        }
        else
        {
            return _enemyPlayer;
        }
    }
}