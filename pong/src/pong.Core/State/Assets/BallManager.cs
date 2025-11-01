using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Helpers.DirectionHelper;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class BallManager(StatusManager statusManager) : ISubscriber
{
    private readonly StatusManager _statusManager = statusManager;

    private readonly ConsoleColor _ballColor = ConsoleColor.Red;

    private HorizontalDirectionEnum _xDirection;
    private VerticalDirectionEnum _yDirection;

    private (int, int) _dimensions;

    private Block? _previous;

    public record BallMoveNotification : INotification;

    private void Create()
    {
        _dimensions = (_statusManager.Height, _statusManager.Width);

        InitializeBallPosition(_dimensions, out int y, out int x);
        InitializeDirections((y, x), _dimensions, ref _yDirection, ref _xDirection);

        _previous = new(y, x, BallBlockSymbol, _ballColor);

        _statusManager.Update(_previous);
    }

    private void Move()
    {
        (int y, int x, _, _) = _previous!;
        _statusManager.GetBlock(x, out char symbol, out ConsoleColor color);

        Block cleared = new(y, x, symbol, color);

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

        _statusManager.Update(cleared);
        _statusManager.Update(_previous);
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GameCreateNotification:
                Create();
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

    private void GetNextBallPosition(
        in (int, int) dimensions,
        in (int, int) cordinates,
        in object[] directions,
        out (int, int) nextCordinates)
    {
        (int y, int x) = cordinates;
        GetInCordinates(directions, ref y, ref x);
        if (!_statusManager.Validate(y, x, out PlayerSideEnum? playerSide))
        {
            nextCordinates = (y, x);
            _statusManager.EndRound();
            return;
        }

        if (playerSide != null)
        {
            _statusManager.ScorePoint(playerSide.Value);
        }

        GetOutCordinates(dimensions, directions, (y, x), out nextCordinates);
    }

    private static void GetOutCordinates(
        (int, int) dimensions,
        in object[] directions,
        (int, int) cordinates,
        out (int, int) nextCordinates)
    {
        (int y, int x) = cordinates;
        (int height, int width) = dimensions;
        VerticalDirectionEnum yDirection = (VerticalDirectionEnum)directions[0];
        HorizontalDirectionEnum xDirection = (HorizontalDirectionEnum)directions[1];
        if (y >= height - 1)
        {
            y -= 2;
            yDirection = VerticalDirectionEnum.Up;
        }
        else if (y < 1)
        {
            y += 2;
            yDirection = VerticalDirectionEnum.Down;
        }

        if (x >= width - 1)
        {
            x -= 2;
            xDirection = HorizontalDirectionEnum.Left;
        }
        else if (x < 1)
        {
            x += 2;
            xDirection = HorizontalDirectionEnum.Right;
        }

        directions[0] = yDirection;
        directions[1] = xDirection;

        nextCordinates = (y, x);
    }

    private static void GetInCordinates(
        in object[] directions,
        ref int y,
        ref int x)
    {
        VerticalDirectionEnum yDirection = (VerticalDirectionEnum)directions[0];
        HorizontalDirectionEnum xDirection = (HorizontalDirectionEnum)directions[1];
        if (yDirection == VerticalDirectionEnum.Down)
        {
            y++;
        }
        else
        {
            y--;
        }

        if (xDirection == HorizontalDirectionEnum.Right)
        {
            x++;
        }
        else
        {
            x--;
        }
    }

    private static void InitializeBallPosition(in (int, int) dimensions, out int y, out int x)
    {
        (int height, int width) = dimensions;
        y = Random.Shared.Next(1, height - 1);
        x = new int[2]
        {
            Random.Shared.Next(1, width / 2 - 1),
            Random.Shared.Next(width / 2 + 1, width - 2)
        }[Random.Shared.Next(2)];
    }
}