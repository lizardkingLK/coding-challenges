using tetris.Core.Abstractions;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;

namespace tetris.Core.State.Tetrominos;

public record TetrominoL : ITetromino
{
    private readonly bool[,] _variantA = new bool[,]
    {
        { true, false, false },
        { true, true, true },
        { false, false, false },
    };

    private readonly bool[,] _variantB = new bool[,]
    {
        { false, true, true },
        { false, true, false },
        { false, true, false },
    };

    private readonly bool[,] _variantC = new bool[,]
    {
        { false, false, false },
        { true, true, true },
        { false, false, true },
    };

    private readonly bool[,] _variantD = new bool[,]
    {
        { false, true, false },
        { false, true, false },
        { true, true, false },
    };

    private readonly HashMap<int, bool[,]> _variants;

    public TetrominoL() => _variants = new(
        (0, _variantA),
        (1, _variantB),
        (2, _variantC),
        (3, _variantD));

    public void Get()
    {
        
    }

    public void Move(DirectionEnum direction)
    {
        throw new NotImplementedException();
    }

    public void Rotate()
    {
        throw new NotImplementedException();
    }
}