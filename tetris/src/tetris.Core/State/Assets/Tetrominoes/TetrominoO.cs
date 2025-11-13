using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoO : Tetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { true, true, },
        { true, true, },
    };

    public override int Size { get; }
    public override int Width { get; }
    public override int Height { get; }
    protected override HashMap<int, bool[,]> Variants { get; }
    protected override ConsoleColor Color { get; }

    public TetrominoO()
    {
        Variants = new(
            (0, _variantA));

        Color = ConsoleColor.Yellow;
        Size = Variants.Count();
        Height = 2;
        Width = 2;
    }
}