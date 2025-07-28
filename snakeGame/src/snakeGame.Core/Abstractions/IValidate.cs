using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IValidate
{
    public IValidate? Next { get; init; }

    public HashMap<string, string> Inputs { get; init; }

    public Result<bool> Validate(ref Arguments arguments);

    public bool IsValueIncluded(out string? value);

    public bool IsValidValue(string valueString, out object? value);
}