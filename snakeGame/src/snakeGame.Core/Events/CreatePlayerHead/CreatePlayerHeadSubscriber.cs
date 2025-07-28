using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Events.CreatePlayerHead;

public class CreatePlayerHeadSubscriber(Manager manager) : ISubscribe<GameState>
{
    private readonly Manager _manager = manager;

    public void Notify(GameState state)
    {
        DynamicArray<Block> spaces = _manager.Spaces;

        UpdateSpaceBlockOut(spaces, Random.Shared.Next(spaces.Size), out Block playerHead);
        UpdateMapBlock(_manager.Map, playerHead.Cordinates, CharPlayerHead);
        state.Data = _manager.Map[playerHead.CordinateY, playerHead.CordinateX];

        _manager.Player = new(playerHead);
        _manager.Output!.Stream(state);
    }
}