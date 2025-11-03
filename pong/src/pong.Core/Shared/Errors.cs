namespace pong.Core.Shared;

public static class Errors
{
    public static string ErrorInvalidArguments(string argument)
        => new($"error. invalid argument given '{argument}'");
    public static string ErrorDuplicateArguments(string argument)
        => new($"error. duplicate arguments were given '{argument}'");
    public static string ErrorInvalidOutputType()
        => new("error. invalid output type was given");
    public static string ErrorInvalidGameMode()
        => new("error. invalid game mode was given");
    public static string ErrorInvalidDifficulty()
        => new("error. invalid difficulty level was given");
    public static string ErrorInvalidPointsToWin()
        => new("error. invalid points to win was given");
}