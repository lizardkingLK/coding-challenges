namespace snakeGame.Core.Shared;

public static class Constants
{
    public const string FlagHeight = "--height";
    public const string FlagHeightPrefixed = "-h";
    public const string FlagWidth = "--width";
    public const string FlagWidthPrefixed = "-w";
    public const char CharWallBlock = '#';
    public const char CharSpaceBlock = ' ';
    public const char CharPlayerHead = '@';
    public const char CharEnemy = 'ඞ';
    public const char CharPlayerBody = 'o';
    public const ConsoleColor WallColor = ConsoleColor.DarkYellow;
    public const ConsoleColor EnemyColor = ConsoleColor.Red;
    public const ConsoleColor PlayerColor = ConsoleColor.Green;
    public const string ERROR_INVALID_ARGUMENTS = "error. invalid arguments given";
    public const string ERROR_INVALID_TERMINAL = "error. invalid terminal environment";
}