using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ScoresHelper;

namespace tetris.Core.Handlers.Games;

public record ClassicGame : GameManager
{
    public ClassicGame(Arguments arguments) : base(arguments)
    {
        StartedAt = DateTime.UtcNow;
    }

    public override Result<bool> Save() => Insert(
        Arguments,
        (int)(DateTime.UtcNow - StartedAt).TotalMilliseconds,
        Points);
}
