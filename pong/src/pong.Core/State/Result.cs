namespace pong.Core.State;

public record Result<T>(T? Data, string? Errors = null);