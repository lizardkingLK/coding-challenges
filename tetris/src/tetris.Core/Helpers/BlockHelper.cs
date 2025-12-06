using tetris.Core.State.Cordinates;

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
}