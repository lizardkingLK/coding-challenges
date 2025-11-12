using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoI : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { true, true, true, true },
        { false, false, false, false },
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
    public override int Width { get; }
    protected override int Height { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override ConsoleColor Color { get; }

    public TetrominoI()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.Cyan;
        Size = Variants.Count();
        Height = 4;
        Width = 4;
    }
}