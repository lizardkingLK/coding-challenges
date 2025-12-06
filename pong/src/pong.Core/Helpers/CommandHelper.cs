using pong.Core.Abstractions;
using pong.Core.Controllers;
using pong.Core.Enums;
using pong.Core.State.Common;
using pong.Core.State.Misc;

namespace pong.Core.Helpers;

public static class ControllerHelper
{
    public static Result<Controller> GetController(Arguments arguments)
    => arguments.ControllerType switch
    {
        ControllerTypeEnum.HelpController => new(new HelpController(arguments)),
        ControllerTypeEnum.InteractiveController => new(new InteractiveController(arguments)),
        _ => new(new GameController(arguments)),
    };

    public static void Execute(Controller controller)
    => controller.Execute();
}