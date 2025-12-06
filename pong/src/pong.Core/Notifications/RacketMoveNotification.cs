using pong.Core.Abstractions;
using pong.Core.Enums;

namespace pong.Core.Notifications;

public record RacketMoveNotification(
    VerticalDirectionEnum Direction,
    PlayerSideEnum PlayerSide,
    int Speed,
    bool UseDistance = false) : Notification;
