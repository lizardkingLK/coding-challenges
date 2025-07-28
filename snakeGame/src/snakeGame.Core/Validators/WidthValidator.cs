using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Validators;

public class WidthValidator : IValidate
{
    public IValidate? Next { get; init; }

    public required HashMap<string, string> Inputs { get; init; }

    public Result<bool> Validate(ref Arguments arguments)
    {
        int widthValue = MinWidth;
        if (!IsValueIncluded(out string? widthValueString))
        {
            arguments.Width = widthValue;
            return Next?.Validate(ref arguments) ?? new(true, null);
        }

        if (!IsValidValue(widthValueString!, out object? parsedWidthValue))
        {
            return new(false, ERROR_INVALID_ARGUMENTS);
        }

        arguments.Width = (int)parsedWidthValue!;

        return Next?.Validate(ref arguments) ?? new(true, null);
    }

    public bool IsValidValue(string valueString, out object? value)
    {
        value = null;

        if (string.IsNullOrEmpty(valueString))
        {
            return false;
        }

        if (!int.TryParse(valueString, out int parsedValue))
        {
            return false;
        }

        if (parsedValue < MinWidth || parsedValue > MaxWidth)
        {
            return false;
        }

        value = parsedValue;

        return true;
    }

    public bool IsValueIncluded(out string? value)
    {
        return Inputs.TryGetValue(FlagWidth, out value)
        || Inputs.TryGetValue(FlagWidthPrefixed, out value);
    }
}