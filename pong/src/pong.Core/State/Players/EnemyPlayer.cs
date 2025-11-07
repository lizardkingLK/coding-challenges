using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Notifications;
using static pong.Core.Helpers.DistanceHelper;

namespace pong.Core.State.Players;

public record EnemyPlayer : Input
{
    private BallMoveNotification? _ballMoveNotification;

    public Subscriber? Racket { get; set; }

    private bool _isPaused;

    public EnemyPlayer(int _)
    {
    }

    public override void Play()
    {
        int ballTop;
        int playerTop;
        int playerBottom;
        VerticalDirectionEnum direction;
        while (true)
        {
            if (_ballMoveNotification == null || _isPaused)
            {
                continue;
            }

            ballTop = _ballMoveNotification.Position!.Value.Top;
            playerTop = _ballMoveNotification.Player!.Head!.Value.Top;
            playerBottom = _ballMoveNotification.Player!.Tail!.Value.Top;
            if (ballTop >= playerTop && ballTop <= playerBottom)
            {
                continue;
            }

            direction = GetShorterDistance(ballTop - playerTop, ballTop - playerBottom, out int distance);
            Racket?.Listen(new RacketMoveNotification(direction, PlayerSideEnum.PlayerRight, distance, true));

            _ballMoveNotification = null;
        }
    }

    public override void Notify(BallMoveNotification ballMoveNotification)
    {
        _ballMoveNotification = ballMoveNotification;
    }

    public override void Toggle() => _isPaused = !_isPaused;
}