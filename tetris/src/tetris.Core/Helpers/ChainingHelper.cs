using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.Validators;

namespace tetris.Core.Helpers;

public static class ChainingHelper
{
    public static Result<bool> GetValidator(string[] args, out IValidate validator)
    {
        validator = new DifficultyValidator()
        {
            Next = null,
        };

        return new(true, null);
    }
}