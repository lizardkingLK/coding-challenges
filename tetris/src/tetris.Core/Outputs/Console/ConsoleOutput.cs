using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Console.Scalers;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console;

public record ConsoleOutput : IOutput
{
    private readonly int _colorsLength = Enum.GetValues<ConsoleColor>().Length;

    public int Height { get; set; }
    public int Width { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public ConsoleScaler _scaler;

    public ConsoleOutput(Arguments arguments)
    {
        if (arguments.MapSize == MapSizeEnum.Normal)
        {
            Height = HeightNormal;
            Width = WidthNormal;
            _scaler = new NormalScaler();
        }
        else
        {
            Height = HeightScaled;
            Width = WidthScaled;
            _scaler = new DoubleScaler();
        }
    }

    public void Clear() => System.Console.Clear();

    public Result<bool> Create()
    {
        Result<bool> dimensionResult = Validate();
        if (!dimensionResult.Data)
        {
            return dimensionResult;
        }

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        System.Console.CancelKeyPress += (sender, _)
        => IOutput.Toggle(isOn: false);

        IOutput.Toggle(isOn: true);

        return new(true);
    }

    private Result<bool> Validate()
    {
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

        (_scaler.Height, _scaler.Width) = (Height, Width);
        _scaler.SetRoot(Height, Width);
        (Height, Width) = (HeightNormal, WidthNormal);

        return new(true);
    }

    public void WriteAll(Block[,] map)
    {
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        DynamicallyAllocatedArray<Block> blocks = [];
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = map[y, x];
            _scaler.Scale(block, blocks);
        }

        Output(blocks);
    }

    public void Write(Block block, Block[,] _)
    {
        DynamicallyAllocatedArray<Block> blocks = [];
        _scaler.Scale(block, blocks);
        Output(blocks);
    }

    public void WriteScore(int score, Block[,] map)
    {
        Position position = _scaler.ScorePosition;
        Position oneLeft = new(0, -1);
        int length = 1 + (int)Math.Log10(score);
        int tempScore = score;
        char symbol;
        DynamicallyAllocatedArray<Block> blocks = [];
        for (int i = 0; i < length; i++)
        {
            symbol = (char)((tempScore % 10) + '0');
            blocks.Add(CreateBlock(position, symbol, ColorWall));
            tempScore /= 10;
            position += oneLeft;
        }

        Output(blocks);
    }

    public void WriteContent(string content, int height, int width)
    {
        int length = content.Length;
        Position origin = new(
            _scaler.Height / 2 - height / 2,
            _scaler.Width / 2 - width / 2);

        DynamicallyAllocatedArray<Block> blocks = [];
        char symbol;
        Position position;
        int y;
        int x;
        ConsoleColor color;
        for (int i = 0; i < length; i++)
        {
            y = i / width;
            x = i % width;
            symbol = content[i];
            if (symbol == SymbolNewLine)
            {
                continue;
            }

            position = origin + _scaler.Root + new Position(y, x);
            color = (ConsoleColor)Random.Shared.Next(1, _colorsLength);
            blocks.Add(CreateBlock(position, symbol, color));
        }

        Output(blocks);
    }

    private static void Output(DynamicallyAllocatedArray<Block> blocks)
    {
        foreach (((int y, int x), char symbol, ConsoleColor color) in blocks)
        {
            WriteAt(symbol, y, x, color);
        }
    }
}