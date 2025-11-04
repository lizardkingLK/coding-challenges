using pong.Core.Enums;
using pong.Core.Notifications;
using pong.Core.State.Handlers;
using static pong.Core.Helpers.DistanceHelper;

namespace pong.Core.State.Players;

public class EnemyPlayer(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;

    private BallMoveNotification? _ballMoveNotification;

    public void Play()
    {
        int ballTop;
        int playerTop;
        int playerBottom;
        VerticalDirectionEnum direction;
        while (true)
        {
            if (_ballMoveNotification == null)
            {
                continue;
            }

            ballTop = _ballMoveNotification.Position!.Value.Top;
            playerTop = _ballMoveNotification.Enemy!.Head!.Value.Top;
            playerBottom = _ballMoveNotification.Enemy!.Tail!.Value.Top;

            direction = GetShorterDistance(ballTop - playerTop, ballTop - playerBottom, out int distance);
            _gameManager.Publish(new RacketMoveNotification
            (direction, PlayerSideEnum.PlayerRight, distance));

            _ballMoveNotification = null;

            Thread.Sleep(_gameManager.Difficulty.CPUWaitTimeout);
        }
    }

    public void Move(BallMoveNotification notification)
    {
        _ballMoveNotification = notification;
    }
}