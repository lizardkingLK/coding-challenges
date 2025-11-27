using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers.Games;

public record ClassicGame(Arguments Arguments) : GameManager(Arguments)
{
    public override Result<bool> Save()
    {
        var xd = Points;
        
        return new(true);
    }
}
