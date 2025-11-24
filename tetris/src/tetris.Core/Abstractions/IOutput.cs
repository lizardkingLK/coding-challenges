using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IOutput
{
    public int Height { get; set; }
    public int Width { get; set; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }

    public Result<bool> Create();
    public void Clear();
    public void Flush(Block[,] map);
    public void Stream(Block block, Block[,] map);
    public void Score(int score, Block[,] map);
    public static void Toggle(bool isOn)
    {
        Console.CursorVisible = !isOn;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
    }
}