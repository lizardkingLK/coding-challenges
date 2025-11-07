using pong.Core.Abstractions;

namespace pong.Core.Views;

public class PlayerSideView : View
{
    public override string Message => """
    
    Select player side from below.
    (Left = 0, Right = 1)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. invalid input was given.
    """;
}