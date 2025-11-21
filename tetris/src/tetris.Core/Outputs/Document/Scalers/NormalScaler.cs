using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Document.Scalers;

public record NormalScaler : DocumentScaler
{
    public override Position Root { get; set; }

    public override void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks)
    {
        return;   
    }
}