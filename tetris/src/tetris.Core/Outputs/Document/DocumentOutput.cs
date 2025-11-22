using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Document.Scalers;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Document;

public record DocumentOutput : IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public DocumentScaler _scaler;

    public DocumentOutput(Arguments arguments)
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

    public void Flush(Block[,] map)
    {
        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        DynamicallyAllocatedArray<Block> blocks = new(HeightScaled * WidthScaled);


        int length = HeightNormal * HeightNormal;
        int y;
        int x;
        int yPrevious = 0;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            if (yPrevious != y)
            {
                fileStream.WriteByte((byte)SymbolNewLine);
            }

            (_, char symbol, _) = map[y, x];
            fileStream.WriteByte((byte)symbol);

            yPrevious = y;
        }

        fileStream.Close();
    }

    public void Stream(Block block, Block[,] map)
    {
        Flush(map);
    }

    private void Output(DynamicallyAllocatedArray<Block> blocks)
    {

    }
}