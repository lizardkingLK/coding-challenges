using tetris.Core.Enums.Commands;
using tetris.Core.Handlers;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.Players;

public class Player(GameManager gameManager)
{
    private readonly GameManager _gameManager = gameManager;
    private readonly HashMap<ConsoleKey, InputTypeEnum> _keyAndInputs = new(
        (ConsoleKey.Escape, InputTypeEnum.PauseGame),
        (ConsoleKey.Z, InputTypeEnum.RotateIt),
        (ConsoleKey.LeftArrow, InputTypeEnum.GoLeft),
        (ConsoleKey.RightArrow, InputTypeEnum.GoRight),
        (ConsoleKey.DownArrow, InputTypeEnum.GoDown),
        (ConsoleKey.Spacebar, InputTypeEnum.SlamDown));

    public void Input()
    {
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            if (_keyAndInputs.TryGetValue(
                Console.ReadKey().Key,
                out InputTypeEnum inputType))
            {
                _gameManager.Input(inputType);
            }
        }
    }
}