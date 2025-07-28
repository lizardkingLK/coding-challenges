using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

namespace snakeGame.Core.Validators;

public class OutputTypeValidator : IValidate
{
    public IValidate? Next { get; init; }

    public required HashMap<string, string> Inputs { get; init; }

    private static readonly int[] outputTypeEnumValues;

    static OutputTypeValidator()
    {
        int i;
        OutputTypeEnum[] outputTypeEnums = Enum.GetValues<OutputTypeEnum>();
        int length = outputTypeEnums.Length;
        outputTypeEnumValues = new int[length];
        for (i = 0; i < length; i++)
        {
            outputTypeEnumValues[i] = (int)outputTypeEnums[i];
        }
    }

    public Result<bool> Validate(ref Arguments arguments)
    {
        OutputTypeEnum outputTypeValue = OutputTypeEnum.Console;
        if (!IsValueIncluded(out string? outputTypeValueString))
        {
            arguments.OutputType = outputTypeValue;
            return Next?.Validate(ref arguments) ?? new(true, null);
        }

        if (!IsValidValue(outputTypeValueString!, out object? parsedOutputTypeValue))
        {
            return new(false, ERROR_INVALID_ARGUMENTS);
        }

        arguments.OutputType = (OutputTypeEnum)parsedOutputTypeValue!;

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

        if (!IncludesInValues(parsedValue, outputTypeEnumValues))
        {
            return false;
        }

        value = parsedValue;

        return true;
    }

    public bool IsValueIncluded(out string? value)
    {
        return Inputs.TryGetValue(FlagOutput, out value)
        || Inputs.TryGetValue(FlagOutputPrefixed, out value);
    }
}