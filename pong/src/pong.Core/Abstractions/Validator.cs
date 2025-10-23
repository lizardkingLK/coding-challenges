using pong.Core.State.Common;

namespace pong.Core.Abstractions;

public abstract record Validator<TInput, TOutput>(
    TInput Input,
    TOutput Output,
    Validator<TInput, TOutput>? Next = null) where TInput : notnull
{
    public abstract Result<TOutput> Validate();
}