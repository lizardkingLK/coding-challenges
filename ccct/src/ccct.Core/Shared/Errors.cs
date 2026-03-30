namespace ccct.Core.Shared;

public static class Errors
{
    public const string ErrorDynamicallyAllocatedArrayInvalidCapacity = "error. invalid capacity value was given";
    public const string ErrorHashMapInvalidCapacity = "error. invalid capacity value was given";
    public const string ErrorHashMapKeyAlreadyExist = "error. cannot add. key already exist";
    public const string ErrorDoublyLinkedListCannotRemoveFromFront = "error. cannot remove front front. list is empty";
    public const string ErrorDoublyLinkedListCannotRemoveFromRear = "error. cannot remove front rear. list is empty";
}