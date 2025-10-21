using pong.Core.Abstractions;
using pong.Core.State.Game;

namespace pong.Core.Commands.Controllers;

public record GameController : Command
{
    public GameController(Arguments Arguments) : base(Arguments)
    {
    }

    public override void Execute()
    {
        
    }
}