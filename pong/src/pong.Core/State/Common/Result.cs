namespace pong.Core.State.Common;

public record Result<T>(T? Data, string? Errors = null);