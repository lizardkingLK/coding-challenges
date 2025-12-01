using tetris.Core.Abstractions;

namespace tetris.Core.Views.Controller.Interactive;

public class GameModeInteractionView : IView
{
    public string Message => """
    
    Select game mode from below.
    """;

    public string Verbose => """
    0  - Classic (Score multiplier on difficulty)
    1  - Timed (Score multiplier on difficulty and time elapsed)
    """;

    public string Data => string.Empty;

    public string Error => """
    error. invalid input was given.
    """;
}