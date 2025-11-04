using pong.Core.Enums;
using pong.Core.Notifications;
using pong.Core.State.Assets;
using pong.Core.State.Handlers;

namespace pong.Core.State.Players;

public class InputPlayer(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;
    private readonly GamePausedNotification gamePausedNotification
    = new();

    private readonly int _speed = gameManager.Difficulty.RacketSpeed;

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
                _gameManager.Publish(new RacketManager.RacketMoveNotification
                (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _speed));
            }
            else if (consoleKey == ConsoleKey.DownArrow || consoleKey == ConsoleKey.J)
            {
                _gameManager.Publish(new RacketManager.RacketMoveNotification
                (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _speed));
            }
        }
    }
}