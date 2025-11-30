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
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    public TetrominoO()
    {
        Variants = new(
            (0, _variantA));

        Color = ConsoleColor.Yellow;
        Size = Variants.Count();
        Side = 2;
    }
}