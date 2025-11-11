using static tetris.Core.Shared.Constants;

namespace tetris.Core.State.Cordinates;

public record struct Block(
    int Y,
    int X)
{
    public int Y { get; set; } = Y;
    public int X { get; set; } = X;

    public char Symbol { get; set; } = SymbolSpaceBlock;

    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public bool IsFree { get; set; } = true;

    public readonly void Destruct(out int y, out int x)
    => (y, x) = (Y, X);
    public readonly void Destruct(out char symbol, out ConsoleColor color)
    => (symbol, color) = (Symbol, Color);

    public readonly void Deconstruct(
        out Position position,
        out char symbol,
        out ConsoleColor color,
        out bool isFree)
    => (position, symbol, color, isFree) = (new Position(Y, X), Symbol, Color, IsFree);
}