using pong.Core.Abstractions;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Commands.Controllers;

public record GameController : Command
{
    private readonly InputManager _inputManager = new();

    private readonly GameManager _gameManager;

    public GameController(Arguments arguments) : base(arguments)
    {
        _gameManager = new(arguments);        
    }

    public override void Execute()
    {
        WriteInformation("the game is on");
        _gameManager.Initialize();
    }
}