using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.State.Misc;
using tetris.Core.Validators;
using tetris.Core.Views.Controller.Interactive;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Interactions;

public class GameModeInteraction : IInteraction
{
    private readonly GameModeInteractionView _view = new();

    public Arguments? Arguments { get; set; }

    private GameModeEnum _gameMode;

    public void Display()
    {
        WriteLine(_view.Message, ColorSuccess);
        WriteLine(_view.Verbose, ColorInfo);
    }

    public void Prompt()
    {
        string? input;
        while (true)
        {
            WritePrompt();
            input = ReadInput();
            if (GameModeValidator.TryValidate(input, out _gameMode))
            {
                break;
            }

            WriteLine(_view.Error, ColorError);
            Display();
        }
    }

    public void Process()
    => Arguments!.GameMode = _gameMode;
}