using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Console;

public abstract record ConsoleScaler : IScaler
{
    public Position Root { get; set; }

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