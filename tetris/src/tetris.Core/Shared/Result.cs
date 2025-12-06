namespace tetris.Core.Shared;

public record Result<T>(T? Data = default, string? Errors = null);