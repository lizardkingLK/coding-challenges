using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Assets.Tetrominoes;

public record TetrominoS : Tetromino
{
    // TODO: add rebuild_tool.ps1 file and shell file
    // TODO: add README.md file

    // TODO: add help screens
    // TODO: add interactive game creation mode
    private readonly bool[,] _variantA = new bool[,]
    {
        { false, true, true },
        { true, true, false },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, false },
        { false, true, true },
        { false, false, true },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { false, true, true },
        { true, true, false },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { true, false, false },
        { true, true, false },
        { false, true, false },
    };

    public override int Size { get; }
    public override int Side { get; }
    public override ConsoleColor Color { get; }
    protected override HashMap<int, bool[,]> Variants { get; }

    public TetrominoS()
    {
        Variants = new(
            (0, _variantA),
            (1, _variantB),
            (2, _variantC),
            (3, _variantD));

        Color = ConsoleColor.Green;
        Size = Variants.Count();
        Side = 3;
    }
}