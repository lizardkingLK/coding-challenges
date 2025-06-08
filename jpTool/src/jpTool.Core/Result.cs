namespace jpTool.Core;

public class Result<T>(T? data, string? errors)
{
    public T? Data { get; } = data;

    public string? Errors { get; } = errors;
}