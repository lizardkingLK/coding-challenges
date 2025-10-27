using pong.Core.Abstractions;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Utilities.ConsoleUtility;

namespace pong.Core.Outputs.Console;

public record ConsoleOutput : Output
{
    public ConsoleOutput(GameManager GameManager) : base(GameManager)
    {
        (Height, Width) = GetWindowDimensions();

        ClearConsole();

        Task.Run(ListenOnResize);
    }

    public override void Draw(Block block)
    {
        block.Deconstruct(out int top, out int left, out char symbol, out ConsoleColor color);

        WriteSymbolAt(top, left, symbol, color);
    }

    private void ListenOnResize()
    {
        while (ListenToResize(out int height, out int width))
        {
            (Height, Width) = (height, width);

            GameManager.gamePaused = true;

            GameManager.Publish(new GameManager.GamePausedNotification());
        }
    }
}