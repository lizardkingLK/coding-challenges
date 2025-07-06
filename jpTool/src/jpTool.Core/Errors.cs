namespace jpTool.Core;

using static Constants;

internal static class Errors
{
    internal static Result<bool> ErrorFileIsEmpty = new(false, ERROR_FILE_IS_EMPTY);
    internal static Result<bool> ErrorInvalidJsonFound = new(false, ERROR_INVALID_JSON_FOUND);
    internal static Result<(bool, int)> ErrorInvalidJsonFoundWithIndex = new((false, -1), ERROR_INVALID_JSON_FOUND);
}