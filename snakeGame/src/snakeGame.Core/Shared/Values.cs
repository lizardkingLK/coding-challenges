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

    public static readonly string[] outputFlags =
    [
        FlagOutput,
        FlagOutputPrefixed,
    ];

    public static readonly string[] gameModeFlags =
    [
        FlagGameMode,
        FlagGameModePrefixed,
    ];

    public static readonly
    Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> ErrorInvalidArguments
    = new((false, -1, -1, default, default), ERROR_INVALID_ARGUMENTS);
}