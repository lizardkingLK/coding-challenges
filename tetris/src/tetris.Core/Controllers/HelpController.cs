using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.Views.Controller.Help;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Controllers;

public class HelpController : IController
{
    private readonly HelpView _helpView = new();

    public Result<bool> Execute()
    {
        WriteLine(_helpView.Message, ColorSuccess);

        return new(true);
    }
}
