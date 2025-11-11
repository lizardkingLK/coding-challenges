using tetris.Core.Abstractions;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Playables;

public class MapPlayable(
    Arguments Arguments,
    IOutput Output,
    IPlayable? Next = null) : IPlayable
{
    public Arguments Arguments { get; init; } = Arguments;
    public IPlayable? Next { get; init; } = Next;
    public IOutput Output { get; init; } = Output;

    private readonly ConsoleColor _wallColor = ConsoleColor.Gray;

    public Result<bool> Create()
    {
        Output.Map = new Block[Output.Height, Output.Width];
        int length = Output.Height * Output.Width;
        int y;
        int x;
        Position block;
        for (int i = 0; i < length; i++)
        {
            y = i / Output.Width;
            x = i % Output.Width;
            block = Output.Root + new Position(y, x);
            Output.Map[y, x] = GetMapBlock(block.Y, block.X);
        }

        return new(true);
    }

    public Result<bool> Play()
    {
        return new(true);
    }

    private Block GetMapBlock(int y, int x) => IsNonWallBlock(y, x)
    ? new(y, x) { Symbol = SymbolSpaceBlock }
    : new(y, x) { Symbol = SymbolWallBlock, Color = _wallColor, IsFree = false };

    private bool IsNonWallBlock(int y, int x)
    => x > Output.Borders![DirectionEnum.Left]
    && x < Output.Borders[DirectionEnum.Right]
    && y > Output.Borders[DirectionEnum.Up]
    && y < Output.Borders[DirectionEnum.Down];
}