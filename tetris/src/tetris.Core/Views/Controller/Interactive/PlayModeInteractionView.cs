using tetris.Core.Abstractions;

namespace tetris.Core.Views.Controller.Interactive;

public class PlayModeInteractionView : IView
{
    public string Message => """
    
    Select play mode from below.
    """;

    public string Verbose => """
    0  - Drop (Tetrominoes drops down with input)
    1  - Float (Tetrominoes waits for the input)
    """;

    public string Data => string.Empty;

    public string Error => """
    error. invalid input was given.
    """;
}