namespace tetris.Core.Shared;

public static class Constants
{
    public const int HeightNormal = 22;
    public const int WidthNormal = 12;
    public const int HeightScaled = 44;
    public const int WidthScaled = 24;
    public const int BlockMoveInterval = 200;
    public const int BlockClearTimeout = 20;
    public const int TimeoutInterval = 1000;
    public const char SymbolSpaceBlock = ' ';
    public const char SymbolWallBlock = '@';
    public const char SymbolTetrominoBlock = '#';
    public const char SymbolNewLine = '\n';
    public const string FileName = "tetris.txt";
    public const ConsoleColor ColorWall = ConsoleColor.Gray;
    public const ConsoleColor ColorSpace = ConsoleColor.White;
}