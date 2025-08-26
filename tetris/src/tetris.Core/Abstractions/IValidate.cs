using tetris.Core.Shared;

namespace tetris.Core.Abstractions;

public interface IValidate
{
    public IValidate? Next { get; init; }

    public Result<bool> Validate();
}