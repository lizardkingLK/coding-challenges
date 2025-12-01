using tetris.Core.Abstractions;

namespace tetris.Core.Views.Controller.Interactive;

public class DifficultyInteractionView : IView
{
    public string Message => """
    
    Select difficulty level from below.
    """;

    public string Verbose => """
    -1  - Easy
    0   - Medium
    1   - Hard
    """;

    public string Data => string.Empty;

    public string Error => """
    error. invalid input was given.
    """;
}