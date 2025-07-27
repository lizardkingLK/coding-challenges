namespace snakeGame.Core.Shared;

public static class Constants
{
    public const int MaxWidth = 40;
    public const int MinWidth = 10;
    public const int MaxHeight = 20;
    public const int MinHeight = 10;
    public const int ScorePerMeal = 100;
    public const int StepInterval = 500;
    public const string FlagHeight = "--height";
    public const string FlagHeightPrefixed = "-h";
    public const string FlagWidth = "--width";
    public const string FlagWidthPrefixed = "-w";
    public const string FlagOutput = "--output";
    public const string FlagOutputPrefixed = "-o";
    public const string FlagGameMode = "--game-mode";
    public const string FlagGameModePrefixed = "-gm";
    public const int PlayerInitialLength = 3;
    public const char CharWallBlock = '&';
    public const char CharSpaceBlock = ' ';
    public const char CharPlayerHead = '0';
    public const char CharEnemy = '+';
    public const char CharPlayerBody = 'O';
    public const char CharNewLine = '\n';
    public const string DefaultFileName = "textFileOutput.txt";
    public const string ApiUrl = "http://localhost:5000";
    public const string SUCCESS_GAME_COMPLETE = "Game Complete. Your Score is {0}";
    public const string ERROR_INVALID_ARGUMENTS = "error. invalid arguments given";
    public const string ERROR_INVALID_TERMINAL = "error. invalid terminal environment";
    public const string ERROR_INVALID_KEY_PRESSED = "error. invalid key pressed";
    public const string INFO_AWAITING_KEY_PRESS = "info. awaiting key press... (^ | k, > | l, < | h, v | j)";
    public const string INFO_GAME_OVER = "Game Over. Your Score is {0}";
    public const string INFO_HELP = @"
Options

width       = [-[w|-width] [10-40]]
height      = [-[h|-height] [10-20]]
game-mode   = [-[gm|-game-mode] [0-1]]
output      = [-[o|-output] [0-4]]

Game Modes

0 - Automatic
1 - Manual

Output Types

0 - Default Console uses terminal
1 - Stream Writer Console Output uses terminal
2 - String Builder Console Output uses terminal
3 - Text File Console Output use VSCode editor
4 - Web Output uses Web Browser";
}