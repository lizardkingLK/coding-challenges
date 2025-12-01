using tetris.Core.Abstractions;

namespace tetris.Core.Views.Game;

public class PauseMenuView : IView
{
    public string Message => "-PAUSED---------New----1Quit---2";

    public int Width = 8;
    public int Height = 4;

    public string Verbose => string.Empty;
    public string Data => string.Empty;
    public string Error => string.Empty;
}
