using tetris.Core.Abstractions;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.PlayableHelper;

namespace tetris.Core.Handlers.Managers;

public class ClassicGameManager(Arguments arguments) : IManager
{
    private readonly IPlayable _playable = GetPlayable(arguments);

    public void New()
    {
        _playable.Create();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
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