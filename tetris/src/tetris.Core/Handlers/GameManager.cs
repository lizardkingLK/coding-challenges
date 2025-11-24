using tetris.Core.Enums.Commands;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;

namespace tetris.Core.Handlers;

public abstract record GameManager
{
    public abstract Block[,]? Map { get; set; }
    public abstract bool[,]? Availability { get; set; }
    public abstract HashMap<int, int>? FilledTracker { get; set; }
    public abstract HashMap<int, int>? HeightsTracker { get; set; }

    public abstract Result<bool> Validate();
    public abstract Result<bool> New();
    public abstract Result<bool> Play();
    public abstract void Input(CommandTypeEnum commandType);
}