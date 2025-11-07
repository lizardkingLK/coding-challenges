using pong.Core.Abstractions;
using pong.Core.State.Misc;
using pong.Core.Views;
using static pong.Core.Helpers.GameStateHelper;

namespace pong.Core.Controllers;

public record HelpController(Arguments Arguments) : Controller(Arguments)
{
    private readonly HelpView _helpView = new();

    public override void Execute() => HandleInformation(_helpView.Data!);
}