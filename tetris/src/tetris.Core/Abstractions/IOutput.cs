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
    public MapSizeEnum MapSize { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public bool[,]? Availability { get; set; }

    public Result<bool> Create();

    public void Clear() => Streamer.Clear();
    public void Flush() => Streamer.Flush(Height, Width, Map!);
    public void Stream(Block block);
}