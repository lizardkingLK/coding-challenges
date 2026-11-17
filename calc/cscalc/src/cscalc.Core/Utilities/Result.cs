namespace cscalc.Core.Utilities;

public readonly record struct Result<T>(T? Value, string? Error)
{
    public bool IsSuccess => Value is not null;
    public T Data => Value!;
    public string ErrorMessage => Error ?? "error. unknown error occurred";
}
