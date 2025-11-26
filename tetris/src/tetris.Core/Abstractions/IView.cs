namespace tetris.Core.Abstractions;

public interface IView
{
    public string Message { get; }
    public int Height { get; }
    public int Width { get; }
}