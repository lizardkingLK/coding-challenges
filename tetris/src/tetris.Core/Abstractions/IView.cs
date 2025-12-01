namespace tetris.Core.Abstractions;

public interface IView
{
    public string Message { get; }
    public string Data { get; }
    public string Error { get; }
}