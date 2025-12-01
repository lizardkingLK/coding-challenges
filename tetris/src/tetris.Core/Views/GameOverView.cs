using tetris.Core.Abstractions;

namespace tetris.Core.Views;

public class GameOverView : IView
{
    public string Message => " GAME  OVER ";
    public string Data => string.Empty;

    public string Error => string.Empty;

    public int Width = 6;
    public int Height = 2;
}