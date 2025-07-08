namespace snakeGame.Core.Shared;

using snakeGame.Core.Enums;
using static Constants;

public readonly struct Values
{
    public static readonly string[] widthFlags =
    [
        FlagWidth,
        FlagWidthPrefixed,
    ];

    public static readonly string[] heightFlags =
    [
        FlagHeight,
        FlagHeightPrefixed,
    ];

    public static readonly DirectionEnum[] directions = Enum.GetValues<DirectionEnum>();
}