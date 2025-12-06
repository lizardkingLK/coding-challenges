using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.State.Assets;

public abstract record Tetromino
{
    public abstract int Size { get; }
    public abstract int Side { get; }
    public abstract ConsoleColor Color { get; }
    protected abstract HashMap<int, bool[,]> Variants { get; }
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

    public bool CanSpawn(bool[,] availability, Position origin)
    {
        int y;
        int x;
        int length = Side * Side;
        for (int i = 0; i < length; i++)
        {
            y = i / Side;
            x = i % Side;
            if (!Variants[Index]![y, x])
            {
                continue;
            }

            (y, x) = origin + new Position(y, x);
            if (!availability[y, x])
            {
                return false;
            }
        }

        return true;
    }

    public bool CanMove(
        bool[,] availability,
        (Position, Position) positions)
    {
        (Position position, Position change) = positions;
        int y;
        int x;
        int length = Side * Side;
        for (int i = 0; i < length; i++)
        {
            y = i / Side;
            x = i % Side;
            if (!Variants[Index]![y, x])
            {
                continue;
            }

            (y, x) = position + change + new Position(y, x);
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

            if (variant[y, x]
            && !availability[y + yPosition, x + xPosition])
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