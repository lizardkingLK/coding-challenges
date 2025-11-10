using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs;

public class DocumentOutput : IOutput
{
    public int Height { get; set; }

    public int Width { get; set; }

    public Result<bool> Create()
    {
        throw new NotImplementedException();
    }

    public Result<bool> Create(MapSizeEnum mapSize)
    {
        throw new NotImplementedException();
    }

    public void Listen()
    {
        throw new NotImplementedException();
    }

    public void Update(Block block)
    {
        throw new NotImplementedException();
    }
}