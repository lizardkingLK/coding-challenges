using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.State.Misc;
using tetris.Core.Validators;
using tetris.Core.Views.Controller.Interactive;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Interactions;

public class DifficultyInteraction : IInteraction
{
    private readonly DifficultyInteractionView _view = new();

    public Arguments? Arguments { get; set; }

    private DifficultyLevelEnum _difficulty;

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
            if (DifficultyValidator.TryValidate(input, out _difficulty))
            {
                break;
            }

            WriteLine(_view.Error, ColorError);
            Display();
        }
    }

    public void Process()
    => Arguments!.DifficultyLevel = _difficulty;
}