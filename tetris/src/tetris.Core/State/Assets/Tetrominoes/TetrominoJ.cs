using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoJ : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, false, true },
        { true, true, true },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, false },
        { false, true, false },
        { false, true, true },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { true, true, true },
        { true, false, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { true, true, false },
        { false, true, false },
        { false, true, false },
    };

    public override int Size { get; }
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    protected override HashMap<int, Position[][]> Borders { get; }

    public TetrominoJ()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Borders = new(
            (0, new Position[3][]
            {
                [new(0, 2), new(1, 2)],
                [new(1, 0), new(1, 1), new(1, 2)],
                [new(1, 0)]
            }),
            (1, new Position[3][]
            {
                [new(2, 2)],
                [new(2, 1), new(2, 2)],
                [new(0, 1), new(1, 1), new(2, 1)]
            }),
            (2, new Position[3][]
            {
                [new(1, 2)],
                [new(2, 0)],
                [new(1, 0), new(2, 0)],
            }),
            (3, new Position[3][]
            {
                [new(0, 1), new(1, 1), new(2, 1)],
                [new(2, 1)],
                [new(0, 0)],
            }));

        Color = ConsoleColor.DarkYellow;
        Size = Variants.Count();
        Side = 3;
    }
}