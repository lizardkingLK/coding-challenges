using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Helpers;

public static class BlockHelper
{
    public static Block CreateBlock(int y, int x, char symbol, ConsoleColor color) => new(y, x)
    {
        Symbol = symbol,
        Color = color,
    };

    public static Block CreateBlock(Position position, char symbol, ConsoleColor color) => new(position)
    {
        Symbol = symbol,
        Color = color,
    };

    public static Block CreateBlock(Position position) => new(position);

    public static Block CreateBlock(Position position, Block block) => new(position, block);

    public static IEnumerable<Block> CreateScaledBlock(Position Root, Block block)
    {
        (int y, int x) = block.Position;
        Position position;
        for (int i = 0; i < 4; i++)
        {
            position = Root
            + new Position(y * 2, x * 2)
            + scaledBlockPositions[i / 2, i % 2];

            yield return CreateBlock(position, block.Symbol, block.Color);
        }
    }
}