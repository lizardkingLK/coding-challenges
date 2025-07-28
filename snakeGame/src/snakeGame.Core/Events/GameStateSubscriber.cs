using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Events.CreateEnemy;
using snakeGame.Core.Events.CreatePlayerBody;
using snakeGame.Core.Events.CreatePlayerHead;
using snakeGame.Core.Events.CreateSpaceBlock;
using snakeGame.Core.Events.CreateWallBlock;
using snakeGame.Core.Events.UpdateGameComplete;
using snakeGame.Core.Events.UpdateGameOver;
using snakeGame.Core.Events.UpdatePlayerNewHead;
using snakeGame.Core.Events.UpdatePlayerOldHead;
using snakeGame.Core.Events.UpdatePlayerTail;
using snakeGame.Core.Library;
using snakeGame.Core.State;

namespace snakeGame.Core.Events;

public class GameStateSubscriber : ISubscribe<GameState>
{
    private readonly HashMap<GameStateEnum, ISubscribe<GameState>> _subscribers = new();

    public GameStateSubscriber(Manager manager)
    {
        _subscribers.Insert(GameStateEnum.CreateWall, new CreateWallBlockSubscriber(manager));
        _subscribers.Insert(GameStateEnum.CreateSpace, new CreateSpaceBlockSubscriber(manager));
        _subscribers.Insert(GameStateEnum.CreatePlayerHead, new CreatePlayerHeadSubscriber(manager));
        _subscribers.Insert(GameStateEnum.CreatePlayerBody, new CreatePlayerBodySubscriber(manager));
        _subscribers.Insert(GameStateEnum.CreateEnemy, new CreateEnemySubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdatePlayerOldHead, new UpdatePlayerOldHeadSubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdatePlayerNewHead, new UpdatePlayerNewHeadSubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdatePlayerTail, new UpdatePlayerTailSubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdateEnemy, new CreateEnemySubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdateGameOver, new UpdateGameOverSubscriber(manager));
        _subscribers.Insert(GameStateEnum.UpdateGameComplete, new UpdateGameCompleteSubscriber(manager));
    }

    public void Notify(GameState state)
    {
        if (_subscribers.TryGetValue(state.Type, out ISubscribe<GameState>? subscriber))
        {
            subscriber!.Notify(state);
        }
    }
}