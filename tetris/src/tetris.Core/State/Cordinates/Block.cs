using static tetris.Core.Shared.Constants;

namespace tetris.Core.State.Cordinates;

public record struct Block
{
    public int Y { get; set; }
    public int X { get; set; }
    public char Symbol { get; set; } = SymbolSpaceBlock;
    public ConsoleColor Color { get; set; } = ColorSpace;

    public Block(int y, int x) => (Y, X) = (y, x);
    public Block(Position position) => (Y, X) = position;
    public Block(Position position, Block block) : this(position)
    => (Symbol, Color) = (block.Symbol, block.Color);

    public readonly Position Position => new(Y, X);

    public readonly void Deconstruct(
        out Position position,
        out char symbol,
        out ConsoleColor color)
    => (position, symbol, color) = (new Position(Y, X), Symbol, Color);
}