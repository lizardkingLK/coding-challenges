using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Gamplay.Console;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Gamplay;

public abstract class ConsoleGameplay : IGameplay
{
    public int Height { get; set; }
    public int Width { get; set; }
    public MapSizeEnum MapSize { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public Position Root { get; set; }
    public IOutput Output { get; }
    public bool[,]? Availability { get; set; }
    public HashMap<int, int>? FilledTracker { get; set; }

    public ConsoleGameplay(MapSizeEnum mapSize)
    {
        MapSize = mapSize;
        System.Console.CancelKeyPress += (sender, _)
        => ((IGameplay)this).Toggle(isOn: false);
        Output = new ConsoleOutput();
    }

    public static ConsoleGameplay CreateScaled(MapSizeEnum mapSize)
    => mapSize == MapSizeEnum.Normal
    ? new NormalScaler(mapSize)
    : new DoubleScaler(mapSize);

    public Result<bool> Create()
    {
        Result<bool> dimensionResult = Validate();
        if (!dimensionResult.Data)
        {
            return dimensionResult;
        }

        Root = new(
            System.Console.WindowHeight / 2 - Height / 2,
            System.Console.WindowWidth / 2 - Width / 2);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        ((IGameplay)this).Toggle(isOn: true);

        ((IGameplay)this).Clear();

        return new(true);
    }

    public abstract void Flush();

    public abstract void Stream(Block block);

    public abstract Result<bool> Validate();
}