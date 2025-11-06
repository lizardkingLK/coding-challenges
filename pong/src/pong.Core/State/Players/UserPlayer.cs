using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Notifications;

namespace pong.Core.State.Players;

public record UserPlayer : Input
{
    private readonly int _speed;

    public Subscriber? Racket { get; set; }

    public UserPlayer(int speed)
    {
        _speed = speed;
    }

    public override void Play()
    {
        ConsoleKeyInfo key;
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            key = Console.ReadKey(true);
            ConsoleKey consoleKey = key.Key;
            if (consoleKey == ConsoleKey.UpArrow || consoleKey == ConsoleKey.K)
            {
                Racket?.Listen(new RacketMoveNotification
                (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _speed));
            }
            else if (consoleKey == ConsoleKey.DownArrow || consoleKey == ConsoleKey.J)
            {
                Racket?.Listen(new RacketMoveNotification
                (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _speed));
            }
        }
    }

    public override void Notify(BallMoveNotification ballMoveNotification)
    {
    }
}