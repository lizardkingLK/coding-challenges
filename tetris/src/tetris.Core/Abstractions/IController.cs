using tetris.Core.Shared;

namespace tetris.Core.Abstractions;

public interface IController
{
    public Result<bool> Execute();
}