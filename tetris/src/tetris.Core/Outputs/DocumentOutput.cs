using System.Text;
using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public class DocumentOutput : IOutput
{
    public int Height { get; set; }

    public int Width { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public Position Root { get; set; }

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

        Clear();

        return new(true);
    }

    public void Listen()
    {
        throw new NotImplementedException();
    }

    public void Update(Block block)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        Console.Clear();

        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        fileStream.Close();
    }

    public void Flush()
    {
        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        int length = Width * Height;
        int y;
        int x;
        int yPrevious = 0;
        for (int i = 0; i < length; i++)
        {
            y = i / Width;
            x = i % Width;
            if (yPrevious != y)
            {
                fileStream.WriteByte((byte)SymbolNewLine);
            }

            (_, char symbol, _) = Map![y, x];
            fileStream.WriteByte((byte)symbol);
            
            yPrevious = y;
        }

        fileStream.Close();
    }
}