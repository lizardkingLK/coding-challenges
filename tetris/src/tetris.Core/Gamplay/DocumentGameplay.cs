using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Gamplay.Document;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Gamplay;

public abstract class DocumentGameplay(MapSizeEnum mapSize) : IGameplay
{
    public int Height { get; set; }
    public int Width { get; set; }
    public MapSizeEnum MapSize { get; set; } = mapSize;
    public Block[,]? Map { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Position Root { get; set; }
    public IOutput Output { get; } = new DocumentOutput();
    public bool[,]? Availability { get; set; }
    public HashMap<int, int>? FilledTracker { get; set; }

    public static DocumentGameplay CreateScaled(MapSizeEnum mapSize)
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

        ((IGameplay)this).Clear();

        return new(true);
    }

    public void Flush()
    => Output.Flush(HeightNormal, WidthNormal, Map!);

    public void Stream(Block block)
    => Output.Stream(block, HeightNormal, WidthNormal, Map!);

    public abstract Result<bool> Validate();
}