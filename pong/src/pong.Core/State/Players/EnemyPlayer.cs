using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.State.Assets;
using pong.Core.State.Game;
using pong.Core.State.Handlers;

namespace pong.Core.State.Players;

public class EnemyPlayer(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;

    private BallManager.BallMoveNotification? _ballMoveNotification;

    public void Play()
    {
        Position position;
        Deque<Block> player;
        while (true)
        {
            if (_ballMoveNotification == null)
            {
                continue;
            }

            position = _ballMoveNotification.Position!.Value;
            player = _ballMoveNotification.Enemy!;
            Console.WriteLine((position.Top, player.Head!.Value.Top, player.Tail!.Value.Top));
            Thread.Sleep(200);

            // if (consoleKey == ConsoleKey.UpArrow || consoleKey == ConsoleKey.K)
            // {
            //     _gameManager.Publish(new RacketManager.RacketMoveNotification
            //     (VerticalDirectionEnum.Up, PlayerSideEnum.PlayerLeft, _speed));
            // }
            // else if (consoleKey == ConsoleKey.DownArrow || consoleKey == ConsoleKey.J)
            // {
            //     _gameManager.Publish(new RacketManager.RacketMoveNotification
            //     (VerticalDirectionEnum.Down, PlayerSideEnum.PlayerLeft, _speed));
            // }

            _ballMoveNotification = null;
        }
    }

    public void Move(BallManager.BallMoveNotification notification)
    {
        _ballMoveNotification = notification;
    }
}