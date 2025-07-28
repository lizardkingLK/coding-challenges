using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Validators;

public class HeightValidator : IValidate
{
    public IValidate? Next { get; init; }

    public required HashMap<string, string> Inputs { get; init; }

    public Result<bool> Validate(ref Arguments arguments)
    {
        int heightValue = MinHeight;
        if (!IsValueIncluded(out string? heightValueString))
        {
            arguments.Height = heightValue;
            return Next?.Validate(ref arguments) ?? new(true, null);
        }

        if (!IsValidValue(heightValueString!, out object? parsedHeightValue))
        {
            return new(false, ERROR_INVALID_ARGUMENTS);
        }

        arguments.Height = (int)parsedHeightValue!;

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

        if (parsedValue < MinHeight || parsedValue > MaxHeight)
        {
            return false;
        }

        value = parsedValue;

        return true;
    }

    public bool IsValueIncluded(out string? value)
    {
        return Inputs.TryGetValue(FlagHeight, out value)
        || Inputs.TryGetValue(FlagHeightPrefixed, out value);
    }
}