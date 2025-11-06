using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.Notifications;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public record LeftRacketManager : Racket
{
    private readonly StatusManager _statusManager;

    private readonly ConsoleColor _racketColor;

    private readonly Deque<Block> _playerBody;

    private Position _leftTop;
    private Position _leftBottom;

    private readonly Input? _player;

    public LeftRacketManager(StatusManager statusManager, Input player)
    {
        _statusManager = statusManager;
        _racketColor = ConsoleColor.Cyan;
        _playerBody = new();
        _player = player;
    }

    public override void Create()
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
            _playerBody.InsertToRear(appendBlock);
            _statusManager.Map(appendBlock);

            yOffset++;
        }
    }

    public override void Move(RacketMoveNotification racketMove)
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

    public override void MovePlayerUpIfSatisfies(int speed)
    {
        Block _playerBodyHeadBlock = _playerBody.Head!.Value;
        bool canProceedWithMove = _leftTop.Top != _playerBodyHeadBlock.Top;
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

    public override void MovePlayerDownIfSatisfies(int speed)
    {
        Block _playerBodyTailBlock = _playerBody.Tail!.Value;
        bool canProceedWithMove = _leftBottom.Top != _playerBodyTailBlock.Top;
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

    public override void Notify(BallMoveNotification notification) => _player?.Notify(
        new BallMoveNotification(notification.Position, _playerBody));
}