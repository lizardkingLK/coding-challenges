namespace snakeGame.Core.Shared;

public static class Constants
{
    public const int MaxWidth = 69;
    public const int MaxHeight = 31;
    public const string FlagHeight = "--height";
    public const string FlagHeightPrefixed = "-h";
    public const string FlagWidth = "--width";
    public const string FlagWidthPrefixed = "-w";
    public const string FlagOutput = "--output";
    public const string FlagOutputPrefixed = "-o";
    public const int PlayerInitialLength = 3;
    public const char CharWallBlock = '*';
    public const char CharSpaceBlock = ' ';
    public const char CharPlayerHead = '0';
    public const char CharEnemy = '+';
    public const char CharPlayerBody = 'O';
    public const string INFO_AWAITING_KEY_PRESS = "info. awaiting key press... (^ | k, > | l, < | h, v | j)";
    public const string ERROR_INVALID_ARGUMENTS = "error. invalid arguments given";
    public const string ERROR_INVALID_TERMINAL = "error. invalid terminal environment";
    public const string ERROR_INVALID_KEY_PRESSED = "error. invalid key pressed";
}