using tetris.Core.Abstractions;
using tetris.Core.Shared;

namespace tetris.Core.Validators;

public class DifficultyValidator : IValidate
{
    public IValidate? Next { get; init; }

    public Result<bool> Validate()
    {
        return new(true);
    }
}