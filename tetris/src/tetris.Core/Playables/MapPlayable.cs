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

    private readonly ConsoleColor _wallColor = ConsoleColor.DarkYellow;

    public Result<bool> Create()
    {
        Position root = Output.Corners![CornerEnum.LeftTop]!.Value;
        Output.Map = new Block[Output.Height, Output.Width];
        int length = Output.Height * Output.Width;
        int y;
        int x;
        Position blockPosition;
        for (int i = 0; i < length; i++)
        {
            y = i / Output.Height;
            x = i % Output.Width;
            blockPosition = root + new Position(y, x);
            Output.Map[y, x] = new Block(
                blockPosition.Y,
                blockPosition.X,
                SymbolWallBlock,
                _wallColor);
        }

        return new(true);
    }

    public Result<bool> Play()
    {
        return new(true);
    }
}