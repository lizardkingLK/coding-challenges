using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;

namespace tetris.Core.Outputs.Console.Scalers;

public record NormalScaler : ConsoleScaler
{
    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
        => blocks.Add(
            CreateBlock(
                Root + block.Position,
                block.Symbol,
                block.Color));
}