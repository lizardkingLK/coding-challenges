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

    public static readonly
    Result<(bool, int, int, OutputTypeEnum)> ErrorInvalidArguments
    = new((false, -1, -1, default), ERROR_INVALID_ARGUMENTS);
}