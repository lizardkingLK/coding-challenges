using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.Handlers;

public class PlayerManager(IManager gameManager)
{
    private readonly IManager _gameManager = gameManager;
    private readonly HashMap<ConsoleKey, CommandTypeEnum> _keyAndInputs
    = new(
        (ConsoleKey.Escape, CommandTypeEnum.PauseGame),
        (ConsoleKey.Z, CommandTypeEnum.RotateIt),
        (ConsoleKey.LeftArrow, CommandTypeEnum.GoLeft),
        (ConsoleKey.RightArrow, CommandTypeEnum.GoRight),
        (ConsoleKey.DownArrow, CommandTypeEnum.GoDown),
        (ConsoleKey.Spacebar, CommandTypeEnum.SlamDown));

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
                out CommandTypeEnum commandType))
            {
                _gameManager.Input(commandType);
            }
        }
    }
}