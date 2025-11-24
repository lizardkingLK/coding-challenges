using tetris.Core.Enums.Commands;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Assets;
using tetris.Core.State.Assets.Tetrominoes;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Shared;

public static class Values
{
    internal static readonly DynamicallyAllocatedArray<Tetromino> tetrominoes
    = new(
        new TetrominoI(),
        // new TetrominoJ(),
        new TetrominoL()//,
                        // new TetrominoO(),
                        // new TetrominoS(),
                        // new TetrominoT(),
                        // new TetrominoZ()
        );

    internal static readonly HashMap<ConsoleKey, CommandTypeEnum> keyAndInputs = new(
        (ConsoleKey.Escape, CommandTypeEnum.PauseGame),
        (ConsoleKey.Z, CommandTypeEnum.RotateIt),
        (ConsoleKey.LeftArrow, CommandTypeEnum.GoLeft),
        (ConsoleKey.RightArrow, CommandTypeEnum.GoRight),
        (ConsoleKey.DownArrow, CommandTypeEnum.GoDown),
        (ConsoleKey.Spacebar, CommandTypeEnum.SlamDown));

    internal static readonly Position[,] scaledBlockPositions =
    {
        { new(0, 0), new(0, 1) },
        { new(1, 0), new(1, 1) },
    };
}