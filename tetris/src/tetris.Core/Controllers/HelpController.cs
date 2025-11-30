using tetris.Core.Abstractions;
using tetris.Core.Shared;

namespace tetris.Core.Controllers;

public class HelpController : IController
{
    public Result<bool> Execute()
    {
        Console.WriteLine(nameof(HelpController));

        return new(true);
    }
}
