using pong.Core.Enums;
using static pong.Core.Utilities.ArithmeticUtility;

namespace pong.Core.Helpers;

public static class DistanceHelper
{
    public static VerticalDirectionEnum GetShorterDistance(int distanceA, int distanceB, out int distance)
    {
        VerticalDirectionEnum verticalDirection;
        
        int absoluteDistanceA = GetAbsoluteValue(distanceA);
        int absoluteDistanceB = GetAbsoluteValue(distanceB);
        if (absoluteDistanceA <= absoluteDistanceB)
        {
            distance = absoluteDistanceA;
            verticalDirection = VerticalDirectionEnum.Up;
        }
        else
        {
            distance = absoluteDistanceB;
            verticalDirection = VerticalDirectionEnum.Down;
        }

        return verticalDirection;
    }
}