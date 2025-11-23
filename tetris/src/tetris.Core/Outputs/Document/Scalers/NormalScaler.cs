using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Document.Scalers;

public record NormalScaler : DocumentScaler
{
    public override Position Root { get; set; }
    public override int Height { get; set; }
    public override int Width { get; set; }
    // TODO: fix issue with document output double scaler
    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
        => blocks.Add(block);
}