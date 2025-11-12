using tetris.Core.Enums.Commands;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IPlayable
{
    public IPlayable? Next { get; init; }
    public Arguments Arguments { get; init; }
    public IOutput Output { get; init; }

    public Result<bool> Create();
    public void Input(InputTypeEnum inputType);
    public Result<bool> Play();
    public void Pause();
}