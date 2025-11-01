namespace pong.Core.Abstractions;

public interface ISubscriber
{
    public void Subscribe();
    public void Listen();
    public void Listen(INotification notification);
}