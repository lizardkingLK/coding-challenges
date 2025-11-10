using tetris.Core.Abstractions;
using tetris.Core.Players;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers.Managers;

public class ClassicGameManager : IManager
{
    public Arguments? Arguments { get; set; }
    public Player? Player { get; set; }
    public IPlayable? Playable { get; set; }
    public IOutput? Output { get; set; }

    public Result<bool> New()
    {
        Result<bool> dimensionResult = Output!.Create(Arguments!.MapSize);
        if (dimensionResult.Errors != null)
        {
            return dimensionResult;
        }

        return Playable!.Create(Output.Height, Output.Width);
    }

    public Result<bool> Play()
    {
        return new(true);
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