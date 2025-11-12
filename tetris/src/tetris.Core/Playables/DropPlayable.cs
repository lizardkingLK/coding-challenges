using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;
using tetris.Core.Shared;
using tetris.Core.State.Assets;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Playables;

public class DropPlayable(
    Arguments Arguments,
    IOutput Output,
    IPlayable? Next = null) : IPlayable
{
    private DynamicallyAllocatedArray<(Block[,], Position)>? _tetrominoes;
    private readonly ArrayQueue<(Block[,], Position)>? _tetrominoQueue = new(QueuedTetrominoCount);

    public Arguments Arguments { get; init; } = Arguments;
    public IPlayable? Next { get; init; } = Next;
    public IOutput Output { get; init; } = Output;

    public Result<bool> Create()
    {
        _tetrominoes = [];

        int i;
        int length;
        int width;
        Position center;
        Block[,] map;
        foreach (Tetromino? tetromino in Tetromino.allTetrominoes)
        {
            if (tetromino == null)
            {
                continue;
            }

            length = tetromino.Size;
            width = tetromino.Width;
            center = new(1, Output.Width / 2 - width / 2);
            for (i = 0; i < length; i++)
            {
                map = tetromino.Get(i);
                _tetrominoes!.Add((map, center));
            }
        }

        for (i = 0; i < QueuedTetrominoCount; i++)
        {
            _tetrominoQueue!.Enqueue(GetRandomTetromino());
        }

        return Next?.Create() ?? new(true);
    }

    public Result<bool> Play() => Drop();

    public void Input(InputTypeEnum inputType)
    {
        if (inputType == InputTypeEnum.RotateIt)
        {

        }
    }

    public void Pause() => Next?.Pause();

    private Result<bool> Drop()
    {
        if (!TrySpawn(out Block[,]? map))
        {
            return new(false);
        }

        // while (true)
        // {
        Thread.Sleep(1000);
        //     break;
        // }

        return new(true);
    }

    private bool TrySpawn(out Block[,] map)
    {
        (Block[,], Position) drop = _tetrominoQueue!.Dequeue();
        (map, Position origin) = drop;
        if (!Output.Availability![origin.Y, origin.X])
        {
            return false;
        }

        Position previous;
        Position spawn;
        Block newBlock;
        foreach (Block block in map!)
        {
            ((int y, int x), _, _) = block;
            spawn = origin + block.Position;
            previous = Output.Map![spawn.Y, spawn.X].Position;
            newBlock = new(previous, block);
            Output.Map![spawn.Y, spawn.X] = newBlock;
            Output.Stream(newBlock);
        }

        _tetrominoQueue!.Enqueue(GetRandomTetromino());

        return true;
    }

    private (Block[,], Position) GetRandomTetromino()
    => _tetrominoes![Random.Shared.Next(_tetrominoes!.Size)];
}