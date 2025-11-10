using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }

    public Result<bool> Create(MapSizeEnum mapSize);
    public void Update(Block block);
    public void Listen();
}