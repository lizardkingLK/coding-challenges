using pong.Core.Abstractions;
using pong.Core.State.Game;
using static pong.Core.Helpers.OutputHelper;

namespace pong.Core.Commands.Controllers;

public record InteractiveController(Arguments Arguments) : Command(Arguments)
{
    public override void Execute()
    {
        HandleInformation("interactive helper is here");
    }
}