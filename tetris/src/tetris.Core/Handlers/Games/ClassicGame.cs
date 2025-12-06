using System.Diagnostics;
using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ScoresHelper;

namespace tetris.Core.Handlers.Games;

public record ClassicGame : GameManager
{
    public ClassicGame(Arguments arguments) : base(arguments)
    => Timer = Stopwatch.StartNew();

    public override Result<bool> Save()
    => Insert(
        Arguments,
        Timer!.ElapsedMilliseconds,
        Points *= Arguments.DifficultyLevel switch
        {
            DifficultyLevelEnum.Easy => 2,
            DifficultyLevelEnum.Medium => 3,
            DifficultyLevelEnum.Hard => 4,
            _ => -1,
        });
}
