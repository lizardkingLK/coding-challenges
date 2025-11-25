using tetris.Core.Abstractions;

namespace tetris.Core.Views;

public class PauseMenuView : IView
{
    public string Message => "PAUSED----------Resume-1Restart2New----3Quit---4";

    public int Width => 8;
    public int Height => 6;
}
