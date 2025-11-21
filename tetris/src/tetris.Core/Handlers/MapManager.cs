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

public class MapManager(IGameplay gameplay)
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

    private readonly IGameplay _gameplay = gameplay;
    private readonly LinkedStack<CommandTypeEnum> _actionStack = new();
    private readonly ArrayQueue<(Tetromino, Block[,], Position)> _tetrominoQueue = new(_tetrominoes.Count());

    private (Tetromino Tetromino, Block[,] Map, Position Position) _current;
    private int _yRoof = HeightNormal;

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
        if (!_gameplay.Availability![origin.Y, origin.X])
        {
            return false;
        }

        _actionStack.Push(CommandTypeEnum.SpawnIt);

        return true;
    }

    private void CreateBoard()
    {
        _gameplay.Map = new Block[HeightNormal, WidthNormal];
        _gameplay.Availability = new bool[HeightNormal, WidthNormal];
        _gameplay.FilledTracker = [];

        int i;
        for (i = 0; i < HeightNormal; i++)
        {
            _gameplay.FilledTracker.Add(i, WidthNormal);
        }

        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Position position;
        for (i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            position = new Position(y, x);
            if (IsNonWallBlock(position.Y, position.X))
            {
                _gameplay.Map[y, x] = CreateBlock(position);
                _gameplay.Availability[y, x] = true;
                _gameplay.FilledTracker[y]--;
            }
            else
            {
                _gameplay.Map[y, x] = CreateBlock(position, SymbolWallBlock, ColorWall);
                _gameplay.Availability[y, x] = false;
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
            if (!_gameplay.Availability![y, x])
            {
                return true;
            }
        }

        return false;
    }

    private void StoreIt()
    {
        (Tetromino? tetromino, Block[,] Map, Position position) = _current;

        int y;
        int x;
        int side = tetromino.Side;
        int length = side * side;
        Block block;
        int? lowestYPoint = null;
        LinkedStack<int> yPoints = new();
        for (int i = 0; i < length; i++)
        {
            y = i / side;
            x = i % side;
            block = Map[y, x];
            if (block.Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            block = _gameplay.Map![position.Y + y, position.X + x];
            (y, x) = block.Position;
            if (y < _yRoof)
            {
                _yRoof = y;
            }

            _gameplay.Availability![y, x] = false;
            _gameplay.FilledTracker![y]++;
            if (_gameplay.FilledTracker[y] == WidthNormal)
            {
                yPoints.Push(y);
                lowestYPoint = y;
            }
        }

        if (lowestYPoint != null)
        {
            ClearLines(yPoints, lowestYPoint.Value);
        }
    }

    private void ClearLines(LinkedStack<int> yPoints, int yFLoor)
    {
        int size = yPoints.Size;
        while (yPoints.TryPop(out int yPoint))
        {
            ClearLine(yPoint);
        }

        int bottom = yFLoor;
        int top = yFLoor - 1;
        while (top < bottom && _yRoof <= top)
        {
            if (_gameplay.FilledTracker![bottom] > 2)
            {
                bottom--;
            }

            if (_gameplay.FilledTracker![top] == 2)
            {
                top--;
                continue;
            }

            DownShift(bottom, top);
        }

        _yRoof += size;
    }

    private void DownShift(int bottom, int top)
    {
        int filledCountTop = _gameplay.FilledTracker![top];
        int length = WidthNormal - 1;
        Block oldBlock;
        Block newBlock;
        int y;
        int x;
        char symbol;
        ConsoleColor color;
        bool isOldBlockAvailable;
        for (int i = 1; i < length; i++)
        {
            ((y, x), symbol, color) = _gameplay.Map![top, i];
            isOldBlockAvailable = _gameplay.Availability![top, i];

            oldBlock = CreateBlock(y, x, SymbolSpaceBlock, ColorSpace);
            _gameplay.Map![top, i] = oldBlock;
            _gameplay.Availability![top, i] = true;

            newBlock = CreateBlock(bottom, i, symbol, color);
            _gameplay.Map![bottom, i] = newBlock;
            _gameplay.Availability![bottom, i] = isOldBlockAvailable;

            if (symbol != SymbolSpaceBlock)
            {
                _gameplay.Stream(oldBlock);
                _gameplay.Stream(newBlock);
            }
        }

        _gameplay.FilledTracker![top] = 2;
        _gameplay.FilledTracker![bottom] = filledCountTop;
    }

    private void ClearLine(int yPoint)
    {
        int length = WidthNormal - 1;
        Block block;
        for (int i = 1; i < length; i++)
        {
            block = CreateBlock(yPoint, i, SymbolSpaceBlock, ColorSpace);
            _gameplay.Map![yPoint, i] = block;
            _gameplay.Availability![yPoint, i] = true;
            _gameplay.Stream(block);
        }

        _gameplay.FilledTracker![yPoint] = 2;
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
            relative = _gameplay.Map![spawn.Y, spawn.X].Position;
            newBlock = CreateBlock(relative, block);
            map[y, x] = newBlock;
            _gameplay.Map![spawn.Y, spawn.X] = newBlock;
            _gameplay.Stream(newBlock);
        }

        _current = (tetromino, map, position);
    }

    private void RotateIt()
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanRotate(_gameplay.Availability!, position))
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
            _gameplay.Map![y, x] = block;
            _gameplay.Stream(block);
        }

        _current = (tetromino, map, position);
    }

    private void Move(DirectionEnum direction, Position positionChange)
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanMove(_gameplay.Availability!, (position, positionChange), direction))
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

            block = _gameplay.Map![y + position.Y, x + position.X];
            block = CreateBlock(block.Position, SymbolSpaceBlock, ColorSpace);
            (y, x) = block.Position;
            _gameplay.Map![y, x] = block;
            _gameplay.Stream(block);
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

            block = _gameplay.Map![y + previous.Y, x + previous.X];
            block = CreateBlock(
                block.Position + change,
                symbol,
                color);
            (y, x) = block.Position;
            map[i / side, i % side] = block;
            _gameplay.Map![y, x] = block;
            _gameplay.Stream(block);
        }
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _gameplay.Borders![DirectionEnum.Left]
    && x < _gameplay.Borders[DirectionEnum.Right]
    && y > _gameplay.Borders[DirectionEnum.Up]
    && y < _gameplay.Borders[DirectionEnum.Down];
}