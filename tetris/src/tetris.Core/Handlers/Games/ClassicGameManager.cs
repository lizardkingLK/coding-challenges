using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Commands;
using tetris.Core.Enums.Cordinates;
using tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;
using tetris.Core.Library.DataStructures.Linear.Stacks.LinkedStack;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Outputs.Console;
using tetris.Core.Shared;
using tetris.Core.State.Assets;
using tetris.Core.State.Cordinates;
using tetris.Core.State.Misc;
using tetris.Core.Views;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Handlers.Games;

public record ClassicGameManager(Arguments Arguments) : GameManager
{
    private readonly ArrayQueue<(Tetromino, Block[,], Position)> _tetrominoQueue = new(tetrominoes.Count());
    private readonly LinkedStack<CommandTypeEnum> _actionStack = new();
    private readonly PauseMenuView _pauseMenuView = new();
    private readonly Arguments _arguments = Arguments;
    private readonly ConsoleOutput? _output = new(Arguments);

    private (Tetromino Tetromino, Block[,] Map, Position Position) _current;
    private HashMap<CommandTypeEnum, Action?>? _commandActions;
    private int _yRoof = HeightNormal;
    private int _actionInterval;
    private int _score;
    private bool _isPaused;
    private bool _isReset;
    private bool _isQuit;

    public override Block[,]? Map { get; set; }
    public override HashMap<int, int>? FilledTracker { get; set; }
    public override bool[,]? Availability { get; set; }

    public override Result<bool> Validate()
    {
        Result<int> actionIntervalResult = SetInterval();
        if (actionIntervalResult.Errors != null)
        {
            return new(false, actionIntervalResult.Errors);
        }

        _actionInterval = actionIntervalResult!.Data;
        Result<bool> gameCreationResult = _output!.Create();
        if (gameCreationResult.Errors != null)
        {
            return new(false, gameCreationResult.Errors);
        }

        _commandActions = SetCommandActions();

        return new(true);
    }

    public override Result<bool> New()
    {
        CreateBoard();
        CreateQueue();

        return new(true);
    }

    public override Result<bool> Play()
    {
        _output!.Timeout();

        while (!_isQuit)
        {
            if (!TryChooseTetromino() || !TryTravelTetromino())
            {
                return new(false);
            }
        }

        return new(true);
    }

    public override void Input(CommandTypeEnum commandType)
    => _actionStack.Push(commandType);

    private bool TryTravelTetromino()
    {
        while (!_isReset)
        {
            Thread.Sleep(_actionInterval);

            if (_actionStack.TryPop(out CommandTypeEnum commandType))
            {
                HandleAction(commandType);
            }

            if (commandType == CommandTypeEnum.StoreIt)
            {
                break;
            }
        }

        return true;
    }

    private bool TryChooseTetromino()
    {
        _actionStack.Purge();
        _isReset = false;

        if (!_tetrominoQueue.TryPeek(out _current))
        {
            CreateQueue();
        }

        _current = _tetrominoQueue.Dequeue();
        (_, _, Position origin) = _current;
        if (_yRoof == origin.Y)
        {
            return false;
        }

        _actionStack.Push(CommandTypeEnum.SpawnIt);

        return true;
    }

    private void CreateBoard()
    {
        Map = new Block[HeightNormal, WidthNormal];
        Availability = new bool[HeightNormal, WidthNormal];
        FilledTracker = [];

        SetFilledTracker();

        int i;
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
                Map[y, x] = CreateBlock(position);
                Availability[y, x] = true;
                FilledTracker[y]--;
            }
            else
            {
                Map[y, x] = CreateBlock(position, SymbolWallBlock, ColorWall);
                Availability[y, x] = false;
            }
        }

        _output!.WriteAll(Map!);
    }

    private void CreateQueue()
    {
        tetrominoes.Shuffle();
        Block[,] map;
        int length;
        Position position;
        foreach (Tetromino? tetromino in tetrominoes.Values)
        {
            if (tetromino == null)
            {
                continue;
            }

            map = tetromino.Get();
            length = tetromino.Side;
            position = new(1, WidthNormal / 2 - length / 2);
            _tetrominoQueue.Enqueue((tetromino, map, position));
        }
    }

    private void HandleAction(CommandTypeEnum commandType)
    {
        if (ShouldPause(commandType))
        {
            return;
        }

        _commandActions![commandType]!.Invoke();
        if (HasLodged())
        {
            _actionStack.Push(CommandTypeEnum.StoreIt);
        }
        else
        {
            _actionStack.Push(CommandTypeEnum.GoDown);
        }
    }

    private void Toggle()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            _output!.WriteContent(
                _pauseMenuView.Message,
                _pauseMenuView.Height,
                _pauseMenuView.Width);
        }
        else
        {
            _output!.WriteAll(Map!);
            Thread.Sleep(_actionInterval);
            _output!.WriteScore(_score);
        }
    }

    private void NewGame()
    {
        if (!_isPaused)
        {
            return;
        }

        _output!.WriteAll(Map!);

        SetFilledTracker();

        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        char symbol;
        Position position;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            position = new(y, x);
            (_, symbol, _) = Map![y, x];
            if (IsNonWallBlock(y, x))
            {
                Map[y, x] = CreateBlock(position);
                Availability![y, x] = true;
                FilledTracker![y]--;
            }
            else
            {
                Availability![y, x] = false;
            }

            if (symbol == SymbolTetrominoBlock)
            {
                _output.Write(Map[y, x], Map);
                Thread.Sleep(BlockClearTimeout);
            }
        }

        _score = 0;
        _actionStack.Purge();
        _yRoof = HeightNormal;
        _isPaused = false;
        _isReset = true;

        _output!.Timeout();
    }

    private void QuitGame()
    {
        if (!_isPaused)
        {
            return;
        }

        _isReset = true;
        _isPaused = false;
        _isQuit = true;

        _output!.Clear();
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
            if (!Availability![y, x])
            {
                return true;
            }
        }

        return false;
    }

    private void StoreIt()
    {
        (Tetromino? tetromino, Block[,] map, Position position) = _current;

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
            block = map![y, x];
            if (block.Symbol != SymbolTetrominoBlock)
            {
                continue;
            }

            block = Map![position.Y + y, position.X + x];
            (y, x) = block.Position;
            if (y < _yRoof)
            {
                _yRoof = y;
            }

            Availability![y, x] = false;
            FilledTracker![y]++;
            if (FilledTracker[y] == WidthNormal)
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
        int points = yPoints.Size;
        while (yPoints.TryPop(out int yPoint))
        {
            ClearLine(yPoint);
        }

        int bottom = yFLoor;
        int top = yFLoor - 1;
        while (top < bottom && _yRoof <= top)
        {
            if (FilledTracker![bottom] > 2)
            {
                bottom--;
            }

            if (FilledTracker![top] == 2)
            {
                top--;
                continue;
            }

            DownShift(bottom, top);
        }

        _yRoof += points;

        Score(points);
    }

    private void DownShift(int bottom, int top)
    {
        int filledCountTop = FilledTracker![top];
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
            ((y, x), symbol, color) = Map![top, i];
            isOldBlockAvailable = Availability![top, i];

            oldBlock = CreateBlock(y, x, SymbolSpaceBlock, ColorSpace);
            Map![top, i] = oldBlock;
            Availability![top, i] = true;

            Thread.Sleep(BlockClearTimeout);

            newBlock = CreateBlock(bottom, i, symbol, color);
            Map![bottom, i] = newBlock;
            Availability![bottom, i] = isOldBlockAvailable;

            if (symbol != SymbolSpaceBlock)
            {
                _output!.Write(oldBlock, Map);
                _output!.Write(newBlock, Map);
            }
        }

        FilledTracker![top] = 2;
        FilledTracker![bottom] = filledCountTop;
    }

    private void ClearLine(int yPoint)
    {
        int length = WidthNormal - 1;
        Block block;
        for (int i = 1; i < length; i++)
        {
            block = CreateBlock(yPoint, i, SymbolSpaceBlock, ColorSpace);
            Map![yPoint, i] = block;
            Availability![yPoint, i] = true;
            _output!.Write(block, Map);
            Thread.Sleep(BlockClearTimeout);
        }

        FilledTracker![yPoint] = 2;
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
            relative = Map![spawn.Y, spawn.X].Position;
            newBlock = CreateBlock(relative, block);
            map[y, x] = newBlock;
            Map![spawn.Y, spawn.X] = newBlock;
            _output!.Write(newBlock, Map);
        }

        _current = (tetromino, map, position);
    }

    private void RotateIt()
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanRotate(Availability!, position))
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
            Map![y, x] = block;
            _output!.Write(block, Map);
        }

        _current = (tetromino, map, position);
    }

    private void SlamDown()
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        Position positionChange = new(1, 0);
        while (tetromino.CanMove(Availability!, (position, positionChange), DirectionEnum.Down))
        {
            ClearIt(map, position, tetromino.Side);
            MoveIt((position, positionChange), tetromino, map);

            position += positionChange;
            _current = (tetromino, map, position);
        }
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

            block = Map![y + position.Y, x + position.X];
            block = CreateBlock(block.Position, SymbolSpaceBlock, ColorSpace);
            (y, x) = block.Position;
            Map![y, x] = block;
            _output!.Write(block, Map);
        }
    }

    private void Move(DirectionEnum direction, Position positionChange)
    {
        (Tetromino? tetromino, Block[,]? map, Position position) = _current;
        if (!tetromino.CanMove(Availability!, (position, positionChange), direction))
        {
            return;
        }

        ClearIt(map, position, tetromino.Side);
        MoveIt((position, positionChange), tetromino, map);

        _current = (tetromino, map, position + positionChange);
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

            block = Map![y + previous.Y, x + previous.X];
            block = CreateBlock(
                block.Position + change,
                symbol,
                color);
            (y, x) = block.Position;
            map[i / side, i % side] = block;
            Map![y, x] = block;
            _output!.Write(block, Map);
        }
    }

    private void Score(int points)
    {
        _score += points * _arguments.DifficultyLevel switch
        {
            DifficultyLevelEnum.Easy => 2,
            DifficultyLevelEnum.Medium => 3,
            DifficultyLevelEnum.Hard => 4,
            _ => -1,
        };

        _output!.WriteScore(_score);
    }

    private void SetFilledTracker()
    {
        for (int i = 0; i < HeightNormal; i++)
        {
            FilledTracker!.TryAddOrUpdate(i, WidthNormal);
        }
    }

    private Result<int> SetInterval()
    => _arguments.DifficultyLevel switch
    {
        DifficultyLevelEnum.Easy => new(BlockMoveInterval * 2),
        DifficultyLevelEnum.Medium => new(BlockMoveInterval),
        DifficultyLevelEnum.Hard => new(BlockMoveInterval / 2),
        _ => new(-1, "error. difficulty level not implemented"),
    };

    private HashMap<CommandTypeEnum, Action?> SetCommandActions()
    {
        HashMap<CommandTypeEnum, Action?> commandActions = new(
            (CommandTypeEnum.ToggleGame, Toggle),
            (CommandTypeEnum.NewGame, NewGame),
            (CommandTypeEnum.QuitGame, QuitGame),
            (CommandTypeEnum.SpawnIt, SpawnIt),
            (CommandTypeEnum.RotateIt, RotateIt),
            (CommandTypeEnum.SlamDown, SlamDown),
            (CommandTypeEnum.StoreIt, StoreIt),
            (CommandTypeEnum.GoRight, () => Move(DirectionEnum.Right, new(0, 1))),
            (CommandTypeEnum.GoDown, () => Move(DirectionEnum.Down, new(1, 0))),
            (CommandTypeEnum.GoLeft, () => Move(DirectionEnum.Left, new(0, -1))));

        return commandActions;
    }

    private bool ShouldPause(CommandTypeEnum commandType)
    {
        if (_isPaused
        && (commandType == CommandTypeEnum.NewGame
        || commandType == CommandTypeEnum.QuitGame))
        {
            return false;
        }

        if (_isPaused && commandType != CommandTypeEnum.ToggleGame)
        {
            return true;
        }

        return false;
    }

    private bool IsNonWallBlock(int y, int x)
    => x > _output!.Borders![DirectionEnum.Left]
    && x < _output.Borders[DirectionEnum.Right]
    && y > _output.Borders[DirectionEnum.Up]
    && y < _output.Borders[DirectionEnum.Down];
}