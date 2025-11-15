using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;

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

    protected override HashMap<int, Position[][]> Borders => throw new NotImplementedException();

    public TetrominoO()
    {
        Variants = new(
            (0, _variantA));

        Color = ConsoleColor.Yellow;
        Size = Variants.Count();
        Side = 2;
    }
}