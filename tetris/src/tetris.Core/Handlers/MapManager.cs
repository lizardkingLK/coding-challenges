using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;
using tetris.Core.Library.DataStructures.Linear.Queues.LinkedDeque;
using tetris.Core.Shared;
using tetris.Core.State.Assets;
using tetris.Core.State.Assets.Tetrominoes;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Handlers;

public class MapManager(IOutput output)
{
    private static readonly DynamicallyAllocatedArray<Tetromino> _tetrominoes
    = new(
        new TetrominoI(),
        new TetrominoJ(),
        new TetrominoL(),
        new TetrominoO(),
        new TetrominoS(),
        new TetrominoT(),
        new TetrominoZ());

    private readonly IOutput _output = output;
    private readonly ConsoleColor _wallColor = ConsoleColor.Gray;
    private readonly Deque<CommandTypeEnum> _actionsQueue = new();
    private readonly ArrayQueue<(Tetromino, Block[,], Position)> _tetrominoQueue = new(_tetrominoes.Count());

    private (Tetromino Tetromino, Block[,] Map, Position Position) _current;

    public Result<bool> Create()
    {
        CreateBoard();
        CreateQueue();

        return new(true);
    }

    public Result<bool> Play()
    {
        if (!TryChooseTetromino())
        {
            return new(false);
        }

        if (!TryTravelTetromino())
        {
            return new(false);
        }

        return new(true);
    }

    public void Input(CommandTypeEnum commandType)
    {
        _actionsQueue.AddToRear(commandType);
    }

    private bool TryTravelTetromino()
    {
        CommandTypeEnum commandType;
        while (!_actionsQueue.IsEmpty())
        {
            commandType = _actionsQueue.RemoveFromFront();
            if (commandType == CommandTypeEnum.StoredIt)
            {
                break;
            }

            HandleTetrominoAction(commandType);
            Thread.Sleep(1000);
        }

        _actionsQueue.Purge();

        return true;
    }

    private bool TryChooseTetromino()
    {
        if (!_tetrominoQueue.TryDequeue(out _current))
        {
            CreateQueue();
        }

        (_, _, Position origin) = _current;
        if (!_output.Availability![origin.Y, origin.X])
        {
            return false;
        }

        _actionsQueue.AddToRear(CommandTypeEnum.SpawnIt);

        return true;
    }

    private void CreateBoard()
    {
        _output.Map = new Block[_output.Height, _output.Width];
        _output.Availability = new bool[_output.Height, _output.Width];

        int length = _output.Height * _output.Width;
        int y;
        int x;
        Position position;
        for (int i = 0; i < length; i++)
        {
            y = i / _output.Width;
            x = i % _output.Width;
            position = _output.Root + new Position(y, x);
            if (IsNonWallBlock(position.Y, position.X))
            {
                _output.Map[y, x] = new(position) { Symbol = SymbolSpaceBlock };
                _output.Availability[y, x] = true;
            }
            else
            {
                _output.Map[y, x] = new(position) { Symbol = SymbolWallBlock, Color = _wallColor };
                _output.Availability[y, x] = false;
            }
        }
    }

    private void CreateQueue()
    {
        _tetrominoes.Shuffle();
        Block[,] map;
        int width;
        Position position;
        foreach (Tetromino tetromino in _tetrominoes.Values!)
        {
            map = tetromino.Get();
            width = tetromino.Width;
            position = new(1, _output.Width / 2 - width / 2);
            _tetrominoQueue.Enqueue((tetromino, map, position));
        }
    }

    private void HandleTetrominoAction(CommandTypeEnum commandType)
    {
        if (commandType == CommandTypeEnum.SpawnIt)
        {
            SpawnIt();
        }
        else if (commandType == CommandTypeEnum.RotateIt)
        {
            RotateIt();
        }

        // TODO: add to queue of go down if not stored it if touched bottom
    }

    private void SpawnIt()
    {
        Position previous;
        Position spawn;
        Block newBlock;
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        foreach (Block block in map)
        {
            ((int y, int x), _, _) = block;
            spawn = position + block.Position;
            previous = _output.Map![spawn.Y, spawn.X].Position;
            newBlock = new(previous, block);
            map[y, x] = newBlock;
            _output.Map![spawn.Y, spawn.X] = newBlock;
            _output.Stream(newBlock);
        }

        _current = (tetromino, map, position);
    }

    private void RotateIt()
    {
        throw new NotImplementedException();
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _output.Borders![DirectionEnum.Left]
    && x < _output.Borders[DirectionEnum.Right]
    && y > _output.Borders[DirectionEnum.Up]
    && y < _output.Borders[DirectionEnum.Down];
}