using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Events.CreateWallBlock;

public class CreateWallBlockSubscriber(Manager manager) : ISubscribe<GameState>
{
    private readonly Manager _manager = manager;

    public void Notify(GameState state)
    {
        Block block = state.Data!.Value;
        UpdateMapBlock(_manager.Map, block.Cordinates, block);

        _manager.Output!.Stream(state);
    }
}