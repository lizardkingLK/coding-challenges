using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Handlers.Managers;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs;
using tetris.Core.Playables;
using tetris.Core.Players;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Handlers;

public class GameManager : IManager
{
    private static readonly HashMap<GameModeEnum, IManager> _gameSelector
    = new((GameModeEnum.Classic, new ClassicGameManager()));

    private readonly IManager _game;

    public Arguments? Arguments { get; set; }
    public Player? Player { get; set; }
    public IPlayable? Playable { get; set; }
    public IOutput? Output { get; set; }

    // TODO: initialize output that handles game outputs. output means the display for game
    //
    // TODO: timed game mode means countdown of a 10 minutes and should score high scores
    //
    // TODO: scored game mode means the time it takes to beat 100 at score. and less time means high multiplier to score.

    public GameManager(Arguments arguments)
    {
        Arguments = arguments;
        _game = _gameSelector[arguments.GameMode]!;
        _game.Arguments = Arguments;

        Output = GetOutput(arguments.OutputType);
        _game.Output = Output;

        Playable = GetPlayable();
        _game.Playable = Playable;

        Player = GetPlayer();
        _game.Player = Player;
    }

    public Result<bool> New()
    {
        Result<bool> newGameResult = _game.New();
        if (newGameResult.Errors != null)
        {
            return newGameResult;
        }

        return new(true);
    }

    public Result<bool> Play()
    {
        Result<bool> result = _game.Play();


        // Output!.Clear();

        return result;
    }

    public void Pause() => _game.Pause();

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Quit()
    {
        throw new NotImplementedException();
    }

    public IPlayable GetPlayable()
    {
        IPlayable dropPlayable = new DropPlayable(Arguments!, Output!, null);
        IPlayable mapPlayable = new MapPlayable(Arguments!, Output!, dropPlayable);

        return mapPlayable;
    }

    public static IOutput GetOutput(OutputTypeEnum outputType)
    {
        return outputType switch
        {
            OutputTypeEnum.Console => new ConsoleOutput(),
            OutputTypeEnum.Document => new DocumentOutput(),
            _ => throw new NotImplementedException("error. cannot find output. invalid output type given"),
        };
    }

    private static Player? GetPlayer() => new();
}