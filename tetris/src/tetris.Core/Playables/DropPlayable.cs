using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Assets;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;

namespace tetris.Core.Playables;

public class DropPlayable(
    Arguments Arguments,
    IOutput Output,
    IPlayable? Next = null) : IPlayable
{
    private DynamicallyAllocatedArray<(Block[,], Position)>? _tetrominoes;

    public Arguments Arguments { get; init; } = Arguments;
    public IPlayable? Next { get; init; } = Next;
    public IOutput Output { get; init; } = Output;

    public Result<bool> Create()
    {
        SetTetrominoes();

        // TODO: queue first three tetrominos

        (Block[,] map, Position origin) = _tetrominoes![Random.Shared.Next(_tetrominoes!.Size)];

        // Tetromino.Update(map, Output.Map!, center);
        Position previous;
        Position spawn;
        Block newBlock;
        foreach (Block block in map)
        {
            ((int y, int x), _, _) = block;
            spawn = origin + block.Position;
            previous = Output.Map![spawn.Y, spawn.X].Position;
            newBlock = new(previous, block);
            Output.Map![spawn.Y, spawn.X] = newBlock;
        }

        return Next?.Create() ?? new(true);
    }

    public Result<bool> Play()
    {
        return new(true);
    }

    private void SetTetrominoes()
    {
        _tetrominoes = [];

        foreach (Tetromino? tetromino in Tetromino.allTetrominoes)
        {
            if (tetromino == null)
            {
                continue;
            }

            CreateTetrominoes(tetromino);
        }
    }

    private void CreateTetrominoes(Tetromino tetromino)
    {

        int i;
        int length = tetromino.Size;
        int width = tetromino.Width;
        Position center = new(1, Output.Width / 2 - width / 2);
        Block[,] map;
        for (i = 0; i < length; i++)
        {
            map = tetromino.Get(i);
            _tetrominoes!.Add((map, center));
        }
    }
}