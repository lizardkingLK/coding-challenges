using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Helpers.ConsoleHelper;

namespace tetris.Core.Outputs;

public class ConsoleOutput : IOutput
{
    public int Height { get; set; }

    public int Width { get; set; }

    public Block[,]? Map { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Position Root { get; set; }

    public ConsoleOutput()
    {
        Console.CancelKeyPress += (sender, _) => Toggle(isOn: false);
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
            (DirectionEnum.Up, Root.Y),
            (DirectionEnum.Right, Root.X + Width - 1),
            (DirectionEnum.Down, Root.Y + Height - 1),
            (DirectionEnum.Left, Root.X));

        Toggle(isOn: true);

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

    public void Clear() => Console.Clear();

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

    public void Flush()
    {
        foreach (((int y, int x), char symbol, ConsoleColor color, _) in Map!)
        {
            WriteAt(symbol, y, x, color);
        }
    }

    private static void Toggle(bool isOn)
    {
        Console.CursorVisible = !isOn;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
    }
}