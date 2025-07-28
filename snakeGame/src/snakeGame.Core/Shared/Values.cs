namespace snakeGame.Core.Shared;

using static Constants;

public readonly struct Values
{
    public static readonly Result<bool> ErrorInvalidArguments = new(false, ERROR_INVALID_ARGUMENTS);
}