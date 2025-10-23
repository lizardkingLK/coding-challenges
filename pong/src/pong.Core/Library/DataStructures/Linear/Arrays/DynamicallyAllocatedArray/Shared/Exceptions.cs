using static pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared.Constants;

namespace pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared;

public static class Exceptions
{
    public static readonly Exception InvalidListSizeException = new ApplicationException(ErrorInvalidCapacity);
    public static readonly Exception ListEmptyException = new ApplicationException(ErrorListEmpty);
    public static readonly Exception InvalidIndexException = new ApplicationException(ErrorInvalidIndex);
    public static readonly Exception ItemDoesNotExistException = new ApplicationException(ErrorItemDoesNotExist);
}