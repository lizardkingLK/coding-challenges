using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

namespace snakeGame.Core.Validators;

public class GameModeValidator : IValidate
{
    public IValidate? Next { get; init; }

    public required HashMap<string, string> Inputs { get; init; }

    private static readonly int[] gameModeEnumValues;

    static GameModeValidator()
    {
        int i;
        GameModeEnum[] gameModeEnums = Enum.GetValues<GameModeEnum>();
        int length = gameModeEnums.Length;
        gameModeEnumValues = new int[length];
        for (i = 0; i < length; i++)
        {
            gameModeEnumValues[i] = (int)gameModeEnums[i];
        }
    }

    public Result<bool> Validate(ref Arguments arguments)
    {
        GameModeEnum gameModeValue = GameModeEnum.Automatic;
        if (!IsValueIncluded(out string? gameModeValueString))
        {
            arguments.GameMode = gameModeValue;
            return Next?.Validate(ref arguments) ?? new(true, null);
        }

        if (!IsValidValue(gameModeValueString!, out object? parsedGameModeValue))
        {
            return new(false, ERROR_INVALID_ARGUMENTS);
        }

        arguments.GameMode = (GameModeEnum)parsedGameModeValue!;

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

        if (!IncludesInValues(parsedValue, gameModeEnumValues))
        {
            return false;
        }

        value = parsedValue;

        return true;
    }

    public bool IsValueIncluded(out string? value)
    {
        return Inputs.TryGetValue(FlagGameMode, out value)
        || Inputs.TryGetValue(FlagGameModePrefixed, out value);
    }
}