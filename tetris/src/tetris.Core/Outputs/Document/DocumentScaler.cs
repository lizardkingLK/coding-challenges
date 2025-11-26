using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Document;

public abstract record DocumentScaler : IScaler
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Position Root { get; set; }

    public abstract Position ScorePosition { get; }

    public abstract void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks);
}