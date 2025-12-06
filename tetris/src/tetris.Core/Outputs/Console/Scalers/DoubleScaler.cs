using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Outputs.Console.Scalers;

public record DoubleScaler : ConsoleScaler
{
    public override Position ScorePosition
    => Root + new Position(0, WidthScaled - 1);

    public override int Timeout { get; } = TimeoutInterval / 4;

    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
    {
        (int y, int x) = block.Position;
        Position position;
        for (int i = 0; i < 4; i++)
        {
            position = Root
            + new Position(y * 2, x * 2)
            + scaledBlockPositions[i / 2, i % 2];

            blocks.Add(CreateBlock(
                position,
                block.Symbol,
                block.Color));
        }
    }
}