using tetris.Core.Abstractions;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Document;

public record DocumentOutput : IOutput
{
    private readonly Arguments _arguments;

    public int Height { get; set; }
    public int Width { get; set; }
    public Position Root { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public DocumentOutput(Arguments arguments)
    {
        _arguments = arguments;

        // TODO: set scalars and aligners
    }

    public Result<bool> Create()
    {
        Root = new(0, 0);

        Borders = new(
            (DirectionEnum.Up, 0),
            (DirectionEnum.Right, WidthNormal - 1),
            (DirectionEnum.Down, HeightNormal - 1),
            (DirectionEnum.Left, 0));

        Clear();

        return new(true);
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

    public void Flush(Block[,] map)
    {
        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

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

    public void Stream(in Block block, Block[,] map)
    => Flush(map);
}