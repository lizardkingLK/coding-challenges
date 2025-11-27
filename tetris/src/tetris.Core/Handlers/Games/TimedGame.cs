using tetris.Core.State.Misc;

namespace tetris.Core.Handlers.Games;

public record TimedGame(Arguments Arguments) : GameManager(Arguments)
{
    public void Save()
    {
        
    }
}
