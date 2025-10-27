using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Helpers.CordinatesHelper;
using static pong.Core.Helpers.DirectionHelper;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class BallManager : ISubscriber
{
    private readonly Output _output;
    private readonly ConsoleColor _ballColor = ConsoleColor.Red;
    private HorizontalDirectionEnum _xDirection;
    private VerticalDirectionEnum _yDirection;
    private (int, int) _dimensions;
    private Block _previous;

    public record BallMoveNotification : INotification;

    public BallManager(Output output)
    {
        _output = output;
        _dimensions = (_output.Height, _output.Width);

        Create();
    }

    private void Create()
    {
        InitializeBallPosition(_dimensions, out int y, out int x);
        InitializeDirections((y, x), _dimensions, ref _yDirection, ref _xDirection);

        _previous = new(y, x, BallBlockSymbol, _ballColor);

        _output.Draw(_previous);
    }

    private void Move()
    {
        (int y, int x, _, _) = _previous;
        char symbol = x == _output.Width / 2 ? NetBlockSymbol : SpaceBlockSymbol;
        Block cleared = new(y, x, symbol);

        object[] directions = [_yDirection, _xDirection];
        GetNextBallPosition(
            _dimensions,
            (y, x),
            directions,
            out (int, int) nextCordinates);

        (y, x) = nextCordinates;

        _yDirection = (VerticalDirectionEnum)directions[0];
        _xDirection = (HorizontalDirectionEnum)directions[1];
        _previous = new(y, x, BallBlockSymbol, _ballColor);

        _output.Draw(_previous);
        _output.Draw(cleared);
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GamePausedNotification:
                // Create();
                break;
            case BallMoveNotification:
                Move();
                break;
            default:
                break;
        }
    }

    public void Subscribe()
    {

    }
}