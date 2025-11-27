using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers.Games;

public record TimedGame(Arguments Arguments) : GameManager(Arguments)
{
    public override Result<bool> Save()
    {

        return new(true);
    }
}
