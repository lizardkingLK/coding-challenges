using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Game;
using pong.Core.State.Outputs;

namespace pong.Core.State.Handlers;

public record StatusManager : Status, ISubscriber
{
    private readonly Output _output;

    public int Height => _output.Height;
    public int Width => _output.Width;

    public StatusManager(GameManager gameManager)
    {
        _output = new ConsoleOutput(gameManager);

        MapGrid = new(values:
        [.. Enumerable.Range(0, _output.Height).Select(_
            => new DynamicallyAllocatedArray<Block>(_output.Width))]);
    }

    public void Update(Block block)
    {
        MapGrid[block.Top]![block.Left] = block;

        _output.Draw(block, MapGrid);
    }

    public void Subscribe()
    {
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GameCreateNotification:
                Output();
                break;
            default:
                break;
        }
    }

    public override void Output() => _output.Draw(MapGrid);
}