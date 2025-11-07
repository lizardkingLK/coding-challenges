using pong.Core.Abstractions;

namespace pong.Core.Views;

public class DifficultyView : View
{
    public override string Message => """
    
    Select difficulty level from below.
    (Easy = -1, Medium = 0, Hard = 1)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. invalid input was given.
    """;
}