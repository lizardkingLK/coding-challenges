using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Shared;

public static class Values
{
    public static readonly Position[,] scaledBlockPositions =
    {
        { new(0, 0), new(0, 1) },
        { new(1, 0), new(1, 1) },
    };

    public static int durationMoveInterval = DurationMoveInterval;
}