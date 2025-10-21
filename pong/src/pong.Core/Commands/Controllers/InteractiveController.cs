using pong.Core.Abstractions;
using pong.Core.State.Game;

namespace pong.Core.Commands.Controllers;

public record InteractiveController : Command
{
    public InteractiveController(Arguments Arguments) : base(Arguments)
    {
    }

    public override void Execute()
    {

    }
}