using pong.Core.Enums;
using pong.Core.State.Assets;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public class InputManager(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;
    private readonly GameManager.GamePausedNotification gamePausedNotification
    = new();

    private int _speed = DefaultSpeed;
    private VerticalDirectionEnum _previousDirection;

    public void Play()
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
                _speed = _previousDirection == VerticalDirectionEnum.Up ? _speed + 1 : 1;
                _gameManager.Publish(new RacketManager.RacketMoveNotification
                (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _speed));
                _previousDirection = VerticalDirectionEnum.Up;
            }
            else if (consoleKey == ConsoleKey.DownArrow || consoleKey == ConsoleKey.J)
            {
                _speed = _previousDirection == VerticalDirectionEnum.Down ? _speed + 1 : 1;
                _gameManager.Publish(new RacketManager.RacketMoveNotification
                (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _speed));
                _previousDirection = VerticalDirectionEnum.Down;
            }
        }
    }
}