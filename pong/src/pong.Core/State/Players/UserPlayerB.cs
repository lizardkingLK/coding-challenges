using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Notifications;

namespace pong.Core.State.Players;

public record UserPlayerB : Input
{
    private readonly int _distance;

    public Subscriber? Racket { get; set; }

    private readonly ConsoleKey _upKey;
    private readonly ConsoleKey _downKey;

    public UserPlayerB(int distance)
    {
        _distance = distance;
        _upKey = ConsoleKey.K;
        _downKey = ConsoleKey.J;
    }

    public override void Play()
    {
        ConsoleKeyInfo key;
        while (true)
        {
            key = Console.ReadKey(true);
            ConsoleKey consoleKey = key.Key;
            if (consoleKey == _upKey)
            {
                Racket?.Listen(new RacketMoveNotification
                (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _distance));
            }
            else if (consoleKey == _downKey)
            {
                Racket?.Listen(new RacketMoveNotification
                (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _distance));
            }
        }
    }

    public override void Notify(BallMoveNotification ballMoveNotification)
    {
    }
}