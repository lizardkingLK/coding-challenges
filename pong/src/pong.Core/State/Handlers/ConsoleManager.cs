using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.State.Handlers;

public record ConsoleManager : IPublisher
{
    private readonly GameManager _gameManagerListener;

    public DynamicallyAllocatedArray<ISubscriber> Subscribers { get; } = new();

    public ConsoleManager(GameManager gameManagerListener)
    {
        _gameManagerListener = gameManagerListener;
    }

    public void Register()
    {
        WriteInformation(_gameManagerListener.Arguments.OutputType);
        if (_gameManagerListener.Arguments.OutputType == OutputTypeEnum.Console)
        {
            Subscribers.Add(_gameManagerListener);
        }
    }

    public void Publish()
    {
        int previousHeight = Console.WindowHeight;
        int currentHeight;
        int previousWidth = Console.WindowWidth;
        int currentWidth;
        while (true)
        {
            currentHeight = Console.WindowHeight;
            if (currentHeight != previousHeight)
            {
                WriteInformation(string.Format("info. changed height. previous = {0}. current = {1}",
                previousHeight,
                currentHeight));
                previousHeight = currentHeight;
            }

            currentWidth = Console.WindowWidth;
            if (currentWidth != previousWidth)
            {
                WriteInformation(string.Format("info. changed height. previous = {0}. current = {1}",
                previousWidth,
                currentWidth));
                previousWidth = currentWidth;
            }

            Thread.Sleep(1000);
        }
    }
    
    public void Publish(INotification notification)
    {
        
    }
}