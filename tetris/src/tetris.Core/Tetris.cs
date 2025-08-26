using tetris.Core.Abstractions;
using tetris.Core.Shared;

using static tetris.Core.Helpers.ChainingHelper;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core;

public static class Tetris
{
    public static void Play(string[] args)
    {
        Result<bool> validatorResult = GetValidator(args, out IValidate validator);
        if (!validatorResult.Data)
        {
            WriteError(ERROR_INVALID_ARGUMENTS, 0, 0, ConsoleColor.Red);
            Environment.Exit(1);
        }

        Result<bool> validationResult = validator.Validate();
        if (!validatorResult.Data)
        {
            WriteError(ERROR_INVALID_ARGUMENTS, 0, 0, ConsoleColor.Red);
            Environment.Exit(1);
        }
    }
}
