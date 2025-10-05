namespace tetris.Core.Shared;

public record Result<T>(T? Data, string? Errors);