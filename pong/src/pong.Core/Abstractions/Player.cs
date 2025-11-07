using pong.Core.Notifications;

namespace pong.Core.Abstractions;

public abstract record Input
{
    public abstract void Play();
    public abstract void Notify(BallMoveNotification ballMoveNotification);
    public abstract void Toggle();
}