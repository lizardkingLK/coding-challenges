using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

namespace snakeGame.Core.Validators;

public class DifficultyLevelValidator : IValidate
{
    public IValidate? Next { get; init; }

    public required HashMap<string, string> Inputs { get; init; }

    private static readonly int[] difficultyLevelEnumValues;

    static DifficultyLevelValidator()
    {
        int i;
        DifficultyLevelEnum[] difficultyLevelEnums = Enum.GetValues<DifficultyLevelEnum>();
        int length = difficultyLevelEnums.Length;
        difficultyLevelEnumValues = new int[length];
        for (i = 0; i < length; i++)
        {
            difficultyLevelEnumValues[i] = (int)difficultyLevelEnums[i];
        }
    }

    public Result<bool> Validate(ref Arguments arguments)
    {
        DifficultyLevelEnum difficultyLevelValue = DifficultyLevelEnum.Medium;
        if (!IsValueIncluded(out string? difficultyLevelValueString))
        {
            arguments.DifficultyLevel = difficultyLevelValue;
            return Next?.Validate(ref arguments) ?? new(true, null);
        }

        if (!IsValidValue(difficultyLevelValueString!, out object? parsedDifficultyLevelValue))
        {
            return new(false, ERROR_INVALID_ARGUMENTS);
        }

        arguments.DifficultyLevel = (DifficultyLevelEnum)parsedDifficultyLevelValue!;

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

        if (!IncludesInValues(parsedValue, difficultyLevelEnumValues))
        {
            return false;
        }

        value = parsedValue;

        return true;
    }

    public bool IsValueIncluded(out string? value)
    {
        return Inputs.TryGetValue(FlagDifficulty, out value)
        || Inputs.TryGetValue(FlagDifficultyPrefixed, out value);
    }
}