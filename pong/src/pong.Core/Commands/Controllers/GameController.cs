using pong.Core.Abstractions;
using pong.Core.State.Game;
using pong.Core.State.Handlers;

namespace pong.Core.Commands.Controllers;

public record GameController : Command
{
    private readonly GameManager _gameManager;

    public GameController(Arguments arguments) : base(arguments)
    {
        _gameManager = new(arguments);
    }

    public override void Execute()
    {
        _ = _gameManager.Play();
    }
}