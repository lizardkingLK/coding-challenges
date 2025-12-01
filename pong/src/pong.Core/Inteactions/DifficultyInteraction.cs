using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.State.Misc;
using pong.Core.Validators;
using pong.Core.Views;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Inteactions;

public record DifficultyInteraction : Interaction
{
    private readonly Arguments _arguments;

    private readonly DifficultyView _view = new();

    private DifficultyLevelEnum _difficultyLevel;

    public DifficultyInteraction(Arguments arguments)
    => _arguments = arguments;

    public override void Display()
    => WriteInformation(_view.Message);

    public override void Prompt()
    {
        string? input;

        while (true)
        {
            WritePrompt();
            input = ReadInput();
            if (DifficultyValidator.TryValidate(input, out _difficultyLevel))
            {
                break;
            }

            WriteError(_view.Error);
            Display();
        }
    }

    public override void Process()
    => _arguments.DifficultyLevel = _difficultyLevel;
}