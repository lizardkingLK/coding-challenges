using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
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
        Result<bool> dimensionCreateResult = Output!.Create(Arguments!.MapSize);
        if (dimensionCreateResult.Errors != null)
        {
            return dimensionCreateResult;
        }

        Result<bool> playableCreateResult = Playable!.Create();
        if (playableCreateResult.Errors != null)
        {
            return playableCreateResult;
        }

        Output!.Flush();

        return new(true);
    }

    public Result<bool> Play()
    {
        Playable!.Play();

        return new(true);
    }

    public void Pause() => Playable!.Pause();

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Quit()
    {
        throw new NotImplementedException();
    }

    public void Input(InputTypeEnum inputType)
    => Playable!.Input(inputType);
}