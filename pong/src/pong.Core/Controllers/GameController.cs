using pong.Core.Abstractions;
using pong.Core.Builder;
using pong.Core.State.Handlers;
using pong.Core.State.Misc;

namespace pong.Core.Controllers;

public record GameController : Controller
{
    private readonly GameManager _gameManager;

    public GameController(Arguments arguments) : base(arguments)
    {
        _gameManager = new GameBuilder(arguments).Create();
    }

    public GameController(Arguments arguments, GameBuilder gameBuilder) : base(arguments)
    {
        _gameManager = gameBuilder.Create();
    }

    public override void Execute()
    {
        _gameManager.Create();

        _ = _gameManager.Play();
    }
}