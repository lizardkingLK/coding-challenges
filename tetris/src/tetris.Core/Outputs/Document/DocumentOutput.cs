using tetris.Core.Abstractions;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Document.Scalers;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Helpers.BlockHelper;

namespace tetris.Core.Outputs.Document;

public record DocumentOutput : IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public DocumentScaler _scaler;

    public DocumentOutput(Arguments _)
    {
        Height = HeightNormal;
        Width = WidthNormal;
        _scaler = new NormalScaler();
    }

    public void Clear()
    {
        System.Console.Clear();

        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        fileStream.Close();
    }

    public Result<bool> Create()
    {
        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        _scaler.Root = new(0, 0);
        _scaler.Height = Height;
        _scaler.Width = Width;

        Height = HeightNormal;
        Width = WidthNormal;

        System.Console.CancelKeyPress += (sender, _)
        => IOutput.Toggle(isOn: false);

        Clear();

        return new(true);
    }

    public void WriteAll(Block[,] map)
    {
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        DynamicallyAllocatedArray<Block> blocks = new(HeightScaled * WidthScaled);
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = map[y, x];
            _scaler.Scale(block, blocks);
        }

        Output(blocks);
    }

    public void Write(Block _, Block[,] map) => WriteAll(map);

    public void WriteScore(int score, Block[,] map)
    {
        Position position = _scaler.ScorePosition;
        Position oneLeft = new(0, -1);
        int length = 1 + (int)Math.Log10(score);
        int tempScore = score;
        char symbol;
        DynamicallyAllocatedArray<Block> blocks = [];
        Block block;
        for (int i = 0; i < length; i++)
        {
            symbol = (char)((tempScore % 10) + '0');
            block = CreateBlock(position, symbol, ColorWall);
            blocks.Add(block);
            map[position.Y, position.X] = block;
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
            blocks.Add(CreateBlock(position, symbol, ColorSpace));
        }

        Output(blocks);
    }

    public void ClearScore(Block[,] map)
    {
        DynamicallyAllocatedArray<Block> blocks = [];
        Position oneLeft = new(0, -1);
        Position position = new(0, WidthNormal - 1);
        Block block;
        char symbol;
        do
        {
            (_, symbol, _) = map[position.Y, position.X];
            block = CreateBlock(position, SymbolWallBlock, ColorWall);
            blocks.Add(block);
            map[position.Y, position.X] = block;
            position += oneLeft;
        }
        while (symbol != SymbolWallBlock);

        Output(blocks);
    }

    private static void Output(DynamicallyAllocatedArray<Block> blocks)
    {
        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        int y;
        int x;
        int length = blocks.Size;
        int yPrevious = 0;
        for (int i = 0; i < length; i++)
        {
            ((y, x), char symbol, _) = blocks[i];
            if (yPrevious != y)
            {
                fileStream.WriteByte((byte)SymbolNewLine);
            }

            fileStream.WriteByte((byte)symbol);

            yPrevious = y;
        }

        fileStream.Close();
    }
}