using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Commands;
using tetris.Core.Gamplay;
using tetris.Core.Outputs;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ValueHelper;

namespace tetris.Core.Handlers.Games;

public class ClassicGameManager : IManager
{
    public IGameplay? Output { get; set; }
    public PlayerManager? PlayerManager { get; set; }
    public MapManager? MapManager { get; set; }

    public ClassicGameManager(Arguments arguments)
    {
        Output = arguments.OutputType switch
        {
            OutputTypeEnum.Console => ConsoleGameplay.CreateScaled(arguments.MapSize),
            OutputTypeEnum.Document => DocumentGameplay.CreateScaled(arguments.MapSize),
            _ => throw new NotImplementedException("error. cannot find gameplay. invalid gameplay type given"),
        };

        MapManager = new MapManager(Output);
        PlayerManager = new(this);

        SetDifficultyValues(arguments.DifficultyLevel);
    }

    public Result<bool> New()
    {
        Result<bool> dimensionCreateResult = Output!.Create();
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
        Task.Run(PlayerManager!.Input);

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

    public void Input(CommandTypeEnum commandType)
    {
        if (commandType == CommandTypeEnum.PauseGame)
        {
            return;
        }

        MapManager!.Input(commandType);
    }
}