using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Events.CreateEnemy;

public class CreateEnemySubscriber(Manager manager) : ISubscribe<GameState>
{
    private readonly Manager _manager = manager;

    public void Notify(GameState state)
    {
        DynamicArray<Block> spaces = _manager.Spaces;

        UpdateSpaceBlockOut(spaces, Random.Shared.Next(spaces.Size), out Block enemyBlock);
        UpdateMapBlock(_manager.Map, enemyBlock.Cordinates, CharEnemy);

        _manager.Enemy = enemyBlock;
        _manager.Output!.Stream(state);
    }
}