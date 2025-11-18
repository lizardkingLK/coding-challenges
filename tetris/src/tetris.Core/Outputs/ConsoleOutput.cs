using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.Streamers;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public class ConsoleOutput : IOutput
{
    public MapSizeEnum MapSize { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; }
    public bool[,]? Availability { get; set; }

    public ConsoleOutput(MapSizeEnum mapSize)
    {
        Console.CancelKeyPress += (sender, _) => Toggle(isOn: false);
        Streamer = new ConsoleStreamer();
        MapSize = mapSize;
    }

    public Result<bool> Create()
    {
        Result<bool> dimensionResult = ValidateDimensions(
            out int height,
            out int width);
        if (!dimensionResult.Data)
        {
            return dimensionResult;
        }

        Root = new(
            Console.WindowHeight / 2 - height / 2,
            Console.WindowWidth / 2 - width / 2);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        Toggle(isOn: true);

        ((IOutput)this).Clear();

        return new(true);
    }

    public void Flush()
    {
        if (MapSize == MapSizeEnum.Normal)
        {
            NormalFlush();
        }
        else if (MapSize == MapSizeEnum.Scaled)
        {
            ScaledFlush();
        }
    }

    public void Stream(Block block)
    {
        if (MapSize == MapSizeEnum.Normal)
        {
            NormalStream(block);
        }
        else if (MapSize == MapSizeEnum.Scaled)
        {
            ScaledStream(block);
        }
    }

    private void ScaledStream(Block block)
    {
        foreach (Block transformed in CreateScaledBlock(Root, block))
        {
            Streamer.Stream(transformed, HeightScaled, WidthScaled, Map!);
        }
    }

    private void NormalStream(Block block)
    {
        Streamer.Stream(
            CreateBlock(Root + block.Position, block),
            HeightNormal,
            WidthNormal,
            Map!);
    }

    private void ScaledFlush()
    {
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = Map![y, x];
            ScaledStream(block);
        }
    }

    private void NormalFlush()
    {
        Block[,] centered = new Block[HeightNormal, WidthNormal];
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = Map![y, x];
            centered[y, x] = CreateBlock(
                Root + block.Position,
                block.Symbol,
                block.Color);
        }

        Streamer.Flush(HeightNormal, WidthNormal, centered);
    }

    private Result<bool> ValidateDimensions(out int height, out int width)
    {
        height = 0;
        width = 0;
        if (MapSize == MapSizeEnum.Normal)
        {
            height = HeightNormal;
            width = WidthNormal;
        }
        else if (MapSize == MapSizeEnum.Scaled)
        {
            height = HeightScaled;
            width = WidthScaled;
        }

        int availableHeight = Console.WindowHeight;
        if (availableHeight < height)
        {
            return new(false, "error. cannot create map. height not enough");
        }

        int availableWidth = Console.WindowWidth;
        if (availableWidth < width)
        {
            return new(false, "error. cannot create map. width not enough");
        }

        return new(true);
    }

    private static void Toggle(bool isOn)
    {
        Console.CursorVisible = !isOn;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
    }
}