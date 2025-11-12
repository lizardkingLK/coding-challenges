using tetris.Core.Enums.Commands;
using tetris.Core.Players;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IManager
{
    public Arguments? Arguments { get; set; }
    public IPlayable? Playable { get; set; }
    public Player? Player { get; set; }
    public IOutput? Output { get; set; }

    public Result<bool> New();
    public Result<bool> Play();
    public void Input(InputTypeEnum inputType);
    public void Pause();
    public void Quit();
}