using pong.Core.Enums;

namespace pong.Core.Helpers;

public static class CordinatesHelper
{
    public static void InitializeBallPosition(in (int, int) dimensions, out int y, out int x)
    {
        (int height, int width) = dimensions;
        y = Random.Shared.Next(1, height - 1);
        x = new int[2]
        {
            Random.Shared.Next(1, width / 2 - 1),
            Random.Shared.Next(width / 2 + 1, width - 2)
        }[Random.Shared.Next(2)];
    }

    public static void GetNextBallPosition(
        in (int, int) dimensions,
        in (int, int) cordinates,
        in object[] directions,
        out (int, int) nextCordinates)
    {
        (int y, int x) = cordinates;
        GetNextCordinates(directions, ref y, ref x);
        SetNextCordinates(dimensions, directions, ref y, ref x);

        nextCordinates = (y, x);
    }

    private static void SetNextCordinates(
        (int, int) dimensions,
        in object[] directions,
        ref int y,
        ref int x)
    {
        (int height, int width) = dimensions;
        VerticalDirectionEnum yDirection = (VerticalDirectionEnum)directions[0];
        HorizontalDirectionEnum xDirection = (HorizontalDirectionEnum)directions[1];
        if (y >= height - 1)
        {
            y -= 2;
            yDirection = VerticalDirectionEnum.Up;
        }
        else if (y < 1)
        {
            y += 2;
            yDirection = VerticalDirectionEnum.Down;
        }

        if (x >= width - 1)
        {
            x -= 2;
            xDirection = HorizontalDirectionEnum.Left;
        }
        else if (x < 1)
        {
            x += 2;
            xDirection = HorizontalDirectionEnum.Right;
        }

        directions[0] = yDirection;
        directions[1] = xDirection;
    }

    private static void GetNextCordinates(
        in object[] directions,
        ref int y,
        ref int x)
    {
        VerticalDirectionEnum yDirection = (VerticalDirectionEnum)directions[0];
        HorizontalDirectionEnum xDirection = (HorizontalDirectionEnum)directions[1];
        if (yDirection == VerticalDirectionEnum.Down)
        {
            y++;

        }
        else
        {
            y--;
        }

        if (xDirection == HorizontalDirectionEnum.Right)
        {
            x++;
        }
        else
        {
            x--;
        }
    }
}