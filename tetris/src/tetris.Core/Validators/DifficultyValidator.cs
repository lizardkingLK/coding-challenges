using tetris.Core.Abstractions;
using tetris.Core.Enums;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State;

namespace tetris.Core.Validators;

public class DifficultyValidator : IValidator<ArgumentTypeEnum, Arguments>
{
    public required Arguments Value { get; init; }

    public required HashMap<ArgumentTypeEnum, string> Values { get; init; }

    public IValidator<ArgumentTypeEnum, Arguments>? Next { get; init; }

    public Result<Arguments> Validate()
    {
        return default!;
    }
}