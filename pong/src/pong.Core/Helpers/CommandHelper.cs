using pong.Core.Abstractions;
using pong.Core.Commands.Controllers;
using pong.Core.State.Common;
using pong.Core.State.Misc;
using static pong.Core.Enums.CommandTypeEnum;

namespace pong.Core.Helpers;

public static class CommandHelper
{
    public static Result<Command> GetCommand(Arguments arguments)
    {
        return arguments.CommandType switch
        {
            HelpCommand => new(new HelpController(arguments)),
            InteractiveCommand => new(new InteractiveController(arguments)),
            _ => new(new GameController(arguments)),
        };
    }

    public static void Execute(Command command) => command.Execute();
}