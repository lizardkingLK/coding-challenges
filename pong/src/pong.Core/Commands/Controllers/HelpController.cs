using pong.Core.Abstractions;
using pong.Core.State.Game;
using pong.Core.Views;
using static pong.Core.Helpers.OutputHelper;

namespace pong.Core.Commands.Controllers;

public record HelpController(Arguments Arguments) : Command(Arguments)
{
    private readonly HelpView _helpView = new();

    public override void Execute()
    {
        HandleInformation(_helpView.Data!);
    }
}