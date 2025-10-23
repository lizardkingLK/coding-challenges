using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;

namespace pong.Core.Abstractions;

public interface IPublisher
{
    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; }
    public void Register();
    public void Publish();
    public void Publish(INotification notification);
}