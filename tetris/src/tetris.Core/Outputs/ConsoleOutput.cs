using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Console;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.Streamers;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public abstract class ConsoleOutput : IOutput
{
    public MapSizeEnum MapSize { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; }
    public bool[,]? Availability { get; set; }

    public ConsoleOutput(MapSizeEnum mapSize)
    {
        MapSize = mapSize;
        System.Console.CancelKeyPress += (sender, _)
        => ((IOutput)this).Toggle(isOn: false);
        Streamer = new ConsoleStreamer();
    }

    public static ConsoleOutput CreateScaled(MapSizeEnum mapSize)
    {
        return mapSize == MapSizeEnum.Normal
        ? new NormalScaler(mapSize)
        : new DoubleScaler(mapSize);
    }

    public Result<bool> Create()
    {
        Result<bool> dimensionResult = Validate(
            out int height,
            out int width);
        if (!dimensionResult.Data)
        {
            return dimensionResult;
        }

        Root = new(
            System.Console.WindowHeight / 2 - height / 2,
            System.Console.WindowWidth / 2 - width / 2);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        ((IOutput)this).Toggle(isOn: true);

        ((IOutput)this).Clear();

        return new(true);
    }

    public abstract void Flush();

    public abstract void Stream(Block block);

    public abstract Result<bool> Validate(out int height, out int width);
}