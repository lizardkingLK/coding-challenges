namespace snakeGame.Core;

public class Result<T>(T? data, string? error)
{
    public T? Data { get; } = data;

    public string? Error { get; } = error;
}