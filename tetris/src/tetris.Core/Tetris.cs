using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.CommandHelper;
using static tetris.Core.Helpers.ControllerHelper;
using static tetris.Core.Helpers.ValidationHelper;

namespace tetris.Core;

public static class Tetris
{
    public static void Play(string[] arguments)
    {
        Result<Arguments> validatorResult = GetValidated(arguments);
        if (validatorResult.Errors != null)
        {
            HandleError(validatorResult.Errors!);
        }

        Result<IController> controllerResult = GetController(validatorResult.Data!);
        if (controllerResult.Errors != null)
        {
            HandleError(controllerResult.Errors!);
        }

        Result<bool> executionResult = controllerResult.Data!.Execute();
        if (executionResult.Errors != null)
        {
            HandleError(executionResult.Errors!);
        }
    }
}
