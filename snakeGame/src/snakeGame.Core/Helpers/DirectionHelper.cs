using snakeGame.Core.Enums;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Helpers;

public static class DirectionHelper
{
    private static readonly Random _random = new();
    private static readonly DirectionEnum[] _directions = Enum.GetValues<DirectionEnum>();

    public static readonly int directionsLength = _directions.Length;

    public static DirectionEnum GetRandomDirection()
    {
        return _directions[_random.Next(directionsLength)];
    }

    public static DirectionEnum GetNextDirection(DirectionEnum direction)
    {
        return (DirectionEnum)(((int)direction + 1) % directionsLength);
    }

    public static void GetNextCordinate(
        (int, int) cordinates,
        DirectionEnum directionEnum,
        out int cordinateY,
        out int cordinateX)
    {
        (int y, int x) = cordinates;
        cordinateY = y;
        cordinateX = x;
        if (directionEnum == DirectionEnum.Right)
        {
            cordinateX++;
        }
        else if (directionEnum == DirectionEnum.Down)
        {
            cordinateY++;
        }
        else if (directionEnum == DirectionEnum.Left)
        {
            cordinateX--;
        }
        else if (directionEnum == DirectionEnum.Up)
        {
            cordinateY--;
        }
    }

    public static bool IsValidCordinate(
        int cordinateY,
        int cordinateX,
        (int, int) dimensions,
        Block[,] map)
    {
        (int height, int width) = dimensions;

        return cordinateY > 0
        && cordinateY < height - 1
        && cordinateX > 0
        && cordinateX < width - 1
        && map[cordinateY, cordinateX].Type == CharSpaceBlock;
    }

    public static bool AreOppositeDirections(DirectionEnum? firstDirection, DirectionEnum? secondDirection)
    {
        return GetReversedDirection(firstDirection) == secondDirection;
    }

    private static DirectionEnum? GetReversedDirection(DirectionEnum? direction)
    {
        return direction switch
        {
            DirectionEnum.Right => DirectionEnum.Left,
            DirectionEnum.Down => DirectionEnum.Up,
            DirectionEnum.Left => DirectionEnum.Right,
            DirectionEnum.Up => DirectionEnum.Down,
            _ => null,
        };
    }
}