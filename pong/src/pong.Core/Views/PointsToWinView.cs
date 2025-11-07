using pong.Core.Abstractions;

namespace pong.Core.Views;

public class PointsToWinView : View
{
    public override string Message => """
    
    Enter points to win below.
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. invalid input was given. please enter value greater than zero.
    """;
}