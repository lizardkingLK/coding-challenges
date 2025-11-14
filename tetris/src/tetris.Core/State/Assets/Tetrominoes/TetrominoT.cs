using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoT : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, true, false },
        { true, true, true },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, false },
        { false, true, true },
        { false, true, false },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { true, true, true },
        { false, true, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, false },
        { true, true, false },
        { false, true, false },
    };

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    public TetrominoT()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.Magenta;
        Size = Variants.Count();
        Height = 3;
        Width = 3;
    }
}