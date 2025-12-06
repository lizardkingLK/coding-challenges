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
            ControllerTypeEnum.Game => new(new GameController(arguments)),
            ControllerTypeEnum.Help => new(new HelpController()),
            ControllerTypeEnum.Scores => new(new ScoresController()),
            ControllerTypeEnum.Interactive => new(new InteractiveController()),
            _ => new(null, "error. cannot get controller. invalid type"),
        };
    }
}