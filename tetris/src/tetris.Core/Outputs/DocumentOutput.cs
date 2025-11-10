using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Outputs;

public class DocumentOutput : IOutput
{
    public int Height { get; set; }

    public int Width { get; set; }
    public HashMap<CornerEnum, Position?>? Corners { get; set; }
    public Block[,]? Map { get; set; }

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

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Flush()
    {
        throw new NotImplementedException();
    }
}