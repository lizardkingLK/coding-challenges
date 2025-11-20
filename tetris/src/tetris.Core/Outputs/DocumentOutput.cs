using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Document;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.Streamers;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public abstract class DocumentOutput(MapSizeEnum mapSize) : IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }
    public MapSizeEnum MapSize { get; set; } = mapSize;
    public Block[,]? Map { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; } = new DocumentStreamer();
    public bool[,]? Availability { get; set; }
    public HashMap<int, int>? FilledTracker { get; set; }

    public static DocumentOutput CreateScaled(MapSizeEnum mapSize)
    {
        return new NormalScaler(mapSize);
    }

    public Result<bool> Create()
    {
        Root = new(0, 0);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        ((IOutput)this).Clear();

        return new(true);
    }

    public void Flush()
    => Streamer.Flush(HeightNormal, WidthNormal, Map!);

    public void Stream(Block block)
    => Streamer.Stream(block, HeightNormal, WidthNormal, Map!);

    public abstract Result<bool> Validate();
}