using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IPlayable
{
    public IPlayable? Next { get; init; }
    public Arguments Arguments { get; init; }
    public IOutput Output { get; init; }

    Result<bool> Create();
    public Result<bool> Play();
}