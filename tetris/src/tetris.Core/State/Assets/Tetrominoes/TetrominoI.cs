using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoI : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, true, false, false },
        { false, true, false, false },
        { false, true, false, false },
        { false, true, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, false, false, false },
        { true, true, true, true },
        { false, false, false, false },
        { false, false, false, false },
    };

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    public TetrominoI()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB));

        Color = ConsoleColor.Cyan;
        Size = Variants.Count();
        Height = 4;
        Width = 4;
    }
}