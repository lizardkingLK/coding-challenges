using tetris.Core.Abstractions;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console;

public record ConsoleOutput : IOutput
{
    private readonly Arguments _arguments;

    public int Height { get; set; }
    public int Width { get; set; }
    public Position Root { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public ConsoleOutput(Arguments arguments)
    {
        _arguments = arguments;
        if (arguments.MapSize == Enums.Arguments.MapSizeEnum.Normal)
        {

        }

        // TODO: set scalars and aligners
    }

    public void Clear() => System.Console.Clear();

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

        System.Console.CancelKeyPress += (sender, _) => Toggle(isOn: false);

        Clear();

        return new(true);
    }

    private Result<bool> Validate()
    {
        Height = HeightNormal;
        Width = WidthNormal;

        int availableHeight = System.Console.WindowHeight;
        if (availableHeight < Height)
        {
            return new(false, "error. cannot create map. Height not enough");
        }

        int availableWidth = System.Console.WindowWidth;
        if (availableWidth < Width)
        {
            return new(false, "error. cannot create map. Width not enough");
        }

        return new(true);
    }

    public void Flush(Block[,] map)
    {
        foreach (((int y, int x), char symbol, ConsoleColor color) in map)
        {
            WriteAt(symbol, y, x, color);
        }
    }

    public void Stream(in Block block, Block[,] _)
    => WriteAt(block.Symbol, block.Y, block.X, block.Color);

    private static void Toggle(bool isOn)
    {
        System.Console.CursorVisible = !isOn;
        System.Console.SetCursorPosition(0, 0);
        System.Console.Clear();
    }
}