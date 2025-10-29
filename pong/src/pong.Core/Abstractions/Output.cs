using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Game;
using pong.Core.State.Handlers;

namespace pong.Core.Abstractions;

public abstract record Output(GameManager GameManager)
{
    public int Height { get; set; }
    public int Width { get; set; }
    public void Draw(DynamicallyAllocatedArray<DynamicallyAllocatedArray<Block>> mapGrid)
    {
        foreach (DynamicallyAllocatedArray<Block> mapRow in mapGrid.NonNullValues)
        {
            foreach (Block mapColumn in mapRow.NonNullValues)
            {
                Draw(mapColumn, mapGrid);
            }
        }
    }
    
    public abstract void Draw(
        Block block,
        DynamicallyAllocatedArray<DynamicallyAllocatedArray<Block>> mapGrid);
}