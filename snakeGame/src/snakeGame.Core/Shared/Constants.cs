namespace snakeGame.Core.Shared;

public static class Constants
{
    public const int MaxWidth = 250;
    public const int MaxHeight = 25;
    public const string FlagHeight = "--height";
    public const string FlagHeightPrefixed = "-h";
    public const string FlagWidth = "--width";
    public const string FlagWidthPrefixed = "-w";
    public const char CharWallBlock = '#';
    public const int PlayerInitialLength = 2;
    public const char CharSpaceBlock = ' ';
    public const char CharPlayerHead = '@';
    public const char CharEnemy = 'P';
    public const char CharPlayerBody = 'o';
    public const string ERROR_INVALID_ARGUMENTS = "error. invalid arguments given";
    public const string ERROR_INVALID_TERMINAL = "error. invalid terminal environment";
}