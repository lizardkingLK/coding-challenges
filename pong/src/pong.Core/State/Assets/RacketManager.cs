using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Queues.Deque;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class RacketManager(StatusManager statusManager) : ISubscriber
{
    private readonly StatusManager _statusManager = statusManager;
    private Deque<Block> _leftRacket = new();
    private Deque<Block> _rightRacket = new();
    private Position _leftTop;
    private Position _rightTop;
    private Position _leftBottom;
    private Position _rightBottom;

    public record RacketMoveNotification(
        VerticalDirectionEnum Direction,
        PlayerSideEnum PlayerSide,
        int Speed) : INotification;

    private void Create()
    {
        InitializeRackets();
        InitializeCorners();

        int count = (_statusManager.Height - 2) / 3;
        int yOffset = (_statusManager.Height / 2) - (count / 2);
        int xLeftOffset = 1;
        int xRightOffset = _statusManager.Width - 2;
        Block leftPlayerBlock;
        Block rightPlayerBlock;
        for (int i = 0; i < count; i++)
        {
            leftPlayerBlock = new(yOffset, xLeftOffset, RacketBlockSymbol, ConsoleColor.Cyan);
            _leftRacket.InsertToRear(leftPlayerBlock);
            _statusManager.Update(leftPlayerBlock);

            rightPlayerBlock = new(yOffset, xRightOffset, RacketBlockSymbol, ConsoleColor.Cyan);
            _rightRacket.InsertToRear(rightPlayerBlock);
            _statusManager.Update(rightPlayerBlock);

            yOffset++;
        }
    }

    private void Move(RacketMoveNotification racketMove)
    {
        racketMove.Deconstruct(
            out VerticalDirectionEnum direction,
            out PlayerSideEnum playerSide,
            out int speed);

        Action<PlayerSideEnum, int> Movement = GetMovementAction(direction);
        for (int i = speed; i > 0; i--)
        {
            Movement(playerSide, i);
        }
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GameCreateNotification:
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

    private Action<PlayerSideEnum, int> GetMovementAction(VerticalDirectionEnum direction)
    => direction switch
    {
        VerticalDirectionEnum.Up => MovePlayerUpIfSatisfies,
        _ => MovePlayerDownIfSatisfies
    };

    private void MovePlayerUpIfSatisfies(PlayerSideEnum playerSide, int speed)
    {
        Deque<Block> player;
        Block playerHeadBlock;
        bool canProceedWithMove;
        if (playerSide == PlayerSideEnum.PlayerLeft)
        {
            player = _leftRacket;
            playerHeadBlock = player.Head!.Value;
            canProceedWithMove = _leftTop.Top != playerHeadBlock.Top;
        }
        else
        {
            player = _rightRacket;
            playerHeadBlock = player.Head!.Value;
            canProceedWithMove = _rightTop.Top != playerHeadBlock.Top;
        }

        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = player.RemoveFromRear().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock.Symbol = RacketBlockSymbol;
        removedBlock.Top = playerHeadBlock.Top - 1;
        player.InsertToFront(removedBlock);
        _statusManager.Update(removedBlock);
    }

    private void MovePlayerDownIfSatisfies(PlayerSideEnum playerSide, int speed)
    {
        Deque<Block> player;
        Block playerTailBlock;
        bool canProceedWithMove;
        if (playerSide == PlayerSideEnum.PlayerLeft)
        {
            player = _leftRacket;
            playerTailBlock = player.Tail!.Value;
            canProceedWithMove = _leftBottom.Top != playerTailBlock.Top;
        }
        else
        {
            player = _rightRacket;
            playerTailBlock = player.Tail!.Value;
            canProceedWithMove = _rightBottom.Top != playerTailBlock.Top;
        }

        if (!canProceedWithMove)
        {
            return;
        }

        Block removedBlock = player.RemoveFromFront().Value;
        removedBlock.Symbol = SpaceBlockSymbol;
        _statusManager.Update(removedBlock);

        removedBlock.Symbol = RacketBlockSymbol;
        removedBlock.Top = playerTailBlock.Top + 1;
        player.InsertToRear(removedBlock);
        _statusManager.Update(removedBlock);
    }

    private void InitializeCorners()
    {
        _leftTop = new(1, 1);
        _rightTop = new(1, _statusManager.Width - 2);
        _leftBottom = new(_statusManager.Height - 2, 1);
        _rightBottom = new(_statusManager.Height - 2, _statusManager.Width - 2);
    }

    private void InitializeRackets()
    {
        _leftRacket = new();
        _rightRacket = new();
    }
}