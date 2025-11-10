namespace tetris.Core.State.Cordinates;

public record struct Block(
    int Y,
    int X,
    char Symbol,
    ConsoleColor Color = ConsoleColor.White)
{
    public int Y { get; set; } = Y;
    public int X { get; set; } = X;

    public char Symbol { get; set; } = Symbol;
    public ConsoleColor Color { get; set; } = Color;

    public readonly void Destruct(out int y, out int x)
    => (y, x) = (Y, X);    
    public readonly void Destruct(out char symbol, out ConsoleColor color)
    => (symbol, color) = (Symbol, Color);
}