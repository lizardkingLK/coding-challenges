using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Handlers.Games;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Controllers;

public class GameController : IController
{
    private readonly HashMap<GameModeEnum, IManager> _gameSelector;

    private readonly IManager _gameManager;

    public GameController(Arguments arguments)
    {
        _gameSelector = new((GameModeEnum.Classic, new ClassicGameManager(arguments)));
        _gameManager = _gameSelector[arguments.GameMode]!;
    }

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
