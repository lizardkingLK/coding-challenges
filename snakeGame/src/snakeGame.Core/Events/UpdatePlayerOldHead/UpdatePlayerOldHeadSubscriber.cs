using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Events.UpdatePlayerOldHead;

public class UpdatePlayerOldHeadSubscriber(Manager manager) : ISubscribe<GameState>
{
    public Manager _manager = manager;

    public void Notify(GameState state)
    {
        Deque<Block> player = _manager.Player!;

        Block oldHead = player.SeekFront();
        (int, int) oldHeadCordinates = oldHead.Cordinates;

        UpdateMapBlock(_manager.Map, oldHeadCordinates, CharPlayerBody);

        _manager.Output!.Stream(state);
    }
}