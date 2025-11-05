using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Notifications;
using pong.Core.State.Handlers;

namespace pong.Core.State.Players;

public record UserPlayer : Input
{
    private readonly GameManager _gameManager;
    private readonly GamePausedNotification gamePausedNotification;

    private readonly int _speed;

    public UserPlayer(GameManager GameManager)
    {
        _gameManager = GameManager;
        _speed = _gameManager.Difficulty.RacketSpeed;
        gamePausedNotification = new();
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
            if (consoleKey == ConsoleKey.Escape || consoleKey == ConsoleKey.P)
            {
                _gameManager.gamePaused = !_gameManager.gamePaused;
                _gameManager.Publish(gamePausedNotification);
                return;
            }

            if (consoleKey == ConsoleKey.UpArrow || consoleKey == ConsoleKey.K)
            {
                _gameManager.Publish(new RacketMoveNotification
                (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _speed));
            }
            else if (consoleKey == ConsoleKey.DownArrow || consoleKey == ConsoleKey.J)
            {
                _gameManager.Publish(new RacketMoveNotification
                (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _speed));
            }
        }
    }
}