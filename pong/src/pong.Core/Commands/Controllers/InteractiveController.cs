using pong.Core.Abstractions;
using pong.Core.State.Misc;
using static pong.Core.Helpers.GameStateHelper;

namespace pong.Core.Commands.Controllers;

public record InteractiveController(Arguments Arguments) : Command(Arguments)
{
    public override void Execute()
    {
        HandleInformation("interactive helper is here");
    }
}