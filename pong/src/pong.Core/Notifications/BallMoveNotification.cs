using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.State.Game;

namespace pong.Core.Notifications;

public record BallMoveNotification(Position? Position = null, Deque<Block>? Player = null) : Notification
{
    public Position? Position { get; set; } = Position;
    public Deque<Block>? Player { get; set; } = Player;
}