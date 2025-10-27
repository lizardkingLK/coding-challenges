namespace pong.Core.State.Game;

public record struct Block(int Top, int Left, char Symbol, ConsoleColor Color = ConsoleColor.White);