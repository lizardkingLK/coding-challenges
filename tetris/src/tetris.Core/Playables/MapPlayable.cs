using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
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
        Output.Availability = new bool[Output.Height, Output.Width];

        int length = Output.Height * Output.Width;
        int y;
        int x;
        Position block;
        for (int i = 0; i < length; i++)
        {
            y = i / Output.Width;
            x = i % Output.Width;
            block = Output.Root + new Position(y, x);
            Output.Map[y, x] = GetMapBlock(
                block.Y,
                block.X,
                out bool isAvailable);
            Output.Availability[y, x] = isAvailable;
        }

        return Next?.Create() ?? new(true);
    }

    public Result<bool> Play()
    {
        // TODO: check if droppable or blocked

        return Next?.Play()!;
    }

    public void Input(InputTypeEnum inputType)
    {
        // TODO: check if blocked or continue move/rotate

        Console.WriteLine(inputType);
    }

    public void Pause() => Next?.Pause();

    private Block GetMapBlock(int y, int x, out bool isAvailable)
    {
        isAvailable = IsNonWallBlock(y, x);

        return isAvailable
            ? new(y, x) { Symbol = SymbolSpaceBlock }
            : new(y, x) { Symbol = SymbolWallBlock, Color = _wallColor };
    }

    private bool IsNonWallBlock(int y, int x)
    => x > Output.Borders![DirectionEnum.Left]
    && x < Output.Borders[DirectionEnum.Right]
    && y > Output.Borders[DirectionEnum.Up]
    && y < Output.Borders[DirectionEnum.Down];
}