using snakeGame.Core.Enums;

namespace snakeGame.Core.Actors;

public struct Actor(
    Tuple<int, int> position,
    DirectionEnum? direction,
    char state)
{
    public Tuple<int, int> Position { get; } = position;

    public string Id { get; } = string.Intern($"{position.Item1}_{position.Item2}");

    public DirectionEnum? Direction { get; set; } = direction;

    public char State { get; set; } = state;
}