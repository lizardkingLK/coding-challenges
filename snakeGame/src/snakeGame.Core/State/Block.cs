using snakeGame.Core.Enums;

namespace snakeGame.Core.State;

public struct Block
{
    public readonly int CordinateY { get; init; }

    public readonly int CordinateX { get; init; }

    public readonly (int, int) Cordinates
    {
        get => (CordinateY, CordinateX);
    }

    public DirectionEnum Direction { get; set; }

    public char Type { get; set; }

    public readonly void Deconstruct(
        out int cordinateY,
        out int cordinateX,
        out DirectionEnum direction,
        out char type)
    {
        cordinateY = CordinateY;
        cordinateX = CordinateX;
        direction = Direction;
        type = Type;
    }
}