using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IPlayable
{
    public int Height { get; set; }
    public int Width { get; set; }

    public IPlayable? Next { get; init; }
    public Arguments Arguments { get; init; }

    public Result<bool> Create(int height, int width);
    public Result<bool> Play();
}