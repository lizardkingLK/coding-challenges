using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Playables;

public class DropPlayable(Arguments Arguments, IPlayable? Next = null) : IPlayable
{
    public Arguments Arguments { get; init; } = Arguments;
    public IPlayable? Next { get; init; } = Next;

    public int Height { get; set; }
    public int Width { get; set; }

    public Result<bool> Create(int height, int width)
    {
        (Height, Width) = (height, width);

        return new(true);
    }

    public Result<bool> Play()
    {
        throw new NotImplementedException();
    }
}