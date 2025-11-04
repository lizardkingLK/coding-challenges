using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.State.Game;

namespace pong.Core.Notifications;

public record BallMoveNotification : INotification
{
    public Position? Position { get; set; }
    public Deque<Block>? Enemy { get; set; }
}