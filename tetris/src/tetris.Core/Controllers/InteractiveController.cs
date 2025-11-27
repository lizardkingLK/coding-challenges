using tetris.Core.Abstractions;
using tetris.Core.Shared;

namespace tetris.Core.Controllers;

public class InteractiveController : IController
{
    public Result<bool> Execute()
    {
        Console.WriteLine(nameof(InteractiveController));

        return new(true);
    }
}
