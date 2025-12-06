namespace pong.Core.Helpers;

public static class EnumHelper
{
    public static bool TryNextEnum<T>(int? value, out T? nextValue) where T : notnull, Enum
    {
        nextValue = default;

        if (value == null)
        {
            return false;
        }

        Type type = typeof(T);
        int length = Enum.GetNames(type).Length;

        nextValue = (T)Enum.ToObject(type, (value + 1) % length);

        return true;
    }
}