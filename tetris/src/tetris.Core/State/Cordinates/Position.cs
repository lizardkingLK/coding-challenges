namespace tetris.Core.State.Cordinates;

public record struct Position(int Y, int X)
{
    public int Y { get; set; } = Y;
    public int X { get; set; } = X;

    public static Position operator +(
        Position positionA,
        Position positionB)
    => new(positionA.Y + positionB.Y, positionA.X + positionB.X);

    public static Position operator -(
        Position positionA,
        Position positionB)
    => new(positionA.Y - positionB.Y, positionA.X - positionB.X);
}