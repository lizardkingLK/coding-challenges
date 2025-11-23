using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IScaler
{
    public void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks);
}