using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Helpers.BlockHelper;

namespace tetris.Core.State.Assets;

public abstract record Tetromino
{
    public abstract int Size { get; }
    public abstract int Width { get; }
    public abstract int Height { get; }
    protected abstract HashMap<int, bool[,]> Variants { get; }
    protected abstract ConsoleColor Color { get; }
    public int Index { get; private set; }

    public Block[,] Get(int index)
    {
        Block[,] transformed = new Block[Height, Width];

        bool[,] variant = Variants[index]!;
        int length = Height * Width;
        int y;
        int x;
        char symbol;
        ConsoleColor color;
        for (int i = 0; i < length; i++)
        {
            y = i / Width;
            x = i % Width;
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

    public Block[,] Get()
    => Get(Random.Shared.Next(Size));

    public Block[,] Next()
    => Get((Index + 1) % Size);
}