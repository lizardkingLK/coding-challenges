using tetris.Core.Shared;
using tetris.Core.State.Game;
using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IPlayable
{
    public IPlayable? Next { get; init; }
    public Arguments Arguments { get; init; }

    public Result<bool> Create();
    public Result<Score> Play();
}