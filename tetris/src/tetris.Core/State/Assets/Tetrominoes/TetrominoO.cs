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

    protected override HashMap<int, Position[][]> Borders { get; }

    public TetrominoO()
    {
        Variants = new(
            (0, _variantA));

        Borders = new(
            (0, new Position[3][]
            {
                [new(0, 1), new(1, 1)],
                [new(1, 0), new(1, 1)],
                [new(0, 0), new(1, 0)],
            }));

        Color = ConsoleColor.Yellow;
        Size = Variants.Count();
        Side = 2;
    }
}