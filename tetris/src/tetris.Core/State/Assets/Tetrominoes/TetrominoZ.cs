using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoZ : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { true, true, false },
        { false, true, true },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, false, true },
        { false, true, true },
        { false, true, false },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { true, true, false },
        { false, true, true },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, false },
        { true, true, false },
        { true, false, false },
    };

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    public TetrominoZ()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.Red;
        Size = Variants.Count();
        Height = 3;
        Width = 3;
    }
}