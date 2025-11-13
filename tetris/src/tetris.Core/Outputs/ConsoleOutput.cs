using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.Streamers;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Helpers.BlockHelper;

namespace tetris.Core.Outputs;

public class ConsoleOutput : IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }

    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public Position Root { get; set; }
    public IStreamer Streamer { get; }
    public bool[,]? Availability { get; set; }

    public ConsoleOutput()
    {
        Console.CancelKeyPress += (sender, _) => Toggle(isOn: false);
        Streamer = new ConsoleStreamer();
    }

    public Result<bool> Create(MapSizeEnum mapSize)
    {
        Result<bool> dimensionResult = ValidateDimensions(mapSize);
        if (!dimensionResult.Data)
        {
            return dimensionResult;
        }

        Root = new(
            Console.WindowHeight / 2 - Height / 2,
            Console.WindowWidth / 2 - Width / 2);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, Width - 1),
            (DirectionEnum.Down, Height - 1),
            (DirectionEnum.Left, 0));

        Toggle(isOn: true);

        ((IOutput)this).Clear();

        return new(true);
    }

    // TODO: add center aligned as an option in arguments
    public void Stream(Block block)
    => Streamer.Stream(
        CreateBlock(Root + block.Position, block),
        Height,
        Width,
        Map!);

    public void Flush()
    {
        Block[,] centered = new Block[Height, Width];
        int length = Height * Width;
        int y;
        int x;
        Block block;
        for (int i = 0; i < length; i++)
        {
            y = i / Width;
            x = i % Width;
            block = Map![y, x];
            centered[y, x] = new(Root + block.Position)
            {
                Symbol = block.Symbol,
                Color = block.Color,
            };
        }

        Streamer.Flush(Height, Width, centered);
    }

    private Result<bool> ValidateDimensions(MapSizeEnum mapSize)
    {
        int chosenHeight = 0;
        int chosenWidth = 0;
        if (mapSize == MapSizeEnum.Normal)
        {
            chosenHeight = HeightNormal;
            chosenWidth = WidthNormal;
        }
        else if (mapSize == MapSizeEnum.Scaled)
        {
            chosenHeight = HeightScaled;
            chosenWidth = WidthScaled;
        }

        Height = Console.WindowHeight;
        if (Height < chosenHeight)
        {
            return new(false, "error. cannot create map. height not enough");
        }

        Width = Console.WindowWidth;
        if (Width < chosenWidth)
        {
            return new(false, "error. cannot create map. width not enough");
        }

        Height = chosenHeight;
        Width = chosenWidth;

        return new(true);
    }

    private static void Toggle(bool isOn)
    {
        Console.CursorVisible = !isOn;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
    }
}