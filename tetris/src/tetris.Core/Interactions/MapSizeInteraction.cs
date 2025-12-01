using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.State.Misc;
using tetris.Core.Validators;
using tetris.Core.Views.Controller.Interactive;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Interactions;

public class MapSizeInteraction : IInteraction
{
    private readonly MapSizeInteractionView _view = new();

    public Arguments? Arguments { get; set; }

    private MapSizeEnum _mapSize;

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
            if (MapSizeValidator.TryValidate(input, out _mapSize))
            {
                break;
            }

            WriteLine(_view.Error, ColorError);
            Display();
        }
    }

    public void Process()
    => Arguments!.MapSize = _mapSize;
}