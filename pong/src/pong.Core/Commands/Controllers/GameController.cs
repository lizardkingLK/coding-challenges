using pong.Core.Abstractions;
using pong.Core.State.Handlers;
using pong.Core.State.Misc;

namespace pong.Core.Commands.Controllers;

public record GameController : Command
{
    private readonly GameManager _gameManager;

    public GameController(Arguments arguments) : base(arguments)
    {
        _gameManager = new();
    }

    public override void Execute()
    {
        _ = _gameManager.Play();
    }
}