using tetris.Core.Abstractions;
using tetris.Core.Handlers;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Controllers;

public class GameController(Arguments arguments) : IController
{
    private GameManager _gameManager = new(arguments);

    public Result<bool> Execute()
    {
        // TODO: loop interactive controller and creates a game when gamemanager responds if new game
        return new(true);
    }
}
