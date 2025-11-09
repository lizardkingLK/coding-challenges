using System.Reflection;
using tetris.Core.Abstractions;
using tetris.Core.Attributes;
using tetris.Core.Enums.Game;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using tetris.Core.Validators;

namespace tetris.Core.Helpers;

public static class ValidationHelper
{
    public static Result<Arguments> GetValidated(string[] arguments)
    {
        Result<HashMap<ArgumentTypeEnum, string>> argumentMapResult = GetArgumentMap(arguments);
        if (argumentMapResult.Errors != null)
        {
            return new(null, argumentMapResult.Errors);
        }

        Result<Arguments> validatorResult = GetValidator(argumentMapResult.Data!).Validate();
        if (validatorResult.Errors != null)
        {
            return new(null, validatorResult.Errors);
        }

        return new(validatorResult.Data);
    }

    private static IValidator<ArgumentTypeEnum, Arguments> GetValidator(HashMap<ArgumentTypeEnum, string> values)
    {
        Arguments value = new();

        IValidator<ArgumentTypeEnum, Arguments> difficultyValidator = new DifficultyValidator(value, values);
        IValidator<ArgumentTypeEnum, Arguments> gameModeValidator = new GameModeValidator(value, values, difficultyValidator);
        IValidator<ArgumentTypeEnum, Arguments> interactiveValidator = new InteractiveValidator(value, values, gameModeValidator);
        IValidator<ArgumentTypeEnum, Arguments> helpValidator = new HelpValidator(value, values, interactiveValidator);

        return helpValidator;
    }

    private static Result<HashMap<ArgumentTypeEnum, string>> GetArgumentMap(string[] arguments)
    {
        HashMap<string, (ArgumentTypeEnum, bool)> allArgumentsMap = GetAllArguments();

        HashMap<ArgumentTypeEnum, string> argumentsMap = [];

        int length = arguments.Length;
        string argumentKey;
        string? argumentValue;
        bool moveToNextKeyValue;
        for (int i = 0; i < length; i++)
        {
            argumentKey = arguments[i].ToLower();
            argumentValue = arguments.ElementAtOrDefault(i + 1);
            if (!allArgumentsMap.TryGetValue(argumentKey, out (ArgumentTypeEnum, bool) argumentTypeValue))
            {
                return new(null, $"error. invalid arguments given: {argumentKey}");
            }

            (ArgumentTypeEnum argumentType, bool isSwitch) = argumentTypeValue;
            if (!argumentsMap.TryAdd(argumentType, argumentValue))
            {
                return new(null, $"error. duplicate arguments were given: {argumentKey}");
            }

            moveToNextKeyValue = string.IsNullOrEmpty(argumentValue) || isSwitch;
            if (moveToNextKeyValue)
            {
                continue;
            }

            i++;
        }

        return new(argumentsMap);
    }

    private static HashMap<string, (ArgumentTypeEnum, bool)> GetAllArguments()
    {
        HashMap<string, (ArgumentTypeEnum, bool)> allArgumentMap = [];

        IEnumerable<Type> declarations = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(item => item.IsDefined(typeof(ArgumentAttribute)));

        string prefix;
        string name;
        ArgumentTypeEnum type;
        bool isSwitch;
        foreach (Type item in declarations)
        {
            IEnumerable<ArgumentAttribute> attributes = item.GetCustomAttributes<ArgumentAttribute>()!;
            foreach (ArgumentAttribute attribute in attributes)
            {
                prefix = attribute.Prefix;
                name = attribute.Name;
                type = attribute.Type;
                isSwitch = attribute.IsSwitch;

                allArgumentMap.AddRange([(prefix, (type, isSwitch)), (name, (type, isSwitch))]);
            }
        }

        return allArgumentMap;
    }
}