using pong.Core.Abstractions;
using pong.Core.State.Common;
using pong.Core.State.Misc;
using static pong.Core.Helpers.ControllerHelper;
using static pong.Core.Helpers.GameStateHelper;
using static pong.Core.Helpers.ValidatorHelper;

namespace pong.Core;

public static class PongGame
{
    public static void Initiate(string[] arguments)
    {
        Result<Arguments> validatedResult = GetValidated(arguments);
        if (validatedResult.Errors != null)
        {
            HandleError(validatedResult.Errors);
        }

        Result<Controller> controllerResult = GetController(validatedResult.Data!);
        if (controllerResult.Errors != null)
        {
            HandleError(controllerResult.Errors);
        }

        Execute(controllerResult.Data!);
    }
}