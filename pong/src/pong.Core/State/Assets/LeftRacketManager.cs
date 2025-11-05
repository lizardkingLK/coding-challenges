using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.Notifications;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class LeftRacketManager(StatusManager statusManager) : ISubscriber
{
    private readonly StatusManager _statusManager = statusManager;

    private readonly ConsoleColor _racketColor = ConsoleColor.Cyan;

    private readonly Deque<Block> _player = new();

    private Position _leftTop;
    private Position _leftBottom;

    private void Create()
    {
        _leftTop = new(1, 0);
        _leftBottom = new(_statusManager.Height - 2, 0);

        int racketLength = (_statusManager.Height - 2) / 3;
        int yOffset = (_statusManager.Height / 2) - (racketLength / 2);
        int xLeftOffset = 0;
        Block appendBlock;
        for (int i = 0; i < racketLength; i++)
        {
            appendBlock = new(yOffset, xLeftOffset, RacketBlockSymbol, _racketColor);
            _player.InsertToRear(appendBlock);
            _statusManager.Map(appendBlock);

            yOffset++;
        }
    }

    private void Move(RacketMoveNotification racketMove)
    {
        racketMove.Deconstruct(
            out VerticalDirectionEnum direction,
            out _,
            out int speed);

        Action<int> Movement = GetMovementAction(direction);
        for (int i = speed; i > 0; i--)
        {
            Movement(i);
        }
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameCreateNotification:
                Create();
                break;
            case RacketMoveNotification:
                Move((RacketMoveNotification)notification);
                break;
            default:
                break;
        }
    }

    public void Subscribe()
    {
    }

    private Action<int> GetMovementAction(VerticalDirectionEnum direction)
    => direction switch
    {
        VerticalDirectionEnum.Up => MovePlayerUpIfSatisfies,
        _ => MovePlayerDownIfSatisfies
    };

    private void MovePlayerUpIfSatisfies(int speed)
    {
        Block _playerHeadBlock = _player.Head!.Value;
        bool canProceedWithMove = _leftTop.Top != _playerHeadBlock.Top;
        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = _player.RemoveFromRear().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock = new(
            _playerHeadBlock.Top - 1,
            removedBlock.Left,
            RacketBlockSymbol,
            _racketColor);
        _player.InsertToFront(removedBlock);
        _statusManager.Update(removedBlock);
    }

    private void MovePlayerDownIfSatisfies(int speed)
    {
        Block _playerTailBlock = _player.Tail!.Value;
        bool canProceedWithMove = _leftBottom.Top != _playerTailBlock.Top;
        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = _player.RemoveFromFront().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock = new(
            _playerTailBlock.Top - 1,
            removedBlock.Left,
            RacketBlockSymbol,
            _racketColor);
        _player.InsertToFront(removedBlock);
        _statusManager.Update(removedBlock);
    }
}