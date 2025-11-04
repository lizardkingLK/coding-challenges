using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;

namespace pong.Core.Abstractions;

public interface IPublisher
{
    public HashMap<Type, DynamicallyAllocatedArray<ISubscriber>> Subscribers { get; set; }
    public void Publish();
    public void Publish(INotification notification);
}