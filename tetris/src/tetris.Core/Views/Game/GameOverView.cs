using tetris.Core.Abstractions;

namespace tetris.Core.Views.Game;

public class GameOverView : IView
{
    public string Message => " GAME  OVER ";

    public int Width = 6;
    public int Height = 2;

    public string Verbose => string.Empty;
    public string Data => string.Empty;
    public string Error => string.Empty;
}