namespace snakeGame.Core.Shared;

public readonly struct Result<T>(T? data, string? error)
{
    public T? Data { get; } = data;

    public string? Error { get; } = error;
}