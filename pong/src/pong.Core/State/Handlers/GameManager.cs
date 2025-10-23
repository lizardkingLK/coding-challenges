using pong.Core.Abstractions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Game;

namespace pong.Core.State.Handlers;

public record GameManager : IPublisher, ISubscriber
{
    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public Arguments Arguments { get; set; }
    private readonly ConsoleManager _consoleManager;

    public GameManager(Arguments arguments)
    {
        Arguments = arguments;
        _consoleManager = new(this);
    }

    public void Initialize()
    {
        Register();
        Subscribe();
        Listen();
    }

    public void NewGame(Arguments arguments)
    {

    }

    public void Listen(INotification notification)
    {
        throw new NotImplementedException();
    }

    public void Publish(INotification notification)
    {
        throw new NotImplementedException();
    }

    public void Register()
    {

    }

    public void Subscribe()
    {
        _consoleManager.Register();
    }

    public void Listen()
    {
        _consoleManager.Publish();
    }

    public void Publish()
    {
        throw new NotImplementedException();
    }
}