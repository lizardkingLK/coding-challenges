using tetris.Core.Abstractions;
using tetris.Core.State;

namespace tetris.Core.Controllers;

public class GameController(Arguments arguments) : IController
{
    private Arguments _arguments = arguments;

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
