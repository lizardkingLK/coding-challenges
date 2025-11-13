using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

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
        { true, true, true },
        { true, false, false },
        { false, false, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, true },
        { false, false, true },
        { false, false, true },
    };

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override ConsoleColor Color { get; }

    public TetrominoJ()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.DarkYellow;
        Size = Variants.Count();
        Height = 3;
        Width = 3;
    }
}