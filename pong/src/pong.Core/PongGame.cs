using pong.Core.Abstractions;
using pong.Core.State;
using pong.Core.State.Game;
using static pong.Core.Helpers.CommandHelper;
using static pong.Core.Helpers.ConsoleHelper;
using static pong.Core.Helpers.ValidatorHelper;

namespace pong.Core;

public static class PongGame
{
    public static void Initiate(string[] arguments)
    {
        Result<Arguments> validatorResult = GetValidator(arguments);
        if (validatorResult.Errors != null)
        {
            HandleError(validatorResult.Errors);
        }

        Result<Command> commandResult = GetCommand(validatorResult.Data!);
        if (commandResult.Errors != null)
        {
            HandleError(commandResult.Errors);
        }

        Execute(commandResult.Data!);
    }
}