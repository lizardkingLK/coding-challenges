using tetris.Core.Abstractions;

namespace tetris.Core.Views.Controller.Interactive;

public class MapSizeInteractionView : IView
{
    public string Message => """
    
    Select map size from below.
    """;

    public string Verbose => """
    0  - Normal (Normal Map Size)
    1  - Scaled (Doubled Map Size)
    """;

    public string Data => string.Empty;

    public string Error => """
    error. invalid input was given.
    """;
}