using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoS : Tetromino
{
    // TODO: add diagonal moves

    // TODO: add start game countdown sequence 

    // TODO: add persistence local stored highscores preferably sqlite or json

    // TODO: add game over screen

    // TODO: add rebuild_tool.ps1 file and shell file
    // TODO: add README.md file

    // TODO: add help screens
    // TODO: add interactive game creation mode
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, true, true },
        { true, true, false },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, false },
        { false, true, true },
        { false, false, true },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { false, true, true },
        { true, true, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { true, false, false },
        { true, true, false },
        { false, true, false },
    };

    public override int Size { get; }
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override HashMap<int, Position[][]> Borders { get; }

    public TetrominoS()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Borders = new(
            (0, new Position[3][]
            {
                [new(0, 2), new(1, 1)],
                [new(1, 0), new(1, 1), new(0, 2)],
                [new(1, 0), new(0, 1)],
            }),
            (1, new Position[3][]
            {
                [new(1, 2), new(2, 2), new(0, 1)],
                [new(2, 2), new(1, 1)],
                [new(0, 1), new(1, 1), new(2, 2)],
            }),
            (2, new Position[3][]
            {
                [new(1, 2), new(2, 1)],
                [new(2, 0), new(2, 1), new(1, 2)],
                [new(2, 0), new(1, 1)],
            }),
            (3, new Position[3][]
            {
                [new(0, 0), new(1, 1), new(2, 1)],
                [new(2, 1), new(1, 0)],
                [new(0, 0), new(1, 0), new(2, 1)],
            }));

        Color = ConsoleColor.Green;
        Size = Variants.Count();
        Side = 3;
    }
}