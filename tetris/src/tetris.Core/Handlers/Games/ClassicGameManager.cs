using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Commands;
using tetris.Core.Outputs;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers.Games;

public class ClassicGameManager : IManager
{
    public IOutput? Output { get; set; }
    public PlayerManager? PlayerManager { get; set; }
    public MapManager? MapManager { get; set; }
    private MapSizeEnum _mapSize;

    public ClassicGameManager(Arguments arguments)
    {
        _mapSize = arguments.MapSize;

        Output = arguments.OutputType switch
        {
            OutputTypeEnum.Console => new ConsoleOutput(),
            OutputTypeEnum.Document => new DocumentOutput(),
            _ => throw new NotImplementedException("error. cannot find output. invalid output type given"),
        };

        MapManager = new MapManager(Output);
        PlayerManager = new(this);
    }

    public Result<bool> New()
    {
        Result<bool> dimensionCreateResult = Output!.Create(_mapSize);
        if (dimensionCreateResult.Errors != null)
        {
            return dimensionCreateResult;
        }

        Result<bool> playableCreateResult = MapManager!.Create();
        if (playableCreateResult.Errors != null)
        {
            return playableCreateResult;
        }

        Output!.Flush();

        return new(true);
    }

    public Result<bool> Play()
    {
        return MapManager!.Play();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Quit()
    {
        throw new NotImplementedException();
    }

    public void Input(InputTypeEnum inputType)
    {
        throw new NotImplementedException();
    }
}