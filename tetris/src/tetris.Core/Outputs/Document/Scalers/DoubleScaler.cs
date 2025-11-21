using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Document.Scalers;

public record DoubleScaler : DocumentScaler
{
    public override Position Root { get; set; }

    public override void Scale(Block block, DynamicallyAllocatedArray<Block> blocks)
    {
        throw new NotImplementedException();
    }

    public override Result<bool> Validate()
    {
        Height = HeightNormal;
        Width = WidthNormal;

        return new(true);
    }
}