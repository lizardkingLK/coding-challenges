using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Abstractions;

public interface IGameplay
{
    public MapSizeEnum MapSize { get; set; }
    public Position Root { get; set; }
    public IOutput Output { get; }
    public HashMap<DirectionEnum, int>? Borders { get; set; }
    public Block[,]? Map { get; set; }
    public bool[,]? Availability { get; set; }
    public HashMap<int, int>? FilledTracker { get; set; }

    public Result<bool> Validate();
    public Result<bool> Create();

    public void Flush();
    public void Stream(Block block);

    public void Clear() => Output.Clear();

    public void Toggle(bool isOn)
    {
        Console.CursorVisible = !isOn;
        Console.SetCursorPosition(0, 0);
        Console.Clear();
    }
}