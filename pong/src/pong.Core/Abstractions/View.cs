namespace pong.Core.Abstractions;

public abstract class View
{
    public abstract string? Message { get; }
    public abstract string? Data { get; }
    public abstract string? Error { get; }
}