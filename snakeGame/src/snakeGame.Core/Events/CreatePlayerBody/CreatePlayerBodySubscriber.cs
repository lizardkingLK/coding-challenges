using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;

namespace snakeGame.Core.Events.CreatePlayerBody;

public class CreatePlayerBodySubscriber(Manager manager) : ISubscribe<GameState>
{
    private readonly Manager _manager = manager;

    public void Notify(GameState state)
    {
        Block block = state.Data!.Value;
        DynamicArray<Block> spaces = _manager.Spaces;

        bool blockSearchFunction(Block space) => AreSameCordinates(space.Cordinates, block.Cordinates);
        UpdateSpaceBlockOut(spaces, blockSearchFunction, out Block playerBodyBlock);
        UpdateMapBlock(_manager.Map, playerBodyBlock.Cordinates, CharPlayerBody, block.Direction);

        _manager.Player.InsertToRear(playerBodyBlock);
        _manager.Output!.Stream(state);
    }
}