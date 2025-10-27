using pong.Core.Abstractions;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Assets;

public class BoardManager : ISubscriber
{
    private const ConsoleColor BoardColor = ConsoleColor.Yellow;
    private readonly Output _output;

    public BoardManager(Output output)
    {
        _output = output;

        Create();
    }

    private void Create()
    {
        int rows = _output.Height;
        int columns = _output.Width;
        int i;
        int j;
        for (j = 0; j < columns; j++)
        {
            _output.Draw(new(0, j, WallBlockSymbol, BoardColor));
        }

        for (i = 1; i < rows - 1; i++)
        {
            _output.Draw(new(i, 0, WallBlockSymbol, BoardColor));
            _output.Draw(new(i, columns - 1, WallBlockSymbol, BoardColor));
        }

        for (j = 0; j < columns; j++)
        {
            _output.Draw(new(rows - 1, j, WallBlockSymbol, BoardColor));
        }

        rows--;
        j = columns / 2;
        for (i = 1; i < rows; i++)
        {
            _output.Draw(new(i, j, NetBlockSymbol, BoardColor));
        }
    }

    public void Listen()
    {
    }

    public void Listen(INotification notification)
    {
        switch (notification)
        {
            case GameManager.GamePausedNotification:
                // Create();
                break;
            default:
                break;
        }
    }

    public void Subscribe()
    {
    }
}