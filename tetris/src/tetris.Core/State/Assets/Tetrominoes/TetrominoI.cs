using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoI : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, false, false, false },
        { true, true, true, true },
        { false, false, false, false },
        { false, false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, false, true, false },
        { false, false, true, false },
        { false, false, true, false },
        { false, false, true, false },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false, false },
        { false, false, false, false },
        { true, true, true, true },
        { false, false, false, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, false, false },
        { false, true, false, false },
        { false, true, false, false },
        { false, true, false, false },
    };

    public override int Size { get; }
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override HashMap<int, Position[][]> Borders { get; }

    public TetrominoI()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Borders = new(
            (0, new Position[3][]
            {
                [new(1, 3)],
                [new(1, 0), new(1, 1), new(1, 2), new(1, 3)],
                [new(1, 0)],
            }),
            (1, new Position[3][]
            {
                [new(0, 2), new(1, 2), new(2, 2), new(3, 2)],
                [new(3, 2)],
                [new(0, 2), new(1, 2), new(2, 2), new(3, 2)],
            }),
            (2, new Position[3][]
            {
                [new(2, 3)],
                [new(2, 0), new(2, 1), new(2, 2), new(2, 3)],
                [new(2, 0)],
            }),
            (3, new Position[3][]
            {
                [new(0, 1), new(1, 1), new(2, 1), new(3, 1)],
                [new(3, 1)],
                [new(0, 1), new(1, 1), new(2, 1), new(3, 1)],
            }));

        Color = ConsoleColor.Cyan;
        Size = Variants.Count();
        Side = 4;
    }
}