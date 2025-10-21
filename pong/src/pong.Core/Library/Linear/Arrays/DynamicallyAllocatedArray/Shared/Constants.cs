namespace pong.Core.Library.Linear.Arrays.DynamicallyAllocatedArray.Shared;

public static class Constants
{
    public const int INITIAL_CAPACITY = 2;
    public const float SHRINK_FACTOR = .3f;
    public const float GROWTH_FACTOR = .7f;
    public const string ErrorInvalidCapacity = "error. cannot create. invalid capacity";
    public const string ErrorListEmpty = "error. cannot remove. list is empty";
    public const string ErrorInvalidIndex = "error. cannot insert. invalid index";
    public const string ErrorItemDoesNotExist = "error. cannot remove. item does not exist";
}