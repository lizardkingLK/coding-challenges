using pong.Core.Abstractions;
using pong.Core.State.Handlers;

namespace pong.Core.State.Assets;

public class RacketManager : ISubscriber
{
    private readonly Output _output;

    public RacketManager(Output output)
    {
        _output = output;

        Create();
    }

    private void Create()
    {
        // Console.WriteLine("racket create");
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GamePausedNotification:
                Create();
                break;
            default:
                break;
        }
    }

    public void Subscribe()
    {
    }
}