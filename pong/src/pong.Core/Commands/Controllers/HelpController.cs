using pong.Core.Abstractions;
using pong.Core.State.Game;

namespace pong.Core.Commands.Controllers;

public record HelpController : Command
{
    public HelpController(Arguments Arguments) : base(Arguments)
    {
    }

    public override void Execute()
    {

    }
}