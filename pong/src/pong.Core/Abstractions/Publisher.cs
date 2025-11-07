using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;

namespace pong.Core.Abstractions;

public abstract record Publisher
{
    public abstract HashMap<Type, DynamicallyAllocatedArray<Subscriber>> Subscribers { get; set; }
    public abstract void Publish(Notification notification);
}