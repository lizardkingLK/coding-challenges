using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Outputs.Document.Scalers;

public record DoubleScaler : DocumentScaler
{
    public override Position Root { get; set; }
    public override int Height { get; set; }
    public override int Width { get; set; }

    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
    {
        (int y, int x) = block.Position;
        Position position;
        for (int i = 0; i < 4; i++)
        {
            position = new Position(y * 2, x * 2)
            + scaledBlockPositions[i / 2, i % 2];

            block = CreateBlock(
                position,
                block.Symbol,
                block.Color);

            blocks.Add(block, Width * y + x);
        }
    }
}