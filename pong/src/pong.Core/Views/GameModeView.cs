using pong.Core.Abstractions;

namespace pong.Core.Views;

public class GameModeView : View
{
    public override string Message => """
    
    Select game mode from below.
    (Automatic = -1, Offline SinglePlayer = 0, Offline MultiPlayer = 1, Online (TBA) = 2)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. invalid input was given.
    """;
}