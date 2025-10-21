using pong.Core.Abstractions;
using pong.Core.Commands.Controllers;
using pong.Core.State;
using pong.Core.State.Game;
using static pong.Core.Shared.Errors;
using static pong.Core.Enums.CommandTypeEnum;

namespace pong.Core.Helpers;

public static class CommandHelper
{
    public static Result<Command> GetCommand(Arguments arguments)
    {
        return arguments.CommandType switch
        {
            HelpCommand => new(new HelpController(arguments)),
            GameCommand => new(new GameController(arguments)),
            InteractiveCommand => new(new InteractiveController(arguments)),
            _ => new(null, ErrorInvalidCommand()),
        };
    }

    public static void Execute(Command command)
    {

    }
}