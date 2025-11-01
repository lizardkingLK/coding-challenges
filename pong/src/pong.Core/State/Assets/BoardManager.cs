using pong.Core.Abstractions;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class BoardManager(StatusManager statusManager) : ISubscriber
{
    private const ConsoleColor BoardColor = ConsoleColor.Yellow;
    private readonly StatusManager _statusManager = statusManager;

    private void Create()
    {
        int rows = _statusManager.Height;
        int columns = _statusManager.Width;
        int i;
        int j;
        for (j = 0; j < columns; j++)
        {
            _statusManager.Create(new(0, j, WallBlockSymbol, BoardColor));
        }

        for (i = 1; i < rows - 1; i++)
        {
            _statusManager.Create(new(i, 0, WallBlockSymbol, BoardColor));
            _statusManager.CreateRange(i, (1, columns - 1), SpaceBlockSymbol, BoardColor);
            _statusManager.Create(new(i, columns - 1, WallBlockSymbol, BoardColor));
        }

        for (j = 0; j < columns; j++)
        {
            _statusManager.Create(new(rows - 1, j, WallBlockSymbol, BoardColor));
        }

        rows--;
        j = columns / 2;
        for (i = 1; i < rows; i++)
        {
            _statusManager.Create(new(i, j, NetBlockSymbol, BoardColor));
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
            default:
                break;
        }
    }

    public void Subscribe()
    {
    }
}