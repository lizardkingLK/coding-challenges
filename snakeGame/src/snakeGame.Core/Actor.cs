namespace snakeGame.Core;

public readonly struct Actor(Tuple<int, int> position, Direction direction)
{
    public Tuple<int, int> Position { get; } = position;
    public Direction Direction { get; } = direction;
}