using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.State.Game;
using tetris.Core.State.Misc;

namespace tetris.Core.Playables;

public class MapPlayable(Arguments Arguments, IPlayable? Next = null) : IPlayable
{
    public Arguments Arguments { get; init; } = Arguments;
    public IPlayable? Next { get; init; } = Next;

    public Result<bool> Create()
    {
        throw new NotImplementedException();
    }

    public Result<Score> Play()
    {
        throw new NotImplementedException();
    }
}