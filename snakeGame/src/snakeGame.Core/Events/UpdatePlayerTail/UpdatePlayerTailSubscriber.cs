using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Events.UpdatePlayerTail;

public class UpdatePlayerTailSubscriber(Manager manager) : ISubscribe<GameState>
{
    public Manager _manager = manager;

    public void Notify(GameState state)
    {
        Deque<Block> player = _manager.Player!;

        Block oldPlayerTailBlock = player.RemoveFromRear();
        UpdateMapBlock(_manager.Map, oldPlayerTailBlock.Cordinates, CharSpaceBlock);
        UpdateSpaceBlockIn(_manager.Spaces, oldPlayerTailBlock);

        _manager.Output!.Stream(state);
    }
}