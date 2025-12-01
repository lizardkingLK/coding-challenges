using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.Views;
using static tetris.Core.Helpers.ConsoleHelper;

namespace tetris.Core.Controllers;

public class HelpController : IController
{
    private readonly HelpView _helpView = new();

    public Result<bool> Execute()
    {
        Write(_helpView.Message, ConsoleColor.Cyan);

        return new(true);
    }
}
