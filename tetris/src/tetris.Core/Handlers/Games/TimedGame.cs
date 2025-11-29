using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ScoresHelper;

namespace tetris.Core.Handlers.Games;

public record TimedGame : GameManager
{
    public TimedGame(Arguments arguments) : base(arguments)
    {
        StartedAt = DateTime.UtcNow;
    }

    public override Result<bool> Save()
    {
        int time = (int)(DateTime.UtcNow - StartedAt).TotalMilliseconds;
        int points = (int)(1 + time * 1e-3) * Points;

        return Insert(Arguments, time, points);
    }
}
