using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;

namespace snakeGame.Core.Events.UpdatePlayerNewHead;

public class UpdatePlayerNewHeadSubscriber(Manager manager) : ISubscribe<GameState>
{
    public Manager _manager = manager;

    public void Notify(GameState state)
    {
        Block newPlayerHead = state.Data!.Value;

        DynamicArray<Block> spaces = _manager.Spaces;
        Deque<Block> player = _manager.Player!;

        Block oldHead = player.SeekFront();
        (int, int) oldHeadCordinates = oldHead.Cordinates;

        player.InsertToFront(newPlayerHead);
        UpdateMapBlock(_manager.Map, newPlayerHead.Cordinates, CharPlayerHead);
        UpdateSpaceBlockOut(spaces, block => AreSameCordinates(block.Cordinates, oldHeadCordinates), out _);

        _manager.Output!.Stream(state);
    }
}