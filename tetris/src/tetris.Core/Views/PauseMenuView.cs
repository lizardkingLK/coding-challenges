using tetris.Core.Abstractions;

namespace tetris.Core.Views;

public class PauseMenuView : IView
{
    public string Message => "PAUSED----------Restart1New----2Quit---3";

    public int Width => 8;
    public int Height => 5;
}
