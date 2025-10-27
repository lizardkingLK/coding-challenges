using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Outputs.Console;
using pong.Core.Outputs.Document;
using pong.Core.State.Assets;
using pong.Core.State.Game;
using static pong.Core.Shared.Errors;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher
{
    private readonly Output _output;
    private readonly InputManager _inputManager = new();

    public bool gamePaused = false;

    public record GamePausedNotification : INotification;

    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public GameManager(Arguments arguments)
    {
        _output = arguments.OutputType switch
        {
            OutputTypeEnum.Console => new ConsoleOutput(this),
            OutputTypeEnum.Text => new DocumentOutput(this),
            _ => throw new NotImplementedException(ErrorInvalidOutputType()),
        };

        Subscribers.Add(new BoardManager(_output));
        Subscribers.Add(new RacketManager(_output));
        Subscribers.Add(new BallManager(_output));
    }

    public bool Play()
    {
        while (true)
        {
            if (gamePaused)
            {
                continue;
            }

            Publish(new BallManager.BallMoveNotification());
            
            Thread.Sleep(10);
        }
    }

    public void Publish(INotification notification)
    {
        foreach (ISubscriber? subscriber in Subscribers.Values)
        {
            subscriber?.Listen(notification);
        }
    }

    public void Publish()
    {
    }
}