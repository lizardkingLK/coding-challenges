using pong.Core.Abstractions;
using pong.Core.State.Common;
using pong.Core.State.Misc;
using static pong.Core.Helpers.CommandHelper;
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

        Result<Command> commandResult = GetCommand(validatedResult.Data!);
        if (commandResult.Errors != null)
        {
            HandleError(commandResult.Errors);
        }

        Execute(commandResult.Data!);
    }
}