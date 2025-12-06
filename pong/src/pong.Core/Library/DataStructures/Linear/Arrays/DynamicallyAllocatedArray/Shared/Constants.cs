namespace pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared;

public static class Constants
{
    public const int INITIAL_CAPACITY = 2;
    public const float SHRINK_FACTOR = .3f;
    public const float GROWTH_FACTOR = .7f;
    public const string ErrorInvalidCapacity = "error. cannot create. invalid capacity";
    public const string ErrorListEmpty = "error. cannot modify. list is empty";
    public const string ErrorInvalidIndex = "error. cannot create. invalid index";
    public const string ErrorItemDoesNotExist = "error. cannot modify. item does not exist";
}