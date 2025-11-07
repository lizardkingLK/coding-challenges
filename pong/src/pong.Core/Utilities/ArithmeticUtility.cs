namespace pong.Core.Utilities;

public static class ArithmeticUtility
{
    public static int GetAbsoluteValue(int value)
    {
        int mask = value >> 31;

        return (value ^ mask) - mask;
    }
}