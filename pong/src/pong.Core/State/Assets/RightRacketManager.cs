using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.Notifications;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public record RightRacketManager : Subscriber
{
    private readonly StatusManager _statusManager;

    private readonly ConsoleColor _racketColor;

    private readonly Deque<Block> _playerBody;

    private Position _rightTop;
    private Position _rightBottom;

    private readonly Input _player;

    public RightRacketManager(StatusManager statusManager, Input player)
    {
        _statusManager = statusManager;
        _player = player;
        _racketColor = ConsoleColor.Cyan;
        _playerBody = new();
    }

    private void Create()
    {
        _rightTop = new(1, _statusManager.Width - 1);
        _rightBottom = new(_statusManager.Height - 2, _statusManager.Width - 1);

        int racketLength = (_statusManager.Height - 2) / 3;
        int yOffset = (_statusManager.Height / 2) - (racketLength / 2);
        int xRightOffset = _statusManager.Width - 1;
        Block appendBlock;
        for (int i = 0; i < racketLength; i++)
        {
            appendBlock = new(yOffset, xRightOffset, RacketBlockSymbol, _racketColor);
            _playerBody.InsertToRear(appendBlock);
            _statusManager.Map(appendBlock);

            yOffset++;
        }
    }

    private void Move(RacketMoveNotification racketMove)
    {
        racketMove.Deconstruct(
            out VerticalDirectionEnum direction,
            out _,
            out int speed,
            out bool shouldBlock);

        Action<int> Movement = GetMovementAction(direction);
        for (int i = speed; i > 0; i--)
        {
            Movement(i);
        }
    }

    public override void Listen(Notification notification)
    {
        switch (notification)
        {
            case GameCreateNotification:
                Create();
                break;
            case RacketMoveNotification:
                Move((RacketMoveNotification)notification);
                break;
            case BallMoveNotification:
                Notify((BallMoveNotification)notification);
                break;
            default:
                break;
        }
    }

    private Action<int> GetMovementAction(VerticalDirectionEnum direction)
    => direction switch
    {
        VerticalDirectionEnum.Up => MovePlayerUpIfSatisfies,
        _ => MovePlayerDownIfSatisfies
    };

    private void MovePlayerUpIfSatisfies(int speed)
    {
        Block _playerBodyHeadBlock = _playerBody.Head!.Value;
        bool canProceedWithMove = _rightTop.Top != _playerBodyHeadBlock.Top;
        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = _playerBody.RemoveFromRear().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock = new(
            _playerBodyHeadBlock.Top - 1,
            removedBlock.Left,
            RacketBlockSymbol,
            _racketColor);
        _playerBody.InsertToFront(removedBlock);
        _statusManager.Update(removedBlock);
    }

    private void MovePlayerDownIfSatisfies(int speed)
    {
        Block _playerBodyTailBlock = _playerBody.Tail!.Value;
        bool canProceedWithMove = _rightBottom.Top != _playerBodyTailBlock.Top;
        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = _playerBody.RemoveFromFront().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock = new(
            _playerBodyTailBlock.Top + 1,
            removedBlock.Left,
            RacketBlockSymbol,
            _racketColor);
        _playerBody.InsertToRear(removedBlock);
        _statusManager.Update(removedBlock);
    }

    private void Notify(BallMoveNotification notification) => _player?.Notify(
        new BallMoveNotification(notification.Position, _playerBody));
}