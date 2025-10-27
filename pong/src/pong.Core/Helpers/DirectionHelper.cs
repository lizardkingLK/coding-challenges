using pong.Core.Enums;

namespace pong.Core.Helpers;

public static class DirectionHelper
{
    public static void InitializeDirections(
        (int, int) position,
        (int, int) dimensions,
        ref VerticalDirectionEnum yDirection,
        ref HorizontalDirectionEnum _xDirection)
    {
        (int y, int x) = position;
        (int height, int width) = dimensions;
        if (y < height / 2 && x < width / 2)
        {
            yDirection = VerticalDirectionEnum.Down;
            _xDirection = HorizontalDirectionEnum.Right;
        }
        else if (y < height / 2 && x > width / 2)
        {
            yDirection = VerticalDirectionEnum.Down;
            _xDirection = HorizontalDirectionEnum.Left;
        }
        else if (y > height / 2 && x < width / 2)
        {
            yDirection = VerticalDirectionEnum.Up;
            _xDirection = HorizontalDirectionEnum.Right;
        }
        else if (y > height / 2 && x > width / 2)
        {
            yDirection = VerticalDirectionEnum.Up;
            _xDirection = HorizontalDirectionEnum.Left;
        }
    }
}