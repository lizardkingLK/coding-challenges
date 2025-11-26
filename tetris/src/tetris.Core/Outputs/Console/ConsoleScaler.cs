using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Console;

public abstract record ConsoleScaler : IScaler
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Position Root { get; set; }
    
    public abstract Position ScorePosition { get; }

    public void SetRoot(int height, int width)
    {
        Root = new(
            System.Console.WindowHeight / 2 - height / 2,
            System.Console.WindowWidth / 2 - width / 2);
    }

    public abstract void Scale(
        Block block,
        DynamicallyAllocatedArray<Block> blocks);
}