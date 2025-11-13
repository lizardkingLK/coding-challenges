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
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Shared.Values;

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
    private readonly Deque<CommandTypeEnum> _actionsQueue = new();
    private readonly ArrayQueue<(Tetromino, Block[,], Position)> _tetrominoQueue
    = new(_tetrominoes.Count());

    private (Tetromino Tetromino, Block[,] Map, Position Position) _current;

    public Result<bool> Create()
    {
        CreateBoard();
        CreateQueue();

        return new(true);
    }

    public Result<bool> Play()
    {
        while (true)
        {
            if (!TryChooseTetromino())
            {
                return new(false);
            }

            if (!TryTravelTetromino())
            {
                return new(false);
            }
        }
    }

    public void Input(CommandTypeEnum commandType)
    {
        _actionsQueue.AddToRear(commandType);
    }

    private bool TryTravelTetromino()
    {
        while (_actionsQueue.TryRemoveFromFront(out CommandTypeEnum commandType))
        {
            if (commandType == CommandTypeEnum.StoredIt)
            {
                break;
            }

            HandleTetrominoAction(commandType);
            Thread.Sleep(durationMoveInterval);
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
            position = new Position(y, x);
            if (IsNonWallBlock(position.Y, position.X))
            {
                _output.Map[y, x] = CreateBlock(position);
                _output.Availability[y, x] = true;
            }
            else
            {
                _output.Map[y, x] = CreateBlock(position, SymbolWallBlock, ColorWall);
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
        _actionsQueue.AddToRear(CommandTypeEnum.GoDown);

        // _actionsQueue.AddToRear(CommandTypeEnum.RotateIt);
    }

    private void SpawnIt()
    {
        Position relative;
        Position spawn;
        Block newBlock;
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        foreach (Block block in map)
        {
            ((int y, int x), _, _) = block;
            spawn = position + block.Position;
            relative = _output.Map![spawn.Y, spawn.X].Position;
            newBlock = CreateBlock(relative, block);
            map[y, x] = newBlock;
            _output.Map![spawn.Y, spawn.X] = newBlock;
            _output.Stream(newBlock);
        }

        _current = (tetromino, map, position);
    }

    private void RotateIt()
    {
        // TODO: check if rotatable and early return

        (Tetromino? tetromino, _, Position position) = _current;
        int height = tetromino.Height;
        int width = tetromino.Width;
        int length = height * width;
        int i;
        int y;
        int x;
        Block block;
        for (i = 0; i < length; i++)
        {
            y = i / width;
            x = i % width;
            block = _output.Map![y + position.Y, x + position.X];
            block = CreateBlock(block.Position, SymbolSpaceBlock, ColorSpace);
            (y, x) = block.Position;
            _output.Map![y, x] = block;
            _output.Stream(block);
        }

        Block[,]? map = tetromino.Next();
        for (i = 0; i < length; i++)
        {
            y = i / width;
            x = i % width;
            block = map![y, x];
            block = CreateBlock(position + block.Position, block.Symbol, block.Color);
            (y, x) = block.Position;
            _output.Map![y, x] = block;
            _output.Stream(block);
        }

        _current = (tetromino, map, position);
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _output.Borders![DirectionEnum.Left]
    && x < _output.Borders[DirectionEnum.Right]
    && y > _output.Borders[DirectionEnum.Up]
    && y < _output.Borders[DirectionEnum.Down];
}