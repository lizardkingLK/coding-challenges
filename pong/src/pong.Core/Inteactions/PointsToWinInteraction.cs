using pong.Core.Abstractions;
using pong.Core.State.Misc;
using pong.Core.Validators;
using pong.Core.Views;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Inteactions;

public record PointsToWinInteraction : Interaction
{
    private readonly Arguments _arguments;

    private readonly PointsToWinView _view = new();

    private int _pointsToWin;

    public PointsToWinInteraction(Arguments arguments)
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
            if (PointsToWinValidator.TryValidate(input, out _pointsToWin))
            {
                break;
            }

            WriteError(_view.Error!);
            Display();
        }
    }

    public override void Process()
    => _arguments.PointsToWin = _pointsToWin;
}