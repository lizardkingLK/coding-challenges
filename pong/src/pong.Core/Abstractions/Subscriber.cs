namespace pong.Core.Abstractions;

public abstract record Subscriber
{
    public abstract void Listen(Notification notification);
}