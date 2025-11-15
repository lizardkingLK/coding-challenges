using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoL : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { true, false, false },
        { true, true, true },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, true },
        { false, true, false },
        { false, true, false },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { true, true, true },
        { false, false, true },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, false },
        { false, true, false },
        { true, true, false },
    };

    public override int Size { get; }
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    protected override HashMap<int, Position[][]> Borders => throw new NotImplementedException();

    public TetrominoL()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.Blue;
        Size = Variants.Count();
        Side = 3;
    }
}