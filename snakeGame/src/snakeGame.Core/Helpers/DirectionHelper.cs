using snakeGame.Core.Enums;

namespace snakeGame.Core.Helpers;

public static class DirectionHelper
{
    public static Tuple<int, int> GetNextCordinate(int cordinateY, int cordinateX, DirectionEnum directionEnum)
    {
        int nextCordinateY = cordinateY;
        int nextCordinateX = cordinateX;
        if (directionEnum == DirectionEnum.RIGHT)
        {
            nextCordinateX++;
        }
        else if (directionEnum == DirectionEnum.DOWN)
        {
            nextCordinateY++;
        }
        else if (directionEnum == DirectionEnum.LEFT)
        {
            nextCordinateX--;
        }
        else if (directionEnum == DirectionEnum.UP)
        {
            nextCordinateY--;
        }

        return new(nextCordinateY, nextCordinateX);
    }

    public static bool IsValidNonBlockingDirection(int cordinateY, int cordinateX, int height, int width)
    {
        return cordinateY > 1 && cordinateY < height - 1 && cordinateX > 1 && cordinateX < width;
    }
}