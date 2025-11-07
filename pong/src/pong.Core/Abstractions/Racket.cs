using pong.Core.Notifications;

namespace pong.Core.Abstractions;

public abstract record Racket : Subscriber
{
    public abstract void Create();
    public abstract void Move(RacketMoveNotification racketMove);
    public abstract void MovePlayerUpIfSatisfies(int distance);
    public abstract void MovePlayerDownIfSatisfies(int distance);
    public abstract void Notify(BallMoveNotification notification);
    public abstract void Toggle();
}