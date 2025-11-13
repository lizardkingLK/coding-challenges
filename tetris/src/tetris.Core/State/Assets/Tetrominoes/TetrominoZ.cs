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

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override ConsoleColor Color { get; }

    public TetrominoZ()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB));

        Color = ConsoleColor.Red;
        Size = Variants.Count();
        Height = 3;
        Width = 3;
    }
}