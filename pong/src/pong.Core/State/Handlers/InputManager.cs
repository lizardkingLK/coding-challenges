using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Assets;

namespace pong.Core.State.Handlers;

public class InputManager(GameManager gameManager)
{
    private GameManager _gameManager = gameManager;
    private readonly GameManager.GamePausedNotification gamePausedNotification
    = new();
    private readonly RacketManager.RacketMoveNotification racketMoveUpNotification
    = new(Enums.VerticalDirectionEnum.Up, Enums.PlayerSideEnum.PlayerOneLeft);
    private readonly RacketManager.RacketMoveNotification racketMoveDownNotification
    = new(Enums.VerticalDirectionEnum.Down, Enums.PlayerSideEnum.PlayerOneLeft);

    public void Play()
    {
        ConsoleKeyInfo key;
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                _gameManager.gamePaused = !_gameManager.gamePaused;
                _gameManager.Publish(gamePausedNotification);
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                _gameManager.Publish(racketMoveUpNotification);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _gameManager.Publish(racketMoveDownNotification);
            }
        }
    }
}