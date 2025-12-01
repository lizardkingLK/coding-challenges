using tetris.Core.Abstractions;

namespace tetris.Core.Views;

public class PauseMenuView : IView
{
    public string Message => "-PAUSED---------New----1Quit---2";

    public string Data => string.Empty;

    public string Error => string.Empty;

    public int Width = 8;
    public int Height = 4;
}
