using tetris.Core.Abstractions;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs.Console;

public abstract record ConsoleAligner : IAligner
{
    public abstract Position Root { get; set; }

    public abstract void Align(ref Block block);

    public abstract Position GetRoot(int height, int width);
}