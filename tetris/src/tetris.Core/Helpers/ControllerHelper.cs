using tetris.Core.Abstractions;
using tetris.Core.Controllers;
using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Helpers;

public static class ControllerHelper
{
    public static Result<IController> GetController(Arguments arguments)
    {
        return arguments.ControllerType switch
        {
            ControllerTypeEnum.Help => new(new HelpController()),
            ControllerTypeEnum.Game => new(new GameController(arguments)),
            ControllerTypeEnum.Interactive => new(new InteractiveController()),
            _ => new(null, "error. cannot get controller. invalid type"),
        };
    }
}