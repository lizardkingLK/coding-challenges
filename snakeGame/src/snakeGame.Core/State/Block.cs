using snakeGame.Core.Enums;

namespace snakeGame.Core.State;

public struct Block
{
    public readonly int CordinateY { get; init; }
    public readonly int CordinateX { get; init; }
    public DirectionEnum Direction { get; set; }
    public char Type { get; set; }
    public readonly void Deconstruct(out int cordinateY, out int cordinateX, out char type)
    {
        cordinateY = CordinateY;
        cordinateX = CordinateX;
        type = Type;
    }

    public override readonly string ToString()
    {
        return $"({CordinateY}, {CordinateX}, {Type})";
    }
}