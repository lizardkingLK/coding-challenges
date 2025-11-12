using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Commands;
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

    public Result<bool> New() => _game.New();

    public Result<bool> Play()
    {
        // TODO: setTimeout of three seconds
        Task.Run(Player!.Input);

        // TODO: if paused continue , if dropping continue dropping
        // if not dropping drop new
        Result<bool> result;
        while (true)
        {
            result = _game.Play();
        }
    }

    public void Input(InputTypeEnum inputType)
    {
        if (inputType == InputTypeEnum.PauseGame)
        {
            Pause();
            return;
        }

        _game.Input(inputType);
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

    private IPlayable GetPlayable()
    {
        IPlayable dropPlayable = new DropPlayable(Arguments!, Output!, null);
        IPlayable mapPlayable = new MapPlayable(Arguments!, Output!, dropPlayable);

        return mapPlayable;
    }

    private static IOutput GetOutput(OutputTypeEnum outputType)
    {
        return outputType switch
        {
            OutputTypeEnum.Console => new ConsoleOutput(),
            OutputTypeEnum.Document => new DocumentOutput(),
            _ => throw new NotImplementedException("error. cannot find output. invalid output type given"),
        };
    }

    private Player? GetPlayer() => new(this);
}