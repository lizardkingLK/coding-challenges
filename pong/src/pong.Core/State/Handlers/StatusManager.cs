using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Game;
using pong.Core.State.Outputs;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public record StatusManager : Status, ISubscriber
{
    private readonly Output _output;

    private readonly GameManager _gameManager;
    private readonly ScoreManager _scoreManager;

    private readonly INotification _roundEndNotification;
    private readonly INotification _gameEndNotification;

    public int Height => _output.Height;
    public int Width => _output.Width;

    public StatusManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _output = new ConsoleOutput(gameManager);
        _scoreManager = new(_output);

        MapGrid = new(values:
        [.. Enumerable.Range(0, _output.Height).Select(_
            => new DynamicallyAllocatedArray<Block>(_output.Width))]);

        _roundEndNotification = new GameManager.GameRoundEndNotification();
        _gameEndNotification = new GameManager.GameEndNotification();
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
                _scoreManager.Output();
                break;
            default:
                break;
        }
    }

    public bool Validate(int y, int x, out PlayerSideEnum? playerSide)
    {
        playerSide = default;

        bool isAtSides = x == 0 || x == Width - 1;
        if (!isAtSides)
        {
            return true;
        }

        bool isTermination = MapGrid![y]![x]!.Symbol == SpaceBlockSymbol;
        if (isTermination)
        {
            return false;
        }

        playerSide = PlayerSideEnum.PlayerLeft;
        if (x > Width / 2)
        {
            playerSide = PlayerSideEnum.PlayerRight;
        }

        return true;
    }

    public void ScorePoint(PlayerSideEnum playerSide)
    {
        if (_scoreManager.Score(playerSide) == PointsToWin)
        {
            _gameManager.Publish(_gameEndNotification);
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

    public override void Output() => _output.Draw(MapGrid);
}