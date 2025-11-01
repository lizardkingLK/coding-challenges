namespace pong.Core.State.Game;

public record Block(int Top, int Left, char Symbol, ConsoleColor Color = ConsoleColor.White)
{
    public int Top { get; set; } = Top;
    public int Left { get; set; } = Left;
    public char Symbol { get; set; } = Symbol;
    public ConsoleColor Color { get; set; } = Color;
}