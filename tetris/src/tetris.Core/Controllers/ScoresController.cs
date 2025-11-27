using tetris.Core.Abstractions;
using tetris.Core.Shared;

namespace tetris.Core.Controllers;

public class ScoresController : IController
{
    public Result<bool> Execute()
    {
        Console.WriteLine(nameof(ScoresController));

        return new(true);
    }
}