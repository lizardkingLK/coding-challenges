using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Document;

public abstract record DocumentScaler : IScaler
{
    public abstract Position Root { get; set; }

    public abstract void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks);
}