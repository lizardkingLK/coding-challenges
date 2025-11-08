using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State;
using static tetris.Core.Helpers.CommandHelper;
using static tetris.Core.Helpers.ValidationHelper;

namespace tetris.Core;

public static class Tetris
{
    public static void Play(string[] args)
    {
        Result<Arguments> validatorResult = GetValidated(args);
        if (validatorResult.Errors != null)
        {
            HandleError(validatorResult.Errors!);
        }

    }
}
