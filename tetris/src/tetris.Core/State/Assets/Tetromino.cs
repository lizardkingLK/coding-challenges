using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Assets.Tetrominos;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.State.Assets;

public abstract record Tetromino
{
    public static readonly DynamicallyAllocatedArray<Tetromino> allTetrominoes
    = new(new TetrominoL());

    public abstract int Size { get; }
    public abstract int Width { get; }
    protected abstract int Height { get; }
    protected abstract HashMap<int, bool[,]> Variants { get; }
    protected abstract ConsoleColor Color { get; }

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
                color = ConsoleColor.White;
            }

            transformed[y, x] = new Block(y, x)
            {
                Symbol = symbol,
                Color = color,
            };
        }

        return transformed;
    }

    public static void Update(Block[,] tetrominoMap, Block[,] fullMap, Position center)
    {
        Position spawn;
        int y;
        int x;
        foreach (Block block in tetrominoMap)
        {
            ((y, x), _, _) = block;
            spawn = fullMap![center.Y, center.X].Position + block.Position;
            tetrominoMap[center.Y + block.Y, center.X + block.X] = new Block(spawn, block);
        }
    }
}