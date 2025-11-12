using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;
using tetris.Core.Library.DataStructures.Linear.Queues.DoublyEndedQueue;
using tetris.Core.Shared;
using tetris.Core.State.Assets;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Handlers;

public class MapManager(IOutput output)
{
    private readonly IOutput _output = output;
    private readonly ConsoleColor _wallColor = ConsoleColor.Gray;
    private readonly ArrayQueue<(Block[,], Position)> _tetrominoQueue = new(QueuedTetrominoCount);
    private readonly Deque<(Block[,], Position, CommandTypeEnum)> _actionsQueue = new();
    private readonly DynamicallyAllocatedArray<(Block[,], Position)> _tetrominoes = [];

    public Result<bool> Create()
    {
        CreateMap();
        CreateList();
        CreateQueue();

        return new(true);
    }

    public Result<bool> Play()
    {
        if (!TryChooseTetromino())
        {
            return new(false);
        }

        if (TryTravelTetromino())
        {
            return new(false);
        }

        return new(true);
    }

    private bool TryTravelTetromino()
    {
        while (!_actionsQueue.IsEmpty())
        {
            (Block[,] map, Position position, CommandTypeEnum commandType) = _actionsQueue.RemoveFromFront();
            (map, position, commandType) = HandleTetrominoAction(map, position, commandType);
            if (commandType == CommandTypeEnum.StoredIt)
            {
                break;
            }

            _actionsQueue.AddToRear((map, position, commandType));

            Thread.Sleep(1000);
        }

        _actionsQueue.Purge();

        return true;
    }

    private bool TryChooseTetromino()
    {
        (Block[,], Position) drop = _tetrominoQueue!.Dequeue();
        (Block[,]? map, Position origin) = drop;
        if (!_output.Availability![origin.Y, origin.X])
        {
            return false;
        }

        _actionsQueue.AddToRear((map, origin, CommandTypeEnum.SpawnIt));

        return true;
    }

    private void CreateQueue()
    {
        for (int i = 0; i < QueuedTetrominoCount; i++)
        {
            _tetrominoQueue!.Enqueue(
                _tetrominoes![Random.Shared.Next(_tetrominoes!.Size)]);
        }
    }

    private void CreateList()
    {
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
            center = new(1, _output.Width / 2 - width / 2);
            for (i = 0; i < length; i++)
            {
                map = tetromino.Get(i);
                _tetrominoes!.Add((map, center));
            }
        }
    }

    private void CreateMap()
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

    private (Block[,], Position, CommandTypeEnum) HandleTetrominoAction(
        Block[,] map,
        Position position,
        CommandTypeEnum commandType)
    {
        if (commandType == CommandTypeEnum.SpawnIt)
        {
            position = SpawnIt(map, position);
        }

        return (map, position, CommandTypeEnum.GoDown);
    }

    private Position SpawnIt(Block[,] map, Position origin)
    {
        Position previous;
        Position spawn;
        Block newBlock;
        foreach (Block block in map!)
        {
            ((int y, int x), _, _) = block;
            spawn = origin + block.Position;
            previous = _output.Map![spawn.Y, spawn.X].Position;
            newBlock = new(previous, block);
            _output.Map![spawn.Y, spawn.X] = newBlock;
            _output.Stream(newBlock);
        }

        _tetrominoQueue!.Enqueue(
            _tetrominoes![Random.Shared.Next(_tetrominoes!.Size)]);

        return origin;
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _output.Borders![DirectionEnum.Left]
    && x < _output.Borders[DirectionEnum.Right]
    && y > _output.Borders[DirectionEnum.Up]
    && y < _output.Borders[DirectionEnum.Down];
}