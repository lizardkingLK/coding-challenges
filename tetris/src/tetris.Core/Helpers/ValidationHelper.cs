using tetris.Core.Abstractions;
using tetris.Core.Enums;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State;

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

    private static IValidator<ArgumentTypeEnum, Arguments> GetValidator(HashMap<ArgumentTypeEnum, string> data)
    {
        throw new NotImplementedException();
    }

    private static Result<HashMap<ArgumentTypeEnum, string>> GetArgumentMap(string[] arguments)
    {
        HashMap<ArgumentTypeEnum, string> argumentMap = new();

        int length = arguments.Length;
        for (int i = 0; i < length; i++)
        {
            
        }

        return new(argumentMap);
    }
}