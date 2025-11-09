using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Handlers.Managers;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers;

public class GameManager(Arguments arguments) : IManager
{
    private readonly HashMap<GameModeEnum, IManager> _game = new(
        (GameModeEnum.Classic, new ClassicGameManager(arguments)));

    // TODO: initialize output that handles game outputs. output means the display for game
    //
    // TODO: timed game mode means countdown of a 10 minutes and should score high scores
    //
    // TODO: scored game mode means the time it takes to beat 100 at score. and less time means high multiplier to score.

    public void New()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Quit()
    {
        throw new NotImplementedException();
    }
}