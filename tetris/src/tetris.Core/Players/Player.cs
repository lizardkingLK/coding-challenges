using tetris.Core.Enums.Commands;
using tetris.Core.Handlers;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Players;

public class Player(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;

    public void Input()
    {
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            if (keyAndInputs.TryGetValue(
                Console.ReadKey(true).Key,
                out CommandTypeEnum commandType))
            {
                _gameManager.Input(commandType);
            }
        }
    }
}