using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Commands;
using tetris.Core.Handlers;
using tetris.Core.Handlers.Games;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Controllers;

public class GameController(Arguments arguments) : IController
{
    private readonly CancellationTokenSource _cancellation = new();

    private readonly GameManager _gameManager = arguments.GameMode switch
    {
        GameModeEnum.Classic => new ClassicGame(arguments),
        GameModeEnum.Timed => new TimedGame(arguments),
        GameModeEnum.Points => throw new NotImplementedException(),
        _ => throw new NotImplementedException(
            "error. game mode not implemented"),
    };

    public Result<bool> Execute()
    {
        Task.Run(Input);

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

        Result<bool> scoreResult = _gameManager.Save();
        if (gamePlayResult.Errors != null)
        {
            return gamePlayResult;
        }

        _cancellation.Cancel();

        return new(true);
    }

    public void Input()
    {
        while (!_cancellation.IsCancellationRequested)
        {
            if (Console.KeyAvailable && keyAndInputs.TryGetValue(
                Console.ReadKey(true).Key, out CommandTypeEnum commandType))
            {
                _gameManager.Input(commandType);
            }
        }
    }
}
