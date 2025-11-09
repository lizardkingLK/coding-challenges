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

    // TODO: initialize output that handles game outputs

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