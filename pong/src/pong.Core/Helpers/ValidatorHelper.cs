using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.NonLinear.HashMaps;
using pong.Core.State;
using pong.Core.State.Game;
using pong.Core.Validators.ArgumentValidators;
using static pong.Core.Shared.Errors;
using static pong.Core.Shared.Values;

namespace pong.Core.Helpers;

public static class ValidatorHelper
{
    public static Result<Arguments> GetValidator(string[] arguments)
    {
        Result<HashMap<ArgumentTypeEnum, string?>> argumentsMapResult = GetArgumentsMap(arguments);
        if (argumentsMapResult.Errors != null)
        {
            return new(null, argumentsMapResult.Errors);
        }

        Result<Arguments> validatedArgumentsResult = GetValidators(argumentsMapResult.Data!).Validate();
        if (validatedArgumentsResult.Errors != null)
        {
            return new(null, validatedArgumentsResult.Errors);
        }

        return new(validatedArgumentsResult.Data!);
    }

    private static Validator<HashMap<ArgumentTypeEnum, string?>, Arguments> GetValidators(
        HashMap<ArgumentTypeEnum, string?> argumentsMap)
    {
        Arguments arguments = new();

        DifficultyValidator difficultyValidator = new(argumentsMap, arguments, null);
        OutputTypeValidator outputTypeValidator = new(argumentsMap, arguments, difficultyValidator);
        GameModeValidator gameModeValidator = new(argumentsMap, arguments, outputTypeValidator);
        InteractiveValidator interactiveValidator = new(argumentsMap, arguments, gameModeValidator);
        HelpValidator helpValidator = new(argumentsMap, arguments, interactiveValidator);

        return helpValidator;
    }

    private static Result<HashMap<ArgumentTypeEnum, string?>> GetArgumentsMap(string[] arguments)
    {
        HashMap<ArgumentTypeEnum, string?> argumentsMap = new();

        int length = arguments.Length;
        string argumentKey;
        string? argumentValue;
        for (int i = 0; i < length; i++)
        {
            argumentKey = arguments[i].ToLower();
            argumentValue = arguments.ElementAtOrDefault(i + 1);
            if (!allArgumentsMap.TryGet(argumentKey, out ArgumentTypeEnum argumentType))
            {
                return new(null, ErrorInvalidArguments(argumentKey));
            }

            if (!argumentsMap.TryAdd(argumentType, argumentValue))
            {
                return new(null, ErrorDuplicateArguments(argumentKey));
            }

            argumentsMap.Add(argumentType, argumentValue);
            if (!string.IsNullOrEmpty(argumentValue))
            {
                i++;   
            }
        }

        return new(argumentsMap);
    }
}