using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }
    public Position Root { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }

    public Result<bool> Create(MapSizeEnum mapSize);
    public void Update(Block block);
    public void Flush();
    public void Listen();
    public void Clear();
}