using tetris.Core.Abstractions;
using tetris.Core.Enums.Commands;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;
using tetris.Core.Library.DataStructures.Linear.Stacks.LinkedStack;
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
    private readonly LinkedStack<CommandTypeEnum> _actionStack = new();
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
        _actionStack.Push(commandType);
    }

    private bool TryTravelTetromino()
    {
        while (_actionStack.TryPop(out CommandTypeEnum commandType))
        {
            HandleAction(commandType);
            if (commandType == CommandTypeEnum.StoreIt)
            {
                break;
            }

            Thread.Sleep(durationMoveInterval);
        }

        return true;
    }

    private bool TryChooseTetromino()
    {
        _actionStack.Purge();

        if (!_tetrominoQueue.TryPeek(out _current))
        {
            CreateQueue();
        }

        _current = _tetrominoQueue.Dequeue();
        (_, _, Position origin) = _current;
        if (!_output.Availability![origin.Y, origin.X])
        {
            return false;
        }

        _actionStack.Push(CommandTypeEnum.SpawnIt);

        return true;
    }

    private void CreateBoard()
    {
        _output.Map = new Block[HeightNormal, WidthNormal];
        _output.Availability = new bool[HeightNormal, WidthNormal];

        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Position position;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
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
        int length;
        Position position;
        foreach (Tetromino tetromino in _tetrominoes.Values!)
        {
            map = tetromino.Get();
            length = tetromino.Side;
            position = new(1, WidthNormal / 2 - length / 2);
            _tetrominoQueue.Enqueue((tetromino, map, position));
        }
    }

    private void HandleAction(CommandTypeEnum commandType)
    {
        if (commandType == CommandTypeEnum.SpawnIt)
        {
            SpawnIt();
        }
        else if (commandType == CommandTypeEnum.RotateIt)
        {
            RotateIt();
        }
        else if (commandType == CommandTypeEnum.GoRight)
        {
            Move(DirectionEnum.Right, new(0, 1));
        }
        else if (commandType == CommandTypeEnum.GoDown)
        {
            Move(DirectionEnum.Down, new(1, 0));
        }
        else if (commandType == CommandTypeEnum.GoLeft)
        {
            Move(DirectionEnum.Left, new(0, -1));
        }
        else if (commandType == CommandTypeEnum.StoreIt)
        {
            StoreIt();
        }

        _actionStack.Push(HasLodged()
        ? CommandTypeEnum.StoreIt
        : CommandTypeEnum.GoDown);
    }

    private bool HasLodged()
    {
        (_, Block[,] map, _) = _current;
        int y;
        int x;
        Position oneLower = new(1, 0);
        foreach (Block block in map)
        {
            if (block.Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            (y, x) = block.Position + oneLower;
            if (!_output.Availability![y, x])
            {
                return true;
            }
        }

        return false;
    }

    private void StoreIt()
    {
        (_, Block[,] Map, _) = _current;
        Block? previousBlock = null;
        foreach (Block block in Map)
        {
            if (block.Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            _output.Availability![block.Y, block.X] = false;
            if (previousBlock != null && previousBlock.Value.Y != block.Y)
            {
                TrackComplete(previousBlock!.Value.Position);
            }

            previousBlock = block;
        }
    }

    private void TrackComplete(Position position)
    {
        (int y, _) = position;

        for (int i = 1; i < WidthNormal; i++)
        {
            if (_output.Availability![y, i])
            {
                _output.Stream(CreateBlock(y, i, SymbolWallBlock, ColorWall));
                return;
            }
        }

        // TODO: add to a priority queue where 
        // y max is highest takes max priority to clear lines
        throw new ApplicationException("success. time to clear");
    }

    private void SpawnIt()
    {
        Position relative;
        Position spawn;
        Block newBlock;
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        foreach (Block block in map)
        {
            ((int y, int x), char symbol, _) = block;
            if (symbol != SymbolTetrominoBlock)
            {
                continue;
            }

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
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanRotate(_output.Availability!, position))
        {
            return;
        }

        int side = tetromino.Side;
        ClearIt(map, position, side);

        map = tetromino.Next();
        int length = side * side;
        int i;
        int y;
        int x;
        Block block;
        for (i = 0; i < length; i++)
        {
            y = i / side;
            x = i % side;
            block = map![y, x];
            block = CreateBlock(position + block.Position, block.Symbol, block.Color);
            if (block.Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            (y, x) = block.Position;
            map[i / side, i % side] = block;
            _output.Map![y, x] = block;
            _output.Stream(block);
        }

        _current = (tetromino, map, position);
    }

    private void Move(DirectionEnum direction, Position positionChange)
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanMove(_output.Availability!, (position, positionChange), direction))
        {
            return;
        }

        ClearIt(map, position, tetromino.Side);
        MoveIt((position, positionChange), tetromino, map);

        _current = (tetromino, map, position + positionChange);
    }

    private void ClearIt(
        in Block[,] map,
        in Position position,
        in int side)
    {
        int length = side * side;
        int i;
        int y;
        int x;
        Block block;
        for (i = 0; i < length; i++)
        {
            y = i / side;
            x = i % side;
            if (map[y, x].Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            block = _output.Map![y + position.Y, x + position.X];
            block = CreateBlock(block.Position, SymbolSpaceBlock, ColorSpace);
            (y, x) = block.Position;
            _output.Map![y, x] = block;
            _output.Stream(block);
        }
    }

    private void MoveIt(
        (Position, Position) positions,
        Tetromino tetromino,
        Block[,] map)
    {
        (Position previous, Position change) = positions;
        int side = tetromino.Side;
        int length = side * side;
        int i;
        int y;
        int x;
        Block block;
        char symbol;
        ConsoleColor color;
        for (i = length - 1; i >= 0; i--)
        {
            y = i / side;
            x = i % side;
            if (map[y, x].Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            symbol = SymbolTetrominoBlock;
            color = tetromino.Color;

            block = _output.Map![y + previous.Y, x + previous.X];
            block = CreateBlock(
                block.Position + change,
                symbol,
                color);
            (y, x) = block.Position;
            map[i / side, i % side] = block;
            _output.Map![y, x] = block;
            _output.Stream(block);
        }
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _output.Borders![DirectionEnum.Left]
    && x < _output.Borders[DirectionEnum.Right]
    && y > _output.Borders[DirectionEnum.Up]
    && y < _output.Borders[DirectionEnum.Down];
}