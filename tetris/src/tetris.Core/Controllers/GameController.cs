using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Handlers;
using tetris.Core.Handlers.Games;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Controllers;

public class GameController(Arguments arguments) : IController
{
    private readonly GameManager _gameManager = arguments.GameMode switch
    {
        GameModeEnum.Classic => new ClassicGameManager(arguments),
        GameModeEnum.Timed => throw new NotImplementedException(),
        GameModeEnum.Points => throw new NotImplementedException(),
        _ => throw new NotImplementedException(
            "error. game mode not implemented"),
    };

    public Result<bool> Execute()
    {
        Result<bool> validateResult = _gameManager.Validate();
        if (validateResult.Errors != null)
        {
            return validateResult;
        }

        Result<bool> newGameResult = _gameManager.New();
        if (newGameResult.Errors != null)
        {
            return newGameResult;
        }

        Result<bool> gamePlayResult = _gameManager.Play();
        if (gamePlayResult.Errors != null)
        {
            return gamePlayResult;
        }

        return new(true);
    }
}
