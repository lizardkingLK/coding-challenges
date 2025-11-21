using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IOutput
{
    public void Clear();
    public void Flush(in int height, in int width, in Block[,] map);
    public void Stream(in Block block, in int height, in int width, in Block[,] map);
}