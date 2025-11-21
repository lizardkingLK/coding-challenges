using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IAligner
{
    public Position Root { get; set; }

    public void Align(ref Block block);

    public Position GetRoot(int height, int width);
}