using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Helpers.BlockHelper;
using tetris.Core.Enums.Cordinates;

namespace tetris.Core.State.Assets;

public abstract record Tetromino
{
    public abstract int Size { get; }
    public abstract int Side { get; }
    public abstract ConsoleColor Color { get; }
    protected abstract HashMap<int, bool[,]> Variants { get; }
    protected abstract HashMap<int, Position[][]> Borders { get; }
    protected int Index { get; private set; }

    public Block[,] Get(int index)
    {
        Block[,] transformed = new Block[Side, Side];

        bool[,] variant = Variants[index]!;
        int length = Side * Side;
        int y;
        int x;
        char symbol;
        ConsoleColor color;
        for (int i = 0; i < length; i++)
        {
            y = i / Side;
            x = i % Side;
            if (variant[y, x])
            {
                symbol = SymbolTetrominoBlock;
                color = Color;
            }
            else
            {
                symbol = SymbolSpaceBlock;
                color = ColorSpace;
            }

            transformed[y, x] = CreateBlock(y, x, symbol, color);
        }

        Index = index;

        return transformed;
    }

    public bool CanMove(
        bool[,] availability,
        (Position, Position) positions,
        DirectionEnum direction)
    {
        Position[]? border = null;
        if (direction == DirectionEnum.Right)
        {
            border = Borders[Index]![0];
        }
        else if (direction == DirectionEnum.Down)
        {
            border = Borders[Index]![1];
        }
        else if (direction == DirectionEnum.Left)
        {
            border = Borders[Index]![2];
        }

        (Position position, Position change) = positions;
        int y;
        int x;
        int length = border!.Length;
        for (int i = 0; i < length; i++)
        {
            (y, x) = position + border![i] + change;
            if (!availability[y, x])
            {
                return false;
            }
        }

        return true;
    }

    public bool CanRotate(
        bool[,] availability,
        Position position)
    {
        (int yPosition, int xPosition) = position;
        bool[,] variant = Variants[(Index + 1) % Size]!;

        int length = Side * Side;
        int y;
        int x;
        for (int i = 0; i < length; i++)
        {
            y = i / Side;
            x = i % Side;
            if (x + position.X < 0)
            {
                continue;
            }

            if (variant[y, x] && !availability[y + yPosition, x + xPosition])
            {
                return false;
            }
        }

        return true;
    }

    public Block[,] Get()
    => Get(Random.Shared.Next(Size));

    public Block[,] Next()
    => Get((Index + 1) % Size);
}