using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console.Scalers;

public record NormalScaler : ConsoleScaler
{
    public override Position ScorePosition
    => Root + new Position(0, WidthNormal - 1);

    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
        => blocks.Add(
            CreateBlock(
                Root + block.Position,
                block.Symbol,
                block.Color));
}