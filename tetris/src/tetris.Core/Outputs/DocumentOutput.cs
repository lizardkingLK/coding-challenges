using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.Streamers;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public class DocumentOutput : IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }

    public Block[,]? Map { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; }
    public bool[,]? Availability { get; set; }

    public DocumentOutput()
    => Streamer = new DocumentStreamer();

    public Result<bool> Create(MapSizeEnum mapSize)
    {
        if (mapSize == MapSizeEnum.Normal)
        {
            Height = HeightNormal;
            Width = WidthNormal;
        }
        else if (mapSize == MapSizeEnum.Scaled)
        {
            Height = HeightScaled;
            Width = WidthScaled;
        }

        Root = new(
            Console.WindowHeight / 2 - Height / 2,
            Console.WindowWidth / 2 - Width / 2);

        Borders = new(
            (DirectionEnum.Up, Root.Y),
            (DirectionEnum.Right, Root.X + Width - 1),
            (DirectionEnum.Down, Root.Y + Height - 1),
            (DirectionEnum.Left, Root.X));

        ((IOutput)this).Clear();

        return new(true);
    }

    public void Stream(Block block)
    => Streamer.Stream(block, Height, Width, Map!);
}