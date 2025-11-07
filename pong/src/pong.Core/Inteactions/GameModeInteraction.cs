using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.State.Misc;
using pong.Core.Validators;
using pong.Core.Views;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Inteactions;

public record GameModeInteraction : Interaction
{
    private readonly Arguments _arguments;

    private readonly GameModeView _view = new();

    private GameModeEnum _gameMode;

    public GameModeInteraction(Arguments arguments)
    => _arguments = arguments;

    public override void Display()
    => WriteInformation(_view.Message!);

    public override void Prompt()
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

            WriteError(_view.Error!);
            Display();
        }
    }

    public override void Process()
    => _arguments.GameMode = _gameMode;
}