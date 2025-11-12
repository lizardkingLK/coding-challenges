using tetris.Core.Abstractions;
using tetris.Core.Handlers;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Controllers;

public class GameController(Arguments arguments) : IController
{
    private readonly GameManager _gameManager = new(arguments);

    public Result<bool> Execute()
    {
        Result<bool> gameCreateResult = _gameManager.New();
        if (gameCreateResult.Errors != null)
        {
            return new(false, gameCreateResult.Errors);
        }

        Result<bool> gamePlayResult = _gameManager.Play();
        if (gamePlayResult.Errors != null)
        {
            return new(false, gamePlayResult.Errors);
        }

        return new(true);
    }
}
